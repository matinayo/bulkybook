using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Utility
{
    // this class is for getting the SendGridKey and SendGridUser from appsettings.json
    public class EmailOptions
    {
        // to populate this properties, configure using dependency injection in startup.cs
        public string SendGridKey { get; set; }

        public string SendGridUser { get; set; }
    }
}
