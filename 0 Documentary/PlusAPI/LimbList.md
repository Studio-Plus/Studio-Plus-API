# StudioPlusAPI
## PlusAPI
### LimbList
LimbList is technically its own struct but branded under PlusAPI because the only reason it is its own struct is to save on words when writing stuff from it out.
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

public static Transform FindLimb(GameObject gameObject, string limbType)
```
Here's the example from above but done with this method:
```cs
var lowerArmFront = LimbList.FindLimb(Instance.transform, LimbList.lowerArmFront);
```
Wihle this is longer than what we started with, the overload actually allows us to make this shorter:
```cs
var lowerArmFront = LimbList.FindLimb(Instance, LimbList.lowerArmFront);
```
Now it is just a bit longer with the benefit of being more straight forward in this example