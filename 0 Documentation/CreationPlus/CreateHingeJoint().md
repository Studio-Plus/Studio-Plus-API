# StudioPlusAPI
## CreationPlus (REQUIRES PlusAPI)
### CreateHingeJoint()
Creates a hinge joint between two objects.
```cs
public static void CreateHingeJoint(this GameObject main, GameObject other, Vector2 position, float minDeg, float maxDeg)
```
I don't think this needs further explanation, only an example:
```cs
gameObject.CreateHingeJoint(myObject, new Vector2(0f, -4.5f) * ModAPI.PixelSize, -45f, 45f);
```
I did not provide an overload that allows you to not need to input Vector2 because tbh why would you need to, but if you _really_ have to:
```cs
gameObject.CreateHingeJoint(myObject, Vector2.zero, -45f, 45f);
```