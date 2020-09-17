namespace Petroteks.MvcUi.Models
{
    public class GcsEntity
    {
        public GcsEntity(dynamic entity)
        {
            Title = entity.htmlTitle;
            Link = entity.link;
            FormattedUrl = entity.htmlFormattedUrl;
            Snippet = entity.htmlSnippet;
            if (entity?.pagemap?.cse_thumbnail?[0] != null)
                Image = new GcsEntityImage(entity.pagemap.cse_thumbnail[0]);
        }
        public string Title { get; set; }
        public string Link { get; set; }
        public string FormattedUrl { get; set; }
        public string Snippet { get; set; }
        public GcsEntityImage Image { get; set; }
    }
    public class GcsEntityImage
    {
        public GcsEntityImage(dynamic image)
        {
            Src = image.src;
            Width = image.width;
            Height = image.height;
        }
        public string Src { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
    }
}