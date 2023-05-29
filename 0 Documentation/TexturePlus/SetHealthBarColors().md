# StudioPlusAPI
## TexturePlus (REQUIRES PlusAPI)
### SetHealthBarColors()
This method allows you to change the health bar color of any entity.
```cs
public static void SetHealthBarColors(this PersonBehaviour person, Color color)
```
There isn't exactly much to say here.
```cs
PersonBehaviour person = Instance.GetComponent<PersonBehaviour>();
person.SetHealthBarColors(new Color32(200, 0, 255, 255));
```
There is also a method for resetting the health bar color back to normal:
```cs
public static void ResetHealthBarColors(this PersonBehaviour person)
```
```cs
person.ResetHealthBarColors();
//Default is new Color32(55, 255, 0, 255)
```
### SetHealthBarColor()
Almost like SetHealthBarColors(), but for individual limbs. There is also a ResetHealthBarColor() method.
```cs
public static void SetHealthBarColor(this LimbBehaviour limb, Color color)

public static void ResetHealthBarColor(this LimbBehaviour limb)
```
I don't think an example is in order. 