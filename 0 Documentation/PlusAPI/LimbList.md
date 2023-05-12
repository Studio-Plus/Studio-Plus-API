# StudioPlusAPI
## PlusAPI
### LimbList
LimbList is technically its own struct but branded under PlusAPI because the only reason it is its own struct is to save on words when writing stuff from it out.
#### Recommended:
Add "using static StudioPlusAPI.LimbList" to the beginning of your code, like this:
```cs
using UnityEngine;
using System;
//Whatever else
using StudioPlusAPI;
using static StudioPlusAPI.LimbList;
```
It is not really used in the API itself for simplicity's sake, but if you're annoyed of typing 'LimbList' in front of a lot of things, this will make it so you don't have to add it because C# will know that it's meant to be there.<br/>
You can also do this to other structs.

#### Limb List
LimbList contains a list of every single Limb transform ever:
```cs
public const string head = "Head";

public const string upperBody = "Body/UpperBody";
public const string middleBody = "Body/MiddleBody";
public const string lowerBody = "Body/LowerBody";

public const string upperArmFront = "FrontArm/UpperArmFront";
public const string lowerArmFront = "FrontArm/LowerArmFront";

public const string upperArmBack = "BackArm/UpperArm";
public const string lowerArmBack = "BackArm/LowerArm";

public const string upperLegFront = "FrontLeg/UpperLegFront";
public const string lowerLegFront = "FrontLeg/LowerLegFront";
public const string footFront = "FrontLeg/FootFront";

public const string upperLegBack =  "BackLeg/UpperLeg";
public const string lowerLegBack = "BackLeg/LowerLeg";
public const string footBack = "BackLeg/Foot";

public const string upperArm = upperArmBack;
public const string lowerArm = upperArmBack;

public const string upperLeg = upperLegBack;
public const string lowerLeg = lowerLegBack;
public const string foot = footBack;
```
They are in general named the same way as the transform you're looking for except anything in BackArm and BackFoot, although there is also an alternative name for each one.<br/>
Here's an example:
```cs
var lowerArmFront = Instance.transform.Find(LimbList.lowerArmFront);
```
Notice how when this variable is named the same way the limb in this list is, copy-pasting this line for different limbs becomes a very easy Job.

#### LimbList.FindLimb()
Speaking of Find, this struct also contains a method that returns the child transform by giving in 2 parameters (Contains 2 overloads):
```cs
public static Transform FindLimb(Transform transform, string limbType)

public static GameObject FindLimb(GameObject gameObject, string limbType)
```

Here's the example from above but done with this method:
```cs
var lowerArmFront = LimbList.FindLimb(Instance.transform, LimbList.lowerArmFront);
```

Wihle this is longer than what we started with, if you use the recommended line at the beginning of the entry, it would look more like this:
```cs
//at the beginning of your file
//using static StudioPlusAPI.LimbList;

var lowerArmFront = FindLimb(Instance.transform, lowerArmFront);
```

As you can see, this is now actually shorter than what we started with, but we could in theory make it even shorter:
```cs
//at the beginning of your file
//using static StudioPlusAPI.LimbList;

var lowerArmFront = FindLimb(Instance, lowerArmFront);
```

Instance is nothing other than a GameObject, so you don't actually have to get its transform in order for it to work.<br/>
The most significant difference however is that this will return a GameObject instead. Use whatever will make the code shorter, here is a short cheatsheet:
```cs
//at the beginning of your file
//using static StudioPlusAPI.LimbList;
//In general, use the version below in a block (Note that depending on what you're doing this may not apply)

FindLimb(Instance.transform, lowerArmFront).gameObject.AddComponent<MyComponent>();
FindLimb(Instance, lowerArmFront).AddComponent<MyComponent>();

FindLimb(Instance.transform, lowerArmFront).GetComponent<MyComponent>();
FindLimb(Instance, lowerArmFront).GetComponent<MyComponent>();

FindLimb(gameObject, lowerArmFront).GetComponent<MyComponent>();
FindLimb(transform, lowerArmFront).GetComponent<MyComponent>();
```

In addition, the method always first goes to the root of the transform before attempting to find the transform/gameObject, so the following 2 expressions function the same:
```cs
var lowerArmFront = LimbList.FindLimb(limb.transform.root, LimbList.lowerArmFront).GetComponent<MyComponent>();
var lowerArmFront = LimbList.FindLimb(limb.transform, LimbList.lowerArmFront).GetComponent<MyComponent>();
```
#### LimbList.FindLimbBeh()
Often will the search for a limb transform also require you to Get its LimbBehaviour Component. Luckily, we got a method that makes this process shorter (Contains 2 overloads):
```cs
public static LimbBehaviour FindLimb(Transform transform, string limbType)

public static LimbBehaviour FindLimb(GameObject gameObject, string limbType)
```
By adding 3 letters, you can eliminate the need of typing GetComponent for the LimbBehaviour

#### LimbList.FindLimbComp()
Similar in spirit to FindLimbBeh but generalized to any component on the limb (Contains 2 overloads):
```cs
public static T FindLimbComp<T>(Transform transform, string limbType) where T : MonoBehaviour

public static T FindLimbComp<T>(GameObject gameObject, string limbType) where T : MonoBehaviour
```
By adding 4 letters this time, we can  yet  again spare us the time of writing GetComponent after finding the limb transform
```cs
var lowerArmFront = LimbList.FindLimb(limb.transform, LimbList.lowerArmFront).GetComponent<MyComponent>();
var lowerArmFront = LimbList.FindLimbComp<MyComponent>(limb.transform, LimbList.lowerArmFront);
```
Remember that this and the previous methods only work for **getting** a component, not for adding it. We won't be simplifying adding components here.