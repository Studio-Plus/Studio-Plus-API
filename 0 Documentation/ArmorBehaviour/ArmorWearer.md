# StudioPlusAPI
## ArmorBehaviour (REQUIRES CreationPlus and PlusAPI)
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