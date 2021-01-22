using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EducationSystem.Data
{
    public abstract class BaseRepository
    {
        protected SqlConnection _connection;

        protected string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
    }
}
