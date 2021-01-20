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
        public int AddTheme(string name)
        {
            var result = _connection
                .Execute("dbo.Theme_Add",
                new
                {
                    name
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int UpdateTheme(int id, string name)
        {
            var result = _connection
                .Execute("dbo.Theme_Update",
                new
                {
                    id,
                    name
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int DeleteTheme(int id)
        {
            var result = _connection
                .Execute("dbo.Theme_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
