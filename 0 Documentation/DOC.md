# StudioPlusAPI DOCUMENTATION (v4.0.2)
## ChemistryPlus
### AddLiquidToItem() 
This method allows to add a liquid to an already existing item. Contains 2 overloads.<br/>
```cs
public static void AddLiquidToItem(this SpawnableAsset item, string newLiquidID, float amount)

public static void AddLiquidToItem(this SpawnableAsset item, string newLiquidID, float amount, float capacity)
```
By default capacity is adjusted to the amount of liquid you add:
```cs
ModAPI.FindSpawnable("Rotor").AddLiquidToItem(Oil.ID, 1.4f);

//ChemistryPlus.AddLiquidToItem("Rotor", "OIL", 1.4f);
```
While using simple strings for the IDs of liquids is possible, using the ID stored in the actual class is recommended as it will be more resistant to breaking with updates to the game.<br/>
For your convinience, a list of all liquid IDs will be provided in this documentation.

If you so wish, you can provide a capacity for the container (capacity must be larger than amount):
```cs
ModAPI.FindSpawnable("Rotor").AddLiquidToItem(Oil.ID, 1.4f, 2.8f);
```
This is nearly useless, I know.

### LiquidReaction() 
This collection of methods allows you to create custom liquid mixes. Contains 5 overloads.<br/>
```cs
public static void LiquidReaction(string input1, string input2, string target, float ratePerSecond = 0.05f)

public static void LiquidReaction(string input1, string input2, string input3, string target, float ratePerSecond = 0.05f)

public static void LiquidReaction(Liquid[] inputs, Liquid target, float ratePerSecond = 0.05f)

public static void LiquidReaction(Liquid[] inputs, Liquid[] targets, float ratePerSecond = 0.05f)

public static void LiquidReaction(Liquid[] inputs, Liquid[] targets, int[] ratios, float ratePerSecond = 0.05f)
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
For even more advanced users the 4th overload allows you to mix multiple input liquids into multiple output liquids
```cs
ChemistryPlus.LiquidReaction(
    new Liquid[]
    {
        Liquid.GetLiquid(Sugar.ID),
        Liquid.GetLiquid(Spice.ID),
        Liquid.GetLiquid(EverythingNice.ID),
        Liquid.GetLiquid(Chemistry.Tritium.ID)
    }, 
    new Liquid[]
    {
        Liquid.GetLiquid(Blossom.ID),
        Liquid.GetLiquid(Bubbles.ID),
        Liquid.GetLiquid(Buttercup.ID)
    },
    0.06f
)
```
These liquids will be created in equal ratios. However, if you want unique ratios, use the 5th overload:
```cs
ChemistryPlus.LiquidReaction(
    new Liquid[]
    {
        Liquid.GetLiquid(Sugar.ID),
        Liquid.GetLiquid(Spice.ID),
        Liquid.GetLiquid(EverythingNice.ID),
        Liquid.GetLiquid(Chemistry.Tritium.ID)
    }, 
    new Liquid[]
    {
        Liquid.GetLiquid(Blossom.ID),
        Liquid.GetLiquid(Bubbles.ID),
        Liquid.GetLiquid(Buttercup.ID)
    },
    new int[]
    {
        2,
        3,
        4
    },
    0.09f
)
```
I'm going to explain how these ratios work:<br/>
First of all, the number of ratios and outputs must match exactly, otherwise you will get an error.<br/>
When the mixing process takes place, a total of 9 (sum of all numbers in the ratios array) parts of liquid will be made. 2 parts will be Blossom, 3 parts will be Bubbles and 4 parts will be ButterCup.

You might have noticed that for the last 2 overloads I specifically set a ratePerSecond to 0.06f and 0.09f respectively. This is very important so read carefully:<br/>
Because PPG code is garbage, if a given liquid in a container is below 0.02 units, it gets removed. The way these 2 overloads work is that they divide the inputed ratePerSecond, which could result in the liquid reaction not working properly.

To save you the headache of having to adjust these values yourself, the methods will automatically calculate the lowest ratePerSecond that will work and adjust your inputted ratePerSecond accordingly. If you're curious how those values are calculated, the general formula is
```cs
float minRate = 0.02f * divisor / Mathf.Min(ratios);
```
Divisor here is the sum of all ratios.<br/>
For the 4th overload, the divisor is simply the number of output liquids and the minimum value is 1 (Because what the 4th overload does is basically a 1 : 1 : ... ratio) so the formula is:
```cs
float minRate = 0.02f * targets.Length;
```
For the 4th overload example, the minimum value is 0.02f * 3 = 0.06f, and for the 5th overload example the minimum value is 0.02f * 9 / 2 = 0.09f.

The larger the ratePerSecond is, the more inaccurate does the mix become so approach the last 2 overloads with caution.

### AddBottleOpening()
This method allows you to add a bottle opening to any container.
```cs
public static PointLiquidTransferBehaviour AddBottleOpening(this BloodContainer container, Vector2 position, Space outerSpace = Space.Self)
```
It's important to note that BloodContainer class is the base class for all liquid containers, so you can also call it from FlaskBehaviour, CirculationBehaviour, etc.<br/>
Returns PointLiquidTransferBehaviour in case you have to make any additional modifications.

The argument outerSpace determines whether to use relative or absolute position for Space.Self and Space.World respectively. You will almost always want to use relative position so it defaults to Space.Self but what do I know about what kind of item you want to make?<br/>
Here is an example of how to use it:
```cs
PointLiquidTransferBehaviour.bottleOpening = GetComponent<BloodContainer>().AddBottleOpening(new Vector2(-1f, 4.5f) * ModAPI.PixelSize);
```

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

### ReplaceViewSprite()
Part of the 'Advanced texture pack system', allows for replacing the view sprite.
```cs
public static void ReplaceViewSprite(this SpawnableAsset item, Sprite replaceSprite)
```
This one is straight forward and doesn't contain any overloads.
```cs
ModAPI.FindSpawnable("Sword").ReplaceViewSprite(ModAPI.LoadSprite("Upscaled Sword View.png"));
```

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

### SetHealthBarColors()
This method allows you to change the health bar color of any entity.
```cs
public static void SetHealthBarColors(this PersonBehaviour person, Color color)
```
There isn't exactly much to say here.
```cs
PersonBehaviour person = Instance.GetComponent<PersonBehaviour>();
person.SetHealthBarColors(new Color32(200, 0, 255, 255));
```
There is also a method for resetting the health bar color back to normal:
```cs
public static void ResetHealthBarColors(this PersonBehaviour person)
```
```cs
person.ResetHealthBarColors();
//Default is new Color32(55, 255, 0, 255)
```
### SetHealthBarColor()
Almost like SetHealthBarColors(), but for individual limbs. There is also a ResetHealthBarColor() method.
```cs
public static void SetHealthBarColor(this LimbBehaviour limb, Color color)

