using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using static Chapter1.Examples.ConnectionHelper;

namespace Chapter1.Examples
{
    public class DbLogger_V2
    {
      string connString;
      public void Log(LogMessage msg)
      {

        Connect(connString, conn => conn.Execute("sp_create_log", msg, commandType: CommandType.StoredProcedure));
      }

      public void DeleteOldLogs()
      {

         Connect(connString, 
                connection => connection.Execute("DELETE [Logs] WHERE [Timestamp] < @upTo",param: new { upTo = 7.Days().Ago() }));
      }
      public IEnumerable<LogMessage> GetLogs(DateTime since)
      {
         var sqlGetLogs = "SELECT * FROM [Logs] WHERE [Timestamp] > @since";
         return Connect(connString, connection => connection.Query<LogMessage>(sqlGetLogs, new { since = since }));
      }      
    }
}