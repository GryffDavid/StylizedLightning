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

        Vector2 EndPosition, StartPosition;

        public class Node
        {
            public Vector2 NodePosition, NodeEnd, Direction, TangentDirection;
            public float TangentAngle, Angle, Length, Width;
        }

        public Vector2 LengthRange;

        int TotalLength = 100;

        VertexPositionColor[] vertices;// = new VertexPositionColor[200];
        VertexPositionColor[] vertices2;// = new VertexPositionColor[200];
        
        public List<Node> NodeList = new List<Node>();
                
        public ToonLightning(int segments, int width, Vector2 startPoint, Vector2 endPoint, Vector2 lenRang)
        {
            LengthRange = lenRang;

            TotalLength = segments;

            vertices = new VertexPositionColor[TotalLength];
            vertices2 = new VertexPositionColor[TotalLength];

            EndPosition = endPoint;
            StartPosition = startPoint;

            for (int i = 0; i < TotalLength/2; i++)
            {
                Node newNode;

                #region First Node
                if (i == 0)
                {
                    newNode = new Node()
                    {
                        NodePosition = StartPosition,
                        Angle = Random.Next(-90, 90),
                        Width = Random.Next(5, 10)
                    };
                    
                    newNode.Direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(newNode.Angle)),
                                                    (float)Math.Sin(MathHelper.ToRadians(newNode.Angle)));
                    newNode.Direction.Normalize();
                    newNode.NodeEnd = newNode.NodePosition + (newNode.Direction * 25);

                    NodeList.Add(newNode);


                    vertices[i] = new VertexPositionColor(new Vector3(NodeList[0].NodePosition, 0), Color.White);
                    vertices[i + 1] = new VertexPositionColor(new Vector3(NodeList[0].NodeEnd, 0), Color.White);

                    vertices2[0] = new VertexPositionColor(new Vector3(StartPosition, 0), Color.White);
                    vertices2[1] = new VertexPositionColor(new Vector3(StartPosition.X,StartPosition.Y + 10, 0), Color.White);
                }
                else 
                #endregion
                {
                    #region Adjust angle and length
                    float ang, lenp;

                    lenp = Random.Next(20, 40);

                    if (Random.NextDouble() >= 0.5)
                    {
                        ang = (MathHelper.Lerp(NodeList[NodeList.Count - 1].Angle, Random.Next(-90, 90), 0.1f));
                    }
                    else
                    {
                        ang = (MathHelper.Lerp(NodeList[NodeList.Count - 1].Angle, Random.Next(-90, 90), 0.2f)) / i;
                    }

                    if (Random.NextDouble() > 0.8)
                    {
                        lenp = Random.Next((int)LengthRange.X, (int)LengthRange.Y);

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
                    
                    #endregion

                    newNode = new Node()
                    {
                        NodePosition = NodeList[i-1].NodeEnd,
                        Angle = ang,
                        Width = MathHelper.Lerp(NodeList[i - 1].Width, Random.Next(0, width), 0.5f)
                    };

                    if (i > (TotalLength/2)-5)
                    {
                        newNode = new Node()
                        {
                            NodePosition = NodeList[i - 1].NodeEnd,
                            Angle = ang,
                            Width = (TotalLength/2)-i
                        };
                    }

                    newNode.Direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(newNode.Angle)), 
                                                    (float)Math.Sin(MathHelper.ToRadians(newNode.Angle)));
                    newNode.NodeEnd = newNode.NodePosition + (newNode.Direction * lenp);

                    NodeList.Add(newNode);
                }
            }

            #region Adjust Deltas
            float DeltaY = NodeList[NodeList.Count - 1].NodeEnd.Y - EndPosition.Y;
            float DeltaX = NodeList[NodeList.Count - 1].NodeEnd.X - EndPosition.X;

            List<Node> NewNodes = new List<Node>();
            NewNodes.Add(NodeList[0]);
            NewNodes.Add(NodeList[1]);

            for (int i = 2; i < TotalLength / 2; i++)
            {
                double dif = (i / (double)((TotalLength - 2) / 2));

                NodeList[i].NodeEnd.Y -= (float)(DeltaY * dif);
                //node.NodePosition.Y -= (float)(DeltaY * dif);

                NodeList[i].NodeEnd.X -= (float)(DeltaX * dif);
                //node.NodePosition.X -= (float)(DeltaX * dif);

            }
            #endregion
            

            Vector2 diffy;
            float angl;

            
            for (int p = 1; p < TotalLength/2-1; p++)
            {
                diffy = (NodeList[p+1].NodeEnd - NodeList[p-1].NodeEnd);
                angl = (float)Math.Atan2(diffy.Y, diffy.X);

                angl -= (float)MathHelper.ToRadians(90 + Random.Next(-5, 5));

                NodeList[p].TangentDirection = new Vector2((float)Math.Cos(angl), (float)Math.Sin(angl));
                NodeList[p].TangentDirection.Normalize();
            }

            for (int i = 2; i < TotalLength; i += 2)
            {
                vertices2[i] = new VertexPositionColor(new Vector3(NodeList[i / 2].NodeEnd + (NodeList[i / 2].TangentDirection * NodeList[i / 2].Width), 0), Color.White);
                vertices2[i + 1] = new VertexPositionColor(new Vector3(NodeList[i / 2].NodeEnd - (NodeList[i / 2].TangentDirection * NodeList[i / 2].Width), 0), Color.White);
            }
        }

        public void LoadContent(ContentManager content)
        {
            Block = content.Load<Texture2D>("WhiteBlock");
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < vertices2.Length - 1; i++)
            {
                vertices2[i].Color = Color.Lerp(vertices2[i].Color, Color.White * 0f, 0.05f);
            }
        }

        public void Draw(GraphicsDevice graphics)
        {
            //graphics.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, TotalLength / 2);
            graphics.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertices2, 0, TotalLength-2);
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
