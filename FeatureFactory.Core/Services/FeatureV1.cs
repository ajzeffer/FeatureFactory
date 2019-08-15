using FeatureFactory.Core.Contracts;

namespace FeatureFactory.Core.Services
{
    // DELETE THIS FILE WHEN
    // myCoolFeat is removed
    public class FeatureV1 : IFeature
    {
        public string DoStuff()
        {
            // Duplicate Code is OK since we are going to blow away the 
            // old version 
            
            var val = "My Cool Feat 1"; 
            
            // Do Stuff 

           return val; 
        }
    }
}