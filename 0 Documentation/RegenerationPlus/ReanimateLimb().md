# StudioPlusAPI
## RegenerationPlus (PlusAPI HIGHLY recommended)
### ReanimateLimb()
A method that reanimates a given limb.
```cs
public static void ReanimateLimb(this LimbBehaviour myLimb)
```
It revives the limb and makes it fully functional again. It does things like healing bones, healing bleeding etc., but doesn't do nothing more than that. It doesn't make the entity conscious and doesn't regenerate anything.

### ReviveLimb()
Does almost the same thing as RegenerateLimb() but also removes pain, makes the entity conscious again and gives them max adrenaline
```cs
public static void ReviveLimb(this LimbBehaviour myLimb)
```

I'm sorry that this one isn't as sophisticated as everything else but there just isn't much to say about this one.