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
    public class BlasterGlove : ArmorBehaviour, Messages.IUse, Messages.IUseContinuous
    {
        [SkipSerialisation]
        private float timer = 0f;
        [SkipSerialisation]
        private readonly float interval = 0.5f;

        protected new void FixedUpdate()
        {
            base.FixedUpdate();
            timer = Mathf.Min(interval, timer + Time.deltaTime);
        }

        public void Use(ActivationPropagation activationPropagation)
        {
            if (timer < interval || !IsAttached || !enabled)
                return;
            Pew();
        }

        public void UseContinuous(ActivationPropagation activationPropagation)
        {
            if (timer < 0.1f || !IsAttached || !enabled)
                return;
            Pew();
        }

        public void Pew()
        {
            timer = 0f;
            GameObject LaserObj = Instantiate(ModAPI.FindSpawnable("Machine Blaster").Prefab.GetComponent<BlasterBehaviour>().Bolt);

            LaserObj.GetComponent<BlasterboltBehaviour>().Speed = LaserObj.GetComponent<BlasterboltBehaviour>().Speed * 2;
            LaserObj.GetComponent<BlasterboltBehaviour>().Trail.startColor = new Color(0f, 1f, 1f, LaserObj.GetComponent<BlasterboltBehaviour>().Trail.startColor.a);
            LaserObj.GetComponent<BlasterboltBehaviour>().Trail.endColor = new Color(0f, 1f, 1f, LaserObj.GetComponent<BlasterboltBehaviour>().Trail.startColor.a);

            LaserObj.transform.position = transform.position;
            GetComponent<PhysicalBehaviour>().PlayClipOnce(ModAPI.FindSpawnable("Machine Blaster").Prefab.GetComponent<BlasterBehaviour>().Clips.PickRandom());

            LaserObj.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90f);
        }
    }

    public class BlasterGloveWearer : BodyArmorWearer
    {
        [SkipSerialisation]
        public GameObject light;
        [SkipSerialisation]
        public LightSprite glow;

        public override void Start()
        {
            base.Start();
            TexturePlus.CreateLightSprite(
                out light,
                ArmorObject.transform,
                UniversalAssets.gloveLight,
                new Vector2(0f, -7.5f) * ModAPI.PixelSize,
                new Color32(0, 255, 255, 63),
                out glow,
                5f,
                0.75f
            );
        }

        protected void FixedUpdate()
        {

                glow.Brightness = UnityEngine.Random.Range(0.65f, 0.85f);
        }

        public void OnDestroy()
        {
            Destroy(light);
            //Destroying Glow.gameObject is unnecessary since it's a child of Light
        }
    }

    public class FirePower : PowerPlus, Messages.IUse
    {
        [SkipSerialisation]
        public GameObject eyeLight;
        [SkipSerialisation]
        public LightSprite eyeGlow;

        [SkipSerialisation]
        public Color powerColor = new Color32(255, 127, 0, 63);

        public void Use(ActivationPropagation a)
        {
            if (PowerActive)
                ForceTogglePower(false);
            else
                ForceTogglePower(true);
        }

        protected override void CreatePower()
        {
            foreach (var limbs in Person.Limbs)
            {
                var phys = limbs.PhysicalBehaviour;

                limbs.DiscomfortingHeatTemperature = float.PositiveInfinity;
                phys.SimulateTemperature = false;

                phys.Properties = UniversalAssets.fireHumanProperties;
            }

            TexturePlus.CreateLightSprite(
                out eyeLight,
                Limb.transform.root.transform.Find(LimbList.head),
                UniversalAssets.eyeLight,
                new Vector2(2.5f, 1.5f) * ModAPI.PixelSize,
                powerColor,
                out eyeGlow
            );
            eyeLight.SetActive(PowerEnabled);

            Abilities.Add(LimbList.FindLimb(Limb.transform, LimbList.lowerArmFront).gameObject.GetOrAddComponent<FireTouch>());
            Abilities.Add(LimbList.FindLimb(Limb.transform, LimbList.lowerArmBack).gameObject.GetOrAddComponent<FireTouch>());
        }
  
        protected override void TogglePower(bool toggled)
        {
            eyeLight.SetActive(toggled);
        }

        protected override void ToggleAbility(bool toggled)
        {
        }

        protected override void DeletePower()
        {
            foreach (var limbs in Person.Limbs)
            {
                var phys = limbs.PhysicalBehaviour;

                limbs.DiscomfortingHeatTemperature = 40f;
                phys.SimulateTemperature = true;

                phys.Properties = UniversalAssets.humanProperties;
            }
        }
    }

    public class FireTouch : Ability
    {
        [SkipSerialisation]
        public GameObject armLight;
        [SkipSerialisation]
        public LightSprite armGlow;
        [SkipSerialisation]
        public Color powerColor = new Color32(255, 127, 0, 31);

        

        public void Start()
        {
            TexturePlus.CreateLightSprite(
                out armLight,
                transform,
                UniversalAssets.powerLight,
                Vector2.zero,
                powerColor,
                out armGlow,
                5f,
                0.75f
            );
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (enabled && gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= 1f)
            {
                var phys = other.transform.GetComponent<PhysicalBehaviour>();
                phys.Temperature += 200f;
                phys.Ignite();
                if (phys.OnFire)
                {
                    phys.BurnIntensity = 1f;
                }
            }
        }

        public override void OnEnable()
        {
            armLight?.SetActive(true);
        }

        public override void OnDisable()
        {
            armLight?.SetActive(false);
        }
    }
}