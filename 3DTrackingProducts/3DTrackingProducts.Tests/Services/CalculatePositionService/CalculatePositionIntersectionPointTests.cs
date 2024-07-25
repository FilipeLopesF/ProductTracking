using _3DTrackingProducts.Api.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DTrackingProductsTests.Services.CalculatePositionService
{
    public class CalculatePositionIntersectionPointTests
    {
        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_IncreasingAngularCoeficientRightSideMiddle()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 1);
            Tuple<int, int> antenna02 = new Tuple<int, int>(4, 4);
            double angularCoeficient01 = 10;
            double angularCoeficient02 = -45;

            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeGreaterThan(antenna01.Item1);
            result.Item2.Should().BeLessThanOrEqualTo(antenna02.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_IncreasingAngularCoeficientRightSideHigh()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 1);
            Tuple<int, int> antenna02 = new Tuple<int, int>(4, 4);
            double angularCoeficient01 = 30;
            double angularCoeficient02 = 10;

            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeGreaterThan(antenna02.Item1);
            result.Item2.Should().BeGreaterThan(antenna02.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_IncreasingAngularCoeficientRightSideLow()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 1);
            Tuple<int, int> antenna02 = new Tuple<int, int>(4, 4);
            double angularCoeficient01 = -30;
            double angularCoeficient02 = 50;

            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeGreaterThanOrEqualTo(antenna01.Item1);
            result.Item2.Should().BeLessThanOrEqualTo(antenna01.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_IncreasingAngularCoeficientLeftSideMiddle()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 1);
            Tuple<int, int> antenna02 = new Tuple<int, int>(4, 4);
            double angularCoeficient01 = -30;
            double angularCoeficient02 = 10;

            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeLessThanOrEqualTo(antenna01.Item1);
            result.Item2.Should().BeLessThanOrEqualTo(antenna02.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_IncreasingAngularCoeficientLeftSideHigh()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 1);
            Tuple<int, int> antenna02 = new Tuple<int, int>(4, 4);
            double angularCoeficient01 = 50;
            double angularCoeficient02 = -40;

            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeGreaterThan(antenna01.Item1);
            result.Item2.Should().BeGreaterThan(antenna02.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_IncreasingAngularCoeficientLeftSideLow()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 1);
            Tuple<int, int> antenna02 = new Tuple<int, int>(4, 4);
            double angularCoeficient01 = 10;
            double angularCoeficient02 = 40;

            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeLessThan(antenna01.Item1);
            result.Item2.Should().BeLessThanOrEqualTo(antenna01.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_DecreasingAngularCoeficientRighSideMiddle()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 4);
            Tuple<int, int> antenna02 = new Tuple<int, int>(4, 1);
            double angularCoeficient01 = 10;
            double angularCoeficient02 = 50;


            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeGreaterThan(antenna02.Item1);
            result.Item2.Should().BeGreaterThan(antenna01.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_DecreasingAngularCoeficientRightSideHigh()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 4);
            Tuple<int, int> antenna02 = new Tuple<int, int>(4, 1);
            double angularCoeficient01 = -80;
            double angularCoeficient02 = -50;


            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeLessThanOrEqualTo(antenna01.Item1);
            result.Item2.Should().BeGreaterThanOrEqualTo(antenna01.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_DecreasingAngularCoeficientRightSideLow()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 4);
            Tuple<int, int> antenna02 = new Tuple<int, int>(4, 1);
            double angularCoeficient01 = -30;
            double angularCoeficient02 = -10;


            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeGreaterThan(antenna02.Item1);
            result.Item2.Should().BeLessThanOrEqualTo(antenna02.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_DecreasingAngularCoeficientLeftSideMiddle()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 4);
            Tuple<int, int> antenna02 = new Tuple<int, int>(4, 1);
            double angularCoeficient01 = 80;
            double angularCoeficient02 = 20;


            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeLessThan(antenna02.Item1);
            result.Item2.Should().BeLessThan(antenna01.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_DecreasingAngularCoeficientLeftSideHigh()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 4);
            Tuple<int, int> antenna02 = new Tuple<int, int>(4, 1);
            double angularCoeficient01 = -10;
            double angularCoeficient02 = -30;


            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeLessThanOrEqualTo(antenna01.Item1);
            result.Item2.Should().BeGreaterThanOrEqualTo(antenna01.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_DecreasingAngularCoeficientLeftSideLow()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 4);
            Tuple<int, int> antenna02 = new Tuple<int, int>(4, 1);
            double angularCoeficient01 = -50;
            double angularCoeficient02 = -70;


            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeGreaterThan(antenna02.Item1);
            result.Item2.Should().BeLessThanOrEqualTo(antenna02.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_ZeroAngularCoeficientUpSideMiddle()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1,2);
            Tuple<int, int> antenna02 = new Tuple<int, int>(3,2);
            double angularCoeficient01 = 45;
            double angularCoeficient02 = -45;


            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeGreaterThan(antenna01.Item1);
            result.Item1.Should().BeLessThan(antenna02.Item1);
            result.Item2.Should().BeGreaterThan(antenna01.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_ZeroAngularCoeficientUpSideLeft()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 2);
            Tuple<int, int> antenna02 = new Tuple<int, int>(3, 2);
            double angularCoeficient01 = -45;
            double angularCoeficient02 = -10;


            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeLessThan(antenna01.Item1);
            result.Item2.Should().BeGreaterThan(antenna01.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_ZeroAngularCoeficientUpSideRight()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 2);
            Tuple<int, int> antenna02 = new Tuple<int, int>(3, 2);
            double angularCoeficient01 = 10;
            double angularCoeficient02 = 45;


            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeGreaterThan(antenna02.Item1);
            result.Item2.Should().BeGreaterThan(antenna01.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_ZeroAngularCoeficientDownSideMiddle()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 2);
            Tuple<int, int> antenna02 = new Tuple<int, int>(3, 2);
            double angularCoeficient01 = -45;
            double angularCoeficient02 = 45;


            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeGreaterThan(antenna01.Item1);
            result.Item1.Should().BeLessThan(antenna02.Item1);
            result.Item2.Should().BeLessThan(antenna01.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_ZeroAngularCoeficientDownSideLeft()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 2);
            Tuple<int, int> antenna02 = new Tuple<int, int>(3, 2);
            double angularCoeficient01 = 45;
            double angularCoeficient02 = 10;


            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeLessThan(antenna01.Item1);
            result.Item2.Should().BeLessThanOrEqualTo(antenna01.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_ZeroAngularCoeficientDownSideRight()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 2);
            Tuple<int, int> antenna02 = new Tuple<int, int>(3, 2);
            double angularCoeficient01 = -10;
            double angularCoeficient02 = -45;


            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeGreaterThan(antenna02.Item1);
            result.Item2.Should().BeLessThanOrEqualTo(antenna01.Item2);

        }

        [Fact]
        public void calculatePositionOfIntersectionPoint_returnCorrectPoint_ZeroAngularCoeficientMidle()
        {

            Tuple<int, int> antenna01 = new Tuple<int, int>(1, 2);
            Tuple<int, int> antenna02 = new Tuple<int, int>(3, 2);
            double angularCoeficient01 = 0;
            double angularCoeficient02 = 180;


            var result = CalculatePosition.calculatePositionOfIntersectionPoint(antenna01, antenna02, angularCoeficient01, angularCoeficient02);

            result.Item1.Should().BeGreaterThan(antenna02.Item1);
            result.Item2.Should().BeLessThanOrEqualTo(antenna01.Item2);

        }

    }
}
