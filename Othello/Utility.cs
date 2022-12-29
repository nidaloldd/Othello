using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Othello
{
	public static class Utility
	{
		public static void SaveScores(string player1, string player2, int score1, int score2, string winner, string timeElapsed)
		{
            string json;
            try
            {
                json = File.ReadAllText(@"..\scores.json");
            }
            catch (FileNotFoundException)
            {
                json = "[]";
            }

            List<ScoreData> dataList;
            try
            {
                dataList = JsonSerializer.Deserialize<List<ScoreData>>(json);
            }
            catch (JsonException)
            {
                dataList = new List<ScoreData>();
            }

            dataList.Add(new ScoreData
            {
                Player1 = player1,
                Player2 = player2,
                Score1 = score1,
                Score2 = score2,
                Winner = winner,
                Time = timeElapsed
            });

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            json = JsonSerializer.Serialize(dataList, options);
            
            File.WriteAllText(@"..\scores.json", json);
        }

        public static List<ScoreData> ReadScores()
        {
            string json;
            try
            {
                json = File.ReadAllText(@"..\scores.json");
            }
            catch (FileNotFoundException)
            {
                json = "[]";
            }

            List<ScoreData> dataList;
            try
            {
                dataList = JsonSerializer.Deserialize<List<ScoreData>>(json);
            }
            catch (JsonException)
            {
                dataList = new List<ScoreData>();
            }

            return dataList;
        }
        public static void ClearScores()
        {
            string json = "[]";
            File.WriteAllText(@"..\scores.json", json);
        }
    }
}