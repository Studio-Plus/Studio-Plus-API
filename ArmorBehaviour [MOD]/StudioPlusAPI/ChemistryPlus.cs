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

//StudioPlusAPI is an API for the game people playground, created by Dawid23 Gamer and Studio Plus. It allows for modders to program mods for the game more easily, or at least that's the idea. 
//This API is released under the zlib license, by using it for your mod and/or downloading it you confirm that you read and agreed to the terms of said license.
//Link to the original repository: https://github.com/Studio-Plus/Studio-Plus-API
//API DEPENDENCIES: None

namespace StudioPlusAPI
{
    //Special thanks to pjstatt12 for creating AddLiquidToItem and the original LiquidReaction and TripleLiquidReaction!
    public struct ChemistryPlus
    {
        public static void AddLiquidToItem(string item, string newLiquidID, float amount)
        {
            ModAPI.FindSpawnable(item).Prefab.AddComponent<FlaskBehaviour>();
            ModAPI.FindSpawnable(item).Prefab.GetComponent<FlaskBehaviour>().Capacity = amount;
            ModAPI.FindSpawnable(item).Prefab.GetComponent<FlaskBehaviour>().StartLiquid = new BloodContainer.SerialisableDistribution
            {
                LiquidID = newLiquidID,
                Amount = amount
            };           
        }

        public static void AddLiquidToItem(string item, string newLiquidID, float amount, float capacity)
        {
            if (amount > capacity)
            {
                Debug.LogError("StudioPlusAPI AddLiquidToItem(): Amount is larger than capacity may allow");
                return;
            }
            ModAPI.FindSpawnable(item).Prefab.AddComponent<FlaskBehaviour>();
            ModAPI.FindSpawnable(item).Prefab.GetComponent<FlaskBehaviour>().Capacity = capacity;
            ModAPI.FindSpawnable(item).Prefab.GetComponent<FlaskBehaviour>().StartLiquid = new BloodContainer.SerialisableDistribution
            {
                LiquidID = newLiquidID,
                Amount = amount
            };
        }

        public static void LiquidReaction(string liquid1, string liquid2, string target, float ratePerSecond = 0.05f)
        {
            var mixer = new LiquidMixInstructions(
                Liquid.GetLiquid(liquid1),
                Liquid.GetLiquid(liquid2),
                Liquid.GetLiquid(target),
                ratePerSecond);

            LiquidMixingController.MixInstructions.Add(mixer);
        }

        public static void LiquidReaction(string liquid1, string liquid2, string liquid3, string target, float ratePerSecond = 0.05f)
        {
            var mixer = new LiquidMixInstructions(
                Liquid.GetLiquid(liquid1),
                Liquid.GetLiquid(liquid2),
                Liquid.GetLiquid(liquid3),
                Liquid.GetLiquid(target),
                ratePerSecond);

            LiquidMixingController.MixInstructions.Add(mixer);
        }

        public static void LiquidReaction(Liquid[] ingredientLiquids, Liquid target, float ratePerSecond = 0.05f)
        {
            var mixer = new LiquidMixInstructions(ingredientLiquids, target, ratePerSecond);
            LiquidMixingController.MixInstructions.Add(mixer);
        }
    }
}

