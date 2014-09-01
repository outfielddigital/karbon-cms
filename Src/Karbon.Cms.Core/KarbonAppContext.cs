namespace Karbon.Cms.Core
{
    internal class KarbonAppContext
    {
        #region Factory

        /// <summary>
        /// Gets the current AppContext.
        /// </summary>
        /// <value>
        /// The current AppContext.
        /// </value>
        public static KarbonAppContext Current { get; internal set; }

        #endregion

        /// <summary>
        /// Gets or sets the current environment context.
        /// </summary>
        /// <value>
        /// The environment context.
        /// </value>
        internal EnvironmentContext Environment { get; set; }

        /// <summary>
        /// Whether or not Karbon should ignore the homepage.
        /// </summary>
        /// <value>
        /// Whether to ignore the homepage.
        /// </value>
        internal bool IgnoreHomepage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KarbonAppContext"/> class.
        /// </summary>
        internal KarbonAppContext()
        {
            // Setup the default environment behaviour
            Environment = new EnvironmentContext();
        }
    }
}
