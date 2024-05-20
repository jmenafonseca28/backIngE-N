namespace BackIngE_N.Models.DTO.PlayList
{
    public class PlayListDTOResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public List<BackIngE_N.Models.BD.Channel> Channels { get; set; }

        public PlayListDTOResponse(Guid id, string name, Guid userId, List<BackIngE_N.Models.BD.Channel> channels)
        {
            Id = id;
            Name = name;
            UserId = userId;
            Channels = channels;
        }
    }
}
