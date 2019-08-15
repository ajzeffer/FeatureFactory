using FeatureFactory.Core.Contracts;

namespace FeatureFactory.Core.Services
{
    public class FeatureV2 : IFeature
    {
        public string DoStuff()
        {
            // Duplicate Code is OK since we are going to blow away the 
            // old version 
            
            var val = "My Cool Feat 2"; 
            
            // Do Stuff 
            

            return val; 
        }
    }
}