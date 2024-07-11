using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//=============================
//Author: Kernen Yabut       
//Student ID: 8467979
//Final Assignment
//Date of Creation: November 05, 2019
//Description: A 2D endless runner called Fugutive Runner,
//where a player must jump over objects and the sides of buildings.
//=============================

//=============================
//Assets Used:
//City Background: fridaruiz - https://opengameart.org/content/pixel-night-city 
//Soundtrack: spring - https://opengameart.org/content/the-simplest-song-ever
//Character: Blue Yeti Studios - https://opengameart.org/content/runner-character
//Tileset: software_atelier - https://opengameart.org/content/city-pixel-tileset 
//Title Font: Editundo by Ænigma - https://fontmeme.com/fonts/edit-undo-font/
//Jumping Sound: dklon - https://opengameart.org/content/platformer-jumping-sounds
//=============================
namespace FinalProjectShell
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
       
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            
            // TODO: use this.Content to load your game content here
            StartScene startScene = new StartScene(this);
            this.Components.Add(startScene);
            Services.AddService<StartScene>(startScene);
            

            //create other scenes here and add to component list
            ActionScene actionScene = new ActionScene(this);
            this.Components.Add(actionScene);
            Services.AddService<ActionScene>(actionScene);

            HelpScene helpScene = new HelpScene(this);
            this.Components.Add(helpScene);
            Services.AddService<HelpScene>(helpScene);

            HighScoreScene highScoreScene = new HighScoreScene(this);
            this.Components.Add(highScoreScene);
            Services.AddService<HighScoreScene>(highScoreScene);

            CreditsScene creditsScene = new CreditsScene(this);
            this.Components.Add(creditsScene);
            Services.AddService<CreditsScene>(creditsScene);

            // others.....
            base.Initialize();

            // hide all then show our first scene
            // this has to be done after the initialize methods are called
            // on all our components 
            HideAllScenes();
            startScene.Show();
        }

        public void HideAllScenes()
        {
            GameScene gs = null;
            foreach (GameComponent  item in Components)
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.Hide();
                }
            }
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService<SpriteBatch>(spriteBatch);
           
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
   
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
