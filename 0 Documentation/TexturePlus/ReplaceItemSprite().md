# StudioPlusAPI
## TexturePlus (REQUIRES PlusAPI)
### ReplaceItemSprite()
Part of the 'Advanced texture pack system', allows for replacing sprites under more or less any circumstance. Contains 5 overloads.
```cs
public static void ReplaceItemSprite(this SpawnableAsset item, Sprite replaceTexture)

public static void ReplaceItemSprite(this SpawnableAsset item, string childObject, Sprite childReplaceTexture)

public static void ReplaceItemSprite(this SpawnableAsset item, Sprite replaceTexture, string childObject, Sprite childReplaceTexture)

public static void ReplaceItemSprite(this SpawnableAsset item, string[] childObjects, Sprite[] childReplaceSprites)

public static void ReplaceItemSprite(this SpawnableAsset item, Sprite replaceSprite, string[] childObjects, Sprite[] childReplaceSprites)
```
The most basic version allows for replacing the sprite of regular items lke swords:
```cs
ModAPI.FindSpawnable("Sword").ReplaceItemSprite(ModAPI.LoadSprite("Upscaled Sword.png"));
```
For items like the axe, if you want to specifically replace the sprite of the axe head, you'll have to do this:
```cs
ModAPI.FindSpawnable("Axe").ReplaceItemSpriteOfChild("Axe handle/Axe head", ModAPI.LoadSprite("Futuristic Axe Head.png"));
```
You can get the path for childObject from [here (PPG modding wiki)](https://www.studiominus.nl/ppg-modding/gameAssets.html).<br/>
Keep in mind that this won't work for changing the sprite of your modded axe head (probably). This is a texture pack system specifically made for vanilla items and it's not guaranteed it will work for modded items within your mod (It's another story for texture packs for other mods).<br/>
To change the sprite of your axe head for your custom item, you'll have to do this:
```cs
ModAPI.Register(
    new Modification()
    {
        OriginalItem = ModAPI.FindSpawnable("Axe"),
        NameOverride = "Futuristic Axe [FUTURE]",
        DescriptionOverride = "An axe from the future!",
        CategoryOverride = ModAPI.FindCategory("Malee"),
        ThumbnailOverride = ModAPI.LoadSprite("Textures/Views/View.png"),
        AfterSpawn = (Instance) =>
        {
            Instance.transform.Find("Axe Handle/Axe Head").GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Malee/Future Axe Head.png");
        }
    }
);
```

In case there is a sprite on both the child and root of the item that you want to change, use the 3rd overload:
```cs
ModAPI.FindSpawnable("Example Item").ReplaceItemSpriteOfChild(
    ModAPI.LoadSprite("Sprite.png"), 
    "Child 1", 
    ModAPI.LoadSprite("Sprite 1.png")
);
```
For advanced users, if you want to replace multiple sprites found in children of the root item, use 4th overload:
```cs
ModAPI.FindSpawnable("Example Item").ReplaceItemSprite(
    new string[]
    {
        "Child 1",
        "Child 2"
    }, 
    new Sprite[]
    {
        "Textures/Sprite 1.png",
        "Textures/Sprite 2.png"
    },
);
```
And there is of course a version of this for the case a sprite is in the root:
```cs
ModAPI.FindSpawnable("Example Item").ReplaceItemSprite(
    ModAPI.LoadSprite("Sprite.png");
    new string[]
    {
        "Child 1",
        "Child 2"
    }, 
    new Sprite[]
    {
        ModAPI.LoadSprite("Textures/Sprite 1.png"),
        ModAPI.LoadSprite("Textures/Sprite 2.png")
    },
);
```
It's very important that you use an **equal** amount of children and sprites in the arrays in overload 4 and 5, otherwise the API is going to immediately throw an error