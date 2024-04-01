using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS_API.BusinessLogic
{
    public class TokenGenerator
    {
        public static string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}