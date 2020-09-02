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

        Vector2 EndPosition;

        public struct Node
        {
            public Vector2 NodePosition, NodeEnd, Direction, TangentDirection;
            public float TangentAngle, Angle, Length, Width;
        }

        VertexPositionColor[] vertices = new VertexPositionColor[200];
        VertexPositionColor[] vertices2 = new VertexPositionColor[200];

        public List<Node> NodeList = new List<Node>();


        public ToonLightning()
        {
            EndPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            for (int i = 0; i < 50; i++)
            {
                Node newNode;

                if (i == 0)
                {
                    newNode = new Node()
                    {
                        NodePosition = new Vector2(100, 100),
                        Angle = Random.Next(-90, 90),
                        Width = Random.Next(5, 10)
                    };

                    newNode.TangentAngle = newNode.Angle - 90;
                    newNode.TangentDirection = new Vector2((float)Math.Cos(MathHelper.ToRadians(newNode.TangentAngle)),
                                                           (float)Math.Sin(MathHelper.ToRadians(newNode.TangentAngle)));
                    newNode.TangentDirection.Normalize();



                    newNode.Direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(newNode.Angle)), 
                                                    (float)Math.Sin(MathHelper.ToRadians(newNode.Angle)));
                    newNode.Direction.Normalize();
                    newNode.NodeEnd = newNode.NodePosition + (newNode.Direction * 25);


                    NodeList.Add(newNode);

                    
                    vertices[i] = new VertexPositionColor(new Vector3(NodeList[0].NodePosition, 0), Color.White);
                    vertices[i + 1] = new VertexPositionColor(new Vector3(NodeList[0].NodeEnd, 0), Color.White);
                }
                else
                {
                    float ang, lenp;

                    lenp = Random.Next(20, 40);

                    if (Random.NextDouble() >= 0.5)
                    {
                        ang = (MathHelper.Lerp(NodeList[NodeList.Count - 1].Angle, Random.Next(-180, 180), 0.1f));
                    }
                    else
                    {

                        //ang = Random.Next(-180 / i, 180 / i);
                        ang = (MathHelper.Lerp(NodeList[NodeList.Count - 1].Angle, Random.Next(-90, 90), 0.2f))/i;
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

                        ang = (MathHelper.Lerp(NodeList[NodeList.Count - 1].Angle, Random.Next(90, 120) * m, 0.3f));
                    }

                    newNode = new Node()
                    {
                        NodePosition = NodeList[i-1].NodeEnd,
                        Angle = ang,
                        Width = MathHelper.Lerp(NodeList[i-1].Width, Random.Next(0, 15), 0.3f)
                    };

                    newNode.TangentAngle = newNode.Angle - 90;
                    newNode.TangentDirection = new Vector2((float)Math.Cos(MathHelper.ToRadians(newNode.TangentAngle)),
                                                           (float)Math.Sin(MathHelper.ToRadians(newNode.TangentAngle)));

                    newNode.Direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(newNode.Angle)), 
                                                    (float)Math.Sin(MathHelper.ToRadians(newNode.Angle)));
                    newNode.NodeEnd = newNode.NodePosition + (newNode.Direction * lenp);

                    NodeList.Add(newNode);
                }
            }
            
            //float DeltaY = NodeList[NodeList.Count - 1].NodeEnd.Y - EndPosition.Y;
            //float DeltaX = NodeList[NodeList.Count - 1].NodeEnd.X - EndPosition.X;

            //List<Node> NewNodes = new List<Node>();
            //NewNodes.Add(NodeList[0]);
            //NewNodes.Add(NodeList[1]);


            //for (int i = 2; i < 100 / 2; i++)
            //{
            //    Node node = NodeList[i];
            //    double dif = (i / (double)((100 - 2) / 2));

            //    node.NodeEnd.Y -= (float)(DeltaY * dif);
            //    node.NodePosition.Y -= (float)(DeltaY * dif);

            //    node.NodeEnd.X -= (float)(DeltaX * dif);
            //    node.NodePosition.X -= (float)(DeltaX * dif);

            //    NewNodes.Add(node);
            //}

            //NodeList = NewNodes;


            for (int i = 2; i < 100; i += 2)
            {
                vertices[i] = new VertexPositionColor(new Vector3(NodeList[i / 2].NodePosition, 0), Color.White);
                vertices[i+1] = new VertexPositionColor(new Vector3(NodeList[i / 2].NodeEnd, 0), Color.White);
            }


            for (int i = 0; i < 100; i += 2)
            {
                vertices2[i] = new VertexPositionColor(new Vector3(NodeList[i / 2].NodePosition + (NodeList[i / 2].TangentDirection * NodeList[i / 2].Width), 0), Color.White);
                vertices2[i + 1] = new VertexPositionColor(new Vector3(NodeList[i / 2].NodePosition - (NodeList[i / 2].TangentDirection * NodeList[i / 2].Width), 0), Color.White);
            }

            

            //vertices2[2] = new VertexPositionColor(new Vector3(NodeList[1].NodePosition + (NodeList[1].TangentDirection * 10), 0), Color.White);
            //vertices2[3] = new VertexPositionColor(new Vector3(NodeList[1].NodePosition - (NodeList[1].TangentDirection * 10), 0), Color.White);
            
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
            graphics.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 50);
            graphics.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertices2, 0, 98);

        }
    }
}
