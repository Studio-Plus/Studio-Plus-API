# StudioPlusAPI
## CreationPlus (REQUIRES PlusAPI)
### SpawnItem()
Finally going into detail with this one.<br/>
Allows you to spawn another item.
```cs
public static GameObject SpawnItem(this SpawnableAsset item, Transform transform, Vector3 position = default, bool spawnSpawnParticles = false)
```
The way it works is that it spawns the item in, rotated to align with the specified transform and at the position of said item moved accordingly as defined in the position parameter.<br/>
Here are some examples:<br/>
Example 1:
```cs
GameObject newObject = ModAPI.FindSpawnable("Crossbow Bolt").SpawnItem(transform);
```
This is the most straight forward: Spawns in a Crossbow bolt at the center of your item rotated accordingly.

Example 2:
```cs
GameObject newObject = ModAPI.FindSpawnable("Crossbow Bolt").SpawnItem(transform, new Vector2(5f, 0f) * ModAPI.PixelSize);
```
Same thing happens like in example 1 but the crossbow bolt is moved over by 5 pixels to the right relative to how the item through which it's spawned is rotated and flipped (In other words if you spawn it with q it'll be 5 pixels to the left instead).

### SpawnItemAsChild()
Finally going into detail with this one too.<br/>
Allows you to spawn another item as a child of another item.
```cs
public static GameObject SpawnItemAsChild(this SpawnableAsset item, Transform parent, Vector3 position = default, bool spawnSpawnParticles = false)
```
Basically the same as the regular method but more straight forward because it actually makes the spawned item to the child of the parent transform.
```cs
GameObject newObject = ModAPI.FindSpawnable("Crossbow Bolt").SpawnItemAsChild(transform);
```

### SpawnItemStatic()
The so far only big obsolete method:
```cs
[Obsolete]
public static GameObject SpawnItemStatic(this SpawnableAsset item, Vector2 position = default, bool spawnSpawnParticles = false)
```
It's the old SpawnItem method that spawns the item at a fixed point perfectly rotated towards the plane. I'm not sure if it's useful or not cuz I wrote this last-minute change at 11 PM so I left it hidden in the code.