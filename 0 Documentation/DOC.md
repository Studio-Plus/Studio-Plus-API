# StudioPlusAPI DOCUMENTATION (v3.0.0)
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

#### Inaccessible liquids
- Gasoline.ID
- Chemistry.DebugDiscolorationLiquid.ID
- Chemistry.InertLiquid.ID

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
The so far only hidden method that is blocked by comments.
```cs
/*
public static GameObject SpawnItemStatic(SpawnableAsset item, Vector2 position = default, bool spawnSpawnParticles = false)
*/
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

public static Transform FindLimb(GameObject gameObject, string limbType)
```
Here's the example from above but done with this method:
```cs
var lowerArmFront = LimbList.FindLimb(Instance.transform, LimbList.lowerArmFront);
```
Wihle this is longer than what we started with, the overload actually allows us to make this shorter:
```cs
var lowerArmFront = LimbList.FindLimb(Instance, LimbList.lowerArmFront);
```
Now it is just a bit longer with the benefit of being more straight forward in this example

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

#### ???
Not sure what else I could add. I could go over the other 157 lines of code detailing how the Armor works and interacts with the world, but not sure if that's really needed.

Well I can at least tell you how Armor collides with Other Armor. Basically, if:
- Other Armor is of the same type as Armor
- Other Armor connects to the same limb as Armor
- Armor is equipped

The armor pieces will not collide. The last condition is in place to make placing armor on an entity that already has armor easier.

### public abstract class ArmorWearer : MonoBehaviour
This class gets automatically added to every limb after armor is attached to it. The API by itself comes with ClothingWearer and BodyArmorWearer, which don't do nothing more than the most basic functions they're supposed to do. They also have their own methods as  already stated in  the ArmorBehaviour section

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
            light = new GameObject("Glow"),
            armorObject.transform,
            UniversalAssets.gloveLight,
            new Vector2(0f, -7.5f) * ModAPI.PixelSize,
            new Color32(0, 255, 255, 63),
            glow = TexturePlus.InstantiateLight(light.transform),
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
### Missing
In order to finally get this update out, this was temporarily skipped. it will be put in later. For now you have to figure it out with the Power Plus [Mod] source code.