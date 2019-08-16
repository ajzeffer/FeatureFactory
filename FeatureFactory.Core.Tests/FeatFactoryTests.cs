using FeatureFactory.Core.Services;
using NUnit.Framework;

namespace FeatureFactory.Core.Tests
{
    public class Tests
    {

        [Test]
            public void FeatFactory_ShouldBeV1()
            {
                var feat = FeatFactory.GetFeatureImplementation(Constants.FeatFlag);
                Assert.IsTrue(feat.GetType() == typeof(FeatureV1));
            }
        
        [Test]
        public void FeatFactory_ShouldBeV2()
        {
            var feat = FeatFactory.GetFeatureImplementation("");
            Assert.IsTrue(feat.GetType() == typeof(FeatureV2));
        }
        
        [Test]
        public void FuncFactory_ShouldBeV1String()
        {
            var feat = new NewFeature().Process("");
            Assert.IsTrue(feat == "here is the cool, and this is the 1");
        }
        
        [Test]
        public void FuncFactory_ShouldBeV2()
        {
            var feat = new NewFeature().Process(Constants.FeatFlag);
            Assert.IsTrue(feat == "here is the 1, and this is the cool");

        }
    }
}