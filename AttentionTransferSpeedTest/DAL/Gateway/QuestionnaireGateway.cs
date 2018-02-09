using AttentionTransferSpeedTest.DAL.DBO;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace AttentionTransferSpeedTest.DAL.Gateway
{
    class QuestionnaireGateway
    {
        private static BaseGateway sql = new BaseGateway("data source=mydb.db");
        public void InsertQuestionnaire(Questionnaire questionnaire)
        {
            sql.CreateTable("Questionnaire", new string[] { "Name", "psychiatricHistory", "Drink", "Insomnia", "Mood", "computerGame", "Exercise", "Driving" , "Accident" , "Others" },
                new string[] { "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "INTEGER", "INTEGER","TEXT" });
            sql.InsertValues("Questionnaire", new string[] { questionnaire.Name,questionnaire.psychiatricHistory,questionnaire.Drink,questionnaire.Insomnia,questionnaire.Mood,
            questionnaire.computerGame,questionnaire.Exercise,questionnaire.Driving.ToString(),questionnaire.Accident.ToString(),questionnaire.Others});
        }
        public SQLiteDataReader SelectAllQuestionnaire()
        {
            return sql.ReadFullTable("Questionnaire");
        }
    }
}
