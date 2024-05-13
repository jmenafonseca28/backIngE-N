namespace BackIngE_N.DTO.PlayList {
    public class PlayListDTO {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; } = null!;

        //public virtual ICollection<ChannelPlayList> ChannelPlayLists { get; set; } = new List<ChannelPlayList>();

    }
}
