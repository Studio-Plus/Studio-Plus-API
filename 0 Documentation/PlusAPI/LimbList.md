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
You can also do this to other structs and static classes.

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
Speaking of Find, this struct also contains a method that makes finding limbs easier (Contains 3 overloads):
```cs
public static LimbBehaviour FindLimb(this PersonBehaviour person, string limbType)

public static LimbBehaviour FindLimb(this LimbBehaviour limb, string limbType)

public static LimbBehaviour FindLimb(this CirculationBehaviour circ, string limbType)
```
While it is reliant on a reference to PersonBehaviour and returns LimbBehaviour, it's still useful for a lot of cases. Here is an example
```cs
PersonBehaviour person = Instance.GetComponent<PersonBehaviour>();
person.FindLimb(LimbList.lowerArmFront);
```
If you only got a limb reference, you can simply use the 2nd overload
```cs
LimbBehaviour limb;

public void Start()
{
    limb = GetComponent<LimbBehaviour>();
    var lowerArmFront = limb.FindLimb(LimbList.lowerArmFront);
}
```
If for some strange reason you only got a CirculationBehaviour reference, overload 3 is your friend:
```cs
public override void OnUpdate(BloodContainer container)
{
    if (container is CirculationBehaviour circ)
    {
        var lowerArmFront = circ.FindLimb(LimbList.lowerArmFront);
    }
}
```

This is way easier to follow than what we started with and has the bonus of returning LimbBehaviour which will often save on code, because for 75% of the cases you're getting the transform of a limb to get to LimbBehaviour, very rarely will you need an implicit transform reference.
#### LimbList.FindLimbComp()
To follow up on the previous statement, for 24% of the cases you're trying to get another custom script from the limb. This method covers that 24% with the same 3 overload options:
```cs
public static T FindLimbComp<T>(this PersonBehaviour person, string limbType) where T : MonoBehaviour

public static T FindLimbComp<T>(this LimbBehaviour limb, string limbType) where T : MonoBehaviour

public static T FindLimbComp<T>(this CirculationBehaviour circ, string limbType) where T : MonoBehaviour
```
In fact, FindLimb() is a mere shorter way of writing all of these for LimbBehaviour. For all the cases above you can also write
```cs
person.FindLimbComp<LimbBehaviour>(LimbList.lowerArmFront);

var lowerArmFront = limb.FindLimbComp<LimbBehaviour>(LimbList.lowerArmFront);

var lowerArmFront = circ.FindLimbComp<LimbBehaviour>(LimbList.lowerArmFront);
```
Or you can replace LimbBehaviour with the script you're looking for.