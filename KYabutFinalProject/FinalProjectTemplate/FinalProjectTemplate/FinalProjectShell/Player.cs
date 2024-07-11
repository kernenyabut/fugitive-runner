using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace FinalProjectShell
{
    //Enum used for the player state
    enum PlayerState
    {
        Running,
        Jumping
    }

    /// <summary>
    /// Player class
    /// </summary>
    class Player : DrawableGameComponent
    {
        //Assets
        List<Texture2D> runningTexture = new List<Texture2D>();
        Texture2D jumpingTexture;
        Texture2D activeTexture;
        SoundEffect jump;
        SoundEffect death;
        

        //Variables
        bool isDead = false;
        PlayerState playerState = PlayerState.Running;
        Vector2 position;
        float groundPosition;

        //Variables used for score
        long totalScore = 0;
        const double SCORE_INTERVAL = 1;
        double timeSinceLastScore = 0.0;

        //Variables for animation
        const int RUN_FRAME_COUNT = 7;
        int currentFrame = 0;
        double timeSinceLastFrame = 0.0;
        const int SPEED = 5;
        const double FRAME_RATE = 0.06;

        //Variables for jumping
        bool jumping = false;
        float jumpHeight = 0.0f;

        //Variables for speed
        const double SPEED_INTERVAL = 10;
        double timeSinceLastSpeedIncrease = 0.0;

        /// <summary>
        /// Gets bounds of an obstacle - used for collison
        /// </summary>

        public Player(Game game) : base(game)
        {
            if (Game.Services.GetService<Player>() == null)
            {
                Game.Services.AddService<Player>(this);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();


            sb.Begin();

            //Draws running animation
            if(playerState == PlayerState.Running)
            {
                //sb.Draw(runningTexture[currentFrame],position,Color.White);
                sb.Draw(runningTexture[currentFrame], position, null, Color.White,0f,
                   Vector2.Zero, 2f, SpriteEffects.None, 0f);

                //Active texture used for determining hitboxes
                activeTexture = runningTexture[currentFrame];
            }
            else
            {
                sb.Draw(jumpingTexture, position, null, Color.White, 0f,
                   Vector2.Zero, 2f, SpriteEffects.None, 0f);

                //Active texture used for determining hitboxes
                activeTexture = jumpingTexture;
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
            //If the player hits an object
            if (!isDead)
            {
                //Controls 
                KeyboardState ks = Keyboard.GetState();

                //Jumping Logic: Flatformer - http://flatformer.blogspot.com/2010/02/making-character-jump-in-xnac-basic.html
                if (jumping)
                {
                    //Adds the height to the position
                    position.Y += jumpHeight;

                    //The player will slowly desend
                    jumpHeight++;

                    //
                    if (position.Y >= groundPosition)
                    //If it's farther than ground
                    {
                        position.Y = groundPosition;
                        jumping = false;
                        playerState = PlayerState.Running;
                    }
                }
                else
                {
                    if (ks.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.Jumping;
                        jumping = true;
                        jumpHeight = -10f;

                        //Plays jumping sound
                        jump.Play(0.15f, 0f, 0f);
                    }
                }

                //Updates frame
                timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
                if (timeSinceLastFrame > FRAME_RATE)
                {
                    timeSinceLastFrame = 0.0;
                    if (++currentFrame >= RUN_FRAME_COUNT)
                    {
                        currentFrame = 0;
                    }
                }

                //Adds to score
                timeSinceLastScore += gameTime.ElapsedGameTime.TotalSeconds;
                if (timeSinceLastScore > SCORE_INTERVAL)
                {
                    timeSinceLastScore = 0.0;
                    Score score = Game.Services.GetService<Score>();
                    totalScore++;
                    score.AddToScore(totalScore);
                }

                //Increases the game's speed every half minute
                timeSinceLastSpeedIncrease += gameTime.ElapsedGameTime.TotalSeconds;
                if (timeSinceLastSpeedIncrease > SPEED_INTERVAL)
                {
                    timeSinceLastSpeedIncrease = 0.0;
                    Background backGround = Game.Services.GetService<Background>();
                    GroundTile groundTile = Game.Services.GetService<GroundTile>();
                    ObstacleManager obstacleManager = Game.Services.GetService<ObstacleManager>();

                    backGround.IncreaseSpeed();
                    groundTile.IncreaseSpeed();
                    obstacleManager.IncreaseSpeed();
                }

            }

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            //Loads textures
            for (int i = 1; i <= RUN_FRAME_COUNT; i++)
            {
                runningTexture.Add(Game.Content.Load<Texture2D>($"Images/running/running{i}"));
            }

            jumpingTexture = Game.Content.Load<Texture2D>("Images/jumping/jump");

            //Default active texture
            activeTexture = runningTexture[0];
            //Loads sound effects
            jump = Game.Content.Load<SoundEffect>("SoundEffects/jump");
            death = Game.Content.Load<SoundEffect>("SoundEffects/Hit");

            //Start location
            position.X = GraphicsDevice.Viewport.Width / 4 - runningTexture[0].Bounds.Center.X;
            position.Y = GraphicsDevice.Viewport.Height - (runningTexture[0].Bounds.Center.Y * 8 + 8);
            groundPosition = position.Y;
            base.LoadContent();
        }

        /// <summary>
        /// Adds hitbox to player
        /// </summary>
        public List<int> BoundingRectangle
        {
            get
            {
                List<int> boundingRectangle = new List<int>();
                Rectangle currentBounds = activeTexture.Bounds;
                currentBounds.Location = position.ToPoint();

                boundingRectangle.Add(currentBounds.X);
                boundingRectangle.Add(currentBounds.Y);
                boundingRectangle.Add(currentBounds.Width + 15);
                boundingRectangle.Add(currentBounds.Height + 10);

                return boundingRectangle;
            }

        }
        /// <summary>
        /// Stops the game if the player is dead.
        /// </summary>
        internal void Collided()
        {
            if (!isDead)
            {
                Background backGround = Game.Services.GetService<Background>();
                GroundTile groundTile = Game.Services.GetService<GroundTile>();
                ObstacleManager obstacleManager = Game.Services.GetService<ObstacleManager>();
                Obstacle obstacle = Game.Services.GetService<Obstacle>();
                GameOver gameOver = Game.Services.GetService<GameOver>();
                Score score = Game.Services.GetService<Score>();


                death.Play(0.15f, 0f, 0f); 
                isDead = true;
                MediaPlayer.Stop();

                //Stops game
                backGround.StopMovement();
                groundTile.StopMovement();
                obstacleManager.StopMovement();
                obstacle.StopMovement();

                //Shows gameover screen
                gameOver.DisplayGameOver();

                //Checks high score
                score.CheckForHighScore();

                //Refreshes high score list
                score.RefreshHighScoreList();

            }


        }


    }
}
