using System;
using System.Collections.Generic;
using System.Linq;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Core.Stores;

namespace Karbon.Cms.Core
{
    public static class ContentApiExtensions
    {
        /// <summary>
        /// Gets the parent content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IContent Parent(this IContent content)
        {
            return StoreManager.ContentStore.GetParent(content);
        }

        /// <summary>
        /// Gets the parent content cast to the specified type.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static TContentType Parent<TContentType>(this IContent content)
            where TContentType : IContent
        {
            return (TContentType)content.Parent();
        }

        /// <summary>
        /// Gets the parent/ancestors of the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<IContent> Parents(this IContent content)
        {
            return StoreManager.ContentStore.GetAncestors(content);
        }

        /// <summary>
        /// Gets the parent ancestor content cast to the specified type.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<TContentType> Parents<TContentType>(this IContent content)
            where TContentType : IContent
        {
            return content.Parents(x => typeof(TContentType).IsAssignableFromExtended(x.GetType()))
                .Cast<TContentType>();
        }

        /// <summary>
        /// Gets the parent ancestor content filtered by the provided filter function.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<IContent> Parents(this IContent content, Func<IContent, bool> filter)
        {
            return content.Parents().Where(filter);
        }

        /// <summary>
        /// Gets the parent ancestor content filtered by the provided filter function, cast to the specified type.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<TContentType> Parents<TContentType>(this IContent content, Func<TContentType, bool> filter)
            where TContentType : IContent
        {
            return content.Parents<TContentType>()
                .Where(filter);
        }

        /// <summary>
        /// Gets the closest ancestor based on the filter function.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IContent Closest(this IContent content, Func<IContent, bool> filter)
        {
            return content.Closest<IContent>(filter);
        }

        /// <summary>
        /// Gets the closest ancestor of the given type.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static TContentType Closest<TContentType>(this IContent content)
            where TContentType : IContent
        {
            return content.Closest<TContentType>(x => true);
        }

        /// <summary>
        /// Gets the closest ancestor of the given type filtered by the filter function.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static TContentType Closest<TContentType>(this IContent content, Func<TContentType, bool> filter)
            where TContentType : IContent
        {
            return new[] { content }.Concat(content.Parents())
                .Where(x => typeof(TContentType).IsAssignableFromExtended(x.GetType()))
                .Cast<TContentType>().FirstOrDefault(filter);
        }

        /// <summary>
        /// Gets the child content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<IContent> Children(this IContent content)
        {
            return StoreManager.ContentStore.GetChildren(content);
        }

        /// <summary>
        /// Gets the child content cast to the specified type.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<TContentType> Children<TContentType>(this IContent content)
            where TContentType : IContent
        {
            return content.Children(x => typeof(TContentType).IsAssignableFromExtended(x.GetType()))
                .Cast<TContentType>();
        }

        /// <summary>
        /// Gets the child content filtered by the provided filter function.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<IContent> Children(this IContent content, Func<IContent, bool> filter)
        {
            return content.Children().Where(filter);
        }

        /// <summary>
        /// Gets the child content filtered by the provided filter function, cast to the specified type.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<TContentType> Children<TContentType>(this IContent content, Func<TContentType, bool> filter)
            where TContentType : IContent
        {
            return content.Children<TContentType>()
                .Where(filter);
        }

        /// <summary>
        /// Finds descendant content filtered by the supplied filter function.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<IContent> Find(this IContent content, Func<IContent, bool> filter)
        {
            return StoreManager.ContentStore.GetDescendants(content)
                .Where(filter);
        }

        /// <summary>
        /// Finds descendant content of the given type.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<TContentType> Find<TContentType>(this IContent content)
            where TContentType : IContent
        {
            return content.Find(x => typeof(TContentType).IsAssignableFromExtended(x.GetType())).Cast<TContentType>();
        }

