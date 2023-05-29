# StudioPlusAPI
## TexturePlus (REQUIRES PlusAPI)
### CreateLightSprite()
This method allows for creation of complex light sprites. Contains 2 overloads.
```cs
public static void CreateLightSprite(out GameObject lightObject, Transform parentObject, Sprite sprite, Vector2 position, Color color, bool activate = true)

public static void CreateLightSprite(out GameObject lightObject, Transform parentObject, Sprite sprite, Vector2 position, Color color, out LightSprite glow, float radius = 5f, float brightness = 1.5f, bool activate = true)
```
The difference between this and **ModAPI.CreateLight()** is that this method primarily focuses on creating a light __sprite__ instead of just a light:
```cs
public class ExampleClass : MonoBehaviour
{
    [SkipSerialisation]
    public GameObject light;

    public void Start()
    {
        TexturePlus.CreateLightSprite(
            out light,
            Instance.transform,
            ModAPI.LoadSprite("Textures/ExampleSprite.png"),
            Vector2.zero,
            new Color(1f, 0f, 0f, 0.5f)
        );
    }
}     
```
When added to an object this will create a red glowing sprite (based on ExampleSprite.png) at half brightness.   

Note that a simple Light Sprite does not glow in the dark, it only glows when there is light. If you wish to have a sprite that glows in the dark, use the following overload:
```cs
public class ExampleClass : MonoBehaviour
{
    [SkipSerialisation]
    public GameObject light;
    [SkipSerialisation]
    public LightSprite glow;

    public void Start()
    {
        TexturePlus.CreateLightSprite(
            out light,
            Instance.transform,
            ModAPI.LoadSprite("Textures/ExampleSprite.png"),
            Vector2.zero,
            new Color32(255, 0, 0, 127), //Alternative to Color struct, will also work here
            out glow,
            5f,
            0.75f
        );
    }
}      
```
When added to an object this will create a red glowing sprite (based on ExampleSprite.png) at half brightness and a glow-in-the-dark light at default radius (5f) and half default brightness (Default is 1.5f) in the middle of the light sprite.<br/>  
This overload is besically a merging of both TexturePlus.CreateLightSprite() and ModAPI.CreateLight()

Note that for both cases, you have to write 'out' in front of the light and glow variable or else C# will be mad at you.

There is also an optional 'activate' argument for both overloads. Set to true by default. When set to false, light will be turned off when created. It's mostly there in case the light's activation is dependant on either the script being enabled or some boolean being either true or false. Here are 2 examples:
```cs
public class ExampleClass : MonoBehaviour
{
    [SkipSerialisation]
    public GameObject light;
    [SkipSerialisation]
    public LightSprite glow;

    public void Start()
    {
        TexturePlus.CreateLightSprite(
            out light,
            Instance.transform,
            ModAPI.LoadSprite("Textures/ExampleSprite.png"),
            Vector2.zero,
            new Color32(255, 0, 0, 127), //Alternative to Color struct, will also work here
            out glow,
            5f,
            0.75f,
            enabled //If the script is disabled when this is run, light will be off by default
        );
    }
}

public class MachineExampleClass : MonoBehaviour
{
    [SkipSerialisation]
    public GameObject light;
    [SkipSerialisation]
    public LightSprite glow;

    public bool activated = false;

    public void Start()
    {
        TexturePlus.CreateLightSprite(
            out light,
            Instance.transform,
            ModAPI.LoadSprite("Textures/ExampleSprite.png"),
            Vector2.zero,
            new Color32(255, 0, 0, 127), //Alternative to Color struct, will also work here
            out glow,
            5f,
            0.75f,
            activated //Assuming the script changes the value of activated, the method will either enable or disable the light accordingly
        );
    }
}     
```

