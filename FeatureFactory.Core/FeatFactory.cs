using System;
using System.Security.Cryptography.X509Certificates;
using FeatureFactory.Core.Contracts;
using FeatureFactory.Core.Services;

namespace FeatureFactory.Core
{
    public static class FeatFactory
    {
        public static IFeature GetFeatureImplementation(string featFlag)
        {
            if (featFlag == Constants.FeatFlag)
            {
                return new FeatureV1();
            }
            return new FeatureV2(); 
        }
    }
}