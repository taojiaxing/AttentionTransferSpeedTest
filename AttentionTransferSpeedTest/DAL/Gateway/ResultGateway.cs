using AttentionTransferSpeedTest.DAL.DBO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttentionTransferSpeedTest.DAL.Gateway
{
    class ResultGateway:BaseGateway
    {
        

        public override void getResultset(MySqlCommand mySqlCommand)
        {
            throw new NotImplementedException();
        }
        private MySqlConnection mysql = getMySqlCon();
        public void InsertResult(Result result)
        {
            mysql.Open();
            CreateTable("Result", new string[] { "Name", "Num", "ISI", "Combination" , "P", "Correct", "Input","RT" },
                new string[] { "TEXT", "INTEGER", "INTEGER", "TEXT", "INTEGER", "INTEGER", "INTEGER","INTEGER" },mysql);
            getInsert("Result", new string[] { result.Name, result.Num.ToString(), result.ISI.ToString(), result.Combination,result.P.ToString(),result.Correct.ToString(),
            result.Input.ToString(),result.RT.ToString()},mysql);
            mysql.Close();
        }
        public List<Result> SelectAllResultByName(string Name)
        {
            mysql.Open();
            CreateTable("Result", new string[] { "Name", "Num", "ISI", "Combination", "P", "Correct", "Input", "RT" },
               new string[] { "TEXT", "INTEGER", "INTEGER", "TEXT", "INTEGER", "INTEGER", "INTEGER", "INTEGER" }, mysql);
            MySqlCommand mySqlCommand = getSqlCommand("select * from Result where Name = " + "'" + Name + "';", mysql);
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            List<Result> results= new List<Result>();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        Result result = new Result();
                        result.Name = reader.GetString(0);
                        result.Num = reader.GetInt32(1);
                        result.ISI = reader.GetInt32(2);
                        result.Combination = reader.GetString(3);
                        result.P = reader.GetInt32(4);
                        result.Correct = reader.GetInt32(5);
                        result.Input = reader.GetInt32(6);
                        result.RT = reader.GetInt32(7);
                        results.Add(result);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("查询失败了！");
            }
            finally
            {
                reader.Close();
            }
            mysql.Close();
            return results;
        }
    }
}