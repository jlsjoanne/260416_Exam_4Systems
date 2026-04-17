using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BCrypt.Net;

namespace _260416_Exam_4Systems
{
    public static class SecurityHelper
    {
        // Hash pwd when signup
        public static string HashPassword(string pwd)
        {
            return BCrypt.Net.BCrypt.HashPassword(pwd);
        }

        public static bool VerifyPassword(string pwd, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(pwd, storedHash);
        }
    }
}