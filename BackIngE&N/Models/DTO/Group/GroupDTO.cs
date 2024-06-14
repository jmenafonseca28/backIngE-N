namespace BackIngE_N.Models.DTO.Group {
    public class GroupDTO {
        public Guid? Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid? IdPlaylist { get; set; }
    }
}
