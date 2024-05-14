namespace BackIngE_N.DTO.PlayList {
    public class PlayListRequest {
        public string Name { get; set; } = null!;
        public Guid UserId { get; set; }
    }
}
