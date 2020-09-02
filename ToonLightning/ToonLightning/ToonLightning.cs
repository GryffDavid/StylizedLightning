using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ToonLightning
{
    class ToonLightning
    {
        VertexPositionColor[] vertices = new VertexPositionColor[3];

        public ToonLightning()
        {
            vertices[0] = new VertexPositionColor(new Vector3(0, 0, 0), Color.White);
            vertices[1] = new VertexPositionColor(new Vector3(120, 0, 0), Color.White);
            vertices[2] = new VertexPositionColor(new Vector3(120, 120, 0), Color.White);
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GraphicsDevice graphics)
        {
            graphics.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1);
        }
    }
}
