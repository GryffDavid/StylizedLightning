using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ToonLightning
{
    class ToonLightning
    {
        Texture2D Block;

        static Random Random = new Random();

        struct Node
        {
            public Vector2 startPosition, EndPosition, Direction;
            public float Angle, Length, Width;
        }

        int length = 202;

        VertexPositionColor[] vertices;// = new VertexPositionColor[length];
        VertexPositionColor[] vertices2;// = new VertexPositionColor[length];
        List<Node> Nodes = new List<Node>();

        public Vector2 StartPosition, EndPosition;

        public ToonLightning(int len)
        {
            length = len;
            vertices = new VertexPositionColor[length];
            vertices2 = new VertexPositionColor[length];

            StartPosition = new Vector2(100, 400);
            EndPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            Node firstNode = new Node() { startPosition = StartPosition, Angle = Random.Next(0, 45), Length = Random.Next(15, 50) };
            firstNode.Direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(firstNode.Angle)) * firstNode.Length, (float)Math.Sin(MathHelper.ToRadians(firstNode.Angle)) * firstNode.Length);
            firstNode.EndPosition = firstNode.startPosition + new Vector2((float)Math.Cos(MathHelper.ToRadians(firstNode.Angle)) * firstNode.Length, (float)Math.Sin(MathHelper.ToRadians(firstNode.Angle)) * firstNode.Length);

            Nodes.Add(firstNode);

            for (int i = 1; i < length/2; i++)
            {
                float ang, lenp;

                lenp = Random.Next(20, 40);

                if (Random.NextDouble() >= 0.5)
                {
                    ang = (MathHelper.Lerp(Nodes[Nodes.Count - 1].Angle, Random.Next(-180, 180), 0.1f));
                }
                else
                {
                    
                    //ang = Random.Next(-180 / i, 180 / i);
                    ang = (MathHelper.Lerp(Nodes[Nodes.Count - 1].Angle, Random.Next(-90, 90), 0.2f));
                }

                if (Random.NextDouble() > 0.8)
                {
                    lenp = Random.Next(80, 100);

                    int m;

                    if (Random.NextDouble() >= 0.5)
                    {
                        m = -1;
                    }
                    else
                    {
                        m = 1;
                    }

                    ang = (MathHelper.Lerp(Nodes[Nodes.Count - 1].Angle, Random.Next(90, 120) * m, 0.3f));
                }

                Node secondNode = new Node() { startPosition = Nodes[i - 1].EndPosition, Angle = ang, Length = lenp };
                secondNode.EndPosition = secondNode.startPosition + new Vector2((float)Math.Cos(MathHelper.ToRadians(secondNode.Angle)) * secondNode.Length, (float)Math.Sin(MathHelper.ToRadians(secondNode.Angle)) * secondNode.Length);
                secondNode.Direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(secondNode.Angle)) * secondNode.Length, (float)Math.Sin(MathHelper.ToRadians(secondNode.Angle)) * secondNode.Length);

                Nodes.Add(secondNode);
            }

            float DeltaY = Nodes[Nodes.Count - 1].EndPosition.Y - EndPosition.Y;
            float DeltaX = Nodes[Nodes.Count - 1].EndPosition.X - EndPosition.X;

            List<Node> NewNodes = new List<Node>();
            NewNodes.Add(Nodes[0]);
            NewNodes.Add(Nodes[1]);


            for (int i = 2; i < length / 2; i++)
            {
                Node node = Nodes[i];
                double dif = (i / (double)((length-2)/2));

                node.EndPosition.Y -= (float)(DeltaY * dif);
                node.startPosition.Y -= (float)(DeltaY * dif);

                node.EndPosition.X -= (float)(DeltaX * dif);
                node.startPosition.X -= (float)(DeltaX * dif);

                NewNodes.Add(node);
            }
            
            vertices[0] = new VertexPositionColor(new Vector3(firstNode.startPosition, 0), Color.White);
            vertices[1] = new VertexPositionColor(new Vector3(firstNode.EndPosition, 0), Color.White);


            for (int i = 2; i < length; i += 2)
            {
                vertices[i] = new VertexPositionColor(new Vector3(NewNodes[(i / 2) - 1].EndPosition, 0), Color.White);
                vertices[i + 1] = new VertexPositionColor(new Vector3(NewNodes[i / 2].EndPosition, 0), Color.White);
            }
        }

        public void LoadContent(ContentManager content)
        {
            Block = content.Load<Texture2D>("WhiteBlock");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GraphicsDevice graphics)
        {
            graphics.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, (length/2)-2);
            graphics.DrawUserPrimitives(PrimitiveType.LineList, vertices2, 0, (length / 2) - 2);

        }
    }
}
