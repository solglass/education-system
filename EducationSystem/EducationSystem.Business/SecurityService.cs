using System;
using System.Collections.Generic;
using System.Text;
using BCrypt.Net;

namespace EducationSystem.Business
{
    class SecurityService
    {

        private const string _secret = "";

        public string GetHash(string password)
        { 
             
            return BCrypt.Net.BCrypt.HashPassword(password + _secret);
        }

        public bool VerifyHashAndPassword(string hashedPwdFromDatabase, string userEnteredPassword)
        {
            return BCrypt.Net.BCrypt.Verify(userEnteredPassword + _secret, hashedPwdFromDatabase);
        }
    }
}
