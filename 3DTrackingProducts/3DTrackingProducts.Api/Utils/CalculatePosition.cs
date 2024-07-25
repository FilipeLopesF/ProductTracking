using _3DTrackingProducts.Api.Dtos;
using _3DTrackingProducts.Api.Migrations;
using _3DTrackingProducts.Api.Models.Exceptions;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;
using _3DTrackingProducts.Api.Dtos;

namespace _3DTrackingProducts.Api.Models
{
    public class CalculatePosition
    {

        public static CalculatePositionDto calculate(List<Log> logsFromIPAddress1, Tuple<int,int> antenna01Position, List<Log> logsFromIPAddress2, Tuple<int, int> antenna02Position, Room room)
        {
            double angle1 = GetAngleFromLogs(logsFromIPAddress1);
            double angle2 = GetAngleFromLogs(logsFromIPAddress2);

            // angularCoeficient => angular coeficient of line created between the two antennas
            double angularCoeficient = calculateAngularCoeficientBetweenAntennas(antenna01Position, antenna02Position);

            double angularCoeficientAntenna01 = 0;
            double angularCoeficientAntenna02 = 0;

            bool verticalLineAntenna01 = false;

            bool verticalLineAntenna02 = false;

            Tuple<double, double> point;

            // calculate angular coeficient between tag and antenna01
            try
            {
                angularCoeficientAntenna01 = calculateAngularCoeficientOfTagAndAntenna(
                    angularCoeficient, 
                    angle1, 
                    antenna01Position.Item1
                );

            }catch(VerticalLineException e)
            {
                verticalLineAntenna01 = true;
                angle1 = e.angle;
            }

            // calculate angular coeficient between tag and antenna02
            try
            {
                angularCoeficientAntenna02 = calculateAngularCoeficientOfTagAndAntenna(
                    angularCoeficient, 
                    angle2, 
                    antenna02Position.Item1
                );

            }catch(VerticalLineException e)
            {
                verticalLineAntenna02 = true;
                angle2 = e.angle;
            }

            if(verticalLineAntenna01 && verticalLineAntenna02 && angularCoeficient == 0)
            {
                throw new ParallelLinesException("");
            }

            if(angle1 == angle2 && (angle1 != 0 && angle1 != 180))
            {
                throw new ParallelLinesException("");
            }

            if((angle1 == 0 && angle2 == 0) ||
                (angle1 == 180 && angle2 == 180) ||
                    (angle1 == 0 && angle2 == 180) || 
                        (angle1 == 180 && angle2 == 0))
            {
                point = calculatePositionForOverlapingLines(antenna01Position, antenna02Position, angle1, angle2, angularCoeficient, room);

                return validateTagPosition(point, room, angle1, angle2);
            }

            if (verticalLineAntenna01)
            {
               point = calculatePositionOfIntersectionPointWithVerticalLine(antenna02Position, angle2, antenna01Position.Item1);

                return validateTagPosition(point, room, angle1, angle2);
            }

            if (verticalLineAntenna02)
            {
                point = calculatePositionOfIntersectionPointWithVerticalLine(antenna01Position, angle1, antenna02Position.Item1);

                return validateTagPosition(point, room, angle1, angle2);
            }
            
            point = calculatePositionOfIntersectionPoint(
                antenna01Position, 
                antenna02Position, 
                angularCoeficientAntenna01, 
                angularCoeficientAntenna02
            );

            return validateTagPosition(point, room, angle1, angle2);
        }

        public static double calculateAngularCoeficientBetweenAntennas(Tuple<int, int> pointA, Tuple<int, int> pointB)
        {

            // x1-x2 = 0 => vertical line (angular coeficient is inexistent)
            if(pointA.Item1 - pointB.Item1 == 0)
            {
                return double.MinValue;
            }

            // y1-y2 = 0 => horizontal line (angular coeficient is always 0)
            if(pointA.Item2 - pointB.Item2 == 0)
            {
                return 0;
            }

            double numerator = pointA.Item2 - pointB.Item2;
            double denominator = pointA.Item1 - pointB.Item1;
            //angular coeficient of line => m = (y1-y2)/(x1-x2)
            double angularCoeficient =  numerator / denominator;
            
            return angularCoeficient;
        }

