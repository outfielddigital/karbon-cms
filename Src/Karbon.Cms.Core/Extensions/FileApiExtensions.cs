using System;
using System.Collections.Generic;
using System.Linq;
using Karbon.Cms.Core.IO;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Core.Stores;

namespace Karbon.Cms.Core
{
    public static class FileApiExtensions
    {
        #region Files

        /// <summary>
        /// Determines whether the specified file is an image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if the specified file is an image; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsImage(this IFile file)
        {
            if (file.Extension == null)
                return false;

            return IOHelper.IsImageExtension(file.Extension.ToLower());
        }

        /// <summary>
        /// Determines whether the specified file is a video.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if the specified file is a video; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsVideo(this IFile file)
        {
            if (file.Extension == null)
                return false;

            return IOHelper.IsVideoExtension(file.Extension.ToLower());
        }

        /// <summary>
        /// Determines whether the specified file is a sound.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if the specified file is a sound; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSound(this IFile file)
        {
            if (file.Extension == null)
                return false;

            return IOHelper.IsSoundExtension(file.Extension.ToLower());
        }

        /// <summary>
        /// Determines whether the specified file is a document.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if the specified file is a document; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDocument(this IFile file)
        {
            if (file.Extension == null)
                return false;

            return IOHelper.IsDocumentExtension(file.Extension.ToLower());
        }

        /// <summary>
        /// Gets the mime type for the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static string MimeType(this IFile file)
        {
            return IOHelper.MimeTypes.ContainsKey(file.Extension.ToLower())
                ? IOHelper.MimeTypes[file.Extension.ToLower()]
                : "application/octet-stream";
        }

        /// <summary>
        /// Gets the file size in a readable format.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static string NiceSize(this IFile file)
        {
            var suf = new []{ "B", "KB", "MB", "GB", "TB", "PB", "EB" };

            if (file.Size == 0)
                return "0" + suf[0];

            var bytes = Math.Abs(file.Size);
            var sufIdx = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var newSize = Math.Round(bytes / Math.Pow(1024, sufIdx), 1);

            return (Math.Sign(file.Size) * newSize).ToString("0.##") + suf[sufIdx];
        }

        /// <summary>
        /// Determines whether the specified file has a next sibling.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if the specified file has a next sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasNext(this IFile file)
        {
            return file.HasNext<IFile>();
        }

        /// <summary>
        /// Determines whether the specified file has a next sibling.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if the specified file has a next sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasNext<TFileType>(this IFile file)
            where TFileType : IFile
        {
            return file.HasNext<TFileType>(x => true);
        }

        /// <summary>
        /// Determines whether the specified file has a next sibling.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        ///   <c>true</c> if the specified file has a next sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasNext(this IFile file, Func<IFile, bool> filter)
        {
            return file.HasNext<IFile>();
        }

        /// <summary>
        /// Determines whether the specified file has a next sibling.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="file">The file.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        ///   <c>true</c> if the specified file has a next sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasNext<TFileType>(this IFile file, Func<TFileType, bool> filter)
            where TFileType : IFile
        {
            var next = file.Next<TFileType>(filter);
            return !EqualityComparer<TFileType>.Default.Equals(next, default(TFileType));
        }

        /// <summary>
        /// Determines whether the specified file has a previous sibling.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if the specified file has a previous sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasPrev(this IFile file)
        {
            return file.HasPrev<IFile>();
        }

        /// <summary>
        /// Determines whether the specified file has a previous sibling.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if the specified file has a previous sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasPrev<TFileType>(this IFile file)
            where TFileType : IFile
        {
            return file.HasPrev<TFileType>(x => true);
        }

        /// <summary>
        /// Determines whether the specified file has a previous sibling.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        ///   <c>true</c> if the specified file has a previous sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasPrev(this IFile file, Func<IFile, bool> filter)
        {
            return file.HasPrev<IFile>(filter);
        }

        /// <summary>
        /// Determines whether the specified file has a previous sibling.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="file">The file.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        ///   <c>true</c> if the specified file has a previous sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasPrev<TFileType>(this IFile file, Func<TFileType, bool> filter)
            where TFileType : IFile
        {
            var prev = file.Prev<TFileType>(filter);
            return !EqualityComparer<TFileType>.Default.Equals(prev, default(TFileType));
        }

        /// <summary>
        /// Gets the next sibling.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        /// The next sibling.
        /// </returns>
        public static IFile Next(this IFile file)
        {
            return file.Next<IFile>();
        }

        /// <summary>
        /// Gets the next sibling.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="file">The file.</param>
        /// <returns>
        /// The next sibling.
        /// </returns>
        public static TFileType Next<TFileType>(this IFile file)
            where TFileType : IFile
        {
            return file.Next<TFileType>(x => true);
        }

        /// <summary>
        /// Gets the next sibling.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// The next sibling.
        /// </returns>
        public static IFile Next(this IFile file, Func<IFile, bool> filter)
        {
            return file.Next<IFile>(filter);
        }

        /// <summary>
        /// Gets the next sibling.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="file">The file.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// The next sibling.
        /// </returns>
        public static TFileType Next<TFileType>(this IFile file, Func<TFileType, bool> filter)
            where TFileType : IFile
        {
            var content = StoreManager.ContentStore.GetByUrl(file.ContentRelativeUrl);
            var files = content.Files().ToList();

            return files.SkipWhile(x => x.RelativeUrl != content.RelativeUrl).Skip(1)
                .Where(x => typeof(TFileType).IsAssignableFromExtended(x.GetType()))
                .Cast<TFileType>().FirstOrDefault(filter);
        }

        /// <summary>
        /// Gets the previous sibling.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        /// The previous sibling.
        /// </returns>
        public static IFile Prev(this IFile file)
        {
            return file.Prev<IFile>();
        }

        /// <summary>
        /// Gets the previous sibling.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="file">The file.</param>
        /// <returns>
        /// The previous sibling.
        /// </returns>
        public static TFileType Prev<TFileType>(this IFile file)
            where TFileType : IFile
        {
            return file.Prev<TFileType>(x => true);
        }

        /// <summary>
        /// Gets the previous sibling.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        ///   The previous sibling.
        /// </returns>
        public static IFile Prev(this IFile file, Func<IFile, bool> filter)
        {
            return file.Prev<IFile>(filter);
        }

        /// <summary>
        /// Gets the previous sibling.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="file">The file.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        ///   The previous sibling.
        /// </returns>
        public static TFileType Prev<TFileType>(this IFile file, Func<TFileType, bool> filter)
            where TFileType : IFile
        {
            var content = StoreManager.ContentStore.GetByUrl(file.ContentRelativeUrl);
            var files = content.Files().ToList();

            files.Reverse();

            return files.SkipWhile(x => x.RelativeUrl != content.RelativeUrl).Skip(1)
                .Where(x => typeof(TFileType).IsAssignableFromExtended(x.GetType()))
                .Cast<TFileType>().FirstOrDefault(filter);
        }

        #endregion
    }
}
