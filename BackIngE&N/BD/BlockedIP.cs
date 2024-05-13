namespace BackIngE_N.BD {
    public partial class BlockedIP {
        public Guid Id { get; set; }
        public required string Ip { get; set; }
        public DateTime BlockTime { get; set; }
    }
}
