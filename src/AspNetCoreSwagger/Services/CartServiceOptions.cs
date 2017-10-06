using System;

namespace AspNetCoreSwagger.Services
{
    public class CartServiceOptions
    {
        public string ServiceUrl { get; set; }
        public TimeSpan DefaultTimeout { get; set; }
        public TimeSpan BuyTimeout { get; set; }
    }
}
