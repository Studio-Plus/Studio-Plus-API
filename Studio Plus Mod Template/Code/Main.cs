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
    public class Mod : MonoBehaviour
    {
        public static string tag = " [TEMPLATE]";

        public static void Main()
        {
            ModAPI.FindSpawnable("Human").Prefab.transform.Find("Head").GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            ModAPI.FindSpawnable("Android").Prefab.transform.Find("Head").GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            //Mandatory Studio Plus disclaimer: These 2 things are here so that ArmorBehaviour isn't a mess with head pieces, you're welcome
            //"Might have some unintended side effects that manifest as unexplainable bugs later"
            //-zooi

            //ModAPI.Register(
            //    new Modification()
            //    {
            //        OriginalItem = ModAPI.FindSpawnable("Rod"),
            //        NameOverride = "placeholder item" + ModTag,
            //        DescriptionOverride = "It's 6 am and theres a fly in my room and I'm scared of it.",
            //        CategoryOverride = ModAPI.FindCategory("Entities"),
            //        ThumbnailOverride = ModAPI.LoadSprite("sprites/placeholder.png"),
            //        AfterSpawn = (Instance) =>
            //        {
            //            ModAPI.Notify("This line will run once the item is spawned.");
            //        }
            //    }
            //);
        }
    }
}