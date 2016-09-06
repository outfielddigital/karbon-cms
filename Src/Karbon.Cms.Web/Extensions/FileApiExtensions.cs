using System;
using System.Collections.Generic;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web
{
    public static class FileApiExtensions
    {
        #region Files
        
        #endregion

        #region Images

        /// <summary>
        /// Fits the specified image to the supplied max width.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidth">Width of the max.</param>
        /// <returns></returns>
        public static IFilteredImage FitWidth(this IImageFile image, int maxWidth)
        {
            return new FilteredImage(image).FitWidth(maxWidth);
        }

        /// <summary>
        /// Fits the specified image to the supplied max width.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidth">Width of the max.</param>
        /// <returns></returns>
        public static IFilteredImage FitWidth(this IFilteredImage image, int maxWidth)
        {
            image.Filters.Add("w", maxWidth);
            return image;
        }

        /// <summary>
        /// Fits the specified image to the supplied max height.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxHeight">Max height.</param>
        /// <returns></returns>
        public static IFilteredImage FitHeight(this IImageFile image, int maxHeight)
        {
            return new FilteredImage(image).FitHeight(maxHeight);
        }

        /// <summary>
        /// Fits the specified image to the supplied max width.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxHeight">Max height.</param>
        /// <returns></returns>
        public static IFilteredImage FitHeight(this IFilteredImage image, int maxHeight)
        {
            image.Filters.Add("h", maxHeight);
            return image;
        }

        /// <summary>
        /// Fits the specified image to the supplied max width / height.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidthHeight">max width / height.</param>
        /// <param name="fitMode">The fit mode.</param>
        /// <param name="scaleMode">The scale mode.</param>
        /// <param name="alignMode">The align mode.</param>
        /// <param name="format">The format.</param>
        /// <param name="quality">The quality.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="bgColor">Color of the background.</param>
        /// <returns></returns>
        public static IFilteredImage Fit(this IImageFile image, int maxWidthHeight, 
            FitMode fitMode = FitMode.Pad, ScaleMode scaleMode = ScaleMode.Down,
            AlignMode alignMode = AlignMode.MiddleCenter, ImageFormat format = ImageFormat.Auto,
            int quality = 90, int colors = 256, string bgColor = "")
        {
            return image.Fit(maxWidthHeight, maxWidthHeight, fitMode, scaleMode, alignMode, 
                format, quality, colors, bgColor);
        }

        /// <summary>
        /// Fits the specified image to the supplied max width / height.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidthHeight">max width / height.</param>
        /// <param name="fitMode">The fit mode.</param>
        /// <param name="scaleMode">The scale mode.</param>
        /// <param name="alignMode">The align mode.</param>
        /// <param name="format">The format.</param>
        /// <param name="quality">The quality.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="bgColor">Color of the background.</param>
        /// <returns></returns>
        public static IFilteredImage Fit(this IFilteredImage image, int maxWidthHeight, 
            FitMode fitMode = FitMode.Pad, ScaleMode scaleMode = ScaleMode.Down,
            AlignMode alignMode = AlignMode.MiddleCenter, ImageFormat format = ImageFormat.Auto,
            int quality = 90, int colors = 256, string bgColor = "")
        {
            return image.Fit(maxWidthHeight, maxWidthHeight, fitMode, scaleMode, alignMode, 
                format, quality, colors, bgColor);
        }

        /// <summary>
        /// Fits the specified image to the supplied max width and height.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidth">Width of the max.</param>
        /// <param name="maxHeight">Height of the max.</param>
        /// <param name="fitMode">The fit mode.</param>
        /// <param name="scaleMode">The scale mode.</param>
        /// <param name="alignMode">The align mode.</param>
        /// <param name="format">The format.</param>
        /// <param name="quality">The quality.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="bgColor">Color of the background.</param>
        /// <returns></returns>
        public static IFilteredImage Fit(this IImageFile image, int maxWidth, int maxHeight, 
            FitMode fitMode = FitMode.Pad, ScaleMode scaleMode = ScaleMode.Down,
            AlignMode alignMode = AlignMode.MiddleCenter, ImageFormat format = ImageFormat.Auto,
            int quality = 90, int colors = 256, string bgColor = "")
        {
            return new FilteredImage(image).Fit(maxWidth, maxHeight, fitMode, scaleMode, alignMode, 
                format, quality, colors, bgColor);
        }

        /// <summary>
        /// Fits the specified image to the supplied max width and height.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidth">Width of the max.</param>
        /// <param name="maxHeight">Height of the max.</param>
        /// <param name="fitMode">The fit mode.</param>
        /// <param name="scaleMode">The scale mode.</param>
        /// <param name="alignMode">The align mode.</param>
        /// <param name="format">The format.</param>
        /// <param name="quality">The quality.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="bgColor">Color of the background.</param>
        /// <returns></returns>
        public static IFilteredImage Fit(this IFilteredImage image, int maxWidth, int maxHeight, 
            FitMode fitMode = FitMode.Pad, ScaleMode scaleMode = ScaleMode.Down, 
            AlignMode alignMode = AlignMode.MiddleCenter, ImageFormat format = ImageFormat.Auto,
            int quality = 90, int colors = 256, string bgColor = "")
        {
            image.Filters.Add("w", maxWidth);
            image.Filters.Add("h", maxHeight);

            if(fitMode != FitMode.Pad)
                image.Filters.Add("mode", fitMode.ToString().ToLower());

            if(scaleMode != ScaleMode.Down)
                image.Filters.Add("scale", scaleMode.ToString().ToLower());

            if(alignMode != AlignMode.MiddleCenter)
                image.Filters.Add("anchor", alignMode.ToString().ToLower());

            if(format != ImageFormat.Auto)
                image.Filters.Add("format", format.ToString().ToLower());

            if(quality != 90)
                image.Filters.Add("quality", Math.Min(100, Math.Max(0, quality)));

            if (colors != 256)
                image.Filters.Add("colors", Math.Min(256, Math.Max(2, quality)));

            if (!string.IsNullOrEmpty(bgColor))
                image.Filters.Add("bgcolor", bgColor);

            return image;
        }

        /// <summary>
        /// Gets the absolute url for the given image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public static string Url(this IFilteredImage image)
        {
            return image.Image.Url() + image.Filters.ToQueryString();
        }

        #endregion

        #region Videos

        #endregion

        #region Sounds

        #endregion

        #region Documents

        #endregion
    }

    #region Helper Classes

    public interface IFilteredImage
    {
        IImageFile Image { get; set; }
        IDictionary<string, object> Filters { get; set; }
    }

    public class FilteredImage : IFilteredImage
    {
        public IImageFile Image { get; set; }
        public IDictionary<string, object> Filters { get; set; }

        public FilteredImage(IImageFile image)
        {
            Image = image;
            Filters = new Dictionary<string, object>();
        }

        public override string ToString()
        {
            return this.Url();
        }
    }

    public enum FitMode
    {
        Pad,
        Max,
        Crop,
        Stretch,
        Carve
    }

    public enum ScaleMode
    {
        Down,
        Both,
        Canvas
    }

    public enum AlignMode
    {
        TopLeft, 
        TopCenter, 
        TopRight, 
        MiddleLeft, 
        MiddleCenter, 
        MiddleRight, 
        BottomLeft, 
        BottomCenter, 
        BottomRight
    }

    public enum ImageFormat
    {
        Auto,
        Jpg,
        Gif,
        Png
    }

    #endregion
}
