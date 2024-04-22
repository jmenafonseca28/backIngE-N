namespace BackIngE_N.Config.Jwt {
    public class Jwt {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireTime { get; set; }
        public string Subject { get; set; }

    }
}
