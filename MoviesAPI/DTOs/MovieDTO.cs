namespace MoviesAPI.DTOs
{
    public class MovieDTO
    {
        public int IdMovie { get; set; }
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public int Duration { get; set; }
        public string RouteImage { get; set; }
        public enum TypeClassification { allPublic, over18 }
        public TypeClassification Clasification { get; set; }
        public DateTime CreationDate { get; set; }
        public int categoryId { get; set; }
    }
}
