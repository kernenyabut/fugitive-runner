using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FinalProjectShell
{
    /// <summary>
    /// Ground tile class
    /// </summary>
    class GroundTile : DrawableGameComponent
    {
        //Assets
        private static int tileCount = 0;
        Texture2D tile;
        Vector2 velocity = new Vector2(8, 0);
        Vector2 position = Vector2.Zero;

        List<Rectangle> drawRectangles = new List<Rectangle>();
        public GroundTile(Game game) : base(game)
        {
            if (Game.Services.GetService<GroundTile>() == null)
            {
                Game.Services.AddService<GroundTile>(this);
            }

            DrawOrder = tileCount++;
            this.tile = Game.Content.Load<Texture2D>("Images/city_03");

            drawRectangles = CalculateTiles();
        }

        protected override void LoadContent()
        {
            tile = Game.Content.Load<Texture2D>("Images/city_03");

            
            base.LoadContent();
        }

        //Calculates the amount of backgrounds needed
        private List<Rectangle> CalculateTiles()
        {
            //Variables
            int yPos = Game.GraphicsDevice.Viewport.Height - tile.Height ;
            List<Rectangle> rectList = new List<Rectangle>();
            int rectangleCount = Game.GraphicsDevice.Viewport.Width / tile.Width + 2;

            for (int i = 0; i < rectangleCount; i++)
            {
                rectList.Add(new Rectangle(tile.Width * i,
                    yPos, 
                    tile.Width, 
                    tile.Height));
            }
            return rectList;
        }

        public override void Draw(GameTime gameTime)
        {
            int yPos = Game.GraphicsDevice.Viewport.Height - tile.Height;
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            //Draws full frame

            foreach (Rectangle rect in drawRectangles)
            {
                sb.Draw(tile, rect, Color.White);

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
                first.Location = new Point(last.Location.X + last.Width, Game.GraphicsDevice.Viewport.Height - tile.Height);
                drawRectangles.Add(first);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Stops movement once player is dead
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
