using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ToonLightning
{    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BasicEffect BasicEffect;
        List<ToonLightning> LightningList = new List<ToonLightning>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
        }
                
        protected override void Initialize()
        {
            LightningList.Add(new ToonLightning());
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            BasicEffect = new BasicEffect(GraphicsDevice);
            BasicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, 1280, 720, 0, 0, 1);
        }
        
        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            foreach (EffectPass pass in BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                foreach (ToonLightning lightning in LightningList)
                {
                    lightning.Draw(GraphicsDevice);
                }
            }

            base.Draw(gameTime);
        }
    }
}
