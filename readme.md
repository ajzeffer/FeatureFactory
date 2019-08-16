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


# Don't want / have an interface 
If you don't want to use an interface, there is also the option to do it at the method level using what I will call a FuncFactory ... it looks something like this 

```
        public static T FuncFactory<T>(FeatureFunctions<T> features, bool flagOn)
        {
            return !flagOn ? features.FeatOld : features.FeatNew;
           
        }
```

This uses a FeatureFunction wrapper class that allows us to easily delete old code without having to really chang the logical flow of the application 

```
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

```
As you can see the FeatFunctions.Create() method either takes in two params or a single param. If you pass in two params it knows that there is an "old" and a "new" method, if you pass in only one it will set both method instances to the same value. Allowing us to go back and _cleanup_ the code by just removing a single referrence to the old function. 

This is what it would look like when we have two methods being toggled on and off
```
            var features = FeatureFunctions<Func<string, int, string>>
                            .Create(new NewFeature().ProcessSomeData,
                                    new NewFeature().ProcessSomeDataV2);

            return FeatFactory.FuncFactory(features,Constants.FeatFlag == flag)(val, num);

```

and when we go back and clean up we can just remove the `ProcessSomeData` reference 

```
            var features = FeatureFunctions<Func<string, int, string>>
                            .Create(new NewFeature().ProcessSomeDataV2);

            return FeatFactory.FuncFactory(features,Constants.FeatFlag == flag)(val, num);

```

I think this approach would make sense if you had an existing method being called, that wasn't an implementation of an interfaces members. You could create a new instance of that method and then have a fluid api to choose which method version gets used at runtime.

It would really replace something like this ... 

```
if(Constants.FeatFlag == "mycoolFlag"){
    return new NewFeature().ProcessSomeData("foo",1);
}
return new NewFeature().ProcessSomeDataV2("foo",1);

```

Something similar could be accomplished by doing 
```
           result = (Constants.FeatFlag == "mycoolFlag"
               ? (Func<string,int, string>) new NewFeature().ProcessSomeData
               : new NewFeature().ProcessSomeDataV2)
               ("foo", 1);
```
If you didn't have to do the casting for the compiler's sake I feel like this would be the best option, but I think having a common interface of FuncFactory<T> helps remove the confusion of why the cast is only on 1 of the methods, and keeps developers from abandoning this approach because the compiler yells at them when they try the obvious seemingly obvious implementation. 