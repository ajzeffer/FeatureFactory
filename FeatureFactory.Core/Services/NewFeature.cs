using System;
using System.Dynamic;
using System.Runtime.InteropServices;

namespace FeatureFactory.Core.Services
{
    public class NewFeature
    {

        public string Process(string flag)
        {
            // using delegate 
            var val = "cool";
            var num = 1;


            var features = FeatureFunctions<Func<string, int, string>>
                            .Create(new NewFeature().ProcessSomeData,
                                    new NewFeature().ProcessSomeDataV2);

            return FeatFactory.FuncFactory(features,Constants.FeatFlag == flag)(val, num);
            
        }
        public string ProcessSomeData(string val, int num)
        {
            return $"here is the {val}, and this is the {num}";
        }
        
        public string ProcessSomeDataV2(string val, int num)
        {
            return $"here is the {num}, and this is the {val}";
        }
        
        
    }

    public  class FeatureFunctions<T>
    {
        public   T FeatOld { get;  }
        
        public   T FeatNew { get;  }

         private FeatureFunctions(T featOld, T featNew)
        {
            FeatNew = featNew;
            FeatOld = featOld;
        }

         private FeatureFunctions(T featCurrent)
        {
            FeatNew = featCurrent;
            FeatOld = featCurrent;
        }

         public static FeatureFunctions<T> Create(T featOld, T featNew)
         {
             return new FeatureFunctions<T>(featOld, featNew);
             
         }
         public static FeatureFunctions<T> Create(T featCurrent)
         {
             return new FeatureFunctions<T>(featCurrent);
             
         }
    }
}