        /// <summary>
        /// Gets the sibling content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<IContent> Siblings(this IContent content)
        {
            if (content.Parent() == null)
                return Enumerable.Empty<IContent>();

            return content.Parent().Children(x => x.RelativeUrl != content.RelativeUrl);
        }

        /// <summary>
        /// Gets the sibling content filtered by the supplied filter function
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<IContent> Siblings(this IContent content, Func<IContent, bool> filter)
        {
            return content.Siblings().Where(filter);
        }

        /// <summary>
        /// Gets the sibling content of the given type.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<TContentType> Siblings<TContentType>(this IContent content)
            where TContentType : IContent
        {
            return content.Siblings(x => typeof(TContentType).IsAssignableFromExtended(x.GetType()))
                .Cast<TContentType>();
        }

        /// <summary>
        /// Gets the sibling content of the given type filtered by the supplied filter function.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<TContentType> Siblings<TContentType>(this IContent content, Func<TContentType, bool> filter)
            where TContentType : IContent
        {
            return content.Siblings<TContentType>()
                .Where(filter);
        }

        /// <summary>
        /// Finds descendant content of the given type filtered by the supplied filter function.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<TContentType> Find<TContentType>(this IContent content, Func<TContentType, bool> filter)
            where TContentType : IContent
        {
            return content.Find<TContentType>()
                .Where(filter);
        }

        /// <summary>
        /// Determines whether the specified content is visible.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content is visible; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsVisible(this IContent content)
        {
            return content.SortOrder >= 0;
        }

        /// <summary>
        /// Determines whether the specified content is the home page.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content is the home page; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsHomePage(this IContent content)
        {
            return content.Depth == 1;
        }

        /// <summary>
        /// Gets the files for the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<IFile> Files(this IContent content)
        {
            return content.AllFiles;
        }

        /// <summary>
        /// Gets the files of a given type for the specified content.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<TFileType> Files<TFileType>(this IContent content)
            where TFileType : IFile
        {
            return content.AllFiles.Where(x => typeof(TFileType).IsAssignableFromExtended(x.GetType()))
                .Cast<TFileType>();
        }

        /// <summary>
        /// Gets the files for the specified content filtered by the specified filter function.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<IFile> Files(this IContent content, Func<IFile, bool> filter)
        {
            return content.AllFiles.Where(filter);
        }

        /// <summary>
        /// Gets the files of a given type for the specified content filtered by the specified filter function.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<TFileType> Files<TFileType>(this IContent content, Func<TFileType, bool> filter)
            where TFileType : IFile
        {
            return content.Files<TFileType>().Where(filter);
        }

        /// <summary>
        /// Gets the image files for the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<IImageFile> Images(this IContent content)
        {
            return content.Files<IImageFile>(x => x.IsImage());
        }

        /// <summary>
        /// Gets the image files of a given type for the specified content.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<TFileType> Images<TFileType>(this IContent content)
            where TFileType : IImageFile
        {
            return content.Images().Where(x => typeof(TFileType).IsAssignableFromExtended(x.GetType()))
                .Cast<TFileType>();
        }

        /// <summary>
        /// Gets the image files for the specified content filtered by the specified filter function.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<IImageFile> Images(this IContent content, Func<IImageFile, bool> filter)
        {
            return content.Images().Where(filter);
        }

        /// <summary>
        /// Gets the image files of a given type for the specified content filtered by the specified filter function.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<TFileType> Images<TFileType>(this IContent content, Func<TFileType, bool> filter)
            where TFileType : IImageFile
        {
            return content.Images<TFileType>().Where(filter);
        }

        /// <summary>
        /// Gets the video files for the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<IVideoFile> Videos(this IContent content)
        {
            return content.Files<IVideoFile>(x => x.IsVideo());
        }

        /// <summary>
        /// Gets the video files of a given type for the specified content.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<TFileType> Videos<TFileType>(this IContent content)
            where TFileType : IVideoFile
        {
            return content.Videos().Where(x => typeof(TFileType).IsAssignableFromExtended(x.GetType()))
                .Cast<TFileType>();
        }

