namespace BackIngE_N.Models {
    public class PaginationResponse {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int Count { get; set; }
        public object? Data { get; set; }


        public PaginationResponse(int page, int pageSize, int totalPages, int count) {
            Page = page;
            PageSize = pageSize;
            TotalPages = totalPages;
            Count = count;
        }

        public PaginationResponse(int page, int pageSize, int totalPages, int count, object data) {
            Page = page;
            PageSize = pageSize;
            TotalPages = totalPages;
            Count = count;
            Data = data;
        }

    }
}
