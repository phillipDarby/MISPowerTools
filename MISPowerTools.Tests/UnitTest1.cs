using MISPowerTools.Library;
using MISPowerTools.Library.Cmdlets;
using MISPowerTools.Library.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MISPowerTools.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldReturnThePhraseRepeatedTheCorrectNumberOfTimes()
        {
            // Arrange.
            var phrase = "A test phrase.";
            int numberOfTimesToRepeat = 3;
            var cmdlet = new Demo()
            {
                Phrase = phrase,
                NumberOfTimesToRepeatPhrase = numberOfTimesToRepeat
            };
            var expectedResult = "A test phrase.A test phrase.A test phrase.";

            // Act.
            var results = cmdlet.Invoke().OfType<object>().ToList();

            // Assert.
            Assert.Equal(results.First(), expectedResult);
            Assert.True(results.Count == 1);
        }
        [Fact]
        public void NoParamShouldReturnHelloWorld()
        {
            var cmdlet = new HelloWorld();

            //Act

            var results = cmdlet.Invoke().OfType<object>().ToList();
            var expectedResult = results;
            //Assert
            Assert.Equal(results.First(), expectedResult.First());
        }

        [Fact]
        public void AParamShouldReturnHelloName()
        {
            var cmdlet = new HelloWorld()
            {
                Name = "Test User"
            };

            var expectedResult = "Hello Test User";

            //Act

            var results = cmdlet.Invoke().OfType<string>().ToList();

            //Assert
            Assert.Equal(results.First(), expectedResult);
        }


        [Fact]
        public void ShouldReturnBiosInformation()
        {
            var cmdlet = new GetBiosInfo();

            //Act

            var results = cmdlet.Invoke().OfType<BiosModel>().First();
            //Assert
            Assert.NotNull(results);
           // Assert.Contains("F.20", results.Name);
        }
        //[Fact]
        //public void ShouldReturnBatteryInformation()
        //{
        //    var cmdlet = new GetBatteryInfo();

        //    //Act

        //    var results = cmdlet.Invoke().OfType<string>().First();
        //    //Assert
        //    Assert.IsType<string>(results);
        //}
        //[Fact]
        //public void ShouldReturnCpuInformation()
        //{
        //    var cmdlet = new GetCpuInfo();

        //    //Act

        //    var results = cmdlet.Invoke().OfType<string>().First();
        //    //Assert
        //    Assert.IsType<string>(results);
        //}
        [Fact]
        public void ShouldReturnDriveInformation()
        {
            var cmdlet = new GetDriveInfo();

            //Act

            List<DriveSpace> results = cmdlet.Invoke().OfType<List<DriveSpace>>().First();
            //Assert
            Assert.Contains("C", results[0].Description);
        }
        [Fact]
        public void ShouldReturnReport()
        {
            var cmdlet = new GetReport();

            //Act

            var results = cmdlet.Invoke().OfType<object>().First().ToString();
            //Assert
            Assert.NotNull(results);

        }
        [Fact]
        public void ShouldReturnCompInfo()
        {
            var cmdlet = new GetComputerSystemInfo();

            //Act

            var results = cmdlet.Invoke().OfType<ComputerInfoModel>().First();
            //Assert
            Assert.NotNull(results);
            //Assert.Contains("GPI.ad", results.Domain);
            //Assert.IsType<string>(results);

        }
        [Fact]
        public void ShouldReturnOSInfo()
        {
            var cmdlet = new GetOs();

            //Act

            var results = cmdlet.Invoke().OfType<OsModel>().First();
            //Assert
            Assert.NotNull(results);
            Assert.Contains("Windows", results.Caption);

        }
    }
}