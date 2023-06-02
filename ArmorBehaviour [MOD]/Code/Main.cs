using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.Events;
using StudioPlusAPI;

namespace Mod
{
    public class Mod
    {
        public static string tag = " [Armor+]";

        public static void Main()
        {
            ModAPI.FindSpawnable("Human").Prefab.transform.Find("Head").GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            ModAPI.FindSpawnable("Android").Prefab.transform.Find("Head").GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            //Mandatory Studio Plus disclaimer: These 2 things are here so that ArmorBehaviour isn't a mess with head pieces, you're welcome
            //"Might have some unintended side effects that manifest as unexplainable bugs later"
            //-zooi

            UniversalAssets.armorProperties = ModAPI.FindPhysicalProperties("Bowling pin");
            //Fun fact: Only one instance of PhysicalProperties exists for each one of them. Changing them directly through phys.Properties.whatever = 0f; is a disaster.
            //What this does is make a copy of the Bowling pin properties and stores it for later use to avoid the issue of say making all humans resistant to fire (*cough*, Human Tiers, *cough*).
            //Writing ModAPI.FindPhyscialProperties() every single time you change properties creates a new copy of them each time, which is a memory leak.

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Body Armor (Upper Body)" + tag,
                    NameToOrderByOverride = "Z12",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/ArmorVestUpper.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateBodyArmor(LimbList.upperBody, 0.5f);
                    }
                }
            );


            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Body Armor (Middle Body)" + tag,
                    NameToOrderByOverride = "Z13",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/ArmorVestMiddle.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateBodyArmor(LimbList.middleBody, 0.5f);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Body Armor (Lower Body)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/ArmorVestLower.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateBodyArmor(LimbList.lowerBody, 0.5f);
                    }
                }
            );

            //It allows you to spawn in multiple armor pieces with 1 item.
            //Beware that doing that will result in there being potentially dozen of items added by the mod that would be searchable which would screw you up a bit.
            //zooi might or might not add in a thing that will help out with that, but for now: Use this one at your own risk.
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Brick"),
                    NameOverride = "Body Armor" + tag,
                    NameToOrderByOverride = "Z11",
                    DescriptionOverride = "Protect your human!",
                    CategoryOverride = ModAPI.FindCategory("Entities"),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Views/Body Armor View.png"),
                    AfterSpawn = (Instance) =>
                    {
                        UnityEngine.Object.Destroy(Instance.GetComponent<SpriteRenderer>());
                        UnityEngine.Object.Destroy(Instance.GetComponent<PhysicalBehaviour>());
                        UnityEngine.Object.Destroy(Instance.GetComponent<Collider2D>());
                        UnityEngine.Object.Destroy(Instance.GetComponent<Rigidbody2D>());

                        string upperBody = "Body Armor (Upper Body)" + tag;
                        string middleBody = "Body Armor (Middle Body)" + tag;
                        string lowerBody = "Body Armor (Lower Body)" + tag;

                        GameObject[] armorPieces = new GameObject[]
                        {
                            ModAPI.FindSpawnable(upperBody).SpawnItem(Instance.transform, new Vector3(0f, 9f) * ModAPI.PixelSize, true),
                            ModAPI.FindSpawnable(middleBody).SpawnItem(Instance.transform, Vector3.zero, true),
                            ModAPI.FindSpawnable(lowerBody).SpawnItem(Instance.transform, new Vector3(0f, -9f) * ModAPI.PixelSize, true)
                        };
                        Instance.GetOrAddComponent<CreationPlus.InformalChildren>().childrenObjects.AddRange(armorPieces);
                        
                    }
                }
            );

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
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateBodyArmor(LimbList.head, 0.5f);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Sunglasses" + tag,
                    NameToOrderByOverride = "Z2",
                    DescriptionOverride = "Makes your human look cool!",
                    CategoryOverride = ModAPI.FindCategory("Entities"),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Views/Sunglasses View.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Sunglasses.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = ModAPI.FindPhysicalProperties("Glass");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateClothing(LimbList.head);
                    }
                }
            );

            //Special thanks to Kelly The Dragon!#0773 for allowing me to use this texture to showcase the power of my ArmorBheaviour!

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Head)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit Head.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>(LimbList.head, 0.9f, 3);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Upper Body)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit UpperBody.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>(LimbList.upperBody, 0.9f, 3);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Middle Body)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit MidBody.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>(LimbList.middleBody, 0.9f, 3);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Lower Body)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit LowerBody.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>(LimbList.lowerBody, 0.9f, 3);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Upper Arm Front)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit UpperArmFront.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>(LimbList.upperArmFront, 0.9f, 3);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Lower Arm Front)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit LowerArmFront.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>(LimbList.lowerArmFront, 0.9f, 3);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Upper Arm)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit UpperArm.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>(LimbList.upperArmBack, 0.9f, 3);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Lower Arm)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit LowerArm.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>(LimbList.lowerArmBack, 0.9f, 3);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Upper Leg Front)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit UpperLeg.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>(LimbList.upperLegFront, 0.9f, 3);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Lower Leg Front)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit LowerLeg.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>(LimbList.lowerLegFront, 0.9f, 3);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Foot Front)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit Foot.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>(LimbList.footFront, 0.9f, 3);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Upper Leg)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit UpperLeg.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>(LimbList.upperLegBack, 0.9f, 3);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Lower Leg)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit LowerLeg.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>(LimbList.lowerLegBack, 0.9f, 3);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Foot)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit Foot.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>(LimbList.footBack, 0.9f, 3);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Cosmonaut Suit (Backpack)" + tag,
                    NameToOrderByOverride = "Z14",
                    DescriptionOverride = "Please ignore, it's only here to make armor possible.\nThank you.",
                    CategoryOverride = ModAPI.FindCategory(""),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Mod/Thumbnail.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Cosmonaut Suit Backpack.png");
                        Instance.FixColliders();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = UniversalAssets.armorProperties;
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautBackPackWearer>(LimbList.upperBody, 0.9f, 2);
                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Brick"),
                    NameOverride = "Cosmonaut Suit (Tungsten Mod)" + tag,
                    NameToOrderByOverride = "Z3",
                    DescriptionOverride = "Protect your human from space! \n<b><color=#ff0000>WARNING: WEARING THE BACKPACK IS REQUIRED IF YOU WANT TO SURVIVE</color></b>. \n(Texture by Kelly the Dragon from Tungeten Mod)",
                    CategoryOverride = ModAPI.FindCategory("Entities"),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Views/Astronaut Suit View.png"),
                    AfterSpawn = (Instance) =>
                    {
                        UnityEngine.Object.Destroy(Instance.GetComponent<SpriteRenderer>());
                        UnityEngine.Object.Destroy(Instance.GetComponent<PhysicalBehaviour>());
                        UnityEngine.Object.Destroy(Instance.GetComponent<Collider2D>());
                        UnityEngine.Object.Destroy(Instance.GetComponent<Rigidbody2D>());

                        string head = "Cosmonaut Suit (Head)" + tag;

                        string upperBody = "Cosmonaut Suit (Upper Body)" + tag;
                        string middleBody = "Cosmonaut Suit (Middle Body)" + tag;
                        string lowerBody = "Cosmonaut Suit (Lower Body)" + tag;

                        string upperArmFront = "Cosmonaut Suit (Upper Arm Front)" + tag;
                        string lowerArmFront = "Cosmonaut Suit (Lower Arm Front)" + tag;

                        string upperArm = "Cosmonaut Suit (Upper Arm)" + tag;
                        string lowerArm = "Cosmonaut Suit (Lower Arm)" + tag;

                        string frontLegFront = "Cosmonaut Suit (Upper Leg Front)" + tag;
                        string lowerLegFront = "Cosmonaut Suit (Lower Leg Front)" + tag;
                        string footFront = "Cosmonaut Suit (Foot Front)" + tag;

                        string frontLeg = "Cosmonaut Suit (Upper Leg)" + tag;
                        string lowerLeg = "Cosmonaut Suit (Lower Leg)" + tag;
                        string foot = "Cosmonaut Suit (Foot)" + tag;

                        string backpack = "Cosmonaut Suit (Backpack)" + tag;

                        GameObject[] armorPieces = new GameObject[]
                        {
                            ModAPI.FindSpawnable(head).SpawnItem(Instance.transform, Vector3.zero, true),

                            ModAPI.FindSpawnable(upperBody).SpawnItem(Instance.transform, Vector3.zero, true),
                            ModAPI.FindSpawnable(middleBody).SpawnItem(Instance.transform, Vector3.zero, true),
                            ModAPI.FindSpawnable(lowerBody).SpawnItem(Instance.transform, Vector3.zero, true),

                            ModAPI.FindSpawnable(upperArmFront).SpawnItem(Instance.transform, Vector3.zero, true),
                            ModAPI.FindSpawnable(lowerArmFront).SpawnItem(Instance.transform, Vector3.zero, true),

                            ModAPI.FindSpawnable(upperArm).SpawnItem(Instance.transform, Vector3.zero, true),
                            ModAPI.FindSpawnable(lowerArm).SpawnItem(Instance.transform, Vector3.zero, true),

                            ModAPI.FindSpawnable(frontLegFront).SpawnItem(Instance.transform, Vector3.zero, true),
                            ModAPI.FindSpawnable(lowerLegFront).SpawnItem(Instance.transform, Vector3.zero, true),
                            ModAPI.FindSpawnable(footFront).SpawnItem(Instance.transform, Vector3.zero, true),

                            ModAPI.FindSpawnable(frontLeg).SpawnItem(Instance.transform, Vector3.zero, true),
                            ModAPI.FindSpawnable(lowerLeg).SpawnItem(Instance.transform, Vector3.zero, true),
                            ModAPI.FindSpawnable(foot).SpawnItem(Instance.transform, Vector3.zero, true),

                            ModAPI.FindSpawnable(backpack).SpawnItem(Instance.transform, Vector3.zero, true),
                        };
                        Instance.GetOrAddComponent<CreationPlus.InformalChildren>().childrenObjects.AddRange(armorPieces);
                    }
                }
            );
        }
    }
}