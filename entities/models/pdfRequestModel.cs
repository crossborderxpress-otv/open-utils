namespace entities.models
{
    public class pdfRequestModel
    {
        public List<documentModel>? documents { get; set; }
        public List<sectionModel>? sections { get; set; }
        public List<contentModel>? contents { get; set; }
        public byte[]? horizontalFooterSeparator { get; set; }
        public byte[]? verticalSeparator { get; set; }
        public byte[]? horizontalSeparator { get; set; }
    }
}