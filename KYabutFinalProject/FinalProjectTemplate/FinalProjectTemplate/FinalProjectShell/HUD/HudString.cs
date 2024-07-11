using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Base class used for HUD
/// </summary>
namespace FinalProjectShell
{
    class HudString : DrawableGameComponent
    {
        protected HudLocation location;
        protected string displayString;
        string fontName;
        SpriteFont font;

        public HudString(Game game, string fontName, HudLocation location) : base(game)
        {
            this.fontName = fontName;
            this.location = location;
            this.DrawOrder = int.MaxValue;
            displayString = "Not Set";
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();

            sb.DrawString(font, displayString, GetPosition(), Color.Brown);

            sb.End();
            base.Draw(gameTime);
        }

        private Vector2 GetPosition()
        {
            Vector2 position = Vector2.Zero;
            float stringWidth = font.MeasureString(displayString).X;
            float stringHeight = font.MeasureString(displayString).Y;
            float screenWidth = Game.GraphicsDevice.Viewport.Width;
            float screenHeight = Game.GraphicsDevice.Viewport.Height;

            switch (location)
            {
                case HudLocation.TopLeft:
                    return Vector2.Zero;
                case HudLocation.TopCenter:
                    position.X = screenWidth / 2 - stringWidth / 2;
                    break;
                case HudLocation.TopRight:
                    position.X = screenWidth - stringWidth;
                    break;
                case HudLocation.CenterScreen:
                    position.X = screenWidth / 2 - stringWidth / 2;
                    position.Y = screenHeight / 2 - stringHeight / 2;
                    break;
                case HudLocation.BottomLeft:
                    position.Y = screenHeight - stringHeight;
                    break;
                case HudLocation.BottomCenter:
                    position.Y = screenHeight - stringHeight;
                    position.X = screenWidth / 2 - stringWidth / 2;
                    break;
                case HudLocation.BottomRight:
                    position.Y = screenHeight - stringHeight;
                    position.X = screenWidth - stringWidth;
                    break;
                case HudLocation.GameOverScore:
                    position.X = screenWidth / 2 - stringWidth / 2;
                    position.Y = (screenHeight / 2 - stringHeight / 2) + 30;
                    break;
                default:
                    break;
            }
            return position;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>(fontName);
            base.LoadContent();
        }
    }
}
