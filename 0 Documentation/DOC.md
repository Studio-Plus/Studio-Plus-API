# StudioPlusAPI DOCUMENTATION (v3.2.1)
## ChemistryPlus
### AddLiquidToItem() 
This method allows to add a liquid to an already existing item. Contains 2 overloads.<br/>
```cs
public static void AddLiquidToItem(string item, string newLiquidID, float amount)

public static void AddLiquidToItem(string item, string newLiquidID, float amount, float capacity)
```
By default capacity is adjusted to the amount of liquid you add:
```cs
ChemistryPlus.AddLiquidToItem("Rotor", Oil.ID, 1.4f);

//ChemistryPlus.AddLiquidToItem("Rotor", "OIL", 1.4f);
```
While using simple strings for the IDs of liquids is possible, using the ID stored in the actual class is recommended as it will be more resistant to breaking with updates to the game.<br/>
For your convinience, a list of all liquid IDs will be provided in this folder.

If you so wish, you can provide a capacity for the container (capacity must be larger than amount):
```cs
ChemistryPlus.AddLiquidToItem("Rotor", Oil.ID, 1.4f, 2.8f);
```
This is basically useless, I know.

### LiquidReaction() 
This method allows to add a liquid to an already existing item. Contains 3 overloads.<br/>
```cs
public static void LiquidReaction(string liquid1, string liquid2, string target, float ratePerSecond = 0.05f)

public static void LiquidReaction(string liquid1, string liquid2, string liquid3, string target, float ratePerSecond = 0.05f)

public static void LiquidReaction(Liquid[] ingredientLiquids, Liquid target, float ratePerSecond = 0.05f)
```
The most basic version is for mixing 2 liquids into one:
```cs
ChemistryPlus.LiquidReaction(LifeSyringe.LifeSerumLiquid.ID, Chemistry.Tritium.ID, DeathSyringe.InstantDeathPoisonLiquid.ID);
```
Again, when working with liquids it's better to use a reference to the ID string rather than a plain string (More on that in AddLiquidToItem().md)

You can also mix 3 liquids into one:
```cs
ChemistryPlus.LiquidReaction(Sugar.ID, Spice.ID, EverythingNice.ID, ThePerfectLittleGirl.ID);
```
For advanced users, you can use an array to mix as many liquids into one as you want:
```cs
ChemistryPlus.LiquidReaction(
    new Liquid[]
    {
        Liquid.GetLiquid(Sugar.ID),
        Liquid.GetLiquid(Spice.ID),
        Liquid.GetLiquid(EverythingNice.ID),
        Liquid.GetLiquid(Chemistry.Tritium.ID)
    }, 
    Liquid.GetLiquid(PowerpuffGirls.ID)
)
//If you're wondering, no, I'm not a Powerpuff Girls fan, it's just a fun example.
```
It is not possible to mix e.g. 4 liquids into 2 with 1 API method, although it shouldn't be hard to implement something like this.

### Liquid ID List (for v1.26.6)
When working with specific liquids in general, you should use a reference to the liquid class ID string instead of a plain string to avoid zooi's changes to any names causing your mods to break<br/>
This will be a list of every single liquid ID in people playground. This will also serve as a list of every Liquid class in the game.
<!-- How did they get access to our MLC list? -->
#### Regular liquids
1. Blood.ID
1. GorseBlood.ID
1. Nitroglycerine.ID
1. Oil.ID
1. AcidSyringe.AcidLiquid.ID
1. AdrenalineSyringe.AdrenalineLiquid.ID
1. BoneEatingPoisonSyringe.BoneHurtingJuiceLiquid.ID
1. CoagulationSyringe.CoagulationLiquid.ID
1. DeathSyringe.InstantDeathPoisonLiquid.ID
1. FreezeSyringe.FreezePoisonLiquid.ID
1. KnockoutSyringe.KnockoutPoisonLiquid.ID
1. LifeSyringe.LifeSerumLiquid.ID
1. *Here would be Pink syringe if not for it being 1000 liquids*
1. ZombieSyringe.ZombiePoisonLiquid.ID
1. UltraStrengthSyringe.UltraStrengthSerumLiquid.ID
1. MendingSyringe.MendingSerum.ID
1. Chemistry.Tritium.ID
1. WaterBreathingSyringe.WaterBreathingSerum.ID
1. Chemistry.ExoticLiquid.ID
1. ImmortalitySerum.ID
1. OsteomorphosisAgent.ID
1. Chemistry.BeverageM04.ID
1. PainKillerSyringe.PainKillerLiquid.ID

<!-- What are our proceedings Mr. Johnson? -->
#### Pink Liquids
1. PinkSyringe.PinkDormantLiquid.ID
1. PinkSyringe.VestibularPoison.ID
1. PinkSyringe.MusclePoison.ID
1. PinkSyringe.NumbingPoison.ID
1. PinkSyringe.ReflectionPoison.ID
1. PinkSyringe.TransparencyPoison.ID
1. PinkSyringe.CrushingPoison.ID
1. PinkSyringe.MassManipulationPoison.ID
1. PinkSyringe.ExplosionPoison.ID
1. PinkSyringe.DurabilitySerum.ID
1. PinkSyringe.RegenerationSerum.ID
1. PinkSyringe.SizeManipulationPoison.ID
1. PinkSyringe.CirculationPoison.ID
1. PinkSyringe.CombustionAgent.ID
1. PinkSyringe.TissueDeconstructionAgent.ID
1. PinkSyringe.MuscleEnhancementSerum.ID

