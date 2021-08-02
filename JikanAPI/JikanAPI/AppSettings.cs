using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JikanAPI
{
    public static class AppSettings
    {
        public static string Secret { get; }

        static Random _rng = new Random();
        
        const int SECRET_SIZE = 128;

        static AppSettings()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < SECRET_SIZE; i++)
            {
                sb.Append((char)_rng.Next(32, 128));
            }

            Secret = sb.ToString();
        }
    }
}
