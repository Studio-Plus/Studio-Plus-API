# StudioPlusAPI
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
        ThumbnailOverride = ModAPI.LoadSprite("Textures/Views/View.png"),
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