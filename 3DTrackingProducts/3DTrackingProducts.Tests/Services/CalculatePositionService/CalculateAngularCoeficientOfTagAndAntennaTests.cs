using _3DTrackingProducts.Api.Models.Exceptions;
using _3DTrackingProducts.Api.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DTrackingProductsTests.Services.CalculatePositionService
{
    public class CalculateAngularCoeficientOfTagAndAntennaTests
    {

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_throwsVerticalLineException_WhenAngularCoeficientVerticalLineAndAngle0()
        {
            double angularCoeficient = double.MinValue;
            double angle = 0;
            bool verticalLineExceptionThrowned = false;

            try
            {
                CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeTrue();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_throwsVerticalLineException_WhenAngularCoeficientVerticalLineAndAngle180()
        {
            double angularCoeficient = double.MinValue;
            double angle = 180;
            bool verticalLineExceptionThrowned = false;

            try
            {
                CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeTrue();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returnsPositiveAngle_WhenAngularCoeficientVerticalLineAndAngleMoreThanNinety()
        {
            double angularCoeficient = double.MinValue;
            double angle = 91;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
                result.Should().BeGreaterThan(0);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returnsNegativeAngle_WhenAngularCoeficientVerticalLineAndAngleLessThanNinety()
        {
            double angularCoeficient = double.MinValue;
            double angle = 30;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
                result.Should().BeLessThan(0);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returnsZero_WhenAngularCoeficientVerticalLineAndAngleisNinety()
        {
            double angularCoeficient = double.MinValue;
            double angle = 90;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
                result.Should().Be(0);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returnsZero_WhenAngularCoeficientHorizontalLineAndAngleisZero()
        {
            double angularCoeficient = 0;
            double angle = 0;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
                result.Should().Be(0);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returnsZero_WhenAngularCoeficientHorizontalLineAndAngleis180()
        {
            double angularCoeficient = 0;
            double angle = 180;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
                result.Should().Be(180);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_throwsVerticalLineException_WhenAngularCoeficientHorizontalLineAndAngleisNinety()
        {
            double angularCoeficient = 0;
            double angle = 90;
            bool verticalLineExceptionThrowned = false;

            try
            {
                CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeTrue();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returnsAngle_WhenAngularCoeficientHorizontalLineAndAngleisLessThan90()
        {
            double angularCoeficient = 0;
            double angle = 10;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
                result.Should().Be(angle);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returnsNegativeAngle_WhenAngularCoeficientHorizontalLineAndAngleisMoreThan90()
        {
            double angularCoeficient = 0;
            double angle = 150;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
                result.Should().BeLessThan(0);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returnsAngle_WhenAngularCoeficientBiggerThanZeroAndAngleIs180()
        {
            double angularCoeficient = Math.Tan(15 * (Math.PI / 180));
            double angle = 180;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
                result.Should().Be(angle);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_throwsVerticalLineException_WhenAngularCoeficientPlusAngleEquals90()
        {
            double angularCoeficient = Math.Tan(15 * (Math.PI / 180));
            double angle = 75;
            bool verticalLineExceptionThrowned = false;

            try
            {
                CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeTrue();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returnsNegativeAngle_WhenAngularCoeficientPlusAngleLessThan180()
        {
            double angularCoeficient = Math.Tan(15 * (Math.PI / 180));
            double angle = 65;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
                result.Should().BeLessThan(0);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returns0_WhenAngularCoeficientPlusAngleEquals180()
        {
            double angularCoeficient = Math.Tan(15 * (Math.PI / 180));
            double angle = 165;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
                result.Should().Be(0);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returnsPositiveAngle_WhenAngularCoeficientPlusAngleMoreThan180()
        {
            double angularCoeficient = Math.Tan(30 * (Math.PI / 180));
            double angle = 165;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
                result.Should().BeGreaterThan(0);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returns0_WhenAngularCoeficientLessThan0ButEqualToAngle()
        {
            double angularCoeficient = Math.Tan(-30 * (Math.PI / 180));
            double angle = 30;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
                result.Should().Be(0);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_throwsVerticalLineException_WhenAngularCoeficientLessThan0AndAngleMinusCoeficientEquals90()
        {
            double angularCoeficient = Math.Tan(-30 * (Math.PI / 180));
            double angle = 120;
            bool verticalLineExceptionThrowned = false;

            try
            {
                CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeTrue();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returnsPositiveAngle_WhenAngularCoeficientLessThan0AndAngleBiggerThanCoeficient()
        {
            double angularCoeficient = Math.Tan(-30 * (Math.PI / 180));
            double angle = 119;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);

                result.Should().BeGreaterThan(0);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returnsAngularCoeficient_WhenAngularCoeficientLessThan0AndAngleEquals180()
        {
            double angularCoeficient = Math.Tan(-30 * (Math.PI / 180));
            double aungularCoeficientAngle = Math.Round(Math.Atan(angularCoeficient) * (180 / Math.PI));
            double angle = 180;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);

                result.Should().Be(aungularCoeficientAngle);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }

        [Fact]
        public void calculateAngularCoeficientOfTagAndAntenna_returnsNegativeAngle_WhenAngularCoeficientLessThan0AndAngleLessThanCoeficient()
        {
            double angularCoeficient = Math.Tan(-30 * (Math.PI / 180));
            double angle = 29;
            bool verticalLineExceptionThrowned = false;

            try
            {
                var result = CalculatePosition.calculateAngularCoeficientOfTagAndAntenna(angularCoeficient, angle, 1);

                result.Should().BeLessThan(0);
            }
            catch (VerticalLineException e)
            {
                verticalLineExceptionThrowned = true;
            }

            verticalLineExceptionThrowned.Should().BeFalse();
        }
    }
}
