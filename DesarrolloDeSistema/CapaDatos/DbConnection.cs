using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaDatos
{
    public abstract class DbConnection
    {
        public static string cn = "Server=VALVERDE-PC\\SQLEXPRESS;Database=DbventasDesarrollo;User ID=sa;Password=Aa1234567*;Trusted_Connection=true;Encrypt=false;MultipleActiveResultSets=true";


        private readonly string connectionString;
        public DbConnection()
        {
            connectionString = cn;
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
