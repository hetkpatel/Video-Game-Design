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

namespace ScribblePlatformer
{
        public static class RectangleExtensions
        {
            public static Vector2 GetIntersectionDepth(this Rectangle _rectA, Rectangle _rectB)
            {
                //calculate half sizes
                float halfWidthA = _rectA.Width / 2.0f;
                float halfHeightA = _rectA.Height / 2.0f;
                float halfWidthB = _rectA.Width / 2.0f;
                float halfHeightB = _rectA.Height / 2.0f;
                //calculate center
                Vector2 centerA = new Vector2(_rectA.Left + halfWidthA, _rectA.Top + halfHeightA);
                Vector2 centerB = new Vector2(_rectB.Left + halfWidthB, _rectB.Top + halfHeightB);
                //calculate current and minimum non intersecting distances between centers
                float distanceX = centerA.X - centerB.X;
                float distanceY = centerA.Y - centerB.Y;
                float minDistanceX = halfWidthA + halfWidthB;
                float minDistanceY = halfHeightA + halfHeightB;
                //if not intersectin (0,0)
                if (Math.Abs(distanceX) >= minDistanceX || Math.Abs(distanceY) >= minDistanceY)
                    return Vector2.Zero;
                //Calculate and return intersectoin depths
                float depthX = distanceX > 0 ? minDistanceX - distanceX : -minDistanceX - distanceX;
                float depthY = distanceY > 0 ? minDistanceY - distanceY : -minDistanceY - distanceY;
                return new Vector2(depthX, depthY);

            }
        }
    }

