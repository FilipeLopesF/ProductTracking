using _3DTrackingProducts.Api.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DTrackingProductsTests.Services.CalculatePositionService
{
    public class CalculateAngularCoeficientBetweenAntennasTests
    {

        [Fact]
        public void calculateAngularCoeficientBetweenAntennas_returns0_WhenLineIsHorizontal()
        {
            Tuple<int, int> pointA = new Tuple<int, int>(1, 1);
            Tuple<int, int> pointB = new Tuple<int, int>(4, 1);

            var result = CalculatePosition.calculateAngularCoeficientBetweenAntennas(pointA, pointB);

            result.Should().Be(0);
        }

        [Fact]
        public void calculateAngularCoeficientBetweenAntennas_returnsDoubleMinValue_WhenLineIsVertical()
        {
            Tuple<int, int> pointA = new Tuple<int, int>(4, 3);
            Tuple<int, int> pointB = new Tuple<int, int>(4, 1);

            var result = CalculatePosition.calculateAngularCoeficientBetweenAntennas(pointA, pointB);

            result.Should().Be(double.MinValue);
        }

        [Fact]
        public void calculateAngularCoeficientBetweenAntennas_returnsNotZero_WhenLineIsDescending()
        {
            Tuple<int, int> pointA = new Tuple<int, int>(0, 2);
            Tuple<int, int> pointB = new Tuple<int, int>(5, 0);

            var result = CalculatePosition.calculateAngularCoeficientBetweenAntennas(pointA, pointB);

            result.Should().BeLessThan(0);
        }

        [Fact]
        public void calculateAngularCoeficientBetweenAntennas_returnsLessThanZero_WhenLineIsDescending()
        {
            Tuple<int, int> pointA = new Tuple<int, int>(1, 3);
            Tuple<int, int> pointB = new Tuple<int, int>(3, 1);

            var result = CalculatePosition.calculateAngularCoeficientBetweenAntennas(pointA, pointB);

            result.Should().BeLessThan(0);
        }

        [Fact]
        public void calculateAngularCoeficientBetweenAntennas_returnsLessThanZero_WhenLineIsDescendingSwitchedPoints()
        {
            Tuple<int, int> pointA = new Tuple<int, int>(3, 1);
            Tuple<int, int> pointB = new Tuple<int, int>(1, 3);


            var result = CalculatePosition.calculateAngularCoeficientBetweenAntennas(pointA, pointB);

            result.Should().BeLessThan(0);
        }

        [Fact]
        public void calculateAngularCoeficientBetweenAntennas_returnsMoreThanZero_WhenLineIsIncreasing()
        {
            Tuple<int, int> pointA = new Tuple<int, int>(1, 1);
            Tuple<int, int> pointB = new Tuple<int, int>(3, 3);

            var result = CalculatePosition.calculateAngularCoeficientBetweenAntennas(pointA, pointB);

            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public void calculateAngularCoeficientBetweenAntennas_returnsMoreThanZero_WhenLineIsIncreasingSwitchedPoints()
        {
            Tuple<int, int> pointA = new Tuple<int, int>(3, 3);
            Tuple<int, int> pointB = new Tuple<int, int>(1, 1);

            var result = CalculatePosition.calculateAngularCoeficientBetweenAntennas(pointA, pointB);

            result.Should().BeGreaterThan(0);
        }
    }
}
