using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EducationSystem.Data
{
    public class ThemeRepository
    {
        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public ThemeRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<ThemeDto> GetThemes()
        {
            var theme = _connection
                 .Query<ThemeDto>("dbo.Theme_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return theme;
        }
        public ThemeDto GetThemeById(int id)
        {
            var theme = _connection
                .Query<ThemeDto>("dbo.Theme_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return theme;
        }
    }
}
