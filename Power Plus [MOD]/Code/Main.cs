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
    //This will not be released to the workshop because it's only the beginning... the beginning of something big... something that you could call... "Top-tier".
    public class Mod : MonoBehaviour
    {
        public static string modTag = " [Power+]";

        public static void Main()
        {
            ModAPI.FindSpawnable("Human").Prefab.transform.Find("Head").GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            ModAPI.FindSpawnable("Android").Prefab.transform.Find("Head").GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            //Mandatory Studio Plus disclaimer: These 2 things are here so that ArmorBehaviour isn't a mess with head pieces, you're welcome
            //"Might have some unintended side effects that manifest as unexplainable bugs later"
            //-zooi

            UniversalAssets.gloveLight = ModAPI.LoadSprite("Textures/Entities/Blaster glove light.png");
            UniversalAssets.powerLight = ModAPI.LoadSprite("Textures/Entities/Power light.png");
            UniversalAssets.eyeLight = ModAPI.LoadSprite("Textures/Entities/Eye light.png");

            UniversalAssets.humanProperties = ModAPI.FindPhysicalProperties("Human");

            //Each time ModAPI.FindPhysicalProperties is written, a copy of said property is made. Each unique PhysicalProperty you want to make, whether that be of a fire resistant human,
            //of a new sharp object or whatever else where you modify said properties, they should be declared similarly to how they're declared here. Any change I make to
            //UniversalAssets.fireHumanProperties for example will apply to every single entity spawned after said change is made.
            UniversalAssets.fireHumanProperties = ModAPI.FindPhysicalProperties("Human");
            UniversalAssets.fireHumanProperties.Flammability = 0f;
            UniversalAssets.fireHumanProperties.Burnrate = 0f;
            UniversalAssets.fireHumanProperties.BurningTemperatureThreshold = float.PositiveInfinity;

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
                        Instance.GetOrAddComponent<BlasterGlove>().CreateCustom<BlasterGloveWearer>(LimbList.lowerArmFront, 0f, 0);

                    }
                }
            );

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Human"),
                    NameOverride = "Fire Human" + modTag,
                    DescriptionOverride = "Fire punch!",
                    CategoryOverride = ModAPI.FindCategory("Entities"),
                    ThumbnailOverride = ModAPI.LoadSprite("Textures/Views/Fire human view.png"),
                    AfterSpawn = (Instance) =>
                    {
                        var person = Instance.GetComponent<PersonBehaviour>();
                        Instance.transform.Find(LimbList.upperBody).gameObject.GetOrAddComponent<FirePower>();
                    }
                }
            );
        }
    }
}