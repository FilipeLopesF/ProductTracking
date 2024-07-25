using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Models.Exceptions;
using FluentAssertions;

namespace _3DTrackingProductsTests.Services.CalculatePositionService
{
    public class CalculatePositionTests
    {

        [Fact]
        public void validateTagPosition_returnsOrigin_WhenTagPointXAndYAreNegative()
        {
            Tuple<double, double> tagPoint = new Tuple<double, double>(-10, -30);
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.validateTagPosition(tagPoint, room);

            result.locX.Should().Be(0);
            result.locY.Should().Be(0);
        }

        [Fact]
        public void validateTagPosition_returnsValidTag_WhenTagPointOutOfRoom()
        {
            Tuple<double, double> tagPoint = new Tuple<double, double>(20, 20);
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.validateTagPosition(tagPoint, room);

            result.locX.Should().Be(room.Width);
            result.locY.Should().Be(room.Length);
        }

        [Fact]
        public void validateTagPosition_returnsValidTag_WhenTagInRoom()
        {
            Tuple<double, double> tagPoint = new Tuple<double, double>(5, 5);
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.validateTagPosition(tagPoint, room);

            result.locX.Should().Be(5);
            result.locY.Should().Be(5);
        }


        [Fact]
        public void calculate_throwsException_whenBothAreVerticalLinesAndAngularCoeficientIsZero()
        {
            
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 5);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 90
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(5, 5);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 90
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);
            }
            catch(ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeTrue();
           
        }

        [Fact]
        public void calculate_throwsException_whenAnglesAreEqualAndNotExtremes()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 4);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 50
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 50
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeTrue();

        }

        [Fact]
        public void calculate_returnsValidTag_whenAnglesAreEqualAndZeroForPositiveAngularCoeficient()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 0
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 0
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_returnsValidTag_whenAnglesAreEqualAndZeroForNegativeAngularCoeficient()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 4);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 0
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 0
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_returnsValidTag_whenAnglesAreEqualAndZeroForAngularCoeficientZero()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 4);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 0
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 0
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_returnsValidTag_whenAnglesAreEqualAndZeroForVerticalLineBetweenTags()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(4, 1);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 0
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 0
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_returnsValidTag_whenAnglesAreEqualAnd180ForPositiveAngularCoeficient()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 180
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 180
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_returnsValidTag_whenAnglesAreEqualAnd180ForNegativeAngularCoeficient()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 4);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 180
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 180
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_returnsValidTag_whenAnglesAreEqualAnd180ForAngularCoeficientZero()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 180
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 180
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_returnsValidTag_whenAnglesAreEqualAnd180ForVerticalLinesBetweenAntennas()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 180
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(1, 4);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 180
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_returnsValidTag_whenAnglesAreOpositeForPositiveAngularCoeficient()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 180
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 0
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_returnsValidTag_whenAnglesAreOpositeForNegativeAngularCoeficient()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 4);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 180
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 0
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_returnsValidTag_whenAnglesAreOpositeForAngularCoeficientZero()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 180
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 0
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_returnsValidTag_whenAnglesAreOpositeForVerticalLineBetweenAntennas()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 4);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 180
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(1, 1);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 0
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_returnsValidTag_whenVerticalLine1AndPositiveAngularCoeficient()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 45
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 30
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_returnsValidTag_whenVerticalLine2AndPositiveAngularCoeficient()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 5
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 45
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_returnsValidTag_whenPositiveAngularCoeficient()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 30
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 150
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

                result.locX.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Width);
                result.locY.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(room.Length);
            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeFalse();

        }

        [Fact]
        public void calculate_throwsExcetion_whenPositiveAngularCoeficient()
        {

            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            List<Log> logsFromAntenna01 = new List<Log>();
            logsFromAntenna01.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 30
            }
            );
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            List<Log> logsFromAntenna02 = new List<Log>();
            logsFromAntenna02.Add(new Log
            {
                TagEPC = "EPC0001",
                RSSI = 300,
                IPAddress = "127.0.0.1",
                Angle = 30
            }
            );
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            bool exceptionParallelLineThrown = false;

            try
            {
                var result = CalculatePosition.calculate(logsFromAntenna01, antennaPoint01, logsFromAntenna02, antennaPoint02, room);

            }
            catch (ParallelLinesException e)
            {
                exceptionParallelLineThrown = true;
            }

            exceptionParallelLineThrown.Should().BeTrue();

        }
    }
}
