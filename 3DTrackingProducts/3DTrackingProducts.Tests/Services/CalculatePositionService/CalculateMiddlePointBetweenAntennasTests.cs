using _3DTrackingProducts.Api.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DTrackingProductsTests.Services.CalculatePositionService
{
    public class CalculateMiddlePointBetweenAntennasTests
    {
        [Fact]
        public void calculateMiddlePointBetweenAntennas_HorizontalLine()
        {
            Tuple<int, int> pointA = new Tuple<int, int>(1, 1);
            Tuple<int, int> pointB = new Tuple<int, int>(4, 1);

            var result = CalculatePosition.calculateMiddlePointBetweenAntennas(pointA, pointB);

            result.Item1.Should().BeGreaterThan(pointA.Item1).And.BeLessThan(pointB.Item1);
            result.Item2.Should().Be(pointA.Item1);
        }

        [Fact]
        public void calculateMiddlePointBetweenAntennas_IncreasingLine()
        {
            Tuple<int, int> pointA = new Tuple<int, int>(1, 1);
            Tuple<int, int> pointB = new Tuple<int, int>(4, 4);

            var result = CalculatePosition.calculateMiddlePointBetweenAntennas(pointA, pointB);

            result.Item1.Should().BeGreaterThan(pointA.Item1).And.BeLessThan(pointB.Item1);
            result.Item2.Should().BeGreaterThan(pointA.Item2).And.BeLessThan(pointB.Item2);
        }

        [Fact]
        public void calculateMiddlePointBetweenAntennas_DecreasingLine()
        {
            Tuple<int, int> pointA = new Tuple<int, int>(1, 4);
            Tuple<int, int> pointB = new Tuple<int, int>(4, 1);

            var result = CalculatePosition.calculateMiddlePointBetweenAntennas(pointA, pointB);

            result.Item1.Should().BeGreaterThan(pointA.Item1).And.BeLessThan(pointB.Item1);
            result.Item2.Should().BeLessThan(pointA.Item2).And.BeGreaterThan(pointB.Item2);
        }

        [Fact]
        public void calculateMiddlePointBetweenAntennas_VerticalLine()
        {
            Tuple<int, int> pointA = new Tuple<int, int>(1, 4);
            Tuple<int, int> pointB = new Tuple<int, int>(1, 1);

            var result = CalculatePosition.calculateMiddlePointBetweenAntennas(pointA, pointB);

            result.Item1.Should().Be(pointA.Item1);
            result.Item2.Should().BeLessThan(pointA.Item2).And.BeGreaterThan(pointB.Item2);
        }
    }
}
