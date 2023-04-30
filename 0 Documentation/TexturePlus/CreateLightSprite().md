# StudioPlusAPI
## TexturePlus
### CreateLightSprite()
This method allows for creation of complex kught sprites Contains 2 overloads.
```cs
public static void CreateLightSprite(GameObject lightObject, Transform parentObject, Sprite sprite, Vector2 position, Color color)

public static void CreateLightSprite(GameObject lightObject, Transform parentObject, Sprite sprite, Vector2 position, Color color, LightSprite glow, float radius = 5f, float brightness = 1.5f)
```
The difference between this and **ModAPI.CreateLight()** is that this method primarily focuses on creating a light __sprite__ instead of just a light:
```cs
ExampleClass : MonoBehaviour
{
    public GameObject light;

    public void Start()
    {
        TexturePlus.CreateLightSprite(
            light = new GameObject("Light"),
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
ExampleClass : MonoBehaviour
{
    public GameObject light;
    public LightSprite glow;

    public void Start()
    {
        TexturePlus.CreateLightSprite(
            light = new GameObject("Light"),
            Instance.transform,
            ModAPI.LoadSprite("Textures/ExampleSprite.png"),
            Vector2.zero,
            new Color32(255, 0, 0, 127), //Alternative to Color class, will also work here
            glow = TexturePlus.InstantiateLight(light.transform), //must be transform of your light GameObject
            5f,
            0.75f
        );
    }
}      
```
When added to an object this will create a red glowing sprite (based on ExampleSprite.png) at half brightness and a glow-in-the-dark light at default radius (5f) and half default brightness (Default is 1.5f) in the middle of the light sprite.<br/>  
This overload is besically a merging of both TexturePlus.CreateLightSprite() and ModAPI.CreateLight()

### InstantiateLight()
It's basically a partial copy of ModAPI.CreateLight() that makes adding glow more straightforward. I'm just gonna paste the entirety of it here:
```cs
public static LightSprite InstantiateLight(Transform parent)
{
    var component = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/ModLightPrefab"), parent).GetComponent<LightSprite>();
    return component;
}
```