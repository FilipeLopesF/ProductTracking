using _3DTrackingProducts.Api.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DTrackingProductsTests.Services.CalculatePositionService
{
    public class CalculatePositionOfIntersectionPointWithHorizontalLineTests
    {
        [Fact]
        public void calculatePositionOfIntersectionPointWithHorizontalLine_IncreasingLineUp()
        {
            Tuple<int, int> antennaPoint = new Tuple<int, int>(1,1);
            double angularCoeficient = 45;
            double y = 4;

            var result = CalculatePosition.calculatePositionOfIntersectionPointWithHorizontalLine(antennaPoint, angularCoeficient,y);

            result.Item1.Should().Be(4);
        }

        [Fact]
        public void calculatePositionOfIntersectionPointWithHorizontalLine_IncreasingLineDown()
        {
            Tuple<int, int> antennaPoint = new Tuple<int, int>(4, 4);
            double angularCoeficient = 45;
            double y = 1;

            var result = CalculatePosition.calculatePositionOfIntersectionPointWithHorizontalLine(antennaPoint, angularCoeficient, y);

            result.Item1.Should().Be(1);
        }

        [Fact]
        public void calculatePositionOfIntersectionPointWithHorizontalLine_DecreasingLineDown()
        {
            Tuple<int, int> antennaPoint = new Tuple<int, int>(4, 4);
            double angularCoeficient = -45;
            double y = 1;

            var result = CalculatePosition.calculatePositionOfIntersectionPointWithHorizontalLine(antennaPoint, angularCoeficient, y);

            result.Item1.Should().Be(7);
        }

        [Fact]
        public void calculatePositionOfIntersectionPointWithHorizontalLine_DecreasingLineUp()
        {
            Tuple<int, int> antennaPoint = new Tuple<int, int>(3, 3);
            double angularCoeficient = -45;
            double y = 5;

            var result = CalculatePosition.calculatePositionOfIntersectionPointWithHorizontalLine(antennaPoint, angularCoeficient, y);

            result.Item1.Should().Be(1);
        }
    }
}