public static void ResetHealthBarColor(this LimbBehaviour limb)
```
I don't think an example is in order. 


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
It's the old SpawnItem method that spawns the item at a fixed point perfectly rotated towards the plane. I'm not sure if it's useful or not cuz I wrote this last-minute change at 11 PM so I left it hidden in the 

### CreateFixedJoint()
Creates a fixed joint between two objects. Contains 2 overloads.
```cs
public static void CreateFixedJoint(this GameObject main, GameObject other)

public static void CreateFixedJoint(this GameObject main, GameObject other, Vector2 position)
```
In other words, it creates a rigid connection between 2 objects.
```cs
gameObject.CreateFixedJoint(myObject);
```
If for any reason you have to change the position of said joint, you can use the overload:
```cs
gameObject.CreateFixedJoint(myObject, new Vector2(0f, 3f) * ModAPI.PixelSize);
```

### CreateHingeJoint()
Creates a hinge joint between two objects.
```cs
public static void CreateHingeJoint(this GameObject main, GameObject other, Vector2 position, float minDeg, float maxDeg)
```
I don't think this needs further explanation, only an example:
```cs
gameObject.CreateHingeJoint(myObject, new Vector2(0f, -4.5f) * ModAPI.PixelSize, -45f, 45f);
```
I did not provide an overload that allows you to not need to input Vector2 because tbh why would you need to, but if you _really_ have to:
```cs
gameObject.CreateHingeJoint(myObject, Vector2.zero, -45f, 45f);
```

### CreateDebris()
Creates a custom debris object.<br/>
```cs
public static GameObject CreateDebris(string name, Transform parent, Sprite sprite, Vector2 position)
```
This is a very specific method that was only added in here for convinience to use for another mod I made, so this might be completely useless outside of that very specific context, and that context also involes a class that is not contained in StudioPlusAPI.

### CreateParticles()
Creates particles.<br/>
```cs
public static ParticleSystem CreateParticles(GameObject item, Transform parent, Vector2 position = default, Quaternion rotation = default)
```
This is actually very useful unlike the last entry. You know how plates create cool custom particles and sound? What this method does is that it takes the broken plate prefab and removes any GameObjects so that only the particles remain. Here is how you do it for the previously mentioned example:
```cs
ParticleSystem particles = CreationPlus.CreateParticles(ModAPI.FindSpawnable("Plate").Prefab.GetComponent<DestroyableBehaviour>().DebrisPrefab, Instance.transform);
```
If positon parameter is left empty, the particles will play at the center of the object.<br/>
The rotation parameter is a modifier, if left empty the rotation will simply equal the rotation of the parent transform.

The method returns the ParticleSystem component in case you have to modify the particles in any way.


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
A short-hand way to write PPG's method for ignoring collision because I won't be typing IgnoreCollisionStackController.IgnoreCollisionSubstituteMethod() every time I want to do something with collisions. It also has the benefit of being an extension method to Collider2D
```cs
public static void IgnoreCollision(this Collider2D main, Collider2D other, bool ignColl)
{
    IgnoreCollisionStackController.IgnoreCollisionSubstituteMethod(main, other, ignColl);
}
```

### IgnoreEntityCollision()
Allows you to disable collision with multiple colliders (usually entities).
```cs
public static void IgnoreEntityCollision(this Collider2D main, Collider2D[] others, bool ignColl, bool affectItself = false)
```
Tbh Idr exactly how I got my hands on this, I think I stole it from PPG source code. Idk what affectItself does, I am too small brain to figure out what the complex if statement is about. Here's simply an example from ArmorBehaviour:
```cs
GetComponent<Collider2D>().IgnoreEntityCollision(limbColliders, true);
```

### ChangeAlpha()
This method changes the alpha of a given value. Contains 2 overloads.
```cs
public static Color ChangeAlpha(this Color color, float alpha)

