namespace Karbon.Cms.Core.Models
{
    public class ImageFile : File, IImageFile
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
