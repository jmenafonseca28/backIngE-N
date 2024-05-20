namespace BackIngE_N.Models.DTO.PlayList
{
    public class PlayListDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; } = null!;

    }
}