public static Color32 ChangeAlpha(this Color32 color, byte alpha)
```
For the first overload, alpha is clamped between 0f and 1f.<br/>
It's the case for many methods here but I think this is the best one to mention it: This is a extension method so you can do something like this:
```cs
public Color32 exampleColor;

exampleColor.ChangeAlpha(127);
```

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
In this example, myValue will first be 0 and will start going upwards, then after 2 seconds it will be 1 and will start to go down. After 2 more seconds (4 total since this code started running) myValue will be back at 0 and so on. Why 2 seconds? Because the timer measures time in seconds, and the period between the extrema (in this case, 0 and 1) is set to 2f, so 2 seconds.<br/>
The property of it interchanging between 2 different values at a fixed speed could be extremely useful in certain scenarios.
#### WaveClamp0x()
This is another special overload that is its own method but still related to WaveClamp
```cs
public static float WaveClamp0x(float num, float period, float maxNum)
```
Similar to WaveClamp01(), but it  will interchange between 0 and a specified maxNum instead.<br/> 
MaxNum parameter cannot be 0 and the method will throw an exception in that case. If maxNum is negative, the method will automatically convert it to a positive value.<br/>
#### WaveClamp()
This is the generic method.<br/>
```cs
public static float WaveClamp(float num, float period, float minNum, float maxNum)
```
Similar to WaveClamp0x(), but you can also specify the minimum value, so it will interchange between minNum and MaxNum. When num is 0, minNum will be returned. For each multiple of period (1\*period, 2\*period, 3\*period, etc), it will return maxNum, minNum, maxNum, etc. respectively<br/>
minNum and maxNum can't be equal and the method will throw an exception if they are.<br/>
If minNum is greater than maxNum, it will still behave as expected but instead of starting at the minimum value it wil start at the maximum value.

### ToFloat()
Changes a byte (0 to 255) into its corresponding float. Clamped between 0f and 1f:
```cs
public static float ToFloat(this byte value)
{
    float newValue = value;
    float returnValue = newValue / 255f;
    return Mathf.Clamp01(returnValue);
}
```

### ToByte()
Inverse operation of ToFloat, also clamped between 0 and 255:
```cs
public static byte ToByte(this float value)
{
    float newValue = Mathf.Clamp01(value) * 255f;
    return (byte)newValue;
}
```

### Inv()
Returns the inverse of a given float.<br/>
```cs
public static float Inv(this float num)
{
    if (num == 0f) 
        return 0f;
    if (num == 1f)
        return 1f;
    return 1f / num;
}
```
In other words, if you input 2f in the method it will return 1/2, or 0.5f.
If num is equal to 1f it will return 1f, same for 0f.

### GetAbs() (Vector2/Vector3)
Returns the absolute value of a Vector
```cs
public static Vector2 GetAbs(this Vector2 originalVector)

public static Vector3 GetAbs(this Vector3 originalVector)
```

### Sum() (float/int)
Returns the sum of given floats or integers
```cs
public static float Sum(params float[] values)

