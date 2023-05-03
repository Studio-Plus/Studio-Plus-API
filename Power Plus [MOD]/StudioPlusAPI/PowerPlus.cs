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
        public LimbBehaviour Limb { get; protected set; }
        [SkipSerialisation]
        public PersonBehaviour Person { get; protected set; }
        [SkipSerialisation]
        public List<Ability> Abilities { get; protected set; } = new List<Ability>();
        public bool PowerEnabled { get; protected set; } = false;
        public bool AbilityEnabled { get; protected set; } = false;

        public bool PowerActive { get; protected set; } = false;
        public bool AbilityActive { get; protected set; } = false;

        public bool IsCreated { get; protected set; } = false;


        protected virtual void Awake()
        {
            Limb = GetComponent<LimbBehaviour>();
            Person = Limb.Person;
        }

        protected virtual void Start()
        {
            CreatePowerInt();
        }

        protected virtual void FixedUpdate()
        {
            if (!PowerActive && !AbilityActive)
                return;

            if (AbilityActive)
            {
                if (Person.Consciousness < 0.8f && AbilityEnabled)
                    ToggleAbilityInt(false);
                else if (Person.Consciousness >= 0.8f && !AbilityEnabled && PowerEnabled)
                    ToggleAbilityInt(true);
            }

            if (PowerActive)
            {
                if (!Person.transform.Find(LimbList.head).GetComponent<LimbBehaviour>().IsConsideredAlive && PowerEnabled)
                    TogglePowerInt(false);
                else if (Person.transform.Find(LimbList.head).GetComponent<LimbBehaviour>().IsConsideredAlive && !PowerEnabled)
                    TogglePowerInt(true);
            }
        }

        protected void OnDestroy()
        {
            foreach (Ability ability in Abilities)
            {
                Destroy(ability);
            }
            DeletePower();
        }

        //This method adds everything to the entity, like light sprites, strength, ability classes, etc.
        protected void CreatePowerInt()
        {       
            if (!IsCreated)
            {
                PowerActive = true;
                AbilityActive = true;
                Debug.Log("Power created!");
            }
            CreatePower();
            IsCreated = true;
        }

        //This class turns *the power* on or off, as if the power was never there. Main use for when the one with power is killed, but so he can still be revived.
        protected void TogglePowerInt(bool toggled)
        {
            switch (toggled)
            {
                case true:
                    PowerEnabled = toggled;
                    Debug.Log("Power Enabled!");
                    break;
                case false:
                    PowerEnabled = toggled;                  
                    Debug.Log("Power Disabled!");                   
                    break;
            }
            TogglePower(toggled);
            ToggleAbilityInt(toggled);
        }

        //This class turns *abilities* on or off. Main use for when the one with power is knocked unconcsious
        protected void ToggleAbilityInt(bool toggled)
        {
            switch (toggled)
            {
                case true:
                    AbilityEnabled = toggled;
                    foreach (Ability ability in Abilities)
                    {
                        ability.enabled = toggled;
                    }
                    Debug.Log("Abilities Enabled!");
                    break;
                case false:
                    AbilityEnabled = toggled;
                    foreach (Ability ability in Abilities)
                    {
                        ability.enabled = toggled;
                    }
                    Debug.Log("Abilities Disabled!");
                    break;
            }
            ToggleAbility(toggled);
        }

        protected abstract void CreatePower();

        protected abstract void TogglePower(bool toggled);

        protected abstract void ToggleAbility(bool toggled);

        protected abstract void DeletePower();

        public void ForceTogglePower(bool toggled)
        {
            PowerActive = toggled;
            AbilityActive = toggled;
            if (toggled || !PowerEnabled)
                return;
            TogglePowerInt(toggled);
        }

        public void ForceToggleAbility(bool toggled)
        {
            AbilityActive = toggled;
            if (toggled || !AbilityEnabled)
                return;
            ToggleAbilityInt(toggled);
        }
    }

    public abstract class Ability : MonoBehaviour
    {
        [SkipSerialisation]
        public LimbBehaviour Limb { get; protected set; }
        [SkipSerialisation]
        public PersonBehaviour Person { get; protected set; }

        protected void Awake()
        {
            Limb = GetComponent<LimbBehaviour>();
            Person = Limb.Person;
        }

        public virtual void FixedUpdate()
        {
            if (!Limb.NodeBehaviour.IsConnectedToRoot && enabled)
            {
                enabled = false;
            }
        }

        public abstract void OnEnable();

        public abstract void OnDisable();
    }
}