using System;

namespace Karbon.Cms.Core.Models
{
    /// <summary>
    /// Used to decorate a content model
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ContentAttribute : Attribute 
    {
        /// <summary>
        /// Gets or sets the type of the controller.
        /// </summary>
        /// <value>
        /// The type of the controller.
        /// </value>
        public Type ControllerType { get; set; }

        /// <summary>
        /// Gets or sets the allowed views.
        /// </summary>
        /// <value>
        /// The allowed views.
        /// </value>
        //public string[] AllowedViews { get; set; }

        /// <summary>
        /// Gets or sets the default view.
        /// </summary>
        /// <value>
        /// The default view.
        /// </value>
        //public string DefaultView { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentAttribute"/> class.
        /// </summary>
        public ContentAttribute()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentAttribute"/> class.
        /// </summary>
        /// <param name="controllerType">Type of the controller.</param>
        public ContentAttribute(Type controllerType)
        {
            ControllerType = controllerType;
        }
    }
}
