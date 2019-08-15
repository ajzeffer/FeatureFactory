using System;
using FeatureFactory.Core;

namespace FeatureFactory.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
           var result = FeatFactory
                .GetFeatureFactory(Constants.FeatFlag)
                .DoStuff();

           Console.WriteLine(result);
        }
    }
}