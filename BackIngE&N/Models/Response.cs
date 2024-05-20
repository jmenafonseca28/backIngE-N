namespace BackIngE_N.Models {
    public class Response {
        /// <summary>
        /// Gets or sets the message associated with the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the response was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the response.
        /// </summary>
        public Object? Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class with the specified message, success status, and data.
        /// </summary>
        /// <param name="message">The message associated with the response.</param>
        /// <param name="success">A value indicating whether the response was successful.</param>
        /// <param name="data">The data associated with the response.</param>
        public Response(string message, bool success, Object data) {
            Message = message;
            Success = success;
            Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class with the specified message and success status.
        /// </summary>
        /// <param name="message">The message associated with the response.</param>
        /// <param name="success">A value indicating whether the response was successful.</param>
        public Response(string message, bool success) {
            Message = message;
            Success = success;
        }
    }
}
