using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Karbon.Cms.Core.IO
{
    internal class LocalFileStore : FileStore
    {
        private string _rootPath;
        private string _rootPhysicalPath;
        private char _pathSeperator;

        private FileSystemWatcher _fileSystemWatcher;

        public override void Initialize(NameValueCollection config)
        {
            base.Initialize(config);

            // Not keen on the fact this is not type safe
            // but it's a requirement of the provider pattern
            _rootPath = config["rootPath"];
            _pathSeperator = Convert.ToChar(config["pathSeperator"]);

            // Allow passing in the root physical path for testing
            _rootPhysicalPath = config.AllKeys.Any(x => x == "rootPhysicalPath")
                ? config["rootPhysicalPath"]
                : IOHelper.MapPath(_rootPath);

            // Setup a file system watcher to keep track of file changes
            if (Directory.Exists(_rootPhysicalPath))
            {
                RegisterFileSystemWatcher();
            }
        }

        /// <summary>
        /// Registers the file system watcher.
        /// </summary>
        private void RegisterFileSystemWatcher()
        {
            _fileSystemWatcher = new FileSystemWatcher(_rootPhysicalPath)
            {
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };
            _fileSystemWatcher.Created += FileChangedHandler;
            _fileSystemWatcher.Changed += FileChangedHandler;
            _fileSystemWatcher.Renamed += FileChangedHandler;
            _fileSystemWatcher.Deleted += FileChangedHandler;
            _fileSystemWatcher.Error += (sender, args) => RegisterFileSystemWatcher();
        }

        /// <summary>
        /// The handler for monitoring file changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="fileSystemEventArgs">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        private void FileChangedHandler(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            OnFileChanged(new FileChangedEventArgs
            {
                ChangeType = (FileChangeType)Enum.Parse(typeof (FileChangeType), fileSystemEventArgs.ChangeType.ToString()),
                FilePath = GetRelativePath(fileSystemEventArgs.FullPath)
            });
        }

        #region Directories

        public override IEnumerable<string> GetDirectories(string relativePath = "")
        {
            var path = GetPhysicalPath(relativePath)
                .EnsureTrailingDirectorySeparator();

            return Directory.Exists(path) 
                ? Directory.EnumerateDirectories(path).Select(GetRelativePath) 
                : Enumerable.Empty<string>();
        }

        public override void DeleteDirectory(string relativePath, bool recursive = false)
        {
            if (!DirectoryExists(relativePath))
                return;

            Directory.Delete(GetPhysicalPath(relativePath), recursive);
        }

        public override bool DirectoryExists(string relativePath)
        {
            return Directory.Exists(GetPhysicalPath(relativePath));
        }

        #endregion

        #region Files

        public override IEnumerable<string> GetFiles(string relativePath = "", string filter = "*.*")
        {
            var path = GetPhysicalPath(relativePath)
                .EnsureTrailingDirectorySeparator();

            return Directory.Exists(path)
                ? Directory.EnumerateFiles(path, filter).Select(GetRelativePath) 
                : Enumerable.Empty<string>();
        }

        public override void AddFile(string relativePath, Stream stream, bool overwrite = true)
        {
            if (FileExists(relativePath) && !overwrite)
                throw new InvalidOperationException(string.Format("A file at path '{0}' already exists", relativePath));

            EnsureDirectory(Path.GetDirectoryName(relativePath));

            if (stream.CanSeek)
                stream.Seek(0, 0);

            using (var destination = (Stream)File.Create(GetPhysicalPath(relativePath)))
                stream.CopyTo(destination);
        }

        public override Stream OpenFile(string relativePath)
        {
            return new FileStream(GetPhysicalPath(relativePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

        public override void DeleteFile(string relativePath)
        {
            if (!FileExists(relativePath))
                return;

            File.Delete(GetPhysicalPath(relativePath));
        }

        public override bool FileExists(string relativePath)
        {
            return File.Exists(GetPhysicalPath(relativePath));
        }

        #endregion

        #region Helper Methods

        public override DateTimeOffset GetLastModified(string relativePath)
        {
            return DirectoryExists(relativePath)
                ? Directory.GetLastWriteTimeUtc(GetPhysicalPath(relativePath))
                : File.GetLastWriteTimeUtc(GetPhysicalPath(relativePath));
        }

        public override DateTimeOffset GetCreated(string relativePath)
        {
            return DirectoryExists(relativePath)
                ? Directory.GetCreationTimeUtc(GetPhysicalPath(relativePath))
                : File.GetCreationTimeUtc(GetPhysicalPath(relativePath));
        }

        public override DateTimeOffset GetLastAccessed(string relativePath)
        {
            return DirectoryExists(relativePath)
                ? Directory.GetLastAccessTimeUtc(GetPhysicalPath(relativePath))
                : File.GetLastAccessTimeUtc(GetPhysicalPath(relativePath));
        }

        public override DateTimeOffset GetLastWrite(string relativePath)
        {
            return DirectoryExists(relativePath)
                ? Directory.GetLastWriteTimeUtc(GetPhysicalPath(relativePath))
                : File.GetLastWriteTimeUtc(GetPhysicalPath(relativePath));
        }

        public override long GetSize(string relativePath)
        {
            return DirectoryExists(relativePath)
                ? 0
                : new FileInfo(GetPhysicalPath(relativePath)).Length;
        }

        public override string GetAbsolutePath(string relativePath)
        {
            return _rootPath + _pathSeperator + relativePath;
        }

        public override string GetName(string path)
        {
            return GetPathParts(path).Last();
        }

        public override string GetNameWithoutExtension(string path)
        {
            var fileName = GetName(path);
            return fileName == null 
                ? null 
                : Path.GetFileNameWithoutExtension(fileName);
        }

        public override string GetDirectoryName(string path)
        {
            return GetRelativePath(Path.GetDirectoryName(GetAbsolutePath(path)));
        }

        public override IEnumerable<string> GetPathParts(string path)
        {
            return path.Trim(_pathSeperator).Split(_pathSeperator);
        }

        protected virtual string GetRelativePath(string physicalPath)
        {
            return physicalPath
                .TrimStart(_rootPhysicalPath.EnsureTrailingDirectorySeparator())
                .Replace(Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture), _pathSeperator.ToString(CultureInfo.InvariantCulture));
        }

        protected virtual string GetPhysicalPath(string relativePath)
        {
            return _rootPhysicalPath.EnsureTrailingDirectorySeparator() + relativePath
                .Replace(_pathSeperator.ToString(CultureInfo.InvariantCulture), Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture));
        }

        protected virtual void EnsureDirectory(string relativePath)
        {
            Directory.CreateDirectory(GetPhysicalPath(relativePath));
        }

        #endregion
    }
}
