using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.Events;

namespace Mod
{
    public class CustomBodyArmorWearer : BodyArmorWearer
    {
        //This counts as BodyArmorWearer, meaning that it will not stack with other body armor
        public override void Start()
        {
            base.Start();
            //If you want your armor to do something when spawned, you put it in here. It's important that base.Start(); is included
            //You don't have to do this though if you won't be changing anything as shown by BodyArmorWearerin in StudioPlusAPI.cs
        }
    }

    public class AstronautArmorWearer : BodyArmorWearer
    {
        LimbBehaviour limb;
        public void Awake()
        {
            limb = GetComponent<LimbBehaviour>();
        }
        public void Update()
        {
            if (limb.IsConsideredAlive == true && transform.root.GetComponentInChildren<AstronautBackPackWearer>())
            {
                limb.Person.OxygenLevel = 1f;
                if(limb.PhysicalBehaviour.Temperature < limb.BodyTemperature)
                    limb.PhysicalBehaviour.Temperature = limb.BodyTemperature;
            }
        }
    }

    public class AstronautBackPackWearer : ArmorWearer
    {

    }
}