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

        const int length = 102;

        VertexPositionColor[] vertices = new VertexPositionColor[length];
        List<Node> Nodes = new List<Node>();

        public Vector2 StartPosition, EndPosition;

        public ToonLightning()
        {
            StartPosition = new Vector2(100, 400);

            Node firstNode = new Node() { startPosition = StartPosition, Angle = Random.Next(0, 45), Length = Random.Next(5, 20) };
            firstNode.EndPosition = firstNode.startPosition + new Vector2((float)Math.Cos(MathHelper.ToRadians(firstNode.Angle)) * firstNode.Length, (float)Math.Sin(MathHelper.ToRadians(firstNode.Angle)) * firstNode.Length);

            Nodes.Add(firstNode);

            for (int i = 1; i < length/2; i++)
            {
                float ang;

                if (Random.NextDouble() >= 0.25)
                {
                    ang = (MathHelper.Lerp(Nodes[Nodes.Count - 1].Angle, Random.Next(-360, 360), 0.1f));
                }
                else
                {
                    ang = Random.Next((-90)/i, 90/i);
                }

                Node secondNode = new Node() { startPosition = Nodes[i-1].EndPosition, Angle = ang, Length = Random.Next(5, 20) };
                secondNode.EndPosition = secondNode.startPosition + new Vector2((float)Math.Cos(MathHelper.ToRadians(secondNode.Angle)) * secondNode.Length, (float)Math.Sin(MathHelper.ToRadians(secondNode.Angle)) * secondNode.Length);
                Nodes.Add(secondNode);
            }

            vertices[0] = new VertexPositionColor(new Vector3(firstNode.startPosition, 0), Color.White);
            vertices[1] = new VertexPositionColor(new Vector3(firstNode.EndPosition, 0), Color.White);


            for (int i = 2; i < length; i += 2)
            {
                vertices[i] = new VertexPositionColor(new Vector3(Nodes[i/2].startPosition, 0), Color.White);
                vertices[i + 1] = new VertexPositionColor(new Vector3(Nodes[i/2].EndPosition, 0), Color.White);
            }
           
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GraphicsDevice graphics)
        {
            graphics.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, (length/2)-2);
        }
    }
}
