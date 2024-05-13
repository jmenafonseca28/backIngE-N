namespace BackIngE_N.BD {
    public partial class Security {
        public Guid Id { get; set; }
        public string Ip { get; set; }
        public DateTime LoginTime { get; set; }
        public bool StatusLogin { get; set; }
    }
}
