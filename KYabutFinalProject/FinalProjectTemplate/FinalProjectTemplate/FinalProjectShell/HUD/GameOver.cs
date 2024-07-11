using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Displays game over screen
/// </summary>
namespace FinalProjectShell
{
    class GameOver : HudString
    {
        public GameOver(Game game, string fontName, HudLocation location) : base(game, fontName, location)
        {
            if (Game.Services.GetService<GameOver>() == null)
            {
                Game.Services.AddService<GameOver>(this);
            }
            displayString = "";
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Displays game over
        /// </summary>
        public void DisplayGameOver()
        {
            GameOverScore score = Game.Services.GetService<GameOverScore>();
            score.DisplayGameOver();
            displayString = "GAME OVER";
        }
    }
}