public static int Sum(params int[] values)
```
You can input the values as either an array or individual values
```cs
float example1 = PlusAPI.Sum(1.9f, 2.1f, 4.3f, 6f);
float[] exampleArray = new float[]
{
    1.9f, 
    2.1f, 
    4.3f, 
    6f
};
float example2 = PlusAPI.Sum(exampleArray);
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
You can also do this to other structs and static classes.

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
Speaking of Find, this struct also contains a method that makes finding limbs easier (Contains 3 overloads):
```cs
public static LimbBehaviour FindLimb(this PersonBehaviour person, string limbType)

public static LimbBehaviour FindLimb(this LimbBehaviour limb, string limbType)

public static LimbBehaviour FindLimb(this CirculationBehaviour circ, string limbType)
```
While it is reliant on a reference to PersonBehaviour and returns LimbBehaviour, it's still useful for a lot of cases. Here is an example
```cs
PersonBehaviour person = Instance.GetComponent<PersonBehaviour>();
person.FindLimb(LimbList.lowerArmFront);
```
If you only got a limb reference, you can simply use the 2nd overload
```cs
LimbBehaviour limb;

public void Start()
{
    limb = GetComponent<LimbBehaviour>();
    var lowerArmFront = limb.FindLimb(LimbList.lowerArmFront);
}
```
If for some strange reason you only got a CirculationBehaviour reference, overload 3 is your friend:
```cs
public override void OnUpdate(BloodContainer container)
{
    if (container is CirculationBehaviour circ)
    {
        var lowerArmFront = circ.FindLimb(LimbList.lowerArmFront);
    }
}
```

This is way easier to follow than what we started with and has the bonus of returning LimbBehaviour which will often save on code, because for 75% of the cases you're getting the transform of a limb to get to LimbBehaviour, very rarely will you need an implicit transform reference.
#### LimbList.FindLimbComp()
To follow up on the previous statement, for 24% of the cases you're trying to get another custom script from the limb. This method covers that 24% with the same 3 overload options:
```cs
public static T FindLimbComp<T>(this PersonBehaviour person, string limbType) where T : MonoBehaviour

public static T FindLimbComp<T>(this LimbBehaviour limb, string limbType) where T : MonoBehaviour

public static T FindLimbComp<T>(this CirculationBehaviour circ, string limbType) where T : MonoBehaviour
```
In fact, FindLimb() is a mere shorter way of writing all of these for LimbBehaviour. For all the cases above you can also write
```cs
person.FindLimbComp<LimbBehaviour>(LimbList.lowerArmFront);

var lowerArmFront = limb.FindLimbComp<LimbBehaviour>(LimbList.lowerArmFront);

var lowerArmFront = circ.FindLimbComp<LimbBehaviour>(LimbList.lowerArmFront);
```
Or you can replace LimbBehaviour with the script you're looking for.


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


## PowerPlus (REQUIRES PlusAPI)
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
    if (AbilityEnabled && !Person.FindLimb(LimbList.head).IsCapable)
        ToggleAbilityInt(false);
    else if (!AbilityEnabled && Person.FindLimb(LimbList.head).IsCapable && PowerEnabled)
        ToggleAbilityInt(true);
}

if (PowerActive)
{
    if (!Person.FindLimb(LimbList.head).IsConsideredAlive && PowerEnabled)
        TogglePowerInt(false);
    else if (Person.FindLimb(LimbList.head).IsConsideredAlive && !PowerEnabled)
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
    AbilityEnabled = toggled;
    foreach (Ability ability in Abilities)
    {
        ability.enabled = toggled;
    }
    string toggledString = toggled ? "Enabled" : "Disabled";
    Debug.Log($"Abilities {toggledString}!");
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
The only thing that needs to be done when Power is deleted is deleting the ability classes. Everything else is defined by the abstract class.

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
It also contains the last of Power & Abilities Toggle Conditions: When the limb is separated from the body or if it's dead, it disables the class.

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


## RegenerationPlus (PlusAPI HIGHLY recommended)
### ReanimateLimb()
A method that reanimates a given limb.
```cs
public static void ReanimateLimb(this LimbBehaviour myLimb)
```
It revives the limb and makes it fully functional again. It does things like healing bones, healing bleeding etc., but doesn't do nothing more than that. It doesn't make the entity conscious and doesn't regenerate anything.

### ReviveLimb()
Does almost the same thing as RegenerateLimb() but also removes pain, makes the entity conscious again and gives them max adrenaline
```cs
public static void ReviveLimb(this LimbBehaviour myLimb)
```

### ReanimateLimb()
A method that reanimates a given limb.
```cs
public static void ReanimateLimb(this LimbBehaviour myLimb)
```
It revives the limb and makes it fully functional again. It does things like healing bones, healing bleeding etc., but doesn't do nothing more than that. It doesn't make the entity conscious and doesn't regenerate anything.

### ReviveLimb()
Does almost the same thing as RegenerateLimb() but also removes pain, makes the entity conscious again and gives them max adrenaline
```cs
public static void ReviveLimb(this LimbBehaviour myLimb)
```

I'm sorry that this one isn't as sophisticated as everything else but there just isn't much to say about this one.