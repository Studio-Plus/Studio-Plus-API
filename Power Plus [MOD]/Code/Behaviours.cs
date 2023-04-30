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
        private float interval = 0.5f;

        protected new void FixedUpdate()
        {
            base.FixedUpdate();
            timer = Mathf.Min(interval, timer + Time.deltaTime);
        }

        public void Use(ActivationPropagation activationPropagation)
        {
            if (timer < interval || !isAttached || !enabled)
                return;
            Pew();
        }

        public void UseContinuous(ActivationPropagation activationPropagation)
        {
            if (timer < 0.1f || !isAttached || !enabled)
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
                light = new GameObject("Glow"),
                armorObject.transform,
                UniversalAssets.gloveLight,
                new Vector2(0f, -7.5f) * ModAPI.PixelSize,
                new Color32(0, 255, 255, 63),
                glow = TexturePlus.InstantiateLight(light.transform),
                5f,
                0.75f
            );
        }

        protected void FixedUpdate()
        {
            if (glow != null)
                glow.Brightness = UnityEngine.Random.Range(0.65f, 0.85f);
        }

        public void OnDestroy()
        {
            Destroy(light);
            //Destroying Glow.gameObject is unnecessary since it's a child of Light
        }
    }

    public class FirePower : PowerPlus
    {
        [SkipSerialisation]
        public GameObject eyeLight;
        [SkipSerialisation]
        public LightSprite eyeGlow;

        [SkipSerialisation]
        public Color powerColor = new Color32(255, 127, 0, 63);

        public bool eyeActive = true;


        protected override void CreatePower()
        {
            base.CreatePower();

            foreach (var limbs in person.Limbs)
            {
                var phys = limbs.PhysicalBehaviour;

                limbs.DiscomfortingHeatTemperature = float.PositiveInfinity;
                phys.SimulateTemperature = false;

                phys.Properties = UniversalAssets.humanProperties;
                phys.Properties.Flammability = 0f;
                phys.Properties.Burnrate = 0f;
                phys.Properties.BurningTemperatureThreshold = float.PositiveInfinity;
            }

            TexturePlus.CreateLightSprite(
                eyeLight = new GameObject("Light"),
                limb.transform.root.transform.Find(LimbList.head),
                UniversalAssets.eyeLight,
                new Vector2(2.5f, 1.5f) * ModAPI.PixelSize,
                powerColor,
                eyeGlow = TexturePlus.InstantiateLight(eyeLight.transform)
            );
            eyeLight.SetActive(eyeActive);

            abilities.Add(LimbList.FindLimb(limb.transform.root, LimbList.lowerArmFront).gameObject.GetOrAddComponent<FireTouch>());
            abilities.Add(LimbList.FindLimb(limb.transform.root, LimbList.lowerArmBack).gameObject.GetOrAddComponent<FireTouch>());
        }
  
        public override void ToggleAbility(bool toggle)
        {
            //Use this as the basis of the toggle
            switch (toggle)
            {
                case true:
                    abilityEnabled = toggle;
                    Debug.Log("Abilities Enabled!");
                    foreach (FireTouch ability in abilities)
                    {
                        ability.enabled = toggle;
                    }
                    break;
                case false:
                    abilityEnabled = toggle;
                    Debug.Log("Abilities Disabled!");
                    foreach (FireTouch ability in abilities)
                    {
                        ability.enabled = toggle;
                    }
                    break;
            }
        }

        public override void TogglePower(bool toggle)
        {
            switch (toggle)
            {
                case true:
                    powerEnabled = toggle;
                    Debug.Log("Power Enabled!");
                    ToggleAbility(toggle);
                    eyeLight.SetActive(toggle);
                    eyeActive = toggle;
                    break;
                case false:
                    powerEnabled = toggle;
                    Debug.Log("Power Disabled!");
                    ToggleAbility(toggle);
                    eyeLight.SetActive(toggle);
                    eyeActive = toggle;
                    break;
            }
        }

        protected override void DeletePower()
        {
            foreach (var limbs in person.Limbs)
            {
                var phys = limbs.PhysicalBehaviour;

                limbs.DiscomfortingHeatTemperature = 40f;
                phys.SimulateTemperature = true;

                phys.Properties = UniversalAssets.humanProperties;
            }

            foreach (FireTouch ability in abilities)
            {
                Destroy(ability);
            }
        }
    }

    public class FireTouch : MonoBehaviour
    {
        [SkipSerialisation]
        public GameObject armLight;
        [SkipSerialisation]
        public LightSprite armGlow;
        [SkipSerialisation]
        public LimbBehaviour limb;
        [SkipSerialisation]
        public Color powerColor = new Color32(255, 127, 0, 31);

        public void Awake()
        {
            limb = GetComponent<LimbBehaviour>();
        }

        public void Start()
        {
            TexturePlus.CreateLightSprite(
                armLight = new GameObject("ArmLight"),
                transform,
                UniversalAssets.powerLight,
                Vector2.zero,
                powerColor,
                armGlow = TexturePlus.InstantiateLight(armLight.transform),
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

        public void Update()
        {
            if (!limb.NodeBehaviour.IsConnectedToRoot && enabled)
            {
                enabled = false;
            }
        }

        public void OnEnable()
        {
            if (armLight != null)
                armLight.SetActive(true);
        }

        public void OnDisable()
        {
            if (armLight != null)
                armLight.SetActive(false);
        }
    }
}