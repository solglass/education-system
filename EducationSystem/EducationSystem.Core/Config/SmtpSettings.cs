using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Core.Config
{
    public class SmtpSettings
    {
        public string From { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string SmtpServer{ get; set; }
        public int SmtpPort{ get; set; }

    }
}
