using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CarGame
{
    public class ScoreManager
    {
        public static string filename = "Score.xml";

        public List<Score> HighScores { get; private set; }

        public List<Score> Scores { get; private set; }

        public ScoreManager() : this(new List<Score>())
        {

        }

        public ScoreManager(List<Score> scores)
        {
            Scores = scores;

            UpdateHighScores();
        }

        public void Add(Score score)
        {
            Scores.Add(score);

            Scores = Scores.OrderByDescending(c => c.Value).ToList();

            UpdateHighScores();
        }

        public static ScoreManager Load()
        {
            if (!File.Exists("Score.xml"))
                return new ScoreManager();
            
            using (var reader = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));

                var scores = (List<Score>)serilizer.Deserialize(reader);

                return new ScoreManager(scores);
            }
        }

        public void UpdateHighScores()
        {
            HighScores = Scores.Take(5).ToList();
        }

        public static void Save(ScoreManager scoreManager)
        {
            using (var writer = new StreamWriter(new FileStream(filename, FileMode.Create)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));

                serilizer.Serialize(writer, scoreManager.Scores);
            }
        }
    }
}
