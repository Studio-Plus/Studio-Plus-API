# StudioPlusAPI
## TexturePlus
### ChangeLightColor()
Allows for ~~lazy~~ easy changing of your light sprite color. Contains x overloads.
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

### ConvertToGlowColor()
I might as well mention this one in here. All this method actually does is change the alpha of whatever color you input to 1f (255), i.e. makes it opaque.<br/>
Used mainly for the other light sprite methods because doing this manually is a pain.
```cs
public static Color ConvertToGlowColor(Color color)
{
    return new Color(color.r, color.g, color.b, 1f);
}
```