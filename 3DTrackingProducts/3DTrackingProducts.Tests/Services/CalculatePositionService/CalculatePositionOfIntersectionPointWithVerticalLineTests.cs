using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3DTrackingProducts.Api.Models;
using FluentAssertions;

namespace _3DTrackingProductsTests.Services.CalculatePositionService
{
    public class CalculatePositionOfIntersectionPointWithVerticalLineTests
    {

        [Fact]
        public void calculatePositionOfIntersectionPointWithVerticalLine_returnValidPoint_WhenIntersectionExistsForPositiveAngularCoeficient()
        {

            Tuple<int, int> antennaPoint = new Tuple<int, int>(0, 0);
            double angularCoeficient = 10;
            int x = 2;


            var result = CalculatePosition.calculatePositionOfIntersectionPointWithVerticalLine(antennaPoint, angularCoeficient, x);

            result.Item1.Should().Be(x);
            result.Item2.Should().BeGreaterThanOrEqualTo(0);

        }

        [Fact]
        public void calculatePositionOfIntersectionPointWithVerticalLine_returnValidPoint_WhenIntersectionExistsForNegativeAngularCoeficient()
        {

            Tuple<int, int> antennaPoint = new Tuple<int, int>(0, 0);
            double angularCoeficient = -10;
            int x = 2;


            var result = CalculatePosition.calculatePositionOfIntersectionPointWithVerticalLine(antennaPoint, angularCoeficient, x);

            result.Item1.Should().Be(x);
            result.Item2.Should().BeLessThanOrEqualTo(0);

        }
    }
}
