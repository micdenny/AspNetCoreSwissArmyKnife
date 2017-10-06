using System;

namespace AspNetCoreSwagger.Services
{
    public class ProductServiceOptions
    {
        public string ServiceUrl { get; set; }
        public TimeSpan DefaultTimeout { get; set; }
    }
}
