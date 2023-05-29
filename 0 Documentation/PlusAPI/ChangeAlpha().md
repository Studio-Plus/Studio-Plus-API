# StudioPlusAPI
## PlusAPI
### ChangeAlpha()
This method changes the alpha of a given value. Contains 2 overloads.
```cs
public static Color ChangeAlpha(this Color color, float alpha)

public static Color32 ChangeAlpha(this Color32 color, byte alpha)
```
For the first overload, alpha is clamped between 0f and 1f.<br/>
It's the case for many methods here but I think this is the best one to mention it: This is a extension method so you can do something like this:
```cs
public Color32 exampleColor;

exampleColor.ChangeAlpha(127);
```