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


        /// <summary>
        /// Takes FeatureFunction wrapper which has the new and old method we are wanting to switch on
        /// as well as boolean value on whether to use the new feature or not
        /// </summary>
        /// <param name="features"></param>
        /// <param name="flagOn"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FuncFactory<T>(FeatureFunctions<T> features, bool flagOn)
        {
            return !flagOn ? features.FeatOld : features.FeatNew;
           
        }
    }
}