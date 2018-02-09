using AttentionTransferSpeedTest.DAL.DBO;
using System.Data.SQLite;

namespace AttentionTransferSpeedTest.DAL.Gateway
{
    class UserGateway
    {
        private static BaseGateway sql = new BaseGateway("data source=mydb.db");
        public void InsertUser(User user)
        {
            sql.CreateTable("User", new string[] { "Name", "Age", "Tel", "Sex" ,"Time"}, new string[] { "TEXT", "TEXT", "INTEGER", "TEXT","TEXT" });
            sql.InsertValues("User", new string[] { user.Name, user.Age.ToString(), user.Tel.ToString(), user.Sex, user.Time });
        }
        public SQLiteDataReader SelectAllUser()
        {
            return sql.ReadFullTable("User");
        }
    }
}