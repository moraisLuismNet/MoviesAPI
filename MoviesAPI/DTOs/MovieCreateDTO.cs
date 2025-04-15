namespace MoviesAPI.DTOs
{
    public class MovieCreateDTO
    {
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public int Duration { get; set; }
        public string RouteImage { get; set; }
        public enum CreateTypeClassification { allPublic, over18 }
        public CreateTypeClassification Clasification { get; set; }
        public DateTime CreationDate { get; set; }
        public int categoryId { get; set; }
    }
}
