using System;
using FeatureFactory.Core;

namespace FeatureFactory.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
           var result = FeatFactory
                .GetFeatureImplementation(Constants.FeatFlag)
                .DoStuff();

           Console.WriteLine(result);
        }
    }
}