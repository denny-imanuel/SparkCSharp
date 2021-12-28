using System;
using Microsoft.Spark.Sql;

namespace SparkCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // create spark session
            SparkSession spark = SparkSession
                .Builder()
                .AppName("SparkCSharp")
                .GetOrCreate();
            
            // create initial dataframe
            DataFrame dataFrame = spark.Read().Text("input.txt");
            
            // count words
            DataFrame words = dataFrame
                .Select(Functions.Split(Functions.Col("value"), " ").Alias("words"))
                .Select(Functions.Explode(Functions.Col("words")).Alias("word"))
                .GroupBy("word")
                .Count()
                .OrderBy(Functions.Col("count").Desc());
            words.Show();
            spark.Stop();
        }
    }
}