        public static double GetAngleFromLogs(List<Log> logsFromIPAddress)
        {
            int rssiInterval = 4;
            int maxRSSI = 0;
            int total = 0;
            double sum = 0;
            int maxRSSIAux;
            int aux;
            double angle = 0;

            foreach (Log log in logsFromIPAddress) //vai buscar angulo do log com maior RSSI   //Meter no mesmo foreach
            {
                maxRSSIAux = log.RSSI;
                if (maxRSSIAux > maxRSSI)
                {
                    maxRSSI = maxRSSIAux;
                    angle = log.Angle;
                }
            }

            foreach (Log log in logsFromIPAddress)
            {
                aux = log.RSSI;
                if (aux > (maxRSSI - rssiInterval))
                {
                    total++;
                    sum += log.Angle;
                }
            }

            if (total != 0)
            {
                angle = Math.Round(sum / total, 2);
            }

            return angle;
        }

        public static double calculateAngularCoeficientOfTagAndAntenna(double angularCoeficient, double angle, int antennaXPosition)
        {

            double angleCoeficientRadians = Math.Atan(angularCoeficient);
            double angularCoeficientAngle = Math.Round(angleCoeficientRadians * (180 / Math.PI));
            double absAngularCoeficientAngle = Math.Abs(angularCoeficientAngle);

            switch (angularCoeficient)
            {
                case double.MinValue:

                    if(angle == 180 || angle == 0)
                    {
                        throw new VerticalLineException(antennaXPosition, angle);
                    }

                    if(angle > 90)
                    {
                       return angle - 90;
                    }
                   
                    if(angle <= 90)
                    {
                        return -(90 - angle);
                    }
                    break;

                case 0:
                    if(angle == 0 || angle == 180)
                    {
                        return angle;
                    }

                    if(angle == 90)
                    {
                        throw new VerticalLineException(antennaXPosition, angle);
                    }

                    if(angle < 90)
                    {
                        return angle;
                    }
                    
                    if(angle > 90)
                    {
                        return -(180 - angle);
                    }

                    break;
                case > 0:

                    if (angle == 180)
                    {
                        return angle;
                    }

                    if(angle + angularCoeficientAngle == 90)
                    {
                        throw new VerticalLineException(antennaXPosition, angle);
                    }

                    if(angle + angularCoeficientAngle < 180)
                    {
                        return -(180-(angle + angularCoeficientAngle));
                    }

                    if(angle + angularCoeficientAngle == 180)
                    {
                        return 0;
                    }
                    
                    if(angle + angularCoeficientAngle > 180)
                    {
                        return angularCoeficientAngle - (180 - angle);
                    }
                    break;

                case < 0:

                    if (angle == absAngularCoeficientAngle)
                    {
                        return 0;
                    }

                    if (angle == 180)
                    {
                        return angularCoeficientAngle;
                    }

                    if (angle - absAngularCoeficientAngle == 90)
                    {
                        throw new VerticalLineException(antennaXPosition, angle);
                    }

                    if(angle > absAngularCoeficientAngle)
                    {
                        return angle - absAngularCoeficientAngle;
                    }
                    
                    if(angle < absAngularCoeficientAngle)
                    {
                        return -(absAngularCoeficientAngle - angle);
                    }
                    break;

                 default:
                    throw new Exception();
            }

            return 0;
        }

        public static Tuple<double,double> calculatePositionOfIntersectionPoint(Tuple<int,int> antenna01Position, Tuple<int,int> antenna02Position, double angularCoeficient1, double angularCoeficient2)
        {
            double m1 = Math.Tan(angularCoeficient1 * (Math.PI/180));

            double m2 = Math.Tan(angularCoeficient2 * (Math.PI / 180));

            //parallel lines
            if (m1 == m2)
            {
                throw new ParallelLinesException("No intersection between lines created from tag and antennas"); 
            }

            // y = mx + b => b = y - mx
            double b1 = antenna01Position.Item2 - (m1 * antenna01Position.Item1);

            double b2 = antenna02Position.Item2 - (m2 * antenna02Position.Item1);

            double x = (b2 - b1) / (m1 - m2);

            double y = m1 * x + b1;

            return new Tuple<double, double>(x, y);
        }

