# StudioPlusAPI
## TexturePlus
### ReplaceItemSprite()
Part of the 'Advanced texture pack system', allows for replacing the view sprite.
```cs
public static void ReplaceViewSprite(string item, Sprite replaceSprite)
```
This one is straight forward and doesn't contain any overloads.
```cs
TexturePlus.ReplaceItemSpriteOfChild("Sword", ModAPI.LoadSprite("Upscaled Sword View.png"));
```