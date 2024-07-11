using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    /// <summary>
    /// Gamescene used for displaying high score
    /// </summary>
    public class HighScoreScene : GameScene
    {
        public HighScoreScene(Game game) : base(game)
        {
            
        }

        public override void Initialize()
        {
            // create and add any components that belong to 
            // this scene to the Scene components list
            AddComponent(new HighScoreComponent(Game));
            this.Hide();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    ((Game1)Game).HideAllScenes();
                    Game.Services.GetService<StartScene>().Show();
                }
            }
            base.Update(gameTime);
        }
    }
}