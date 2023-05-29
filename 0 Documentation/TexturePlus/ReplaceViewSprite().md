# StudioPlusAPI
## TexturePlus (REQUIRES PlusAPI)
### ReplaceViewSprite()
Part of the 'Advanced texture pack system', allows for replacing the view sprite.
```cs
public static void ReplaceViewSprite(this SpawnableAsset item, Sprite replaceSprite)
```
This one is straight forward and doesn't contain any overloads.
```cs
ModAPI.FindSpawnable("Sword").ReplaceViewSprite(ModAPI.LoadSprite("Upscaled Sword View.png"));
```