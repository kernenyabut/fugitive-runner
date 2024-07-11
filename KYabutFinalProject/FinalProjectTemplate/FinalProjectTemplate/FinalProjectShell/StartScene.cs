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
    public enum MenuSelection
    {
        StartGame,
        Help,
        HighScore,
        Credit,
        Quit
    }

    public class StartScene : GameScene
    {
        List<string> menuItems = new List<string>(new string[ ]
                               {"Start Game",
                                "Help",
                                "High Score",
                                "Credit",
                                "Quit"} );
        public StartScene(Game game): base(game)
        {
            
        }

        public override void Initialize()
        {
            // create and add any components that belong to this scene
            AddComponent(new MenuComponent(Game, menuItems));
            this.Show();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {            
            base.Update(gameTime);
        }

        
    }
}
