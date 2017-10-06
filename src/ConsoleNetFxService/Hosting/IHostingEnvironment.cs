﻿namespace ConsoleNetFxService.Hosting
{
    /// <summary>
    /// Provides information about the web hosting environment an application is running in.
    /// </summary>
    public interface IHostingEnvironment
    {
        /// <summary>
        /// Gets or sets the name of the environment. This property is automatically set by the host to the value
        /// of the "ASPNETCORE_ENVIRONMENT" environment variable.
        /// </summary>
        string EnvironmentName { get; set; }

        /// <summary>
        /// Gets or sets the absolute path to the directory that contains the application content files.
        /// </summary>
        string ContentRootPath { get; set; }
    }
}
