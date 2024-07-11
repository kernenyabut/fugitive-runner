using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    /// <summary>
    /// Displays credits from file
    /// </summary>
    class CreditsTextComponent : DrawableGameComponent
    {
        Texture2D texture;
        public CreditsTextComponent(Game game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Begin();
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Images/Credit Scene");
            base.LoadContent();
        }
    }
}
