namespace BackIngE_N.Models {
    public class Response {
        public string Message { get; set; }
        public bool Success { get; set; }
        public Object Data { get; set; }

        public Response(string message, bool success, Object data) {
            Message = message;
            Success = success;
            Data = data;
        }

        public Response(string message, bool success) {
            Message = message;
            Success = success;
        }
    }
}
