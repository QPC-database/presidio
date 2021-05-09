/*
 * Presidio
 *
 * Context aware, pluggable and customizable PII anonymization service for text and images.
 *
 * The version of the OpenAPI document: 2.0
 * Contact: presidio@microsoft.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Microsoft.Presidio.Client.OpenAPIDateConverter;

namespace Microsoft.Presidio.Model
{
    /// <summary>
    /// AnonymizeRequest
    /// </summary>
    [DataContract(Name = "AnonymizeRequest")]
    public partial class AnonymizeRequest : IEquatable<AnonymizeRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymizeRequest" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected AnonymizeRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymizeRequest" /> class.
        /// </summary>
        /// <param name="text">The text to anonymize (required).</param>
        /// <param name="anonymizers">Object where the key is DEFAULT or the ENTITY_TYPE and the value is the anonymizer definition.</param>
        /// <param name="analyzerResults">Array of analyzer detections (required).</param>
        public AnonymizeRequest(string text = default(string), Dictionary<string, Anonymizer> anonymizers = default(Dictionary<string, Anonymizer>), List<RecognizerResult> analyzerResults = default(List<RecognizerResult>))
        {
            // to ensure "text" is required (not null)
            this.Text = text ?? throw new ArgumentNullException("text is a required property for AnonymizeRequest and cannot be null");
            // to ensure "analyzerResults" is required (not null)
            this.AnalyzerResults = analyzerResults ?? throw new ArgumentNullException("analyzerResults is a required property for AnonymizeRequest and cannot be null");
            this.Anonymizers = anonymizers;
        }

        /// <summary>
        /// The text to anonymize
        /// </summary>
        /// <value>The text to anonymize</value>
        [DataMember(Name = "text", IsRequired = true, EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// Object where the key is DEFAULT or the ENTITY_TYPE and the value is the anonymizer definition
        /// </summary>
        /// <value>Object where the key is DEFAULT or the ENTITY_TYPE and the value is the anonymizer definition</value>
        [DataMember(Name = "anonymizers", EmitDefaultValue = false)]
        public Dictionary<string, Anonymizer> Anonymizers { get; set; }

        /// <summary>
        /// Array of analyzer detections
        /// </summary>
        /// <value>Array of analyzer detections</value>
        [DataMember(Name = "analyzer_results", IsRequired = true, EmitDefaultValue = false)]
        public List<RecognizerResult> AnalyzerResults { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AnonymizeRequest {\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  Anonymizers: ").Append(Anonymizers).Append("\n");
            sb.Append("  AnalyzerResults: ").Append(AnalyzerResults).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as AnonymizeRequest);
        }

        /// <summary>
        /// Returns true if AnonymizeRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of AnonymizeRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AnonymizeRequest input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Text == input.Text ||
                    (this.Text != null &&
                    this.Text.Equals(input.Text))
                ) && 
                (
                    this.Anonymizers == input.Anonymizers ||
                    this.Anonymizers != null &&
                    input.Anonymizers != null &&
                    this.Anonymizers.SequenceEqual(input.Anonymizers)
                ) && 
                (
                    this.AnalyzerResults == input.AnalyzerResults ||
                    this.AnalyzerResults != null &&
                    input.AnalyzerResults != null &&
                    this.AnalyzerResults.SequenceEqual(input.AnalyzerResults)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Text != null)
                    hashCode = hashCode * 59 + this.Text.GetHashCode();
                if (this.Anonymizers != null)
                    hashCode = hashCode * 59 + this.Anonymizers.GetHashCode();
                if (this.AnalyzerResults != null)
                    hashCode = hashCode * 59 + this.AnalyzerResults.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
