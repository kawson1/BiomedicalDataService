using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    internal static class Settings
    {
        public static readonly string  host     = "localhost";
        public static readonly int     port     = 1884;
        public static readonly string  clientId = Guid.NewGuid().ToString();
        public static readonly string  username = "user1";
        public static readonly string  password = "user1";
    }
}
