using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Utility
{
    public class StripeSettings
    {
        // populate values form appsettings.json
        public string SecretKey { get; set; }

        public string PublishableKey { get; set; }

        // load them in startup.cs file
    }
}
