using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Core
{
    public static class Constants
    {

        public static class JwtOptions
        {
            public const string Issuer = Audience;
            public const string Audience = "https://localhost:44382/";
            public const string Secret = "wqeqr2244435WhyMonkey_dont_saypazak_me_520fsdfks";



        }



        public static class Roles
        {
            public static string Manager = "Manager";
            public static string Customer = "Customer";
        }
    }
}
