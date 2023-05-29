# StudioPlusAPI
## TexturePlus (REQUIRES PlusAPI)
### SetBodyTextures() (But custom!)
This extension method allows you to input BodyTextures with the help of an array!
```cs
public static void SetBodyTextures(this PersonBehaviour person, Texture2D[] textures, float scale = 1f, int offset = 0)
```
Before you use it though, there are specific conditions that you have to keep in mind:
1. There must be at least 3 textures in the array.<br/>
    For any textures that you don't want to use, you can enter null to compensate, like this:
    ```cs
    public Texture2D[] myBrokenTextures = new Texture2D
    {
        ModAPI.LoadTexture("skin layer.png"),
        ModAPI.LoadTexture("flesh layer.png")
    };

    public Texture2D[] myTextures = new Texture2D
    {
        ModAPI.LoadTexture("skin layer.png"),
        ModAPI.LoadTexture("flesh layer.png"),
        null
    };
    var person = Instance.GetComponent<PersonBehaviour>();
    /*
    person.SetBodyTextures(myBrokenTextures);
    //SetBodyTexturesArray: Too few body textures in array!
    */   
    //This won't throw you any errors
    person.SetBodyTextures(myTextures);
    ```
1. The amount of textures in the array must be a multiple of 3. If it isn't, the mod will throw you an error.<br/> 
    You can also compensate with null entries here
1. The offset parameter describes the offset for texture groups. Texture groups are 3 consecutive textures, starting at the 1st item in the array.<br/> 
    In the following example:
    ```cs
    public Texture2D[] myTextures = new Texture2D
    {
        ModAPI.LoadTexture("skin layer 1.png"),
        null,
        null,
        ModAPI.LoadTexture("skin layer 2.png"),
        ModAPI.LoadTexture("flesh layer 2.png"),
        null,
        ModAPI.LoadTexture("skin layer 3.png"),
        ModAPI.LoadTexture("flesh layer 3.png"),
        ModAPI.LoadTexture("bone layer 3.png")
    };
    var person = Instance.GetComponent<PersonBehaviour>();
    person.SetBodyTextures(myTextures, 1f, 2);
    ```
    The textures won't be set to **null**, **skin layer 2** and **flesh layer 2** but instead will be set to **skin layer 3**, **flesh layer 3**, **bone layer 3**.
    Keep this in mind to not cause any OutOfBounds exceptions.
1. The offset parameter cannot be less than 0, this will also throw an error.