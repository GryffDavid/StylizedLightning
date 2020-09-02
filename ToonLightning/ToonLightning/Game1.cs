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
        Texture2D blockTex;
        SpriteFont font;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
                
        protected override void Initialize()
        {
            LightningList.Add(new ToonLightning());
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            blockTex = Content.Load<Texture2D>("WhiteBlock");
            font = Content.Load<SpriteFont>("SpriteFont1");

            BasicEffect = new BasicEffect(GraphicsDevice);
            BasicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, 1280, 720, 0, 0, 1);
            BasicEffect.VertexColorEnabled = true;
        }
        
        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                LightningList.Clear();
                ToonLightning newLightning = new ToonLightning();
                LightningList.Add(newLightning);
            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            for (int i = 0; i < LightningList[0].NodeList.Count; i++)
            {
                spriteBatch.Draw(blockTex, LightningList[0].NodeList[i].NodePosition, Color.White);
                spriteBatch.DrawString(font, i.ToString(), LightningList[0].NodeList[i].NodePosition, Color.Red);

            }
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, RasterizerState.CullNone);
            foreach (EffectPass pass in BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                foreach (ToonLightning lightning in LightningList)
                {
                    lightning.Draw(GraphicsDevice);
                }
            }
            spriteBatch.End();

            

            base.Draw(gameTime);
        }

        private int GetEven(int num)
        {
            if (num % 2 == 0)
            {
                return num;
            }
            else
            {
                return num + 1;
            }
        }
    }
}
