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
    public class ActionScene : GameScene
    {


        public ActionScene (Game game): base(game)
        {

        }

        internal int GetObstacleCount()
        {
            return GetComponentCount(typeof(Obstacle));

        }
        public override void Initialize()
        {
            // create and add any components that belong to this scene
            AddComponent(new Background(Game));

            AddComponent(new GroundTile(Game));

            AddComponent(new Player(Game));

            AddComponent(new ObstacleManager(Game, this));
            
            AddComponent(new Score(Game, "Fonts/hudFont", HudLocation.TopRight));

            AddComponent(new GameOver(Game, "Fonts/gameOverTitle", HudLocation.CenterScreen));

            AddComponent(new GameOverScore(Game, "Fonts/gameOverScore", HudLocation.GameOverScore));

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled )
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
