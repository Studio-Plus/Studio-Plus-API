# StudioPlusAPI
## TexturePlus
### ChangeLightColor()
Allows for ~~lazy~~ easy changing of your light sprite color. Contains 2 overloads.
```cs
public static void ChangeLightColor(GameObject lightObject, LightSprite glow, Color newColor)

public static void ChangeLightColor(GameObject lightObject, Color newColor)
```
And all you need to do is to reference those variables you defined earlier in CreateLightSprite()!
```cs
TexturePlus.ChangeLightColor(
    light,
    glow,
    new Color32(255, 255, 0, 127)
    //new Color(1f, 1f, 0, 0.5f)
);
```
In this case it changes the color from red to yellow. (Why tf is this even in here it's literally 2 lines of code...)

The other overload allows you to opt-out of changing glow:
```cs
TexturePlus.ChangeLightColor(
    light,
    new Color32(255, 255, 0, 127)
    //new Color(1f, 1f, 0, 0.5f)
);
```
...<br/>
Okay now this is just straight-up ridiculous, who even came up with this? Oh.

### ChangeAlpha()
Might as well mention it here. This method changes the alpha of a given color. Contains 2 overloads.
```cs
public static Color ChangeAlpha(Color color, float alpha = 1f)

public static Color32 ChangeAlpha(Color32 color, byte alpha = 255)
```
By default it changes the alpha to the maximum (Either 1f or 255), but you can set it to whatever you want.