using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FinalProjectShell
{
    /// <summary>
    /// Class used for background
    /// </summary>
    public class Background : DrawableGameComponent
    {
        //Assets
        private static int backgroundCount = 0;
        Texture2D background;
        Vector2 velocity = new Vector2(2, 0);
        Vector2 position = Vector2.Zero;
        List<Rectangle> drawRectangles = new List<Rectangle>();

        public Background(Game game) : base(game)
        {
            if (Game.Services.GetService<Background>() == null)
            {
                Game.Services.AddService<Background>(this);
            }

            DrawOrder = backgroundCount++;
            this.background = Game.Content.Load<Texture2D>("Images/city_background_night 2");

            drawRectangles = CalculateBackgroundRectangles();
        }

        protected override void LoadContent()
        {

            background = Game.Content.Load<Texture2D>("Images/city_background_night 2");

            base.LoadContent();
        }

        //Calculates the amount of backgrounds needed
        private List<Rectangle> CalculateBackgroundRectangles()
        {
            List<Rectangle> rectList = new List<Rectangle>();
            int rectangleCount = Game.GraphicsDevice.Viewport.Width / background.Width + 2;

            for (int i = 0; i < rectangleCount; i++)
            {
                rectList.Add(new Rectangle(background.Width * i, 0, background.Width, background.Height));
            }
            return rectList;
        }




        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            //Draws full frame
            //sb.Draw(background, new Rectangle(0, 0, 800, 500), Color.White);

            foreach (Rectangle rect in drawRectangles)
            {
                sb.Draw(background, rect, Color.White);
            }
            sb.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //Updates scrolling background 
            for (int i = 0; i < drawRectangles.Count; i++)
            {
                Rectangle currentRectangle = drawRectangles[i];
                currentRectangle.Location -= velocity.ToPoint();
                drawRectangles[i] = currentRectangle;
            }

            //Check if we need to remove first and add another
            Rectangle first = drawRectangles[0];

            if (first.Right < 0)
            {
                drawRectangles.RemoveAt(0);
                Rectangle last = drawRectangles[drawRectangles.Count - 1];
                first.Location = new Point(last.Location.X + last.Width, 0);
                drawRectangles.Add(first);
            }

            base.Update(gameTime);
        }


        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Stops movement when a player is dead
        /// </summary>
        public void StopMovement()
        {
            velocity = Vector2.Zero;
        }

        /// <summary>
        /// Increases speed every 10 seconds
        /// </summary>
        public void IncreaseSpeed()
        {
            float newVelocity = velocity.X + (float)0.25; 
            velocity = new Vector2(newVelocity, 0);
        }
    }
}
