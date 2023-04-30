# StudioPlusAPI
## PlusAPI
### IgnoreCollision()
A short-hand way to write PPG's method for ignoring collision because I won't be typing IgnoreCollisionStackController.IgnoreCollisionSubstituteMethod() every time I want to do something with collisions
```cs
public static void IgnoreCollision(Collider2D main, Collider2D other, bool ignColl)
{
    IgnoreCollisionStackController.IgnoreCollisionSubstituteMethod(main, other, ignColl);
}
```

### IgnoreEntityCollision()
Allows you to disable collision with multiple colliders (usually entities).
```cs
public static void IgnoreEntityCollision(Collider2D main, Collider2D[] others, bool ignColl, bool affectItself = false)
```
Tbh Idr exactly how I got my hands on this, I think I stole it from PPG source code. Idk what affectItself does, I am too small brain to figure out what the complex if statement is about. Here's simply an example from ArmorBehaviour:
```cs
PlusAPI.IgnoreEntityCollision(GetComponent<Collider2D>(), limbColliders, true);
```
