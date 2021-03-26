using EducationSystem.Core.Config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EducationSystem.Data
{
    public abstract class BaseRepository
    {
        protected SqlConnection _connection;
        protected string _connectionString;
        public BaseRepository(IOptions<AppSettingsConfig> options)
        {
            _connectionString = options.Value.CONNECTION_STRING;
        }
    }
}