<!-- Oh whatever, the public knows about all of those anyway -->
#### Inaccessible liquids
- Gasoline.ID
- Chemistry.DebugDiscolorationLiquid.ID
- Chemistry.InertLiquid.ID


## TexturePlus
### CreateLightSprite()
This method allows for creation of complex light sprites. Contains 2 overloads.
```cs
public static void CreateLightSprite(out GameObject lightObject, Transform parentObject, Sprite sprite, Vector2 position, Color color)

public static void CreateLightSprite(out GameObject lightObject, Transform parentObject, Sprite sprite, Vector2 position, Color color, out LightSprite glow, float radius = 5f, float brightness = 1.5f)
```
The difference between this and **ModAPI.CreateLight()** is that this method primarily focuses on creating a light __sprite__ instead of just a light:
```cs
ExampleClass : MonoBehaviour
{
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
ExampleClass : MonoBehaviour
{
    public GameObject light;
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

### ReplaceItemSprite()
Part of the 'Advanced texture pack system', allows for replacing sprites under more or less any circumstance. Contains 5 overloads.
```cs
public static void ReplaceItemSprite(string item, Sprite replaceTexture)

public static void ReplaceItemSprite(string item, string childObject, Sprite childReplaceTexture)

public static void ReplaceItemSprite(string item, Sprite replaceTexture, string childObject, Sprite childReplaceTexture)

public static void ReplaceItemSprite(string item, string[] childObjects, Sprite[] childReplaceSprites)

