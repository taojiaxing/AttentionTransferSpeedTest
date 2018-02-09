using AttentionTransferSpeedTest.DAL.DBO;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace AttentionTransferSpeedTest.DAL.Gateway
{
    class ResultGateway
    {
        private static BaseGateway sql = new BaseGateway("data source=mydb.db");
        public void InsertResult(Result result)
        {
            sql.CreateTable("Result", new string[] { "Name", "Num", "ISI", "Combination" , "P", "Correct", "Input","RT" },
                new string[] { "TEXT", "INTEGER", "INTEGER", "TEXT", "INTEGER", "INTEGER", "INTEGER","INTEGER" });
            sql.InsertValues("Result", new string[] { result.Name, result.Num.ToString(), result.ISI.ToString(), result.Combination,result.P.ToString(),result.Correct.ToString(),
            result.Input.ToString(),result.RT.ToString()});
        }
        public SQLiteDataReader SelectAllResult()
        {
            return sql.ReadFullTable("Result");
        }
    }
}