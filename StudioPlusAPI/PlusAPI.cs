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
using System.Threading;

//StudioPlusAPI is an API for the game people playground, created by Dawid23 Gamer and Studio Plus. It allows for modders to program mods for the game more easily, or at least that's the idea. 
//This API is released under the zlib license, by using it for your mod and/or downloading it you confirm that you read and agreed to the terms of said license.
//Link to the original repository: https://github.com/Studio-Plus/Studio-Plus-API
//API DEPENDENCIES: None

namespace StudioPlusAPI
{
    public struct PlusAPI
    {
        public const float ton = 25f;
        public const float kilogram = 0.025f;
        //For reference, a metal rod weighs about 3 kilograms

        public const float liter = 2.8f;
        public const float syringe = 0.5f * liter;
        public const float flask = liter;
        public const float bloodTank = 5f * liter;

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

        public static float WaveClamp01(float num, float period)
        {
            return -0.5f * Mathf.Cos(num * Mathf.PI / period) + 0.5f;
        }

        public static float WaveClamp(float num, float period, float maxNum)
        {
            if (maxNum == 0)
                throw new ArgumentException("WaveClamp's maxNum cannot equal 0");
            float trueMaxNum = Mathf.Abs(maxNum);
            return trueMaxNum * (-0.5f * Mathf.Cos(num * Mathf.PI / period) + 0.5f);
        }

        public static float WaveClamp(float num, float period, float maxNum, float minNum)
        {
            if (maxNum == minNum)
                throw new ArgumentException("WaveClamp's maxNum and minNum cannot be equal");
            float trueMaxNum = maxNum > minNum ? maxNum : minNum;
            float trueMinNum = maxNum > minNum ? minNum : maxNum;
            return (trueMaxNum - trueMinNum) * (-0.5f * Mathf.Cos(num * Mathf.PI / period) + 0.5f) + trueMinNum;
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
            return transform.root.Find(limbType);
        }

        public static Transform FindLimb(GameObject gameObject, string limbType)
        {
            return gameObject.transform.root.Find(limbType);
        }
    }
}

