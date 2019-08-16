using System;
using FeatureFactory.Core;
using FeatureFactory.Core.Services;

namespace FeatureFactory.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // factory pattern using Interface 
           var result = FeatFactory
                .GetFeatureImplementation(Constants.FeatFlag)
                .DoStuff();

           Console.WriteLine(result);
           
           
           // factory using Func<>
           var features = FeatureFunctions<Func<string, int, string>>
               .Create(new NewFeature().ProcessSomeData,
                   new NewFeature().ProcessSomeDataV2);
           
           result =  FeatFactory.FuncFactory(features,Constants.FeatFlag == "mycoolFlag")("foo", 1);

           Console.WriteLine(result);
        }



    }
}