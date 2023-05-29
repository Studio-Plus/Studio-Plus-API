# StudioPlusAPI
## CreationPlus (REQUIRES PlusAPI)
### CreateFixedJoint()
Creates a fixed joint between two objects. Contains 2 overloads.
```cs
public static void CreateFixedJoint(this GameObject main, GameObject other)

public static void CreateFixedJoint(this GameObject main, GameObject other, Vector2 position)
```
In other words, it creates a rigid connection between 2 objects.
```cs
gameObject.CreateFixedJoint(myObject);
```
If for any reason you have to change the position of said joint, you can use the overload:
```cs
gameObject.CreateFixedJoint(myObject, new Vector2(0f, 3f) * ModAPI.PixelSize);
```