        /// <summary>
        /// Gets the video files for the specified content filtered by the specified filter function.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<IVideoFile> Images(this IContent content, Func<IVideoFile, bool> filter)
        {
            return content.Videos().Where(filter);
        }

        /// <summary>
        /// Gets the video files of a given type for the specified content filtered by the specified filter function.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<TFileType> Videos<TFileType>(this IContent content, Func<TFileType, bool> filter)
            where TFileType : IVideoFile
        {
            return content.Videos<TFileType>().Where(filter);
        }

        /// <summary>
        /// Gets the sound files for the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<ISoundFile> Sounds(this IContent content)
        {
            return content.Files<ISoundFile>(x => x.IsSound());
        }

        /// <summary>
        /// Gets the sound files of a given type for the specified content.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<TFileType> Sounds<TFileType>(this IContent content)
            where TFileType : ISoundFile
        {
            return content.Sounds().Where(x => typeof(TFileType).IsAssignableFromExtended(x.GetType()))
                .Cast<TFileType>();
        }

        /// <summary>
        /// Gets the sound files for the specified content filtered by the specified filter function.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<ISoundFile> Sounds(this IContent content, Func<ISoundFile, bool> filter)
        {
            return content.Sounds().Where(filter);
        }

        /// <summary>
        /// Gets the sound files of a given type for the specified content filtered by the specified filter function.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<TFileType> Sounds<TFileType>(this IContent content, Func<TFileType, bool> filter)
            where TFileType : ISoundFile
        {
            return content.Sounds<TFileType>().Where(filter);
        }

        /// <summary>
        /// Gets the document files for the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<IDocumentFile> Documents(this IContent content)
        {
            return content.Files<IDocumentFile>(x => x.IsDocument());
        }

        /// <summary>
        /// Gets the document files of a given type for the specified content.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<TFileType> Documents<TFileType>(this IContent content)
            where TFileType : IDocumentFile
        {
            return content.Documents().Where(x => typeof(TFileType).IsAssignableFromExtended(x.GetType()))
                .Cast<TFileType>();
        }

        /// <summary>
        /// Gets the document files for the specified content filtered by the specified filter function.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<IDocumentFile> Documents(this IContent content, Func<IDocumentFile, bool> filter)
        {
            return content.Documents().Where(filter);
        }

        /// <summary>
        /// Gets the document files of a given type for the specified content filtered by the specified filter function.
        /// </summary>
        /// <typeparam name="TFileType">The type of the file type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<TFileType> Documents<TFileType>(this IContent content, Func<TFileType, bool> filter)
            where TFileType : IDocumentFile
        {
            return content.Documents<TFileType>().Where(filter);
        }

        /// <summary>
        /// Determines whether this content is a child of the specified content.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if this content is a child of the specified content; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsChildOf(this IContent child, IContent content)
        {
            return child.RelativeUrl == content.RelativeUrl.TrimEnd("/") + "/" + child.Slug; 
        }

        /// <summary>
        /// Determines whether this content is an ancestor of the specified content.
        /// </summary>
        /// <param name="ancestor">The ancestor.</param>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if this content is an ancestor of the specified content; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAncestorOf(this IContent ancestor, IContent content)
        {
            return content.RelativeUrl.StartsWith(ancestor.RelativeUrl.TrimEnd("/") + "/");
        }

        /// <summary>
        /// Determines whether this content is a descendant of the specified content.
        /// </summary>
        /// <param name="descendant">The descendant.</param>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if this content is a descendant of the specified content; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDescendantOf(this IContent descendant, IContent content)
        {
            return content.IsAncestorOf(descendant);
        }

        /// <summary>
        /// Determines whether the specified content has a next sibling.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content has a next sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasNext(this IContent content)
        {
            return content.HasNext(x => true); 
        }

        /// <summary>
        /// Determines whether the specified content has a next sibling.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content has a next sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasNext<TContentType>(this IContent content)
            where TContentType : IContent
        {
            return content.HasNext<TContentType>(x => true);
        }

