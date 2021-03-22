from typing import Dict, List

import pytest

from presidio_anonymizer import AnonymizeEngine
from presidio_anonymizer.entities import InvalidParamException
from presidio_anonymizer.entities.engine import RecognizerResult, AnonymizeConfig
from presidio_anonymizer.entities.engine.operator_metadata import OperatorMetadata
from presidio_anonymizer.entities.engine.result.anonymized_entity import \
    AnonymizedEntity
from presidio_anonymizer.entities.engine.result.engine_result import \
    EngineResult
from presidio_anonymizer.entities.engine.text_metadata import \
    TextMetadata


def test_given_request_anonymizers_return_list():
    engine = AnonymizeEngine()
    expected_list = ["hash", "mask", "redact", "replace", "encrypt"]
    anon_list = engine.get_anonymizers()

    assert anon_list == expected_list


def test_given_empty_text_to_engine_then_we_fail():
    engine = AnonymizeEngine()
    analyzer_result = RecognizerResult("SSN", 0, 1, 0.5)
    with pytest.raises(
            InvalidParamException, match="Invalid input, text can not be empty"
    ):
        engine.anonymize("", [analyzer_result], {})


def test_given_empty_analyzers_list_then_we_get_same_text_back():
    engine = AnonymizeEngine()
    text = "one two three"
    assert engine.anonymize(text, [], {}).text == text


def test_given_empty_anonymziers_list_then_we_fall_to_default():
    engine = AnonymizeEngine()
    text = "please REPLACE ME."
    analyzer_result = RecognizerResult("SSN", 7, 17, 0.8)
    result = engine.anonymize(text, [analyzer_result], {}).text
    assert result == "please <SSN>."


def test_given_none_as_anonymziers_list_then_we_fall_to_default():
    engine = AnonymizeEngine()
    text = "please REPLACE ME."
    analyzer_result = RecognizerResult("SSN", 7, 17, 0.8)
    result = engine.anonymize(text, [analyzer_result]).text
    assert result == "please <SSN>."


def test_given_default_anonymizer_then_we_use_it():
    engine = AnonymizeEngine()
    text = "please REPLACE ME."
    analyzer_result = RecognizerResult("SSN", 7, 17, 0.8)
    anonymizer_config = AnonymizeConfig("replace", {"new_value": "and thank you"})
    result = engine.anonymize(
        text, [analyzer_result], {"DEFAULT": anonymizer_config}
    ).text
    assert result == "please and thank you."


def test_given_specific_anonymizer_then_we_use_it():
    engine = AnonymizeEngine()
    text = "please REPLACE ME."
    analyzer_result = RecognizerResult("SSN", 7, 17, 0.8)
    anonymizer_config = AnonymizeConfig("replace", {"new_value": "and thank you"})
    ssn_anonymizer_config = AnonymizeConfig("redact", {})
    result = engine.anonymize(
        text,
        [analyzer_result],
        {"DEFAULT": anonymizer_config, "SSN": ssn_anonymizer_config},
    ).text
    assert result == "please ."


@pytest.mark.parametrize(
    # fmt: off
    "original_text,start,end",
    [
        ("hello world", 5, 12),
        ("hello world", 12, 16),
    ],
    # fmt: on
)
def test_given_analyzer_result_with_an_incorrect_text_positions_then_we_fail(
        original_text, start, end
):
    engine = AnonymizeEngine()
    analyzer_result = RecognizerResult("type", start, end, 0.5)
    err_msg = (
        f"Invalid analyzer result, start: {start} and end: "
        f"{end}, while text length is only 11."
    )
    with pytest.raises(InvalidParamException, match=err_msg):
        engine.anonymize(original_text, [analyzer_result], {})


@pytest.mark.parametrize(
    # fmt: off
    "anonymizers, result_text",
    [
        ({"number": AnonymizeConfig("fake")}, "Invalid operator class 'fake'."),
    ],
    # fmt: on
)
def test_given_invalid_json_for_anonymizers_then_we_fail(anonymizers, result_text):
    with pytest.raises(InvalidParamException, match=result_text):
        AnonymizeEngine().anonymize("this is my text",
                                    [RecognizerResult("number", 0, 4, 0)],
                                    anonymizers)


def test_given_several_results_then_we_filter_them_and_get_correct_mocked_result():
    analyzer_results = [
        RecognizerResult(start=48, end=57, score=0.55, entity_type="SSN"),
        RecognizerResult(start=24, end=32, score=0.6, entity_type="FULL_NAME"),
        RecognizerResult(start=24, end=28, score=0.9, entity_type="FIRST_NAME"),
        RecognizerResult(start=29, end=32, score=0.6, entity_type="LAST_NAME"),
        RecognizerResult(start=24, end=30, score=0.8, entity_type="NAME"),
        RecognizerResult(start=18, end=32, score=0.8, entity_type="BLA"),
        RecognizerResult(start=23, end=35, score=0.8, entity_type="BLA"),
        RecognizerResult(start=28, end=36, score=0.8, entity_type="BLA"),
        RecognizerResult(start=48, end=57, score=0.95, entity_type="PHONE_NUMBER")]

    anonymizer_config = AnonymizeConfig("replace", {})
    anonymizer_config.operator_name = ""
    engine = AnonymizeEngine()
    engine.operate = operate
    result = engine.anonymize(
        "hello world, my name is Jane Doe. My number is: 034453334",
        analyzer_results,
        {"DEFAULT": anonymizer_config}
    )

    assert result.text == "Number: I am your new text!"
    assert len(result.items) == 1
    assert result.items[0].anonymizer == "hash"
    assert result.items[0].entity_type == "type"
    assert result.items[0].start == 0
    assert result.items[0].end == 35
    assert result.items[0].anonymized_text == "text"


def operate(text: str,
            text_metadata: List[TextMetadata],
            operators: Dict[str, OperatorMetadata]) -> EngineResult:
    assert text == "hello world, my name is Jane Doe. My number is: 034453334"
    assert len(text_metadata) == 4
    expected = [
        RecognizerResult(start=48, end=57, entity_type="PHONE_NUMBER", score=0.95),
        RecognizerResult(start=18, end=32, entity_type="BLA", score=0.8),
        RecognizerResult(start=23, end=35, entity_type="BLA", score=0.8),
        RecognizerResult(start=28, end=36, entity_type="BLA", score=0.8)]
    assert all(elem in text_metadata for elem in expected)
    assert len(operators) == 1
    assert operators["DEFAULT"]
    return EngineResult("Number: I am your new text!",
                        [AnonymizedEntity(0, 35, "text", "type", "hash")])