public static void ReplaceItemSprite(string item, Sprite replaceSprite, string[] childObjects, Sprite[] childReplaceSprites)
```
The most basic version allows for replacing the sprite of regular items lke swords:
```cs
TexturePlus.ReplaceItemSprite("Sword", ModAPI.LoadSprite("Upscaled Sword.png"));
```
For items like the axe, if you want to specifically replace the sprite of the axe head, you'll have to do this:
```cs
TexturePlus.ReplaceItemSpriteOfChild("Axe","Axe handle/Axe head", ModAPI.LoadSprite("Futuristic Axe Head.png"));
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
TexturePlus.ReplaceItemSpriteOfChild(
    "Example Item", 
    ModAPI.LoadSprite("Sprite.png"), 
    "Child 1", 
    ModAPI.LoadSprite("Sprite 1.png")
);
```
For advanced users, if you want to replace multiple sprites found in children of the root item, use 4th overload:
```cs
TexturePlus.ReplaceItemSprite(
    "Example Item",
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
TexturePlus.ReplaceItemSprite(
    "Example Item",
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
It's very important that you use an **equal** amount of children and sprites in the arrays in overload 4 and 5, otherwise if I programmed it correctly the API is going to immediately terminate the execution of this method.

### ReplaceItemSprite()
Part of the 'Advanced texture pack system', allows for replacing the view sprite.
```cs
public static void ReplaceViewSprite(string item, Sprite replaceSprite)
```
This one is straight forward and doesn't contain any overloads.
```cs
TexturePlus.ReplaceItemSpriteOfChild("Sword", ModAPI.LoadSprite("Upscaled Sword View.png"));
```
### ToFloat()
Changes a byte color (0 to 255) into its corresponding float. Clamped between 0f and 1f:
```cs
public static float ToFloat(byte value)
{
    float newValue = (float)value;
    float returnValue = newValue / 255f;
    return Mathf.Clamp01(returnValue);
}
```

### ToByte()
Inverse operation of ToFloat, also clamped between 0 and 255:
```cs
public static byte ToByte(float value)
{
    float newValue = Mathf.Clamp01(value) * 255f;
    return (byte)newValue;
}
```


## CreationPlus
### SpawnItem()
Finally going into detail with this one.<br/>
Allows you to spawn another item.
```cs
public static GameObject SpawnItem(SpawnableAsset item, Transform parent, Vector3 position = default, bool spawnSpawnParticles = false)
```
The way it works is that it doesn't actually make the object the child of the Transform you put in (typical programmer bad variable naming), but it spawns the item in rotated to align with the said item and at the position of said item unless specified otherwise with the position parameter.<br/>
Here are some examples:<br/>
Example 1:
```cs
var newObject = CreationPlus.SpawnItem(ModAPI.FindSpawnable("Crossbow Bolt"), transform);
```
This is the most straight forward: Spawns in a Crossbow bolt at the center of your item rotated accordingly.

Example 2:
```cs
var newObject = CreationPlus.SpawnItem(ModAPI.FindSpawnable("Crossbow Bolt"), transform, new Vector2(5f, 0f) * ModAPI.PixelSize);
```
Same thing happens like in example 1 but the crossbow bolt is moved over by 5 pixels to the right relative to how the item through which it's spawned is rotated and flipped (In other words if you spawn it with q it'll be 5 pixels to the left instead).

### SpawnItemAsChild()
Finally going into detail with this one too.<br/>
Allows you to spawn another item as a child of another item.
```cs
public static GameObject SpawnItemAsChild(SpawnableAsset item, Transform parent, Vector3 position = default, bool spawnSpawnParticles = false)
```
Basically the same as the regular method but more straight forward because it actually makes the spawned item to the child of the parent transform.
```cs
var newObject = CreationPlus.SpawnItemAsChild(ModAPI.FindSpawnable("Crossbow Bolt"), transform);
```

### SpawnItemStatic()
The so far only big obsolete method:
```cs
[Obsolete]
public static GameObject SpawnItemStatic(SpawnableAsset item, Vector2 position = default, bool spawnSpawnParticles = false)
```
It's the old SpawnItem method that spawns the item at a fixed point perfectly rotated towards the plane. I'm not sure if it's useful or not cuz I wrote this last-minute change at 11 PM so I left it hidden in the code.

### CreateFixedJoint()
Creates a fixed joint between two objects. Contains 2 overloads.
```cs
public static void CreateFixedJoint(GameObject main, GameObject other)

public static void CreateFixedJoint(GameObject main, GameObject other, Vector2 position)
```
In other words, it creates a rigid connection between 2 objects.
```cs
CreationPlus.CreateFixedJoint(gameObject, myObject);
```
If for any reason you have to change the position of said joint, you can use the overload:
```cs
CreationPlus.CreateFixedJoint(gameObject, myObject, new Vector2(0f, 3f) * ModAPI.PixelSize);
```

### CreateHingeJoint()
Creates a hinge joint between two objects.
```cs
public static void CreateHingeJoint(GameObject main, GameObject other, Vector2 position, float minDeg, float maxDeg)
```
I don't think this needs further explanation, only an example:
```cs
CreationPlus.CreateHingeJoint(gameObject, myObject, new Vector2(0f, -4.5f) * ModAPI.PixelSize, -45f, 45f);
```
I did not provide an overload that allows you to not need to input Vector2 because tbh why would you need to, but if you _really_ have to:
```cs
CreationPlus.CreateHingeJoint(gameObject, myObject, Vector2.zero, -45f, 45f);
```

## PlusAPI
### PlusAPI.ton and PlusAPI.kilogram
These are 2 constants for changing TrueInitialMass based on 1000kg Weight if you want some specific values.
```cs
public const float ton = 25f;
public const float kilogram = 0.025f;
//For reference, a metal rod weighs about 3 kilograms
```
Here is an example for how to use it taken from Power Plus [MOD]:
```cs
ModAPI.Register(
    new Modification()
    {
        OriginalItem = ModAPI.FindSpawnable("Rod"),
        NameOverride = "Blaster Glove" + modTag,
        DescriptionOverride = "It's so inconvinient to hold a blaster anyway",
        CategoryOverride = ModAPI.FindCategory("Entities"),
        ThumbnailOverride = ModAPI.LoadSprite("Textures/Views/Blaster glove view.png"),
        AfterSpawn = (Instance) =>
        {
            Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Blaster glove.png");
            Instance.FixColliders();
            var phys = Instance.GetComponent<PhysicalBehaviour>();
            phys.TrueInitialMass = 5f * PlusAPI.kilogram;
            Instance.GetOrAddComponent<BlasterGlove>().CreateCustom<BlasterGloveWearer>(LimbList.lowerArmFront, 0f, 0, 1);

        }
    }
);
```
Keep in mind that mostly you'll probably want to modify TrueInitialMass around the original item you spawned, but giving you possibilites you might never need to use is kind of the theme of this API.

### PlusAPI.liter & friends
Stores the value of a liter. Also stores the value of some important liquid containers.
```cs
public const float liter = 2.8f;
public const float syringe = 0.5f * liter;
public const float flask = liter;
public const float bloodTank = 5f * liter;
```

### IgnoreCollision()
A short-hand way to write PPG's method for ignoring collision because I won't be typing IgnoreCollisionStackController.IgnoreCollisionSubstituteMethod() every time I want to do something with collisions
```cs
public static void IgnoreCollision(Collider2D main, Collider2D other, bool ignColl)
{
    IgnoreCollisionStackController.IgnoreCollisionSubstituteMethod(main, other, ignColl);
}
```

### IgnoreEntityCollision()
Allows you to disable collision with multiple colliders (usually entities).
```cs
public static void IgnoreEntityCollision(Collider2D main, Collider2D[] others, bool ignColl, bool affectItself = false)
```
Tbh Idr exactly how I got my hands on this, I think I stole it from PPG source code. Idk what affectItself does, I am too small brain to figure out what the complex if statement is about. Here's simply an example from ArmorBehaviour:
```cs
PlusAPI.IgnoreEntityCollision(GetComponent<Collider2D>(), limbColliders, true);
```

### LimbList
LimbList is technically its own struct but branded under PlusAPI because the only reason it is its own struct is to save on words when writing stuff from it out.
#### Recommended:
Add "using static StudioPlusAPI.LimbList" to the beginning of your code, like this:
```cs
using UnityEngine;
using System;
//Whatever else
using StudioPlusAPI;
using static StudioPlusAPI.LimbList;
```
It is not really used in the API itself for simplicity's sake, but if you're annoyed of typing 'LimbList' in front of a lot of things, this will make it so you don't have to add it because C# will know that it's meant to be there.<br/>
You can also do this to other structs.

#### Limb List
LimbList contains a list of every single Limb transform ever:
```cs
public const string head = "Head";

public const string upperBody = "Body/UpperBody";
public const string middleBody = "Body/MiddleBody";
public const string lowerBody = "Body/LowerBody";

public const string upperArmFront = "FrontArm/UpperArmFront";
public const string lowerArmFront = "FrontArm/LowerArmFront";

public const string upperArmBack = "BackArm/UpperArm";
public const string lowerArmBack = "BackArm/LowerArm";

public const string upperLegFront = "FrontLeg/UpperLegFront";
public const string lowerLegFront = "FrontLeg/LowerLegFront";
public const string footFront = "FrontLeg/FootFront";

public const string upperLegBack =  "BackLeg/UpperLeg";
public const string lowerLegBack = "BackLeg/LowerLeg";
public const string footBack = "BackLeg/Foot";

public const string upperArm = upperArmBack;
public const string lowerArm = upperArmBack;

public const string upperLeg = upperLegBack;
public const string lowerLeg = lowerLegBack;
public const string foot = footBack;
```
They are in general named the same way as the transform you're looking for except anything in BackArm and BackFoot, although there is also an alternative name for each one.<br/>
Here's an example:
```cs
var lowerArmFront = Instance.transform.Find(LimbList.lowerArmFront);
```
Notice how when this variable is named the same way the limb in this list is, copy-pasting this line for different limbs becomes a very easy Job.

#### LimbList.FindLimb()
Speaking of Find, this struct also contains a method that returns the child transform by giving in 2 parameters (Contains 2 overloads):
```cs
public static Transform FindLimb(Transform transform, string limbType)

public static GameObject FindLimb(GameObject gameObject, string limbType)
```

Here's the example from above but done with this method:
```cs
var lowerArmFront = LimbList.FindLimb(Instance.transform, LimbList.lowerArmFront);
```

Wihle this is longer than what we started with, if you use the recommended line at the beginning of the entry, it would look more like this:
```cs
//at the beginning of your file
//using static StudioPlusAPI.LimbList;

var lowerArmFront = FindLimb(Instance.transform, lowerArmFront);
```

As you can see, this is now actually shorter than what we started with, but we could in theory make it even shorter:
```cs
//at the beginning of your file
//using static StudioPlusAPI.LimbList;

var lowerArmFront = FindLimb(Instance, lowerArmFront);
```

Instance is nothing other than a GameObject, so you don't actually have to get its transform in order for it to work.<br/>
The most significant difference however is that this will return a GameObject instead. Use whatever will make the code shorter, here is a short cheatsheet:
```cs
//at the beginning of your file
//using static StudioPlusAPI.LimbList;
//In general, use the version below in a block (Note that depending on what you're doing this may not apply)

FindLimb(Instance.transform, lowerArmFront).gameObject.AddComponent<MyComponent>();
FindLimb(Instance, lowerArmFront).AddComponent<MyComponent>();

FindLimb(Instance.transform, lowerArmFront).GetComponent<MyComponent>();
FindLimb(Instance, lowerArmFront).GetComponent<MyComponent>();

FindLimb(gameObject, lowerArmFront).GetComponent<MyComponent>();
FindLimb(transform, lowerArmFront).GetComponent<MyComponent>();
```

In addition, the method always first goes to the root of the transform before attempting to find the transform/gameObject, so the following 2 expressions function the same:
```cs
var lowerArmFront = LimbList.FindLimb(limb.transform.root, LimbList.lowerArmFront).GetComponent<MyComponent>();
var lowerArmFront = LimbList.FindLimb(limb.transform, LimbList.lowerArmFront).GetComponent<MyComponent>();
```
#### LimbList.FindLimbBeh()
Often will the search for a limb transform also require you to Get its LimbBehaviour Component. Luckily, we got a method that makes this process shorter (Contains 2 overloads):
```cs
public static LimbBehaviour FindLimb(Transform transform, string limbType)

public static LimbBehaviour FindLimb(GameObject gameObject, string limbType)
```
By adding 3 letters, you can eliminate the need of typing GetComponent for the LimbBehaviour

#### LimbList.FindLimbComp()
Similar in spirit to FindLimbBeh but generalized to any component on the limb (Contains 2 overloads):
```cs
public static T FindLimbComp<T>(Transform transform, string limbType) where T : MonoBehaviour

public static T FindLimbComp<T>(GameObject gameObject, string limbType) where T : MonoBehaviour
```
By adding 4 letters this time, we can  yet  again spare us the time of writing GetComponent after finding the limb transform
```cs
var lowerArmFront = LimbList.FindLimb(limb.transform, LimbList.lowerArmFront).GetComponent<MyComponent>();
var lowerArmFront = LimbList.FindLimbComp<MyComponent>(limb.transform, LimbList.lowerArmFront);
```
Remember that this and the previous methods only work for **getting** a component, not for adding it. We won't be simplifying adding components here.

### WaveClamp()
A collection of a few special mathemagical functions that can be easily applied. We will be skipping over boring mathematical details and jump right into the mathemagic!<br/>
Contains a total of 3 overloads:
####  WaveClamp01()
```cs
public static float WaveClamp01(float num, float period)
```
This special overload that is actually its own method will interchangibly return a value between 0 and 1 given a periodic amount of time period and a self-incrementing value num.<br/>
I assume only 0.1% of readers will understand what I mean so a quick example:
```cs
float timer = 0f;
float myValue = 0f;

public void FixedUpdate()
{
    myValue = PlusAPI.WaveClamp01(timer, 2f);
    timer += Time.fixedDeltaTime;
}
```
This method is mostly meant for these kinds of scenarios<br/>
In this sexample, myValue will first be 0 and will start going upwards, then after 2 seconds it will be 1 and will start to go down. After 2 more seconds (4 total since this code started running) myValue will be back at 0 and so on. Why 2 seconds? Because the timer measures time in seconds, and the period between the extrema (in this case, 0 and 1) is set to 2f, so 2 seconds.<br/>
The property of it interchanging between 2 different values at a fixed speed could be extremely useful in certain scenarios.
#### WaveClamp()
This is the generic method containing the 2 remaining overloads.<br/>
Overload 1:
```cs
public static float WaveClamp(float num, float period, float maxNum)
```
Similar to WaveClamp01(), but it  will interchange between 0 and a specified maxNum instead.<br/> 
MaxNum parameter cannot be 0 and the method will throw an exception in that case. If maxNum is negative, the method will automatically convert it to a positive value.<br/>
But wait, why does maxNum here come first? It's because the mathematical function of this overload is way easier than the function of the other overload so it comes first.

Overload 2:
```cs
public static float WaveClamp(float num, float period, float maxNum, float minNum)
```
Similar to the 1st WaveClamp() overload, but you can also specify the minimum value, so it will interchange between minNum and MaxNum. When num is 0, minNum will be returned.<br/>
minNum and maxNum can't be equal and the method will throw an exception if they are. If minNum is larger than maxNum, the values will be flipped around, so minNum will actually be maxNum in the mathematical function and vice versa.


## ArmorBehaviour (REQUIRES CreationPlus and PlusAPI)
### public class ArmorBehaviour : MonoBehaviour
This class must be added to the object you want to make into armor in order for it to be armor.

But simply adding the ArmorBehaviour is not enough. In order for it to work, you must add in one of the default 3 **armor constructor methods**.<br/>

#### Armor constructor method
Unlike the name suggests, those are not actual constructors but methods that essentially do what a constructor would. The behaviour by default contains Body Armor and Clothing, with each of them having their unique constructor:
```cs
public void CreateBodyArmor(string newLimbType, float newStabResistance)

public void CreateClothing(string newLimbType)
```
For clothing you simply have to provide the string of the limb path in the transform hierarchy. For this you can simply use LimbList (See documentation on LimbList).<br/>
For body armor however, you also must provide a stab resistance. This value is clamped between 0 and 1, where 0 means no protection and 1 means full protection. If it's set to 0.75f, there is a 75% chance that the armor will be protected from being stabbed, so 25% chance that human will be stabbed. It's purely based on chance instead of taking factors like velocity into account. I do realize this. Some might consider me lazy for writing this like that, in which case they're right. I was lazy when writing this. I might or might not fix it in a future update to the API.<br/>
For clothing, stab resistance is always 0.

Both of these methods are derived and draw from the actual armor constructor method
```cs
public void CreateCustom<T>(string newLimbType, float newStabResistance, int newArmorSorOrd) where T : ArmorWearer
```
This contains the additional setting of armor sorting order, which defines how many sorting orders above the limb the armor will show.<br/>
For clothing, this number is 2. For body armor, this number is 3. This basically means that body armor will always be displayed above clothing for the limb. There does exist the "head armor visual glitch", but more on that shortly.<br/>
While in theory this number can also be 1, for Studio Plus Mods that sorting order is reserved for light sprites so that cny clothing covering those sprites up will always cover up the light sprite as well, so Clothing uses the next available lowest number 2.<br/>
I already implied this, but this number shouldn't be 0 or a  negative number because then armor will not be shown above the limb.

Also, it's very important that ArmorBehaviour must be added via GetOrAddComponent to ensure a 99.9% bug-free experience, so:
```cs
Instance.GetOrAddComponent<ArmorBehaviour>().CreateBodyArmor(LimbList.head, 0.5f);
```

To end this boring documentation entry, here is a full example of an armor piece being created from ArmorBehaviour [MOD]:
```cs
ModAPI.Register(
    new Modification()
    {
        OriginalItem = ModAPI.FindSpawnable("Rod"),
        NameOverride = "Helmet" + tag,
        NameToOrderByOverride = "Z2",
        DescriptionOverride = "For bike rides and in case of nuclear explosion!",
        CategoryOverride = ModAPI.FindCategory("Entities"),
        ThumbnailOverride = ModAPI.LoadSprite("Textures/Views/Helmet View.png"),
        AfterSpawn = (Instance) =>
        {
            Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Helmet.png");
            Instance.FixColliders();
            var phys = Instance.GetComponent<PhysicalBehaviour>();
            phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin"); //Bad code, search ArmorBehaviour [MOD] source code for how to write this better
            Instance.GetOrAddComponent<ArmorBehaviour>().CreateBodyArmor(LimbList.head, 0.5f);
        }
    }
);
```
#### Head armor visual glitch
Imagine sorting orders like putting transparent pieces of paper with things drawn on them in a specific order. Now, imagine you creating multiple such stacks, compressing them into 1 piece of transparent paper and then putting those in order. This is the most basic explanation of sorting layers in unity.<br/>
The back arm/leg and front arm/leg are both on a different sotring layers when compared to the rest of the limbs (Background and Foreground respectively) which allows ArmorBehaviour to exploit sortinh order to sort armor pieces the way they are. That is, except for the head which for some reason is also on the Foreground layer with the front leg/arm.<br/>
This causes any armor piece that's placed on the head and in the sorting order 2 or above to display above the front arm, which obviously doesn't look good so we need a solution.

One of the solutions would be to put all Armor on the sorting order 1, but that's impossible with the way the ArmorBehaviour system is set up.

I have long dealt with this issue by not dealing with it, the one slight imperfection of my ArmorBehaviour shadowing me every day... What a failure I am because I can't perfect my ArmorBehaviour. How can I even call myself a programmer?<br/>
Luckily, I recently found a solution: Simply forcing the heads of humans and androids to the default sorting layer at the beginning of the code.
```cs
namespace Mod
{
    public class Mod : MonoBehaviour
    {
        public static void Main()
        {
            ModAPI.FindSpawnable("Human").Prefab.transform.Find("Head").GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            ModAPI.FindSpawnable("Android").Prefab.transform.Find("Head").GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            //Mandatory Studio Plus disclaimer: These 2 things are here so that ArmorBehaviour isn't a mess with head pieces, you're welcome
            //"Might have some unintended side effects that manifest as unexplainable bugs later"
            //-zooi

            //Your other code here...
        }
    }
}
```
While this is not the perfect solution like zooi mentioned, I can finally sleep peacefully at night, knowing that one of the biggest failures of this API has been corrected.

#### Armor collision (with other armor)
Basically, when you got an armor piece "Armor" and another armor piece "Other Armor" and:
- Other Armor is of the same type as Armor
- Other Armor connects to the same limb as Armor
- Armor is equipped

The armor pieces won't collide. The last condition is in place to make attaching armor on an entity that already has armor easier.

#### ArmorBehaviour.Equipped
Important property of the class. Returns true when armor is attached to a limb and returns false 5 seconds after detached.<br/>
An armor piece can only be equipped by an entity if Equipped is false.

#### ArmorBehaviour.IsAttached
Another important property of the class. It returns true when armor is attached, but unlike Equipped, returns false immediately when armor is detached.<br/>
It could have some specific uses for you, the modder, which is why it's in the API, because otherwise it is not used by the ArmorBehaviour class itself.

### public abstract class ArmorWearer : MonoBehaviour
This class gets automatically added to the limb after armor is attached to it. The API by itself comes with ClothingWearer and BodyArmorWearer, which don't do nothing more than the most basic functions they're supposed to do. They also have their own methods as already stated in the ArmorBehaviour section

It's a very important part of the ArmorBehaviour, specifically the way that there are different armor flavors like Clothing and Body Armor. This is also the behaviour's weak spot so it's very important that you read this carefully:
1. The class is abstract simply becasue ArmorWearer is not meant to be added in directly to a limb, I did not test how armor behaves in such circumstance.
1. ArmorBehaviour has a system built in to specifically combat stacking. Basically, if you got 2 types of a helmet and sunglasses, you are meant to be able to put sunglasses and a helmet on an entity, but not 2 helmets at once.<br/> 
The way it works is that if you make a subclass of BodyArmorWearer, it will still be considered by the anti-stacking system as BodyArmorWearer. Subclasses and Sub-Subclasses are the only levels of ArmorWearer Subclass that were tested. It is possible that Sub-Sub-Subclasses of ArmorWearer will still be properly treated by the anti-stacking system, but it's not guaranteed.<br/>
There should also be no circumstance in which you would need to make a Sub-Sub-Subclass of ArmorWearer. Making Subclasses of ArmorWearer Subclasses should only be done to introduce a unique behaviour to an armor piece (as presented in Power Plus [Mod]).

A good example of utilizing Sub-Subclasses of ArmorWearer is presented in the blaster glove. If you want your armor piece to activate some lights when it's attached to a limb, putting it into ArmorWearer is the best solution because it naturally tracks the moment an armor piece is attached and detached:
```cs
public class BlasterGloveWearer : BodyArmorWearer
{
    [SkipSerialisation] //THIS IS IMPORTANT. IF YOU DO NOT INCLUDE THIS, THE LIGHT WILL BE COPIED OVER TO THE COPY OF THE ENTITY YOU MAKE. THIS WOULD BE BAD
    public GameObject light;
    [SkipSerialisation] //SIMILAR STORY HERE
    public LightSprite glow;

    public override void Start()
    {
        base.Start();
        TexturePlus.CreateLightSprite(
            out light,
            armorObject.transform,
            UniversalAssets.gloveLight,
            new Vector2(0f, -7.5f) * ModAPI.PixelSize,
            new Color32(0, 255, 255, 63),
            out glow,
            5f,
            0.75f
        );
    }

    protected void FixedUpdate()
    {
        if (glow != null)
            glow.Brightness = UnityEngine.Random.Range(0.65f, 0.85f);
    }

    public void OnDestroy()
    {
        Destroy(light);
        //Destroying Glow.gameObject is unnecessary since it's a child of Light
    }
}
```


## PowerPlus (PlusAPI HIGHLY recommended)
### public abstract class PowerPlus : MonoBehaviour
Allows you to gift humans with power.<br/>
This class is a beautiful and perfect mesh of being simple for you, the modder, to use, yet also being the most advanced piece of code I've written up to date, so this documentation will be split into 2 parts:
- How to use?
- How does it work?

#### How to use?
So first you must know that there are 3 protected virtual methods in this class (That is, methods that you're allowed to override in your subclass).
```cs
protected virtual void Awake()

protected virtual void Start()

protected virtual void FixedUpdate()
```
It's important to get that out of the way because I will be mentioning them throughout this section of the documentation

The class is abstract because you must make a subclass for it, and it contains 4 abstract methods that you have to override:
```cs
protected abstract void CreatePower();

protected abstract void TogglePower(bool toggled);

protected abstract void ToggleAbility(bool toggled);

protected abstract void DeletePower();
```

##### CreatePower()
It's called when the class is first added by default in the Start() method. If you don't wish for the power to be immediately added when created, override Start() without adding base.Start() anywhere.<br/> 
Within it you should add things like creating any light sprites, adding immunities or abilities (See Ability documentation), etc., basically anything meant to be permanent.<br/>
Here is an example from Power Plus [MOD]:
```cs
protected override void CreatePower()
{
    foreach (var limbs in Person.Limbs)
    {
        var phys = limbs.PhysicalBehaviour;

        limbs.DiscomfortingHeatTemperature = float.PositiveInfinity;
        phys.SimulateTemperature = false;

        phys.Properties = UniversalAssets.fireHumanProperties;
    }

    TexturePlus.CreateLightSprite(
        out eyeLight,
        Limb.transform.root.transform.Find(LimbList.head),
        UniversalAssets.eyeLight,
        new Vector2(2.5f, 1.5f) * ModAPI.PixelSize,
        powerColor,
        out eyeGlow
    );
    eyeLight.SetActive(eyeActive);

    abilities.Add(LimbList.FindLimb(Limb.transform.root, LimbList.lowerArmFront).gameObject.GetOrAddComponent<FireTouch>());
    abilities.Add(LimbList.FindLimb(Limb.transform.root, LimbList.lowerArmBack).gameObject.GetOrAddComponent<FireTouch>());
}
```

##### TogglePower(bool toggle)
It toggles the power as a whole. In this state, the human should look as if he has no powers (ignoring e.g. immunities, but that's up to you to decide). **Calls ToggleAbility(bool toggle) by default.**
For Fire Human from Power Plus [MOD] it simply toggles eye glow:
```cs
protected override void TogglePower(bool toggled)
{
    eyeLight.SetActive(toggled);
}
```

##### ToggleAbility(bool toggle)
It toggles ability instead. The class itself deals with this function rather effectively, so the toggle of abilities should be limited to the Ability class (See documentation on Ability). That is the case for Fire Human Power, which is why no example can be provided

##### DeletePower()
Called when the class is removed. Should undo everything that CreatePower() does. If your power is not meant to be removable in that way, you don't have to add anything to it. It already handles removing abiilities by itself so you don't have to worry about that

##### IsCreated { get; protected set; }
Turns true when power is first created with CreatePower(). Not meant to be turned false.<br/>
It should be only used when modifying values by the means of arithmetic operators. Here is an example:
```cs
protected override void CreatePower()
{
    foreach (LimbBehaviour limbs in Person.Limbs)
    {
        limbs.DiscomfortingHeatTemperature = 2000f; //This sets the value to a specific value each time, so it can run 1000 times and it will work just fine
        if (!IsCreated) //This ensures that the code in the curly brackets only runs when this is false, in other words exactly once, considreing the value of IsCreated is later set to true and it never turns back to false.
        {
            limbs.BreakingThreshold *= 2f; //If left outside of the if statement, the breaking threshold will be doubled each time the entity is copied
            //This would be bad, so you ensure with the if statement that this is only run once.
        }        
    }   
}
```
If you are not sure if the method runs correctly, the Debug Log "Power created!" is set up to only run once with the same thought process, so if it only does print out exactly once, you must have done something wrong instead of the API being broken (probably, maybe) 

##### Other stuff
There are also 2 additional methods that you should know about:
```cs
public void ForceTogglePower(bool toggled)

public void ForceToggleAbility(bool toggled)
```
So like the name implies, it forces Powers or Abilities to be toggled on and off right? Well, sort of.<br/>
When you toggle Power/Abilities to false by this method, they will be permanently disabled without considering the Power & Ability Toggle Conditions (More about that later). However when toggled to true by this method, it will only subject it to the previously mentioned Power & Ability Toggle Conditions, so if the entity is dead anyway it actually won't enable it for a frame to have the class then immediately disable it by default.<br/>
Additionally, if power is already off and you use the method to forecully turn it off, it won't turn it off again since it's already turned off. That's an optimization thing, or whatever.

#### How does it work?
I will just go over every method that does not relate to 'how to use' here:

##### protected virtual void Awake()
Defines the following properties thusly:
```cs
/* earlier...
public LimbBehaviour Limb { get; protected set; }
public PersonBehaviour Person { get; protected set; }
*/
protected virtual void Awake()
{
    Limb = GetComponent<LimbBehaviour>();
    Person = Limb.Person;
}
```

##### protected virtual void Start()
Calls PowerCreateInt()<br/>
Woah hold on! What the heck is PowerCreateInt()? Just give me a moment okay?

##### protected virtual void FixedUpdate()
Contains the so-called "Power & Abilities Toggle Conditions".<br/>
Here they are!
```cs
if (!PowerActive && !AbilityActive)
    return;

if (AbilityActive)
{
    if (AbilityEnabled && !LimbList.FindLimbBeh(transform, LimbList.head).IsCapable)
        ToggleAbilityInt(false);
    else if (!AbilityEnabled && LimbList.FindLimbBeh(transform, LimbList.head).IsCapable && PowerEnabled)
        ToggleAbilityInt(true);
}

if (PowerActive)
{
    if (!LimbList.FindLimbBeh(transform, LimbList.head).IsConsideredAlive && PowerEnabled)
        TogglePowerInt(false);
    else if (LimbList.FindLimbBeh(transform, LimbList.head).IsConsideredAlive && !PowerEnabled)
        TogglePowerInt(true);
}
```
I'll quickly summarize how it works here:

If both Power And Abilities are inactive, do nothing

Else...<br/>
if Ability is active:
- If person is unconscious and Ability wasn't disabled yet, disable them
- Else if person is conscious and Ability wasn't enabled yet and Power is enabled, enable them

If Power is active:
- If the head is dead and power wasn't disabled yet, disable it.
- Else if the head is alive and power wasn't enabled yet, enable it.

##### protected void CreatePowerInt(), protected void TogglePowerInt(), protected void ToggleAbilityInt()
We finally get to these guys. 'Int' here stands for 'Internal', and that's because this is the stuff that happens internally in the class. Crazy naming, right?<br/>
They do the necessary behind-the-scenes stuff to make those methods work and is what's actually called every time power is made or toggled. The things that you put into the abstract classes get executed by the Internal classes though so don't worry.

The details of how they exactly work and what they do is kinda boring and unnecessary, say for one:
```cs
//This class turns *abilities* on or off. Main use for when the one with power is knocked unconcsious
protected void ToggleAbilityInt(bool toggled)
{
    switch (toggled)
    {
        case true:
            AbilityEnabled = toggled;
            foreach (Ability ability in abilities)
            {
                ability.enabled = toggled;
            }
            Debug.Log("Abilities Enabled!");
            break;
        case false:
            AbilityEnabled = toggled;
            foreach (Ability ability in abilities)
            {
                ability.enabled = toggled;
            }
            Debug.Log("Abilities Disabled!");
            break;
    }
    ToggleAbility(toggled);
}
```
As you can see, the ToggleAbilityInt on its own changes the enabled variable in the Ability class. This is acknowledged in the Ability class itself and is meant to be the way you handle toggling powers on and off. (See Ability Documentation)

##### protected void OnDestroy()
You might wonder why there is no 'DeletePowerInt()' method. That is because OnDestroy does what it would do:
```cs
protected void OnDestroy()
{
    foreach (Ability ability in Abilities)
    {
        Destroy(ability);
    }
    DeletePower();
}
```
The only thing that needs to be done when Power is deleted is deleting the ability classes. Everything else is defined by the subclass

### public abstract class Ability : MonoBehaviour
This is a class for all abilities added by PowerPlus class.

For starters, tbe PowerPlus class has a list of abilities:
```cs
public List<Ability> abilities = new List<Ability>();
```
These should be added in the abstract method **CreatePower()**, preferably at the end of said method, like here:
```cs
protected override void CreatePower()
{
    //Other code here...

    abilities.Add(LimbList.FindLimb(Limb.transform, LimbList.lowerArmFront).gameObject.GetOrAddComponent<MyAbility>());
    abilities.Add(LimbList.FindLimb(Limb.transform, LimbList.lowerArmBack).gameObject.GetOrAddComponent<MyAbility>());
}
```
It's important that you do it like that by finding the appropriate limb then adding the Ability via GetOrAddComponent to make it work properly.

To spare the explaining of every method (and because the class is short enough) I will put it here as reference:
```cs
public abstract class Ability : MonoBehaviour
{
    [SkipSerialisation]
    public LimbBehaviour Limb { get; protected set; }
    [SkipSerialisation]
    public PersonBehaviour Person { get; protected set; }

    protected virtual void Awake()
    {
        Limb = GetComponent<LimbBehaviour>();
        Person = Limb.Person;
    }

    public virtual void FixedUpdate()
    {
        if (enabled && (!Limb.NodeBehaviour.IsConnectedToRoot || !Limb.IsConsideredAlive))
            enabled = false;
        else if (!enabled && Limb.IsConsideredAlive)
            enabled = true;
    }

    public abstract void OnEnable();

    public abstract void OnDisable();
}
```
As you can see, like PowerPlus it has Limb and Person and immediately assigns values for them.<br/>
It also contains the last of Power & Abilities Toggle Conditions: When the limb is separated from the body or if it's dead, it disables the class. When the limb is revived, it will re-enable the class

The class works the way it works mostly because of that 1 line of code.<br/>
It also contains the 2 absract methods OnEnable() and OnDisable(), in which you should define what happens when this ability is toggled on and off respectively.

Note that enabled simply being set to false doesn't automatically make other behaviours of the class stop, so you will have to adjust whatever you write with that in mind:
```cs
public class FireTouch : Ability
{
    [SkipSerialisation]
    public GameObject armLight;
    [SkipSerialisation]
    public LightSprite armGlow;
    [SkipSerialisation]
    public Color powerColor = new Color32(255, 127, 0, 31);

    public void Start()
    {
        TexturePlus.CreateLightSprite(
            out armLight,
            transform,
            UniversalAssets.powerLight,
            Vector2.zero,
            powerColor,
            out armGlow,
            5f,
            0.75f
        );
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (enabled && gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= 1f) //If statement must check if enabled is true or else this will trigger regardless of whether the ability is toggled on or off
        {
            var phys = other.transform.GetComponent<PhysicalBehaviour>();
            phys.Temperature += 200f;
            phys.Ignite();
            if (phys.OnFire)
            {
                phys.BurnIntensity = 1f;
            }
        }
    }

    public override void OnEnable()
    {
        //This question mark ensures that this is only run if armLight isn't null to ensure no NullReferenceException errors
        armLight?.SetActive(true);
    }

    public override void OnDisable()
    {
        //Same here
        armLight?.SetActive(false);
    }
    }
```