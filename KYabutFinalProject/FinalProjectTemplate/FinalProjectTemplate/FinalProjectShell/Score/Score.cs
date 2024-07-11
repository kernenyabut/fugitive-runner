using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace FinalProjectShell
{
    
    /// <summary>
    /// Score class - manages and updates score
    /// </summary>
    class Score : HudString
    {
        long score = 0;

        public Score(Game game, string fontName, HudLocation location) : base(game, fontName, location)
        {
            if (Game.Services.GetService<Score>() == null)
            {
                Game.Services.AddService<Score>(this);
            }
            displayString = $"Score: {score}";
        }

        //Updates the score on the screen
        public override void Update(GameTime gameTime)
        {
            displayString = $"Score: {score}";
            base.Update(gameTime);
        }

        /// <summary>
        /// Adds score
        /// </summary>
        /// <param name="value"></param>
        public void AddToScore(long value)
        {
            score += value;
        }

        /// <summary>
        /// Displays score
        /// </summary>
        /// <returns></returns>
        public long DisplayScore()
        {
            return score;
        }

        /// <summary>
        /// Checks if the score is a high score
        /// </summary>
        public void CheckForHighScore()
        {
            const int MAX_SCORES = 10;
            string fileName = @"highscores.txt";
            List<long> highScores = new List<long>();
            //If file doesn't exist, it creates it and adds the score into the 
            if (!File.Exists(fileName))
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    //Writes to the file
                    writer.WriteLine(score);
                }
            }

            //Reads the file
            else if (File.Exists(fileName))
            {
                string[] input = File.ReadAllLines(fileName);

                //Adds each input into score
                for (int i = 0; i < input.Length; i++)
                {
                    highScores.Add(Convert.ToInt64(input[i]));
                }

                //Sorts list
                highScores.Sort();
                highScores.Reverse();

                //If the length of the list is shorter than the max amount of scores
                if (highScores.Count < MAX_SCORES)
                {
                    highScores.Add(score);

                    //Sorts list
                    highScores.Sort();
                    highScores.Reverse();

                    using (TextWriter tw = new StreamWriter(fileName))
                    {
                        foreach (long score in highScores)
                            tw.WriteLine(score);
                    }
                }
                else if (highScores.Count == MAX_SCORES)
                {
                    //Checks if the last score is higher than the last score
                    if (score > highScores[highScores.Count - 1])
                    {
                        //Adds score, sorts, then removes the last one
                        highScores.Add(score);
                        highScores.Sort();
                        highScores.Reverse();
                        highScores.RemoveAt(highScores.Count - 1);

                        using (TextWriter tw = new StreamWriter(fileName))
                        {
                            foreach (long score in highScores)
                                tw.WriteLine(score);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Refreshes high score list
        /// </summary>
        public void RefreshHighScoreList()
        {
            HighScoreComponent highScore = Game.Services.GetService<HighScoreComponent>();
            highScore.LoadHighScores();
        }
    }
}
