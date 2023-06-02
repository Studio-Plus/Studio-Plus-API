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
    public static class ChemistryPlus
    {
        public static void AddLiquidToItem(this SpawnableAsset item, string newLiquidID, float amount)
        {
            item.Prefab.AddComponent<FlaskBehaviour>();
            item.Prefab.GetComponent<FlaskBehaviour>().Capacity = amount;
            item.Prefab.GetComponent<FlaskBehaviour>().StartLiquid = new BloodContainer.SerialisableDistribution
            {
                LiquidID = newLiquidID,
                Amount = amount
            };           
        }

        public static void AddLiquidToItem(this SpawnableAsset item, string newLiquidID, float amount, float capacity)
        {
            if (amount > capacity)
                throw new ArgumentException("AddLiquidToItem: Amount cannot exceed capacity!");
            item.Prefab.AddComponent<FlaskBehaviour>();
            item.Prefab.GetComponent<FlaskBehaviour>().Capacity = capacity;
            item.Prefab.GetComponent<FlaskBehaviour>().StartLiquid = new BloodContainer.SerialisableDistribution
            {
                LiquidID = newLiquidID,
                Amount = amount
            };
        }

        public static void LiquidReaction(string input1, string input2, string target, float ratePerSecond = 0.05f)
        {
            LiquidMixInstructions mixer = new LiquidMixInstructions(
                Liquid.GetLiquid(input1),
                Liquid.GetLiquid(input2),
                Liquid.GetLiquid(target),
                ratePerSecond);

            LiquidMixingController.MixInstructions.Add(mixer);
        }

        public static void LiquidReaction(string input1, string input2, string input3, string target, float ratePerSecond = 0.05f)
        {
            LiquidMixInstructions mixer = new LiquidMixInstructions(
                Liquid.GetLiquid(input1),
                Liquid.GetLiquid(input2),
                Liquid.GetLiquid(input3),
                Liquid.GetLiquid(target),
                ratePerSecond);

            LiquidMixingController.MixInstructions.Add(mixer);
        }

        public static void LiquidReaction(Liquid[] inputs, Liquid target, float ratePerSecond = 0.05f)
        {
            LiquidMixInstructions mixer = new LiquidMixInstructions(inputs, target, ratePerSecond);
            LiquidMixingController.MixInstructions.Add(mixer);
        }

        public static void LiquidReaction(Liquid[] inputs, Liquid[] targets, float ratePerSecond = 0.05f)
        {
            float minRate = 0.02f * targets.Length;
            ratePerSecond = minRate < ratePerSecond ? ratePerSecond : minRate;
            foreach (Liquid target in targets)
            {
                LiquidMixInstructions mixer = new LiquidMixInstructions(inputs, target, ratePerSecond / targets.Length);
                LiquidMixingController.MixInstructions.Add(mixer);
            }
        }

        public static void LiquidReaction(Liquid[] inputs, Liquid[] targets, int[] ratios, float ratePerSecond = 0.05f)
        {
            if (targets.Length != ratios.Length)
            {
                string errorType = targets.Length < ratios.Length ? "targets" : "ratios";
                throw new ArgumentException($"LiquidReaction: Not enough {errorType} elements!");
            }
            foreach (int num in ratios)
            {
                if (num < 1)
                    throw new ArgumentException("LiquidReaction: No ratio can be 0 or less!");
            }
            int divisor = PlusAPI.Sum(ratios);
            float minRate = 0.02f * divisor / Mathf.Min(ratios);
            ratePerSecond = minRate < ratePerSecond ? ratePerSecond : minRate;
            for (int i = 0; i < targets.Length; i++)
            {
                float multiplier = (float)ratios[i] / divisor;
                LiquidMixInstructions mixer = new LiquidMixInstructions(inputs, targets[i], ratePerSecond * multiplier);
                LiquidMixingController.MixInstructions.Add(mixer);
            }
        }


        public static PointLiquidTransferBehaviour AddBottleOpening(this BloodContainer container, Vector2 position, Space outerSpace = Space.Self)
        {
            PointLiquidTransferBehaviour cupOpening = container.gameObject.AddComponent<PointLiquidTransferBehaviour>();
            cupOpening.Point = position;
            cupOpening.Space = outerSpace;
            cupOpening.Layers = ModAPI.FindSpawnable("Bottle").Prefab.GetComponent<PointLiquidTransferBehaviour>().Layers;
            return cupOpening;
        }
    }
}

