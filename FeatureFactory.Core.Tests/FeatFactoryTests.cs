using FeatureFactory.Core.Services;
using NUnit.Framework;

namespace FeatureFactory.Core.Tests
{
    public class Tests
    {

        [Test]
        public void FeatFactory_ShouldBeV1()
        {
            var feat = FeatFactory.GetFeatureFactory(Constants.FeatFlag);
            Assert.IsTrue(feat.GetType() == typeof(FeatureV1));
        }
        
        [Test]
        public void FeatFactory_ShouldBeV2()
        {
            var feat = FeatFactory.GetFeatureFactory("");
            Assert.IsTrue(feat.GetType() == typeof(FeatureV2));
        }
    }
}