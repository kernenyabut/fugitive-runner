using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

/// <summary>
/// Displays the final score when a player is dead
/// </summary>
namespace FinalProjectShell
{
    class GameOverScore : HudString
    {
        public GameOverScore(Game game, string fontName, HudLocation location) : base(game, fontName, location)
        {
            if (Game.Services.GetService<GameOverScore>() == null)
            {
                Game.Services.AddService<GameOverScore>(this);
            }
            displayString = "";
        }

        public void DisplayGameOver()
        {
            long finalScore = 0;
            Score score = Game.Services.GetService<Score>();
            finalScore = score.DisplayScore();
            displayString = $"Your Score: {finalScore}";
        }
    }
}
