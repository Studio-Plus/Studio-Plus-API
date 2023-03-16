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

        //I'm going to take my time to once again show how exactly you use TexturePlus.ReplaceItemSpriteOfChild()
        //Not really worth making this into a mod but you should get a rough idea
        //Let's say you're making a futuristic texture pack, and you want to change the texture of the axe head to "Future Axe Head.png" in Textures/Malee.
        //Here is how you do it:

        public static void Main()
        {
            TexturePlus.ReplaceItemSpriteOfChild("Axe", "Axe handle/Axe head", ModAPI.LoadSprite("Textures/Malee/Future Axe Head.png"));
            //This will change the texture of the VANILLA AXE to your desired texture.
            //Note that this will not work for changing the texture of the axe head in your mod.
            //If you're wondering, this is the line you have to put into AfterSpawn(Instance) to edit axe head texture (I think):
            //Instance.transform.Find("Axe Handle/Axe Head").GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Malee/Future Axe Head.png");
        }
    }
}