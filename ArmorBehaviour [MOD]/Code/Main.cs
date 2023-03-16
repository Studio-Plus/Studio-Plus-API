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
        public static string tag = " [ArmorB+]";

        public static void Main()
        {
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateArmor("Body/UpperBody", 0.5f);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateArmor("Body/MiddleBody", 0.5f);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateArmor("Body/LowerBody", 0.5f);
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

                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(upperBody), Instance.transform.position + new Vector3(0f, 9f) * ModAPI.PixelSize, true);
                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(middleBody), Instance.transform.position, true);
                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(lowerBody), Instance.transform.position + new Vector3(0f, -9f) * ModAPI.PixelSize, true);

                        UnityEngine.Object.Destroy(Instance);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateArmor("Head", 0.5f, 0);
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
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateClothing("Head", 0);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>("Head", 0.9f, 3);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>("Body/UpperBody", 0.9f, 3);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>("Body/MiddleBody", 0.9f, 3);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>("Body/LowerBody", 0.9f, 3);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>("FrontArm/UpperArmFront", 0.9f, 3, 1);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>("FrontArm/LowerArmFront", 0.9f, 3, 1);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>("BackArm/UpperArm", 0.9f, 3, -1);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>("BackArm/LowerArm", 0.9f, 3, -1);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>("FrontLeg/UpperLegFront", 0.9f, 3, 1);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>("FrontLeg/LowerLegFront", 0.9f, 3, 1);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>("FrontLeg/FootFront", 0.9f, 3, 1);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>("BackLeg/UpperLeg", 0.9f, 3, -1);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>("BackLeg/LowerLeg", 0.9f, 3, -1);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautArmorWearer>("BackLeg/Foot", 0.9f, 3, -1);
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
                        phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin");
                        Instance.GetOrAddComponent<ArmorBehaviour>().CreateCustom<AstronautBackPackWearer>("Body/UpperBody", 0.9f, 2);
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

                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(head), Instance.transform.position, true);

                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(upperBody), Instance.transform.position, true);
                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(middleBody), Instance.transform.position, true);
                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(lowerBody), Instance.transform.position, true);

                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(upperArmFront), Instance.transform.position, true);
                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(lowerArmFront), Instance.transform.position, true);

                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(upperArm), Instance.transform.position, true);
                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(lowerArm), Instance.transform.position, true);

                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(frontLegFront), Instance.transform.position, true);
                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(lowerLegFront), Instance.transform.position, true);
                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(footFront), Instance.transform.position, true);

                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(frontLeg), Instance.transform.position, true);
                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(lowerLeg), Instance.transform.position, true);
                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(foot), Instance.transform.position, true);

                        CreationPlus.SpawnItem(ModAPI.FindSpawnable(backpack), Instance.transform.position, true);

                        UnityEngine.Object.Destroy(Instance);
                    }
                }
            );
        }
    }
}