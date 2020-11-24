namespace EstudosCSharp.NET.Models
{
    public class CatInfo
    {
        public string _id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public User User { get; set; }
        public int Upvotes { get; set; }
        public int? UserUpvoted { get; set; }
    }
}
