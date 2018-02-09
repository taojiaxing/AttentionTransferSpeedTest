using AttentionTransferSpeedTest.DAL.DBO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace AttentionTransferSpeedTest.DAL.Gateway
{
    internal class UserGateway : BaseGateway
    {
        public override void getResultset(MySqlCommand mySqlCommand)
        {
            throw new System.NotImplementedException();
        }

        private MySqlConnection mysql = getMySqlCon();

        public void InsertUser(User user)
        {
            mysql.Open();
            CreateTable("User", new string[] { "Name", "Age", "Tel", "Sex", "Time" }, new string[] { "TEXT", "INTEGER", "TEXT", "TEXT", "TEXT" }, mysql);
            getInsert("User", new string[] { user.Name, user.Age.ToString(), user.Tel.ToString(), user.Sex, user.Time }, mysql);
            mysql.Close();
        }

        public List<User> SelectAllUser()
        {
            mysql.Open();
            CreateTable("User", new string[] { "Name", "Age", "Tel", "Sex", "Time" }, new string[] { "TEXT", "INTEGER", "TEXT", "TEXT", "TEXT" }, mysql);
            MySqlCommand mySqlCommand = getSqlCommand("select * from User", mysql);
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            List<User> Users = new List<User>();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        User user = new User();
                        user.Name = reader.GetString(0);
                        user.Age = reader.GetInt32(1);
                        user.Tel = reader.GetString(2);
                        user.Sex = reader.GetString(3);
                        user.Time = reader.GetString(4);
                        Users.Add(user);
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
            return Users;
        }

        public User SelectUserByName(String Name)
        {
            mysql.Open();
            CreateTable("User", new string[] { "Name", "Age", "Tel", "Sex", "Time" }, new string[] { "TEXT", "INTEGER", "TEXT", "TEXT", "TEXT" }, mysql);
            MySqlCommand mySqlCommand = getSqlCommand("select * from User where Name = " + "'" + Name + "';", mysql);
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            User user = new User();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        user.Name = reader.GetString(0);
                        user.Age = reader.GetInt32(1);
                        user.Tel = reader.GetString(2);
                        user.Sex = reader.GetString(3);
                        user.Time = reader.GetString(4);
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
            return user;
        }
    }
}