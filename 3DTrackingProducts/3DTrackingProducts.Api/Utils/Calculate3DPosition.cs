using System;
using System.Drawing;
using _3DTrackingProducts.Api.Core;
using _3DTrackingProducts.Api.Dtos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace _3DTrackingProducts.Api.Models
{
    public class Calculate3DPosition
    {
        public static Tag3DPosition Get(Tag? tag, ControlTag controlTag0, ControlTag controlTag1, float radius0, float radius1, float distanceHigh)
        {
            try
            {

                //Absolute values of control tags stored on BD
                var cx0Abs = (float)controlTag0.PositionX;
                var cy0Abs = (float)controlTag0.PositionY;
                var cx1Abs = (float)controlTag1.PositionX;
                var cy1Abs = (float)controlTag1.PositionY;

                var m = (cy0Abs - cy1Abs) / (cx0Abs - cx1Abs); //Declive
                var gamma = new double();
                if (m == 0)
                {
                    gamma = 0; 
                }
                else if (m<0)
                {
                    gamma = - Math.Atan(Math.Abs(cy1Abs - cy0Abs) / Math.Abs(cx1Abs - cx0Abs)); //Gamma
                }
                else if (m>0) {
                    gamma = Math.Atan(Math.Abs(cy1Abs - cy0Abs) / Math.Abs(cx1Abs - cx0Abs)); //Gamma
                }

                //Relative values of control tags
                var cx0 = 0;
                var cy0 = 0;
                var cx1 = (float)DistanceBetween2Points(cx0Abs, cy0Abs, cx1Abs, cy1Abs);
                var cy1 = 0;

                PointF int1 = new PointF();
                PointF int2 = new PointF();
                double h = new double();

                IntersectionTwoCirclesAbsolute(cx0, cy0, radius0, cx1, cy1, radius1, out h, out int1, out int2);

                var relIntX = int1.X > int2.X ? int1.X : int2.X;
                var relIntY = int1.X > int2.X ? int1.Y : int2.Y;
                var dX = cx0Abs; //Translação nos xx
                var dY = cy0Abs; //Translação nos yy

                //position.TagZ = GetZCoordenate(h, distanceHigh);

                Tag3DPosition tag3DPosition = new Tag3DPosition
                {
                    TagEPC = tag.EPC,
                    ControlTagEPCLeft = controlTag0.EPC,
                    ControlTagEPCRight = controlTag1.EPC,
                    DistanceLeft = radius0,
                    DistanceRight = radius1,
                    DistanceHigh = distanceHigh,
                    RelativePosX = relIntX,
                    RelativePosY = relIntY,
                };

                return tag3DPosition;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            };
        }

        //--> baseado:http://www.csharphelper.com/howtos/howto_circle_circle_intersection.html
        // Find the points where the two circles intersect.
        private static int IntersectionTwoCirclesAbsolute(
            float cx0, float cy0, float radius0,
            float cx1, float cy1, float radius1, out double h,
            out PointF intersection1, out PointF intersection2)
        {
            // Find the distance between the centers.
            float dx = cx0 - cx1;
            float dy = cy0 - cy1;
            double dist = Math.Sqrt(dx * dx + dy * dy);
            h = new double();

            // See how many solutions there are.
            if (dist > radius0 + radius1)
            {
                // No solutions, the circles are too far apart.
                intersection1 = new PointF(float.NaN, float.NaN);
                intersection2 = new PointF(float.NaN, float.NaN);
                return 0;
            }
            else if (dist < Math.Abs(radius0 - radius1))
            {
                // No solutions, one circle contains the other.
                intersection1 = new PointF(float.NaN, float.NaN);
                intersection2 = new PointF(float.NaN, float.NaN);
                return 0;
            }
            else if ((dist == 0) && (radius0 == radius1))
            {
                // No solutions, the circles coincide.
                intersection1 = new PointF(float.NaN, float.NaN);
                intersection2 = new PointF(float.NaN, float.NaN);
                return 0;
            }
            else
            {
                // Find a and h.
                double a = (radius0 * radius0 -
                    radius1 * radius1 + dist * dist) / (2 * dist);
                h = Math.Sqrt(radius0 * radius0 - a * a);

                // Find P2.
                double cx2 = cx0 + a * (cx1 - cx0) / dist;
                double cy2 = cy0 + a * (cy1 - cy0) / dist;

                // Get the points P3.
                intersection1 = new PointF(
                    (float)(cx2 + h * (cy1 - cy0) / dist),
                    (float)(cy2 - h * (cx1 - cx0) / dist));
                intersection2 = new PointF(
                    (float)(cx2 - h * (cy1 - cy0) / dist),
                    (float)(cy2 + h * (cx1 - cx0) / dist));

                // See if we have 1 or 2 solutions.
                if (dist == radius0 + radius1) return 1;
                return 2;
            }
        }

        private static Position3DDto GetAbsoluteIntersection(double xA, double yA, double dX, double dY, double gamma)
        {
            var yA_Abs = ((-xA * Math.Sin(gamma)) + (yA * Math.Cos(gamma)) - (dY * Math.Cos(gamma)) + (dX * Math.Sin(gamma))) / Math.Pow(Math.Cos(gamma), 2) + Math.Pow(Math.Sin(gamma), 2);
            var xA_Abs = (xA + (yA_Abs * Math.Sin(gamma) - dX)) / Math.Cos(gamma);

            return new Position3DDto(xA_Abs, yA_Abs, 0);
        }

        private static double GetZCoordenate(double h, double distance3)
        {
            return Math.Sqrt((distance3 * distance3) - (h * h));
        }

        private static double DistanceBetween2Points(double x1, double y1, double x2, double y2) {
            return Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));

        }
    }

}

