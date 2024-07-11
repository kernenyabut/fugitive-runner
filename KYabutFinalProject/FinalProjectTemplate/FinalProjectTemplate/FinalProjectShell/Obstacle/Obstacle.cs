using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FinalProjectShell
{

    /// <summary>
    /// Obstacle class 
    /// </summary>
    class Obstacle : DrawableGameComponent
    {
        //Variables
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;
        bool playerDead = false;

        public Obstacle(Game game, Texture2D texture, Vector2 position, Vector2 velocity) : base(game)
        {
            if (Game.Services.GetService<Obstacle>() == null)
            {
                Game.Services.AddService<Obstacle>(this);
            }

            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
        }

        /// <summary>
        /// Gets bounds of an obstacle - used for collison
        /// </summary>
        public List<int> BoundingRectangle
        {
            get
            {
                List<int> boundingRectangle = new List<int>();
                Rectangle currentBounds = texture.Bounds;
                currentBounds.Location = position.ToPoint();

                boundingRectangle.Add(currentBounds.X);
                boundingRectangle.Add(currentBounds.Y);
                boundingRectangle.Add(currentBounds.Width);
                boundingRectangle.Add(currentBounds.Height + 10);


                return boundingRectangle;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            //sb.Draw(texture, position, Color.White);

            sb.Draw(texture, position, null, Color.White, 0f,
                  Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            sb.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (!playerDead)
            {
                if (position.X < -texture.Width)
                {
                    Game.Components.Remove(this);
                }
                else
                {
                    position -= velocity;
                }

                CheckCollisionWithPlayer();
            }
            else
            {
                velocity = new Vector2(0, 0);
            }

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        /// <summary>
        /// Checks if player has collided with obstacle
        /// </summary>
        private void CheckCollisionWithPlayer()
        {
            Player player = Game.Services.GetService<Player>();
            if (XNA2DCollisionDetection
                .CollisionDetection2D
                .BoundingRectangles(player.BoundingRectangle[0]
                , player.BoundingRectangle[1], player.BoundingRectangle[2], player.BoundingRectangle[3], this.BoundingRectangle[0]
                , this.BoundingRectangle[1], this.BoundingRectangle[2], this.BoundingRectangle[3]))
            {
                player.Collided();
                
            }
        }

        /// <summary>
        /// Stops movement when they are dead
        /// </summary>
        public void StopMovement()
        {
            
            playerDead = true;
        }
    }
}