        /// <summary>
        /// Determines whether the specified content has a next sibling.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        ///   <c>true</c> if the specified content has a next sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasNext(this IContent content, Func<IContent, bool> filter)
        {
            return content.HasNext<IContent>(filter);
        }

        /// <summary>
        /// Determines whether the specified content has a next sibling.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        ///   <c>true</c> if the specified content has a next sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasNext<TContentType>(this IContent content, Func<TContentType, bool> filter)
            where TContentType : IContent
        {
            var next = content.Next<TContentType>(filter);
            return !EqualityComparer<TContentType>.Default.Equals(next, default(TContentType));
        }

        /// <summary>
        /// Gets the next sibling.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IContent Next(this IContent content)
        {
            return content.Next<IContent>();
        }

        /// <summary>
        /// Gets the next sibling.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static TContentType Next<TContentType>(this IContent content)
            where TContentType : IContent
        {
            return content.Next<TContentType>(x => true);
        }

        /// <summary>
        /// Gets the next sibling.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IContent Next(this IContent content, Func<IContent, bool> filter)
        {
            return content.Next<IContent>(filter);
        }

        /// <summary>
        /// Gets the next sibling.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static TContentType Next<TContentType>(this IContent content, Func<TContentType, bool> filter)
            where  TContentType : IContent
        {
            var parent = content.Parent();
            if (parent == null)
                return default(TContentType);

            var children = parent.Children().ToList();
            return children.SkipWhile(x => x.RelativeUrl != content.RelativeUrl).Skip(1)
                .Where(x => typeof(TContentType).IsAssignableFromExtended(x.GetType()))
                .Cast<TContentType>().FirstOrDefault(filter);
        }

        /// <summary>
        /// Determines whether the specified content has a previous sibling.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content has a previous sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasPrev(this IContent content)
        {
            return content.HasPrev(x => true);
        }

        /// <summary>
        /// Determines whether the specified content has a previous sibling.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content has a previous sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasPrev<TContentType>(this IContent content)
            where TContentType : IContent
        {
            return content.HasPrev<TContentType>(x => true);
        }

        /// <summary>
        /// Determines whether the specified content has a previous sibling.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        ///   <c>true</c> if the specified content has a previous sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasPrev(this IContent content, Func<IContent, bool> filter)
        {
            return content.HasPrev<IContent>(filter);
        }

        /// <summary>
        /// Determines whether the specified content has a previous sibling.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        ///   <c>true</c> if the specified content has a previous sibling; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasPrev<TContentType>(this IContent content, Func<TContentType, bool> filter)
            where TContentType : IContent
        {
            var prev = content.Prev<TContentType>(filter);
            return !EqualityComparer<TContentType>.Default.Equals(prev, default(TContentType));
        }

        /// <summary>
        /// Gets the previous sibling.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IContent Prev(this IContent content)
        {
            return content.Prev<IContent>();
        }

        /// <summary>
        /// Gets the previous sibling.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static TContentType Prev<TContentType>(this IContent content)
            where TContentType : IContent
        {
            return content.Prev<TContentType>(x => true);
        }

        /// <summary>
        /// Gets the previous sibling.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IContent Prev(this IContent content, Func<IContent, bool> filter)
        {
            return content.Prev<IContent>(filter);
        }

        /// <summary>
        /// Gets the previous sibling.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static TContentType Prev<TContentType>(this IContent content, Func<TContentType, bool> filter)
            where TContentType : IContent
        {
            var parent = content.Parent();
            if (parent == null)
                return default(TContentType);

            var children = parent.Children().ToList();

            children.Reverse();

            return children.SkipWhile(x => x.RelativeUrl != content.RelativeUrl).Skip(1)
                .Where(x => typeof(TContentType).IsAssignableFromExtended(x.GetType()))
                .Cast<TContentType>().FirstOrDefault(filter);
        }
    }
}
