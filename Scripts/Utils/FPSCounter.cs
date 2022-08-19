using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CodingMath.Utils
{
    public class FPSCounter : DrawableGameComponent
    {
        ContentManager content;
        SpriteFont fpsText;
        float fps;
        SpriteBatch spriteBatch;
        public FPSCounter(Game game) : base(game)
        {
            content = game.Content;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public override void Initialize()
        {
            base.Initialize();
            fps = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fps = 1 / deltaTime;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            fpsText = content.Load<SpriteFont>("ArialBlack");
        }
        protected override void UnloadContent()
        {
            base.UnloadContent();
            content.Unload();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.DrawString(fpsText, "FPS : " + fps.ToString(), Vector2.Zero, Color.Black);
            spriteBatch.End();
        }
    }
}