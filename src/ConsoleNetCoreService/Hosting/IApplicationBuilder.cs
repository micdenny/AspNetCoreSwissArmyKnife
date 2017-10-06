using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleNetCoreService.Hosting
{
    /// <summary>
    /// Defines a class that provides the mechanisms to configure an application's request pipeline.
    /// </summary>
    public interface IApplicationBuilder
    {
        /// <summary>
        /// Gets or sets the <see cref="IServiceProvider"/> that provides access to the application's service container.
        /// </summary>
        IServiceProvider ApplicationServices { get; set; }

        /// <summary>
        /// Gets a key/value collection that can be used to share data between middleware.
        /// </summary>
        IDictionary<string, object> Properties { get; }

        /// <summary>
        /// Adds a middleware delegate to the application's request pipeline.
        /// </summary>
        /// <param name="middleware">The middleware delegate.</param>
        /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
        IApplicationBuilder Use(Func<RunDelegate, RunDelegate> middleware);

        /// <summary>
        /// Builds the delegate used by this application to run.
        /// </summary>
        /// <returns>The request handling delegate.</returns>
        RunDelegate Build();
    }

    public class ApplicationBuilder : IApplicationBuilder
    {
        private readonly IList<Func<RunDelegate, RunDelegate>> _components = new List<Func<RunDelegate, RunDelegate>>();

        public ApplicationBuilder(IServiceProvider serviceProvider)
        {
            Properties = new Dictionary<string, object>(StringComparer.Ordinal);
            ApplicationServices = serviceProvider;
        }

        public IServiceProvider ApplicationServices
        {
            get
            {
                return GetProperty<IServiceProvider>("application.Services");
            }
            set
            {
                SetProperty<IServiceProvider>("application.Services", value);
            }
        }

        public IDictionary<string, object> Properties { get; }

        private T GetProperty<T>(string key)
        {
            object value;
            return Properties.TryGetValue(key, out value) ? (T)value : default(T);
        }

        private void SetProperty<T>(string key, T value)
        {
            Properties[key] = value;
        }

        public IApplicationBuilder Use(Func<RunDelegate, RunDelegate> middleware)
        {
            _components.Add(middleware);
            return this;
        }

        public RunDelegate Build()
        {
            RunDelegate app = () =>
            {
                return Task.CompletedTask;
            };

            foreach (var component in _components.Reverse())
            {
                app = component(app);
            }

            return app;
        }
    }

    /// <summary>
    /// A function that can run the application.
    /// </summary>
    /// <returns>A task that represents the completion of request processing.</returns>
    public delegate Task RunDelegate();
}