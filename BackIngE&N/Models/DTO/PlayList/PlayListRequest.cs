namespace BackIngE_N.Models.DTO.PlayList
{
    public class PlayListRequest
    {
        public string Name { get; set; } = null!;
        public Guid UserId { get; set; }
    }
}
