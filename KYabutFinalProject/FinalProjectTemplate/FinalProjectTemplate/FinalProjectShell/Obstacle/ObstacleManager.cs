using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FinalProjectShell
{
    /// <summary>
    /// Spawns obstacles on screen
    /// </summary>
    class ObstacleManager : DrawableGameComponent
    {
        //Assets
        List<Texture2D> texture = new List<Texture2D>();
        
        //Variables
        int obstacleWidth;
        int minObstacleCount;
        Vector2 velocity = new Vector2(10, 0);
        Random rnd = new Random();
        const int OBSTACLE_COUNT = 7;

        //Variables used for determining the obstacle distance and type
        int obstacleDistance;
        int obstacleId;
        GameScene parent;

        public ObstacleManager(Game game, GameScene parent) : base(game)
        {
            if (Game.Services.GetService<ObstacleManager>() == null)
            {
                Game.Services.AddService<ObstacleManager>(this);
            }

            this.parent = parent;
        }

        public override void Initialize()
        {
            
            obstacleId = rnd.Next(0, 7);
            //Loads textures
            for (int i = 1; i <= OBSTACLE_COUNT; i++)
            {
                texture.Add(Game.Content.Load<Texture2D>($"Images/obstacles/obstacle{i}"));
            }

            obstacleWidth = texture[obstacleId].Width;
            minObstacleCount = 1;

            //Generates obstacles
            obstacleDistance = rnd.Next(50, 100);

            //Position
            Vector2 position = new Vector2(Game.GraphicsDevice.Viewport.Width, 
                GraphicsDevice.Viewport.Height - (obstacleWidth * 4) - 10);

            //Assigns obstacle object then adds to the actionscene 
            Obstacle obj = new Obstacle(Game, texture[obstacleId], position, velocity);

            parent.AddComponent(obj);
            

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            int currentCount = GetObstacleCount();
            
            //Spawns another obstacle when the previous one is offscreen
            if (currentCount < minObstacleCount)
            {
                CreateNewObstacle();
            }
            base.Update(gameTime);
        }

        
        /// <summary>
        /// Creates new obstacle
        /// </summary>
        private void CreateNewObstacle()
        {
            //Variables - randomizes type and distance
            obstacleId = rnd.Next(0, 7);
            obstacleDistance = rnd.Next(50, 100);

            //Position
            Vector2 position = new Vector2(Game.GraphicsDevice.Viewport.Width + obstacleDistance,
                GraphicsDevice.Viewport.Height - (obstacleWidth * 4) - 10);
            
            //Adds obstacle to action screen
            Obstacle obj = new Obstacle(Game, texture[obstacleId], position, velocity);

            parent.AddComponent(obj);
        }

        /// <summary>
        /// Gets obstacle count and returns
        /// </summary>
        /// <returns></returns>
        public int GetObstacleCount()
        {
            int count = 0;
            foreach (GameComponent component in Game.Components)
            {
                if (component is Obstacle)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Stops spawning obstacles once a player is dead
        /// </summary>
        public void StopMovement()
        {
            Obstacle obstacle = Game.Services.GetService<Obstacle>();
            velocity = new Vector2(0, 0);
            obstacle.StopMovement();
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
