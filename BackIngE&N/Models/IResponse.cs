namespace BackIngE_N.Models {
    public interface IResponse<T> {

        public string Message { get; set; }

        public bool Success { get; set; }

        public T? Data { get; set; }

    }
}
