using System;

namespace Karbon.Cms.Core.IO
{
    internal class FileChangedEventArgs : EventArgs
    {
        public FileChangeType ChangeType { get; set; }
        public string FilePath { get; set; }
    }

    internal enum FileChangeType
    {
        All,
        Created,
        Renamed,
        Changed,
        Deleted
    }
}
