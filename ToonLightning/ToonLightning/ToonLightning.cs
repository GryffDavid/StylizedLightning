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
        static Random Random = new Random();

        struct Node
        {
            public Vector2 startPosition, EndPosition, Direction;
            public float Angle, Length;
        }

        VertexPositionColor[] vertices = new VertexPositionColor[4];
        List<Node> Nodes = new List<Node>();

        public ToonLightning()
        {
            Node firstNode = new Node() { startPosition = new Vector2(500, 500), Angle = Random.Next(0, 180), Length = 100 };
            firstNode.EndPosition = firstNode.startPosition + new Vector2((float)Math.Cos(firstNode.Angle) * firstNode.Length, (float)Math.Sin(firstNode.Angle) * firstNode.Length);

            Node secondNode = new Node() { startPosition = firstNode.EndPosition, Angle = Random.Next(0, 180), Length = 100 };
            secondNode.EndPosition = secondNode.startPosition + new Vector2((float)Math.Cos(secondNode.Angle) * secondNode.Length, (float)Math.Sin(secondNode.Angle) * secondNode.Length);


            vertices[0] = new VertexPositionColor(new Vector3(firstNode.startPosition, 0), Color.White);
            vertices[1] = new VertexPositionColor(new Vector3(firstNode.EndPosition, 0), Color.White);

            vertices[2] = new VertexPositionColor(new Vector3(secondNode.startPosition, 0), Color.White);
            vertices[3] = new VertexPositionColor(new Vector3(secondNode.EndPosition, 0), Color.White);
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GraphicsDevice graphics)
        {
            graphics.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 2);
        }
    }
}
