using FluentAssertions;
using Newtonsoft.Json;
using Statistics;
using System.IO;
using Xunit;
namespace StatisticsTest
{
    public class UnitTest1
    {


        [Fact]        
        public void TestDescriptiveStatistics()
        {
                // Act
                var result = Statistics.Statistics.DescriptiveStatistics();

                // Assert
                Assert.NotNull(result);
                Assert.Contains("Maximum:", result); // Check if "Maximum:" is present in the result 
                Assert.Contains("Minimum:", result); // Check if "Minimum:" is present in the result 
                Assert.Contains("Medelvärde:", result); // Check if "Medelvärde:" is present in the result
                Assert.Contains("Median:", result); // Check if "Median:" is present in the result 
                Assert.Contains("Typvärde:", result); // Check if "Typvärde:" is present in the result 
                Assert.Contains("Variationsbredd:", result); // Check if "Variationsbredd:" is present in the result string
                Assert.Contains("Standardavvikelse:", result); // Check if "Standardavvikelse:" is present in the result 

        }

        [Fact]
        public void TestMode()
        {
            var result = Statistics.Statistics.Mode();

            result.Should().NotBeNull(); // Check if the mode is not null
            result.Should().NotBeEmpty(); // Check if the mode array is not empty
            result.Should().ContainItemsAssignableTo<int>(); // Check if all elements in the mode array are integers
            result.Should().ContainInOrder(228, 87, 31); // Check if the mode array contains specified elements in order
           

        }
    }


    
}