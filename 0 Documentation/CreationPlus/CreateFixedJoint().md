# StudioPlusAPI
## CreationPlus
### CreateFixedJoint()
Creates a fixed joint between two objects. Contains 2 overloads.
```cs
public static void CreateFixedJoint(GameObject main, GameObject other)

public static void CreateFixedJoint(GameObject main, GameObject other, Vector2 position)
```
In other words, it creates a rigid connection between 2 objects.
```cs
CreationPlus.CreateFixedJoint(gameObject, myObject);
```
If for any reason you have to change the position of said joint, you can use the overload:
```cs
CreationPlus.CreateFixedJoint(gameObject, myObject, new Vector2(0f, 3f) * ModAPI.PixelSize);
```