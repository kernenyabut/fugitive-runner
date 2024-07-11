using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    /// <summary>
    /// Displays and loads highscore
    /// </summary>
    class HighScoreComponent : DrawableGameComponent
    {
        SpriteFont font;
        Vector2 position = Vector2.Zero;
        bool scoresExists = true;
        List<long> highScores = new List<long>();
        const int MAX_SCORES = 10;

        public HighScoreComponent(Game game) : base(game)
        {
            if (Game.Services.GetService<HighScoreComponent>() == null)
            {
                Game.Services.AddService<HighScoreComponent>(this);
            }
        }

        public override void Initialize()
        {
            LoadHighScores();

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {

            //Initial position used for displaying the high scores
            position = new Vector2();
            position.X = Game.GraphicsDevice.Viewport.Width / 2 - 50;
            position.Y = 40;

            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Begin();
            //If no highscores exist
            if (!scoresExists)
            {
                spriteBatch.DrawString(font, "No scores exists", position, Color.Black);
            }

            //If scores do exist
            else if (scoresExists)
            {
                spriteBatch.DrawString(font, "High Scores:", position, Color.Black);

                //Displays scores
                for (int i = 0; i < MAX_SCORES; i++)
                {

                    position.Y += 30;

                    spriteBatch.DrawString(font, $"{i + 1}.............{highScores[i]}", position, Color.Black);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            
            font = Game.Content.Load<SpriteFont>("Fonts/highScore");
            base.LoadContent();
        }

        /// <summary>
        /// Loads high scores from file
        /// </summary>
        public void LoadHighScores()
        {
            string fileName = "highscores.txt";
            //If file doesn't exist, it will display a message saying that it doesn't exist
            if (!File.Exists(fileName))
            {
                scoresExists = false;
            }

            //Reads the file
            else if (File.Exists(fileName))
            {
                string[] input = File.ReadAllLines(fileName);

                int test = input.Length;
                //Adds each input into score
                for (int i = 0; i < input.Length; i++)
                {
                    highScores.Add(Convert.ToInt64(input[i]));
                }

                //Sorts list
                highScores.Sort();
                highScores.Reverse();
            }
        }
    }
}