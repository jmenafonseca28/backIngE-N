using BackIngE_N.BD;

namespace BackIngE_N.DTO.PlayList {
    public class PlayListDTOResponse {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public List<Channel> Channels { get; set; }

        public PlayListDTOResponse(Guid id, string name, Guid userId, List<Channel> channels) {
            Id = id;
            Name = name;
            UserId = userId;
            Channels = channels;
        }
    }
}
