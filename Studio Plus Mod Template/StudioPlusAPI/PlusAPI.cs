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

//This is a PUBLIC version of StudioPlusAPI by Studio Plus
//Based on version v3.0.0
//The mod author might have added, changed or removed features or might have even changed the name of it.
//StudioPlusAPI is an open-source project gifted to the community, meaning that you can do anything with it
//As long as you don't claim it as your own creation, shown by this mod creator leaving this comment in here.
//Link to the original repository: https://github.com/Studio-Plus/Studio-Plus-API
//API DEPENDENCIES: None

namespace StudioPlusAPI
{
    public struct PlusAPI
    {
        public const float ton = 25f;
        public const float kilogram = 0.025f;
        //For reference, a metal rod weighs about 3 kilograms

        public static void IgnoreEntityCollision(Collider2D main, Collider2D[] others, bool ignColl, bool affectItself = false)
        {
            foreach (Collider2D a in others)
            {
                IgnoreCollisionStackController.IgnoreCollisionSubstituteMethod(main, a, ignColl);
                foreach (Collider2D b in others)
                {
                    if (a && b && a != b && a.transform != b.transform && affectItself)
                        IgnoreCollisionStackController.IgnoreCollisionSubstituteMethod(a, b, ignColl);
                }
            }
        }


        public static void IgnoreCollision(Collider2D main, Collider2D other, bool ignColl)
        {
            IgnoreCollisionStackController.IgnoreCollisionSubstituteMethod(main, other, ignColl);
        }
    }

    public struct LimbList
    {
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

        public static Transform FindLimb(Transform transform, string limbType)
        {
            return transform.Find(limbType);
        }

        public static Transform FindLimb(GameObject gameObject, string limbType)
        {
            return gameObject.transform.Find(limbType);
        }
    }
}

