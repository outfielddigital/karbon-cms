namespace Karbon.Cms.Core.Models
{
    public interface IImageFile : IFile
    {
        int Width { get; set; }
        int Height { get; set; }
    }
}
