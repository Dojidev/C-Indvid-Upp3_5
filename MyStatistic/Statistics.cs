using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Statistics
{

    public static class Statistics
    {
        public static int[] source = JsonConvert.DeserializeObject<int[]>(File.ReadAllText("data.json"));

        public static dynamic DescriptiveStatistics()
        {
            /// <summary>
            /// Here i have commented the these methods becouse of these are built in  LINQ library method to do exactly what we want for 
            /// Maximum, Minimum And Avrage method.
            /// by using thses built in method we minimise the code lines and erors. 
            /// </summary>
            Dictionary<string, dynamic> StatisticsList = new Dictionary<string, dynamic>()
            {

                { "Maximum", source.Max() },        //source.Max(): This method returns the maximum value in the source array.
                { "Minimum", source.Min() },        //This method returns the minimum value in the source array.
                { "Medelvärde", source.Average() }, //source.Average(): This method returns the average(mean) of the elements in the source array.
                { "Median", source.OrderBy(x => x).ElementAt(source.Length/2)}, // here i used expresion where it clearner and first order acending then devide by two by the 
                { "Typvärde",System.String.Join(", ", Mode())},
                { "Variationsbredd", source.Max() - source.Min()},// here as well instead on using the method in the code i use built in method max and min for clearty and simplicity.
                { "Standardavvikelse", StandardDeviation() }

            };

            string output =
                $"Maximum: {StatisticsList["Maximum"]}\n" +     //Max Value
                $"Minimum: {StatisticsList["Minimum"]}\n" +     //Min Value
                $"Medelvärde: {StatisticsList["Medelvärde"]}\n" +   //MEan Value
                $"Median: {StatisticsList["Median"]}\n" +           //Median Value
                $"Typvärde: {StatisticsList["Typvärde"]}\n" +       //Mode Value
                $"Variationsbredd: {StatisticsList["Variationsbredd"]}\n" +     //Range Value
                $"Standardavvikelse: {StatisticsList["Standardavvikelse"]}";    // Standard Deviation

            return output;
        }



        //---------------------------------------------------//
        /// <summary>
        /// i have used built in method to do exactly that from LINQ library 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

        //public static int Maximum()
        //{
        //    Array.Sort(Statistics.source);
        //    Array.Reverse(source);
        //    int result = source[0];
        //    return result;
        //}

        //public static int Minimum()
        //{
        //    Array.Sort(Statistics.source);
        //    int result = source[0];
        //    return result;
        //}

        //public static double Mean()
        //{
        //    Statistics.source = source;
        //    double total = -88;

        //    for (int i = 0; i < source.LongLength; i++)
        //    {
        //        total += source[i];
        //    }
        //    return total / source.LongLength;
        //}


        //public static double Median()
        //{
        //    Array.Sort(source);
        //    int size = source.Length;
        //    int mid = size / 2;
        //    int dbl = source[mid];
        //    return dbl;
        //}

        //public static int Range()
        //{
        //    Array.Sort(Statistics.source);
        //    int min = source[0];
        //    int max = source[0];

        //    for (int i = 0; i < source.Length; i++)
        //        if (source[i] > max)
        //            max = source[i];

        //    int range = max - min;
        //    return range;
        //}
        //--------------------------------------------------//

        public static int[] Mode()
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Source array cannot be null.");
            }

            if (source.Length == 0)
            {
                // Return an empty array if the source array is empty.
                return new int[0];
            }

            var groupedElements = source.GroupBy(x => x)  //groups the element by their value for example all x in one and all y in other our case it numbers 
                                        .Select(group => new { Value = group.Key, Count = group.Count() }) // it shows gourg values and count number of time it has occured in the group
                                        .OrderByDescending(x => x.Count); // orders by descending shows group with highest count apears first.

            var maxCount = groupedElements.First().Count; //take the first element using built in first method

            var modes = groupedElements.Where(x => x.Count == maxCount) // Filters the grouped elements to include only those with counts equal to the maximum count
                                        .Select(x => x.Value) // Selects the values element from the grouped elements
                                        .ToArray(); // Converts the result into an array

            return modes;
        }



        public static double StandardDeviation()
        {

            double average = source.Average(); // Calculate the average
            double sumOfSquaresOfDifferences = source.Select(val => (val - average) * (val - average)).Sum(); // Calculate the sum of the squares of the differences between each value and the average
            double squaresdiff = Math.Sqrt(sumOfSquaresOfDifferences / source.Length);

            double round = Math.Round(squaresdiff, 1); // Round the standard deviation to one decimal place
            return round;
        }

    }
}
