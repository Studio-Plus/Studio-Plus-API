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
    public static class RegenerationPlus
    {
        public static void ReviveLimb(this LimbBehaviour myLimb)
        {
            if (myLimb.HasBrain)
            {
                myLimb.Person.SeizureTime = 0.0f;
                myLimb.Person.ShockLevel = 0.0f;
                myLimb.Person.PainLevel = 0.0f;
                myLimb.Person.OxygenLevel = 1f;
                myLimb.Person.AdrenalineLevel = 1f;
                myLimb.Person.Consciousness = 1f;
            }
            myLimb.ReanimateLimb();
        }

        public static void ReanimateLimb(this LimbBehaviour myLimb)
        {
            if (myLimb.HasBrain)
            {
                myLimb.Person.Braindead = false;
                myLimb.Person.BrainDamaged = false;
                myLimb.Person.BrainDamagedTime = 0.0f;
            }
            myLimb.LungsPunctured = false;
            myLimb.HealBone();
            myLimb.Health = myLimb.InitialHealth;
            myLimb.Numbness = 0.0f;
            myLimb.CirculationBehaviour.HealBleeding();
            myLimb.CirculationBehaviour.IsPump = true;
            myLimb.CirculationBehaviour.BloodFlow = 1f;
            myLimb.CirculationBehaviour.AddLiquid(myLimb.GetOriginalBloodType(), Mathf.Max(0.0f, 1f - myLimb.CirculationBehaviour.GetAmount(myLimb.GetOriginalBloodType())));
        }

        public static void RegenerateLimb(this LimbBehaviour myLimb, float regenSpeed, float acidSpeed, float burnSpeed, float rottenSpeed, float woundsEfficiency, bool regenWhenDead = false)
        {
            if (myLimb.PhysicalBehaviour.isDisintegrated)
                return;
            if (myLimb.IsConsideredAlive || regenWhenDead)
            {
                if  (regenSpeed > 0f)
                {
                    myLimb.CirculationBehaviour.IsPump = myLimb.CirculationBehaviour.WasInitiallyPumping;
                    myLimb.Health += myLimb.InitialHealth * regenSpeed * Time.deltaTime;
                }
                if (acidSpeed > 0f)
                    myLimb.HealAcid(acidSpeed);
                if (burnSpeed > 0f)
                    myLimb.HealBurn(burnSpeed);               
                if (rottenSpeed > 0f)
                    myLimb.HealRotten(rottenSpeed);
                if (woundsEfficiency > 0f)
                    myLimb.HealWounds(woundsEfficiency);
            }

        }

        public static void HealAcid(this LimbBehaviour myLimb, float speed = 1f)
        {
            if (myLimb.SkinMaterialHandler.AcidProgress > 1f)
                myLimb.SkinMaterialHandler.AcidProgress = 1f;
            else if (myLimb.SkinMaterialHandler.AcidProgress > 0f)
                myLimb.SkinMaterialHandler.AcidProgress -= Time.deltaTime * speed;
        }

        public static void HealBurn(this LimbBehaviour myLimb, float speed = 1f)
        {
            if (myLimb.PhysicalBehaviour.BurnProgress > 1f)
                myLimb.PhysicalBehaviour.BurnProgress = 1f;
            else if (myLimb.PhysicalBehaviour.BurnProgress > 0f)
                myLimb.PhysicalBehaviour.BurnProgress -= Time.deltaTime * speed;
        }

        public static void HealRotten(this LimbBehaviour myLimb, float speed = 1f)
        {
            if (myLimb.SkinMaterialHandler.RottenProgress > 1f)
                myLimb.SkinMaterialHandler.RottenProgress = 1f;
            else if (myLimb.SkinMaterialHandler.RottenProgress > 0f)
                myLimb.SkinMaterialHandler.RottenProgress -= Time.deltaTime * speed;
        }

        public static void HealWounds(this LimbBehaviour myLimb, float efficiency = 1f)
        {
            float trueEfficiency = Mathf.Clamp01(efficiency);
            if (UnityEngine.Random.value < trueEfficiency)
            {
                myLimb.CirculationBehaviour.HealBleeding();
            }
            myLimb.BruiseCount = 0;
            myLimb.CirculationBehaviour.GunshotWoundCount = 0;
            myLimb.CirculationBehaviour.StabWoundCount = 0;
            float factor = Mathf.Pow(1f - trueEfficiency, Time.fixedDeltaTime);
            for (int index = 0; index < myLimb.SkinMaterialHandler.damagePoints.Length; ++index)
                myLimb.SkinMaterialHandler.damagePoints[index].z *= factor;
            myLimb.SkinMaterialHandler.Sync();
        }
    }
}