        public static Tuple<double, double> calculateMiddlePointBetweenAntennas(Tuple<int, int> antennaPoint01, Tuple<int,int> antennaPoint02)
        {

            double x = (antennaPoint01.Item1 + antennaPoint02.Item1) / 2;

            double y = (antennaPoint01.Item2 + antennaPoint02.Item2) / 2;

            return new Tuple<double, double>(x, y);
        }

        public static Tuple<double, double> calculatePositionOfIntersectionPointWithVerticalLine(Tuple<int,int> antennaPoint, double angularCoeficient, int x)
        {
            double m1 = Math.Tan(angularCoeficient * (Math.PI / 180));

            //  b = y - mx
            double b1 = antennaPoint.Item2 - (m1 * antennaPoint.Item1);

            // y = mx + b
            double y = m1 * x + b1;

            return new Tuple<double, double>(x, y);
        }

        public static Tuple<double, double> calculatePositionOfIntersectionPointWithHorizontalLine(Tuple<int, int> antennaPoint, double angularCoeficient, double y)
        {
            double m1 = Math.Tan(angularCoeficient * (Math.PI / 180));

            //  b = y - mx
            double b1 = antennaPoint.Item2 - (m1 * antennaPoint.Item1);

            //
            double x = Math.Round((y - b1) / m1);

            return new Tuple<double, double>(x, y);
        }

        public static Tuple<double, double> calculatePositionForOverlapingLines(Tuple<int, int> antennaPoint01, Tuple<int,int> antennaPoint02, double angle01, double angle02, double angularCoeficient, Room room)
        {

            if(angle01 == 0 && angle02 == 0)
            {
                if (angularCoeficient == double.MinValue)
                {
                    if (antennaPoint02.Item2 < antennaPoint01.Item2)
                    {
                        return new Tuple<double, double>(antennaPoint01.Item1, 0);
                    }

                    return new Tuple<double, double>(antennaPoint01.Item1, room.Length);
                }

                if(angularCoeficient == 0)
                {
                    if(antennaPoint02.Item1 < antennaPoint01.Item1)
                    {
                        return new Tuple<double, double>(0, antennaPoint01.Item2);
                    }

                    return new Tuple<double, double>(room.Width, antennaPoint01.Item2);
                }


                if (antennaPoint02.Item2 < antennaPoint01.Item2)
                {
                    return calculatePositionOfIntersectionPointWithHorizontalLine(antennaPoint02, angularCoeficient, 0);
                }

                return calculatePositionOfIntersectionPointWithHorizontalLine(antennaPoint01, angularCoeficient, room.Length);

            }

            if(angle01 == 180 && angle02 == 180)
            {
                if (angularCoeficient == double.MinValue)
                {
                    if (antennaPoint02.Item2 < antennaPoint01.Item2)
                    {
                        return new Tuple<double, double>(antennaPoint01.Item1, room.Length);
                    }

                    return new Tuple<double, double>(antennaPoint01.Item1, 0);
                }

                if (angularCoeficient == 0)
                {
                    if (antennaPoint02.Item1 < antennaPoint01.Item1)
                    {
                        return new Tuple<double, double>(room.Width, antennaPoint01.Item2);
                    }

                    return new Tuple<double, double>(0, antennaPoint01.Item2);
                }

                if (antennaPoint02.Item2 < antennaPoint01.Item2)
                {
                    return calculatePositionOfIntersectionPointWithHorizontalLine(antennaPoint02, angularCoeficient, room.Length);
                }

                return calculatePositionOfIntersectionPointWithHorizontalLine(antennaPoint01, angularCoeficient, 0);
                
            }

            return calculateMiddlePointBetweenAntennas(antennaPoint01, antennaPoint02);
        }

        public static CalculatePositionDto validateTagPosition(Tuple<double, double> point, Room room, double angleAntenna01, double angleAntenna02)
        {
            double locX = point.Item1;
            double locY = point.Item2;

            if (point.Item1 > room.Width)
            {
                locX = room.Width;
            }

            if (point.Item1 < 0)
            {
                locX = 0;
            }

            if (point.Item2 > room.Length)
            {
                locY = room.Length;
            }

            if (point.Item2 < 0)
            {
                locY = 0;
            }

            return new CalculatePositionDto
            {
                locX = locX,
                locY = locY,
                angleAntenna01 = angleAntenna01,
                angleAntenna02 = angleAntenna02
            };
        }
    }
}
