using System;
using System.Data.SqlClient;
using static Plato.Functional.F;

namespace Chapter1.Examples
{
    public static class ConnectionHelper
    {
        public static R Connect<R>(string connectionString, Func<SqlConnection,R> f)
        {
            using(var conn = new SqlConnection(connectionString))
            {
                return f(conn);
            }
        } 
    }
    public static class ConnectionHelperV2
    {
        public static R Connect<R>(string connectionString, Func<SqlConnection,R> f)
        {
            return Using(new SqlConnection(connectionString), conn => {conn.Open(); return f(conn);});
        }
        
    }
}