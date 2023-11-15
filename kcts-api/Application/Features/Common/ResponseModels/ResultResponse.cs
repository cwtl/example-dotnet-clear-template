using Newtonsoft.Json;

namespace Application.Features.Common.ResponseModels
{
    /// <summary>
    /// Encapsulates an error from custom exceptions.
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>
        /// Gets or sets the code for this error.
        /// </summary>
        /// <value>
        /// The code for this error.
        /// </value>
        [JsonProperty("code")]
        public required string Code { get; set; }

        /// <summary>
        /// Gets or sets the description for this error.
        /// </summary>
        /// <value>
        /// The description for this error.
        /// </value>
        [JsonProperty("description")]
        public required string Description { get; set; }
    }
    public class ResultResponse
    {
        [JsonProperty("succeeded")]
        public required bool Succeeded { get; set; }
        [JsonProperty("errors")]
        public IList<ErrorInfo> Errors { get; set; } = new List<ErrorInfo>();
    }
}
