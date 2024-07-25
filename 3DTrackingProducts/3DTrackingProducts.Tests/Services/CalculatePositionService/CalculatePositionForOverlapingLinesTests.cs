using _3DTrackingProducts.Api.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DTrackingProductsTests.Services.CalculatePositionService
{
    public class CalculatePositionForOverlapingLinesTests
    {
        [Fact]
        public void calculatePositionForOverlapingLines_returnsMiddlePoint_IncreasingLineRight()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 180;
            double angle02 = 0;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().BeGreaterThan(antennaPoint01.Item1).And.BeLessThan(antennaPoint02.Item1);
            result.Item2.Should().BeGreaterThan(antennaPoint01.Item2).And.BeLessThan(antennaPoint02.Item2);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsMiddlePoint_IncreasingLineLeft()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 0;
            double angle02 = 180;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().BeGreaterThan(antennaPoint01.Item1).And.BeLessThan(antennaPoint02.Item1);
            result.Item2.Should().BeGreaterThan(antennaPoint01.Item2).And.BeLessThan(antennaPoint02.Item2);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsYRoomLength_IncreasingLineLeft()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 0;
            double angle02 = 0;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().BeGreaterThan(antennaPoint02.Item1);
            result.Item2.Should().Be(room.Length);
        }


        [Fact]
        public void calculatePositionForOverlapingLines_returnsYRoomLength_IncreasingLineRight()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(4, 4);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(1, 1);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 180;
            double angle02 = 180;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().BeGreaterThan(antennaPoint01.Item1);
            result.Item2.Should().Be(room.Length);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsYOrigin_IncreasingLineLeft()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(4,4);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(1,1);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 0;
            double angle02 = 0;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().BeLessThanOrEqualTo(antennaPoint02.Item1);
            result.Item2.Should().Be(0);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsYOrigin_IncreasingLineRight()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 180;
            double angle02 = 180;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().BeLessThanOrEqualTo(antennaPoint02.Item1);
            result.Item2.Should().Be(0);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsMiddlePoint_DecreasingLineRight()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 4);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 180;
            double angle02 = 0;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().BeGreaterThan(antennaPoint01.Item1).And.BeLessThan(antennaPoint02.Item1);
            result.Item2.Should().BeLessThan(antennaPoint01.Item2).And.BeGreaterThan(antennaPoint02.Item2);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsMiddlePoint_DecreasingLineLeft()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 4);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 180;
            double angle02 = 0;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().BeGreaterThan(antennaPoint01.Item1).And.BeLessThan(antennaPoint02.Item1);
            result.Item2.Should().BeLessThan(antennaPoint01.Item2).And.BeGreaterThan(antennaPoint02.Item2);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsYOrigin_DecreasingLineRight()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 4);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 0;
            double angle02 = 0;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().BeGreaterThan(antennaPoint02.Item1);
            result.Item2.Should().Be(0);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsYOrigin_DecreasingLineLeft()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(4, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(1, 4);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 180;
            double angle02 = 180;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().BeGreaterThan(antennaPoint01.Item1);
            result.Item2.Should().Be(0);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsYRoomLength_DecreasingLineRight()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 4);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 180;
            double angle02 = 180;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().BeLessThan(antennaPoint01.Item1);
            result.Item2.Should().Be(room.Length);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsYRoomLength_DecreasingLineLeft()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(4, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(1, 4);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 0;
            double angle02 = 0;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().BeLessThan(antennaPoint02.Item1);
            result.Item2.Should().Be(room.Length);
        }


        [Fact]
        public void calculatePositionForOverlapingLines_returnsMiddlePoint_HorizontalLineRight()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(4, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(1, 1);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 180;
            double angle02 = 0;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().BeLessThan(antennaPoint01.Item1).And.BeGreaterThan(antennaPoint02.Item1);
            result.Item2.Should().Be(antennaPoint01.Item2);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsMiddlePoint_HorizontalLineLeft()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 0;
            double angle02 = 180;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().BeGreaterThan(antennaPoint01.Item1).And.BeLessThan(antennaPoint02.Item1);
            result.Item2.Should().Be(antennaPoint01.Item2);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsXRoomWidth_HorizontalLineUp()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 0;
            double angle02 = 0;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().Be(room.Width);
            result.Item2.Should().Be(antennaPoint01.Item2);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsXRoomWidth_HorizontalLineDown()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(4, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(1, 1);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 180;
            double angle02 = 180;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().Be(room.Width);
            result.Item2.Should().Be(antennaPoint01.Item2);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsXOrigin_HorizontalLineUp()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(1, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 180;
            double angle02 = 180;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().Be(0);
            result.Item2.Should().Be(antennaPoint01.Item2);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsXOrigin_HorizontalLineDown()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(4, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(1, 1);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 0;
            double angle02 = 0;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().Be(0);
            result.Item2.Should().Be(antennaPoint01.Item2);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsMiddlePoint_VerticalLineLeft()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(4, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 180;
            double angle02 = 0;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().Be(antennaPoint01.Item1);
            result.Item2.Should().BeGreaterThan(antennaPoint01.Item2).And.BeLessThan(antennaPoint02.Item2);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsMiddlePoint_VerticalLineRight()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(4, 4);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            double angularCoeficient = CalculatePosition.calculateAngularCoeficientBetweenAntennas(antennaPoint01, antennaPoint02);
            double angle01 = 0;
            double angle02 = 180;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().Be(antennaPoint01.Item1);
            result.Item2.Should().BeGreaterThan(antennaPoint02.Item2).And.BeLessThan(antennaPoint01.Item2);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsYRoomLength_VerticalLineLeft()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(4, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            double angularCoeficient = double.MinValue;
            double angle01 = 0;
            double angle02 = 0;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().Be(antennaPoint01.Item1);
            result.Item2.Should().Be(room.Length);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsYRoomLength_VerticalLineRight()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(4, 4);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            double angularCoeficient = double.MinValue;
            double angle01 = 180;
            double angle02 = 180;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().Be(antennaPoint01.Item1);
            result.Item2.Should().Be(room.Length);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsYOrigin_VerticalLineLeft()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(4, 1);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 4);
            double angularCoeficient = double.MinValue;
            double angle01 = 180;
            double angle02 = 180;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().Be(antennaPoint01.Item1);
            result.Item2.Should().Be(0);
        }

        [Fact]
        public void calculatePositionForOverlapingLines_returnsYOrigin_VerticalLineRight()
        {
            Tuple<int, int> antennaPoint01 = new Tuple<int, int>(4, 4);
            Tuple<int, int> antennaPoint02 = new Tuple<int, int>(4, 1);
            double angularCoeficient = double.MinValue;
            double angle01 = 0;
            double angle02 = 0;
            Room room = new Room
            {
                Length = 10,
                Width = 10
            };

            var result = CalculatePosition.calculatePositionForOverlapingLines(antennaPoint01, antennaPoint02, angle01, angle02, angularCoeficient, room);

            result.Item1.Should().Be(antennaPoint01.Item1);
            result.Item2.Should().Be(0);
        }
    }
}
