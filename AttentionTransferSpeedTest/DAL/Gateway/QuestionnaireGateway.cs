using AttentionTransferSpeedTest.DAL.DBO;
using MySql.Data.MySqlClient;
using System;

namespace AttentionTransferSpeedTest.DAL.Gateway
{
    internal class QuestionnaireGateway : BaseGateway
    {
        public override void getResultset(MySqlCommand mySqlCommand)
        {
            throw new NotImplementedException();
        }

        private MySqlConnection mysql = getMySqlCon();

        public void InsertQuestionnaire(Questionnaire questionnaire)
        {
            mysql.Open();
            CreateTable("Questionnaire", new string[] { "Name", "psychiatricHistory", "Drink", "Insomnia", "Mood", "computerGame", "Exercise", "Driving", "Accident", "Others" },
                new string[] { "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "INTEGER", "INTEGER", "TEXT" }, mysql);
            getInsert("Questionnaire", new string[] { questionnaire.Name,questionnaire.psychiatricHistory,questionnaire.Drink,questionnaire.Insomnia,questionnaire.Mood,
            questionnaire.computerGame,questionnaire.Exercise,questionnaire.Driving.ToString(),questionnaire.Accident.ToString(),questionnaire.Others}, mysql);
            mysql.Close();
        }

        public Questionnaire SelectQuestionnaireByName(String Name)
        {
            mysql.Open();
            CreateTable("Questionnaire", new string[] { "Name", "psychiatricHistory", "Drink", "Insomnia", "Mood", "computerGame", "Exercise", "Driving", "Accident", "Others" },
                new string[] { "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "INTEGER", "INTEGER", "TEXT" }, mysql);
            MySqlCommand mySqlCommand = getSqlCommand("select * from Questionnaire where Name = " + "'" + Name + "';", mysql);
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            Questionnaire questionnaire = new Questionnaire();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        questionnaire.Name = reader.GetString(0);
                        questionnaire.psychiatricHistory = reader.GetString(1);
                        questionnaire.Drink = reader.GetString(2);
                        questionnaire.Insomnia = reader.GetString(3);
                        questionnaire.Mood = reader.GetString(4);
                        questionnaire.computerGame = reader.GetString(5);
                        questionnaire.Exercise = reader.GetString(6);
                        questionnaire.Driving = reader.GetInt32(7);
                        questionnaire.Accident = reader.GetInt32(8);
                        questionnaire.Others = reader.GetString(9);
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
            return questionnaire;
        }
    }
}