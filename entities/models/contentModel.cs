namespace entities.models
{
    public class contentModel
    {
        public int id { get; set; }
        public int sectionId { get; set; }
        public string? content { get; set; }
        public int? order { get; set; }
    }
}