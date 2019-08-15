# Feature Flagging with a Factory Approach

When using a factory pattern in combination with feature flagging it gives you the ability to easily delete code once the new feature has taken over as well as reduce the amount of functional logic you change when you go back through and clean up. 

## Single Logical Flow >  IOC 
```
var result = FeatFactory
                .GetFeatureImplementation(Constants.FeatFlag)
                .DoStuff();
```

## Factory Method
```

        public static IFeature GetFeatureImplementation(string featFlag)
        {
            if (featFlag == Constants.FeatFlag)
            {
                return new FeatureV1();
            }
            return new FeatureV2(); 
        }
```


## New Implementation 
```
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
```

In this example it would be OK to duplicate code in FeatureV1 & Feature V2, because the goal would be to leave FeatureV1 exactly how it is in productio ntoday, and FeatureV2 would have our changes. Once we decide to go full on to V2 we could just delete the FeatureV1.cs file, and then remove the reference in the Factory method. 

## Testing 

This also gives us the ability test our factory method and ensure that the output is what we expect. 

```
     public void FeatFactory_ShouldBeV1()
        {
            var feat = FeatFactory.GetFeatureImplementation(Constants.FeatFlag);
            Assert.IsTrue(feat.GetType() == typeof(FeatureV1));
        }
        
```

This approach allows us to make the minimum amount of logical changes to the flow of our application when transitioning between implementations. Especially when we have feature flags in place and need to remove old implementations at some point in the future. 