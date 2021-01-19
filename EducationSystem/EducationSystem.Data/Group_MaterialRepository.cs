using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EducationSystem.Data
{
    public class Group_MaterialRepository
    {
        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public Group_MaterialRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public void GetGroup_MaterialAdd(int GroupID, int MaterialID)
        {
            var materials = _connection
                .Execute("dbo.Group_Material_Add", new { GroupID, MaterialID }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void GetGroup_MaterialDelete(int Id)
        {
            var materials = _connection
                .Execute("dbo.Group_Material_Delete", new { Id }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
