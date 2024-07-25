using _3DTrackingProducts.Api.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DTrackingProductsTests.Services.CalculatePositionService
{
    public class GetAngleFromLogsTests
    {
        [Fact]
        public void GetAngleFromLogs_returnsZero_WhenNoLogsFound()
        {
            List<Log> logs = new List<Log>();

            var result = CalculatePosition.GetAngleFromLogs(logs);

            result.Should().Be(0);
        }

        [Fact]
        public void GetAngleFromLogs_returnsAngle_WhenOnlyOneLogFound()
        {
            double angle = 90;
            List<Log> logs = new List<Log>();
            logs.Add(new Log
            {
                Angle = angle,
                RSSI = 38
            });


            var result = CalculatePosition.GetAngleFromLogs(logs);

            result.Should().Be(angle);
        }

        [Fact]
        public void GetAngleFromLogs_returnsAverageAngle_WhenMoreThanOneLogFound()
        {
            double angle1 = 90;
            double angle2 = 70;
            List<Log> logs = new List<Log>();
            logs.Add(new Log
            {
                Angle = angle1,
                RSSI = 38
            });
            logs.Add(new Log
            {
                Angle = angle2,
                RSSI = 40
            });

            var result = CalculatePosition.GetAngleFromLogs(logs);

            double expectedResoult = Math.Round((angle1 + angle2) / 2, 2);

            result.Should().Be(expectedResoult);
        }
    }
}
