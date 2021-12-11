using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Examples.Chapter1
{
   public class DbLogger_V2
   {
      string connString;

      public static R Connect<R>(string connectionString, Func<IDbConnection,R> f)
      {
         using(var conn = new SqlConnection(connectionString))
         {
            conn.Open();
            return f(conn);
         }
      }
      public void Log(LogMessage msg)
      {
         Connect(connString, p => p.Execute("sp_create_log", msg, commandType: CommandType.StoredProcedure));
      }
      public void DeleteOldLogs()
      {
         Connect(connString, db => db.Execute("DELETE [Logs] WHERE [Timestamp] < @upTo", param: new { upTo = 7.Days().Ago() }));
      }
      public IEnumerable<LogMessage> GetLogs(DateTime since)
      {
         var sqlGetLogs = "SELECT * FROM [Logs] WHERE [Timestamp] > @since";
         return Connect(connString , db => db.Query<LogMessage>(sqlGetLogs, new { since = since }));
      }
   }
}