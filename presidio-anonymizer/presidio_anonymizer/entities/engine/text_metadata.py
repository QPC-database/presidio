import logging
from abc import ABC

from presidio_anonymizer.entities import InvalidParamException


class TextMetadata(ABC):
    """Abstract class to hold the text we are going to operate on metadata."""

    def __init__(self, start: int, end: int, entity_type: str):
        self.logger = logging.getLogger("presidio-anonymizer")
        self.start = start
        self.end = end
        self.entity_type = entity_type
        self.__validate_fields()

    def __gt__(self, other):
        """Check one entity is greater then other by the text end index."""
        return self.end > other.end

    def __eq__(self, other):
        """Check two text metadata entities are equal."""
        return (self.start == other.start
                and self.end == other.end
                and self.entity_type == other.entity_type)

    def __validate_fields(self):
        if self.start is None:
            self.__validate_field("start")
        if self.end is None:
            self.__validate_field("end")
        if self.entity_type is None:
            self.__validate_field("entity_type")
        if self.start < 0 or self.end < 0:
            raise InvalidParamException(
                "Invalid input, result start and end must be positive"
            )
        if self.start >= self.end:
            raise InvalidParamException(
                f"Invalid input, start index '{self.start}' "
                f"must be smaller than end index '{self.end}'"
            )

    def __validate_field(self, field_name: str):
        self.logger.debug(f"invalid parameter, {field_name} cannot be empty")
        raise InvalidParamException(
            f"Invalid input, result must contain {field_name}"
        )
