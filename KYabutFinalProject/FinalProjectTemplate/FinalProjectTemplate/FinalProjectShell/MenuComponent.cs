using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalProjectShell
{
    public class MenuComponent : DrawableGameComponent
    {
        
        SpriteFont regularFont;
        SpriteFont highlightFont;
        Song menuMusic;

        private List<string> menuItems;
        private int SelectedIndex { get; set; }
        private Vector2 position;

        Texture2D background;

        private Color regularColor = Color.White;
        private Color hilightColor = Color.Red;

        private KeyboardState oldState; // why??

        public MenuComponent(Game game, List<string>menuNames) : base(game)
        {
            
            menuItems = menuNames; 
            

        }

        public override void Update(GameTime gameTime)
        {
            //if (Enabled)
            {               
                KeyboardState ks = Keyboard.GetState();
                if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
                {
                    SelectedIndex++;
                    if (SelectedIndex == menuItems.Count)
                    {
                        SelectedIndex = 0;
                    }
                }
                if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = menuItems.Count - 1;
                    }

                }
                oldState = ks;

                if (ks.IsKeyDown(Keys.Enter))
                {
                    SwitchScenesBasedOnSelection();
                }

            }
            base.Update(gameTime);
        }

        private void SwitchScenesBasedOnSelection()
        {
            ((Game1)Game).HideAllScenes();

            switch ((MenuSelection)SelectedIndex)
            {
                case MenuSelection.StartGame:
                    Game.Services.GetService<ActionScene>().Show();
                    //MediaPlayer.Stop();
                    break;
                case MenuSelection.Help:
                    Game.Services.GetService<HelpScene>().Show();
                    break;
                case MenuSelection.Quit:
                    Game.Exit();
                    break;
                case MenuSelection.HighScore:
                    Game.Services.GetService<HighScoreScene>().Show();
                    
                    break;
                case MenuSelection.Credit:
                    Game.Services.GetService<CreditsScene>().Show();
                    break;
                default:
                    // for now there is nothing handling the other options
                    // we will simply show this screen again
                    Game.Services.GetService<StartScene>().Show();
                    break;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            Vector2 tempPos = position;

            sb.Begin();

            sb.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            for (int i = 0; i < menuItems.Count; i++)
            {
                SpriteFont activeFont = regularFont;
                Color activeColor = regularColor;

                // if the selection is the item we are drawing
                // made it a the special font and colour
                if (SelectedIndex == i)
                {
                    activeFont = highlightFont;
                    activeColor = hilightColor;                    
                }
                
                sb.DrawString(activeFont, menuItems[i], tempPos, activeColor);

                // update the position of next string
                tempPos.Y += regularFont.LineSpacing;                
            }

            sb.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            // starting position of the menu items
            position = new Vector2(GraphicsDevice.Viewport.Width / 2,
                                      GraphicsDevice.Viewport.Height / 2);
               
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // load the fonts we will be using for this menu
            background = Game.Content.Load<Texture2D>("Images/Title Screen Full");
            regularFont = Game.Content.Load<SpriteFont>("Fonts/regularFont");
            highlightFont = Game.Content.Load<SpriteFont>("Fonts/hilightFont");

            //Loads soundtrack
            menuMusic = Game.Content.Load<Song>("Music/MenuMusic");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Stop();
            MediaPlayer.Play(menuMusic);

            base.LoadContent();
        }
    }
}
