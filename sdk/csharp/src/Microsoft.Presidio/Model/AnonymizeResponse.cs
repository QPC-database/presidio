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
    /// AnonymizeResponse
    /// </summary>
    [DataContract(Name = "AnonymizeResponse")]
    public partial class AnonymizeResponse : IEquatable<AnonymizeResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymizeResponse" /> class.
        /// </summary>
        /// <param name="text">text.</param>
        /// <param name="items">Array of anonymized entities.</param>
        public AnonymizeResponse(string text = default(string), List<OperatorEntity> items = default(List<OperatorEntity>))
        {
            this.Text = text;
            this.Items = items;
        }

        /// <summary>
        /// Gets or Sets Text
        /// </summary>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// Array of anonymized entities
        /// </summary>
        /// <value>Array of anonymized entities</value>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public List<OperatorEntity> Items { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AnonymizeResponse {\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  Items: ").Append(Items).Append("\n");
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
            return this.Equals(input as AnonymizeResponse);
        }

        /// <summary>
        /// Returns true if AnonymizeResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of AnonymizeResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AnonymizeResponse input)
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
                    this.Items == input.Items ||
                    this.Items != null &&
                    input.Items != null &&
                    this.Items.SequenceEqual(input.Items)
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
                if (this.Items != null)
                    hashCode = hashCode * 59 + this.Items.GetHashCode();
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
