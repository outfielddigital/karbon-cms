namespace Karbon.Cms.Core.Models
{
    public class File : Entity, IFile
    {
        public string ContentRelativeUrl { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
    }
}
