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
//API DEPENDENCIES: None (PlusAPI.cs highly recommended however)

namespace StudioPlusAPI
{
    public abstract class PowerPlus : MonoBehaviour
    {
        [SkipSerialisation]
        protected LimbBehaviour limb;
        [SkipSerialisation]
        protected PersonBehaviour person;
        [SkipSerialisation]
        public List<MonoBehaviour> abilities = new List<MonoBehaviour>();
        protected bool powerCreated = false;
        protected bool powerEnabled = false;
        protected bool abilityEnabled = false;

        //This is very much a scaffolding, but a useful one

        protected virtual void Awake()
        {
            limb = GetComponent<LimbBehaviour>();
            person = limb.Person;
        }

        protected virtual void Start()
        {
            CreatePower();
        }

        protected virtual void FixedUpdate()
        {
            if (limb.Person.Consciousness < 0.8f && abilityEnabled)
                ToggleAbility(false);
            else if (limb.Person.Consciousness >= 0.8f && !abilityEnabled && powerEnabled)
                ToggleAbility(true);

            if (!person.transform.Find(LimbList.head).GetComponent<LimbBehaviour>().IsConsideredAlive && powerEnabled)
                TogglePower(false);
            else if (person.transform.Find(LimbList.head).GetComponent<LimbBehaviour>().IsConsideredAlive && !powerEnabled)
                TogglePower(true);
        }

        protected void OnDestroy()
        {
            DeletePower();
        }

        //This method adds everything to the entity, like light sprites, strength, ability classes, etc.
        protected virtual void CreatePower()
        {
            powerCreated = true;
            Debug.Log("Power created!");
        }

        //This class turns *abilities* on or off. Main use for when the one with power is knocked unconcsious
        public virtual void ToggleAbility(bool toggle)
        {
            //Use this as the basis of the toggle
            switch (toggle)
            {
                case true:
                    abilityEnabled = true;
                    Debug.Log("Abilities Enabled!");
                    break;
                case false:
                    abilityEnabled = false;
                    Debug.Log("Abilities Disabled!");
                    break;
            }
        }

        //This class turns *the power* on or off, as if the power was never there. Main use for when the one with power is killed, but so he can still be revived.
        public virtual void TogglePower(bool toggle)
        {
            //Use this as the basis of the toggle
            switch (toggle)
            {
                case true:
                    powerEnabled = true;
                    Debug.Log("Power Enabled!");
                    break;
                case false:
                    powerEnabled = false;
                    Debug.Log("Power Disabled!");
                    break;
            }
        }

        protected virtual void DeletePower()
        {

        }
    }
}

