using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.IO;

namespace Karbon.Cms.Core.IO
{
    internal abstract class FileStore : ProviderBase
    {
        public event EventHandler<FileChangedEventArgs> FileChanged;

        public void OnFileChanged(FileChangedEventArgs e)
        {
            if (FileChanged != null)
            {
                FileChanged(this, e);
            }
        }

        public override void Initialize(string name, 
            NameValueCollection config)
        {
            base.Initialize(name, config);
            Initialize(config);
        }

        public virtual void Initialize(NameValueCollection config) { }

        #region Directories

        public abstract IEnumerable<string> GetDirectories(string relativePath = "");
        public abstract void DeleteDirectory(string relativePath, bool recursive = false);
        public abstract bool DirectoryExists(string relativePath);

        #endregion

        #region Files

        public abstract IEnumerable<string> GetFiles(string relativePath = "", string filter = "*.*");
        public abstract void AddFile(string relativePath, Stream stream, bool overwrite = true);
        public abstract Stream OpenFile(string relativePath);
        public abstract void DeleteFile(string relativePath);
        public abstract bool FileExists(string relativePath);

        #endregion

        #region Helper Methods

        public abstract DateTimeOffset GetLastModified(string relativePath);
        public abstract DateTimeOffset GetCreated(string relativePath);
        public abstract DateTimeOffset GetLastAccessed(string relativePath);
        public abstract DateTimeOffset GetLastWrite(string relativePath);
        public abstract long GetSize(string relativePath);
        public abstract string GetAbsolutePath(string relativePath);

        public abstract IEnumerable<string> GetPathParts(string path);

        public abstract string GetName(string path);
        public abstract string GetNameWithoutExtension(string path);
        public abstract string GetDirectoryName(string path);

        #endregion
    }
}
