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

//This is the OFFICIAL version of StudioPlusAPI by Studio Plus, used by all Studio Plus Mods
//Current API version: v2.1.0
//StudioPlusAPI is open-source project gifted to the community, meaning you can do anything with it
//As long as you don't claim it as your own creation
//Link to the repository: https://github.com/Studio-Plus/Studio-Plus-API

//This is the UNOFFICIAL version of StudioPlusAPI by Studio Plus
//The mod author might have added, changed or removed features or might have even changed the name of it.
//StudioPlusAPI is an open-source project gifted to the community, meaning that you can do anything with it
//As long as you don't claim it as your own creation, shown by this mod creator leaving this comment in here.
//Link to the original repository: https://github.com/Studio-Plus/Studio-Plus-API

namespace StudioPlusAPI
{

    //Special thanks to pjstatt12 for creating AddLiquidToItem, LiquidReaction and TripleLiquidReaction!
    public struct ChemistryPlus
    {
        public static void AddLiquidToItem(string ExistingItem, string NewLiquidID, float LiquidAmount)
        {
            ModAPI.FindSpawnable(ExistingItem).Prefab.AddComponent<FlaskBehaviour>();
            ModAPI.FindSpawnable(ExistingItem).Prefab.GetComponent<FlaskBehaviour>().StartLiquid = new BloodContainer.SerialisableDistribution
            {
                LiquidID = NewLiquidID,
                Amount = LiquidAmount
            };
        }
        /*
        ChemistryPlus.AddLiquidToItem("Rotor","OIL",0.28f);
        */


        public static void LiquidReaction(string FirstLiquid, string SecondLiquid, string TargetLiquid, float ratePerSecond = 0.05f)
        {
            var mixer = new LiquidMixInstructions(
                Liquid.GetLiquid(FirstLiquid),
                Liquid.GetLiquid(SecondLiquid),
                Liquid.GetLiquid(TargetLiquid),
                ratePerSecond);

            LiquidMixingController.MixInstructions.Add(mixer);
        }
        /*
        ChemistryPlus.LiquidReaction("LIFE SERUM", "TRITIUM", "INSTANT DEATH POISON");
        */

        //Alternatively:
        /*
        ChemistryPlus.LiquidReaction(LifeSyringe.LifeSerumLiquid.ID, Chemistry.Tritium.ID, DeathSyringe.InstantDeathPoisonLiquid.ID);
        */

        public static void LiquidReaction(string FirstLiquid, string SecondLiquid, string ThirdLiquid, string TargetLiquid, float ratePerSecond = 0.05f)
        {
            var mixer = new LiquidMixInstructions(
                Liquid.GetLiquid(FirstLiquid),
                Liquid.GetLiquid(SecondLiquid),
                Liquid.GetLiquid(ThirdLiquid),
                Liquid.GetLiquid(TargetLiquid),
                ratePerSecond);

            LiquidMixingController.MixInstructions.Add(mixer);
        }
        /*
        ChemistryPlus.LiquidReaction("SUGAR","SPICE","EVERYTHING NICE","THE PERFECT LITTLE GIRL");
        */

        //Alternatively:
        /*
        ChemistryPlus.TripleLiquidReaction(Sugar.ID, Spice.ID, EverythingNice.ID, PerfectLittleGirl.ID);
        */

        public static void LiquidReaction(Liquid[] IngredientLiquids, Liquid TargetLiquid, float ratePerSecond = 0.05f)
        {
            var mixer = new LiquidMixInstructions(IngredientLiquids, TargetLiquid, ratePerSecond);
            LiquidMixingController.MixInstructions.Add(mixer);
        }
        /*
        ChemistryPlus.LiquidReaction(
            new Liquid[]
            {
                Liquid.GetLiquid("SUGAER"),
                Liquid.GetLiquid("SPICE"),
                Liquid.GetLiquid("EVERYTHING NICE"),
                Liquid.GetLiquid(Chemistry.Tritium.ID)
            }, 
            Liquid.GetLiquid("POWERPUFF GIRLS")
        );
        */
    }

    public struct TexturePlus
    {
        public static void CreateLightSprite(GameObject LightObject, Transform parentObject, Sprite sprite, Vector2 position, Color color)
        {
            LightObject.transform.SetParent(parentObject);
            LightObject.transform.rotation = parentObject.rotation;
            LightObject.transform.localPosition = position;
            LightObject.transform.localScale = Vector2.one;

            var lightSprite = LightObject.AddComponent<SpriteRenderer>();
            lightSprite.sprite = sprite; //THIS SPRITE NEEDS TO BE FULLY WHITE OR ELSE YOU'RE GONNA GET WEIRD EFFECTS
            lightSprite.material = ModAPI.FindMaterial("VeryBright");

            lightSprite.color = color;
            lightSprite.sortingLayerName = parentObject.GetComponent<SpriteRenderer>().sortingLayerName;
            lightSprite.sortingOrder = parentObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        //Following examples assume:
        //Whatever : MonoBehaviour
        //{
        //    public GameObject Light;
        //    public LightSprite Glow;
        //}
        /*
        TexturePlus.CreateLightSprite(
            Light = new GameObject("Light"),
            Instance.transform,
            ModAPI.LoadSprite("Textures/SomeLightSpriteLOL.png"),
            Vector2.zero,
            new Color(1f, 0f, 0f, 0.5f)
        );
        */
        //This will create a red glowing shape (defined by your light sprite) at half brightness in the middle of your item


        public static void CreateLightSprite(GameObject LightObject, Transform parentObject, Sprite sprite, Vector2 position, Color color, LightSprite Glow, float radius = 5f, float brightness = 1.5f)
        {
            LightObject.transform.SetParent(parentObject);
            LightObject.transform.rotation = parentObject.rotation;
            LightObject.transform.localPosition = position;
            LightObject.transform.localScale = Vector2.one;

            var lightSprite = LightObject.AddComponent<SpriteRenderer>();
            lightSprite.sprite = sprite; //THIS SPRITE NEEDS TO BE FULLY WHITE OR ELSE YOU'RE GONNA GET WEIRD EFFECTS
            lightSprite.material = ModAPI.FindMaterial("VeryBright");

            lightSprite.color = color;
            lightSprite.sortingLayerName = parentObject.GetComponent<SpriteRenderer>().sortingLayerName;
            lightSprite.sortingOrder = parentObject.GetComponent<SpriteRenderer>().sortingOrder + 1;

            Glow.transform.localPosition = Vector3.zero;
            Glow.Color = ConvertToGlowColor(color);
            Glow.Radius = radius;
            Glow.Brightness = brightness;
        }
        /*
        //TexturePlus.CreateLightSprite(
            Light = new GameObject("Light"),
            Instance.transform,
            ModAPI.LoadSprite("Textures/SomeLightSpriteLOL.png"),
            Vector2.zero,
            new Color32(255, 0, 0, 127),
            Glow = Instantiate(Resources.Load<GameObject>("Prefabs/ModLightPrefab"), Light.transform).GetComponent<LightSprite>(),
            5f,
            0.75f
        );
        */
        //Does almost the same thing like the version above but different.
        //On top of that this will also create a glow sprite of the same color with default radius and half brightness (default is 1.5f)
        //Note that you need the entire Glow = 

        public static void ChangeLightColor(GameObject LightObject, LightSprite glow, Color newColor)
        {
            LightObject.GetComponent<SpriteRenderer>().color = newColor;
            glow.Color = ConvertToGlowColor(newColor);
        }
        //Assmuming the same class variables from before:
        /*
        TexturePlus.CreateLightSprite(
            Light,
            Glow,
            new Color32(255, 255, 0, 127),
        );
        */
        //Will change color from red to yellow. Note that this does not affect glow brightness, you can do that without API.
        //(glow.Brightness = 1000f;)
        public static void ChangeLightColor(GameObject LightObject, Color newColor)
        {
            LightObject.GetComponent<SpriteRenderer>().color = newColor;
        }
        //This version opts out of the Glow change
        //No idea why you'd need this but here you go.

        public static Color ConvertToGlowColor(Color color)
        {
            return new Color(color.r, color.g, color.b, 1f);
        }

        public static void ReplaceItemSprite(string ItemToReplace, Sprite ReplaceTexture)
        {
            ModAPI.FindSpawnable(ItemToReplace).Prefab.GetComponent<SpriteRenderer>().sprite = ReplaceTexture;
        }
        /*
        TexturePlus.ReplaceItemSprite("Sword", ModAPI.LoadSprite("Upscaled Sword.png"));
        */

        public static void ReplaceItemSprite(string ItemToReplace, string ChildObject, Sprite ReplaceTexture)
        {
            ModAPI.FindSpawnable(ItemToReplace).Prefab.transform.Find(ChildObject).GetComponent<SpriteRenderer>().sprite = ReplaceTexture;
        }
        //This one is specifically for e.g. axe:
        /*
        TexturePlus.ReplaceItemSpriteOfChild("Axe","Axe handle/Axe head", ModAPI.LoadSprite("Futuristic Axe Head.png"));
        */
        //Where do you get this path from? From here: https://www.studiominus.nl/ppg-modding/gameAssets.html

        public static void ReplaceViewSprite(string ItemToReplace, Sprite ReplaceTexture)
        {
            ModAPI.FindSpawnable(ItemToReplace).ViewSprite = ReplaceTexture;
        }
        /*
        TexturePlus.ReplaceItemSpriteOfChild("Sword", ModAPI.LoadSprite("Upscaled Sword View.png"));
        */


        public static void ReplaceItemSprite(string ItemToReplace, string[] ChildObjects, Sprite[] ReplaceTextures)
        {
            if (ChildObjects.Length != ReplaceTextures.Length)
            {
                Debug.LogError("StudioAPI ReplaceItemSprite: amount of child objects does not match replace textures");
                return;
            }
            for (int i = 0; i < ChildObjects.Length; i++)
            {
                ReplaceItemSprite(ItemToReplace, ChildObjects[i], ReplaceTextures[i]);
            }
        }
        //If you have to replace multiple child object textures, this one is for you!
        /*
        TexturePlus.ReplaceItemSprite(
            "Some Item"
            new string[]
            {
                "Some Item/Child 1",
                "Some Item/Child 2"
            }, 
            new string[]
            {
                "Some Item/Child 1",
                "Some Item/Child 2"
            },
        );
        */


        public static void ReplaceItemSprite(string ItemToReplace, Sprite ReplaceTexture, string[] ChildObjects, Sprite[] ReplaceTextures)
        {
            if (ChildObjects.Length != ReplaceTextures.Length)
            {
                Debug.LogError("StudioAPI ReplaceItemSprite: amount of child objects does not match replace textures");
                return;
            }
            for (int i = 0; i < ChildObjects.Length; i++)
            {
                ReplaceItemSprite(ItemToReplace, ChildObjects[i], ReplaceTextures[i]);
            }
        }


        public static void ReplaceSprites(string ItemToReplace, Sprite ItemReplaceTexture, Sprite ViewReplaceTexture)
        {
            ModAPI.FindSpawnable(ItemToReplace).Prefab.GetComponent<SpriteRenderer>().sprite = ItemReplaceTexture;
            ModAPI.FindSpawnable(ItemToReplace).ViewSprite = ViewReplaceTexture;
        }

        //Does ReplaceItemSprite and ReplaceViewSprite at once:
        /*
        TexturePlus.ReplaceItemSpriteOfChild("Sword", ModAPI.LoadSprite("Upscaled Sword.png"), ModAPI.LoadSprite("Upscaled Sword View.png"));
        */
    }


    public struct CreationPlus
    {
        public static void SpawnItem(SpawnableAsset item, Vector2 position = default, bool spawnSpawnParticles = false)
        {
            GameObject spawnedItem = UnityEngine.Object.Instantiate(item.Prefab, position, Quaternion.identity);
            PhysicalBehaviour phys = spawnedItem.GetComponent<PhysicalBehaviour>();
            phys.SpawnSpawnParticles = spawnSpawnParticles;
            spawnedItem.AddComponent<AudioSourceTimeScaleBehaviour>();
            spawnedItem.AddComponent<TexturePackApplier>();
            spawnedItem.AddComponent<SerialiseInstructions>().OriginalSpawnableAsset = item;
            spawnedItem.name = item.name;
            CatalogBehaviour.PerformMod(item, spawnedItem);
        }

        public static void SpawnItemAsChild(SpawnableAsset item, GameObject Parent, Vector2 position, bool spawnSpawnParticles = false)
        {
            GameObject spawnedItem = UnityEngine.Object.Instantiate(item.Prefab, Parent.transform.position, Quaternion.identity);
            spawnedItem.transform.SetParent(Parent.transform);
            spawnedItem.transform.position = position;
            PhysicalBehaviour phys = spawnedItem.GetComponent<PhysicalBehaviour>();
            phys.SpawnSpawnParticles = spawnSpawnParticles;
            spawnedItem.AddComponent<AudioSourceTimeScaleBehaviour>();
            spawnedItem.AddComponent<TexturePackApplier>();
            spawnedItem.AddComponent<SerialiseInstructions>().OriginalSpawnableAsset = item;
            spawnedItem.name = item.name;
            CatalogBehaviour.PerformMod(item, spawnedItem);
        }
        //These 2 are somewhat hard to explain so I can't go into detail here, sorry


        public static void CreateHingeJoint(GameObject main, GameObject other, Vector2 position, float minDeg, float maxDeg)
        {
            HingeJoint2D joint = main.AddComponent<HingeJoint2D>();
            joint.connectedBody = other.GetComponent<Rigidbody2D>();
            joint.anchor = position;
            JointAngleLimits2D limits = joint.limits;
            limits.min = minDeg;
            limits.max = maxDeg;
            joint.limits = limits;
            joint.useLimits = true;
        }
        //CreationPlus.CreateHingeJoint(gameObject, myObject, new Vector2(0f, -4.5f), -45f, 45f);


        public static void CreateFixedJoint(GameObject main, GameObject other)
        {
            FixedJoint2D joint = main.AddComponent<FixedJoint2D>();
            joint.dampingRatio = 1;
            joint.frequency = 0;
            joint.connectedBody = other.GetComponent<Rigidbody2D>();
        }
        //CreationPlus.CreateFixedJoint(gameObject, myObject);
    }


    public struct PlusAPI
    {
        public const float gram = 0.025f;
        public const float kilogram = 1000f * gram;
        //For reference, a metal rod weighs about 3 grams.

        public static void ToggleEntityCollision(Collider2D main, Collider2D[] others, bool ignColl, bool affectItself = false)
        {
            foreach (Collider2D a in others)
            {
                IgnoreCollisionStackController.IgnoreCollisionSubstituteMethod(main, a, ignColl);
                foreach (Collider2D b in others)
                {
                    if ((bool)a && (bool)b && a != b && a.transform != b.transform && affectItself == true)
                        IgnoreCollisionStackController.IgnoreCollisionSubstituteMethod(a, b, ignColl);
                }
            }
        }


        public static void ToggleCollision(Collider2D main, Collider2D other, bool ignColl)
        {
            IgnoreCollisionStackController.IgnoreCollisionSubstituteMethod(main, other, ignColl);
        }
        //This is basically a short-hand version of the actual PPG method because I won't be typing IgnoreCollisionStackController.IgnoreCollisionSubstituteMethod every time I want to do something with collisions
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
        public const string lpperLegFront = "FrontLeg/LowerLegFront";
        public const string footFront = "FrontLeg/FootFront";

        public const string upperLegBack =  "BackLeg/UpperLeg";
        public const string lowerLegBack = "BackLeg/LowerLeg";
        public const string footBack = "BackLeg/Foot";

        public const string upperArm = upperArmBack;
        public const string lowerArm = upperArmBack;

        public const string upperLeg = upperLegBack;
        public const string lowerLeg = lowerLegBack;
        public const string foot = footBack;
    }

    public class ArmorBehaviour : MonoBehaviour
    {
        [SkipSerialisation]
        protected PhysicalBehaviour phys;
        [SkipSerialisation]
        protected string limbType;
        [SkipSerialisation]
        protected float stabResistance;
        [SkipSerialisation]
        protected int defaultSortingOrder;
        [SkipSerialisation]
        protected int armorSortingOrder;
        protected bool equipped = false; //This one is not accessible and is turned false 5 seconds after detachment
        public bool isAttached = false; //This one is accessible and is turned false immediately after detachment
        //Why the names like this? Can't be changed cuz serialisation.
        protected Collider2D[] limbColliders;
        protected List<Collider2D> otherArmorColliders = new List<Collider2D>();
        protected GameObject attachedLimb;
        protected Type armorWearerType;

        public void CreateArmor(string newLimbType, float newStabResistance, int newDefSortingOrder = 0)
        {
            CreateCustom<BodyArmorWearer>(newLimbType, newStabResistance, 3, newDefSortingOrder);
        }

        public void CreateClothing(string newLimbType, int newDefSortingOrder = 0)
        {
            CreateCustom<ClothingWearer>(newLimbType, 0f, 2, newDefSortingOrder);
        }

        public void CreateCustom<T>(string newLimbType, float newStabResistance, int newArmorSorOrd, int newDefSortingOrder = 0) where T : ArmorWearer
        {
            limbType = newLimbType; //The limb that the armor will attach to (List below)
            stabResistance = Mathf.Clamp01(newStabResistance); //How likely is it for the armor to be penetrated by sharp objects? 1f = full protection, 0f = no protection
            armorSortingOrder = newArmorSorOrd; //In what order will the armor be rendered? For Studio Plus Mods, the minimum number is 2 (clothing), but 1 would also be okay
            defaultSortingOrder = newDefSortingOrder; //set this to 1 for Front arms and legs, -1 for back arms and legs and leave it at 0 for everything else
            GetComponent<SpriteRenderer>().sortingOrder = defaultSortingOrder;
            armorWearerType = typeof(T); //What type of armor is it? (Mod adds Armor and Clothing by default which have their own shorthand methods above)
        }
        //Short list of all limbs to enter into newLimbType (For humans and androids only, fuck gorses.):
        //"Head"
        //"Body/UpperBody"
        //"Body/MiddleBody"
        //"Body/LowerBody"
        //"FrontArm/UpperArmFront"
        //"FrontArm/LowerArmFront"
        //"BackArm/UpperArm"
        //"BackArm/LowerArm"
        //"FrontLeg/UpperLegFront"
        //"FrontLeg/LowerLegFront"
        //"FrontLeg/FootFront"
        //"BackLeg/UpperLeg"
        //"BackLeg/LowerLeg"
        //"BackLeg/Foot"
        //You can use LimbList for convinience

        //What follows this are 120 lines of delicate code that if altered could break everything.
        //Change anything at your own risk
        //(There is still stuff you can mess with down below though)
        protected void Start()
        {
            phys = GetComponent<PhysicalBehaviour>();
            phys.ContextMenuOptions.Buttons.Add(
                new ContextMenuButton(
                    () => equipped == true,
                    "detachArmor",
                    "Detach armor",
                    "Detach multiple armor pieces",
                    () =>
                    {
                        Detach();
                    }
                )
            );
        }

        protected void Update()
        {
            foreach (Collider2D collider in otherArmorColliders)
            {
                if (collider == null)
                    return;
                var armorPiece = collider.gameObject.GetComponent<ArmorBehaviour>();
                if (equipped == true || armorWearerType == armorPiece.armorWearerType || limbType == armorPiece.limbType)
                    PlusAPI.ToggleCollision(GetComponent<Collider2D>(), collider, true);
                else
                {
                    PlusAPI.ToggleCollision(GetComponent<Collider2D>(), collider, false);
                }
            }
            if (equipped == true && attachedLimb == null)
            {
                equipped = false;
            }
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out LimbBehaviour limb))
            {
                foreach (ArmorBehaviour armor in transform.root.GetComponentsInChildren<ArmorBehaviour>())
                {
                    foreach (var armorWearer in limb.transform.root.Find(armor.limbType).gameObject.GetComponents<ArmorWearer>())
                    {
                        if (armorWearer.GetType().IsSubclassOf(armorWearerType) || armorWearer.GetType() == armorWearerType || armorWearer.GetType().IsAssignableFrom(armorWearerType) || equipped == true)
                            return;
                    }
                    armor.Attach(limb.transform.root.Find(armor.limbType).gameObject);
                }
            }
            else if (collision.transform.TryGetComponent<ArmorBehaviour>(out ArmorBehaviour armor))
            {
                otherArmorColliders.Add(collision.collider);
            }
            else
            {
                if (UnityEngine.Random.value > stabResistance && equipped == true && collision.transform.GetComponent<PhysicalBehaviour>().Properties && collision.transform.GetComponent<PhysicalBehaviour>().Properties.Sharp == true)
                {
                    GetComponent<Collider2D>().isTrigger = true;
                }
            }
        }

        protected void OnTriggerExit2D(Collider2D other)
        {
            GetComponent<Collider2D>().isTrigger = false;
        }

        public void Attach(GameObject limbObject)
        {
            phys = GetComponent<PhysicalBehaviour>();
            isAttached = true;
            equipped = true;
            attachedLimb = limbObject;
            GetComponent<SpriteRenderer>().sortingLayerName = attachedLimb.GetComponent<SpriteRenderer>().sortingLayerName;
            GetComponent<SpriteRenderer>().sortingOrder = attachedLimb.GetComponent<SpriteRenderer>().sortingOrder + armorSortingOrder;
            GetComponent<Rigidbody2D>().isKinematic = true;
            limbColliders = attachedLimb.transform.root.GetComponentsInChildren<Collider2D>();
            PlusAPI.ToggleEntityCollision(GetComponent<Collider2D>(), limbColliders, true);

            ArmorWearer armorWearer = attachedLimb.AddComponent(armorWearerType) as ArmorWearer;
            armorWearer.armorExists = true;
            armorWearer.armorName = transform.root.gameObject.name;
            armorWearer.armorObject = this;

            transform.SetParent(attachedLimb.transform);
            transform.rotation = attachedLimb.transform.rotation;
            transform.localPosition = Vector2.zero;
            transform.localScale = Vector2.one;

            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.dampingRatio = 1;
            joint.frequency = 0;
            joint.connectedBody = attachedLimb.GetComponent<Rigidbody2D>();
            GetComponent<Rigidbody2D>().isKinematic = false;

            foreach (GripBehaviour grip in attachedLimb.transform.root.GetComponentsInChildren<GripBehaviour>())
            {
                grip.RefreshNoCollide(false);
                grip.CollidersToIgnore.Add(GetComponent<Collider2D>());
                grip.RefreshNoCollide(true);
            }
        }

        public void Detach()
        {
            isAttached = false;
            GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            GetComponent<SpriteRenderer>().sortingOrder = defaultSortingOrder;
            Destroy(GetComponent<FixedJoint2D>());
            Destroy(attachedLimb.GetComponent(armorWearerType));
            transform.SetParent(null);
            foreach (GripBehaviour grip in attachedLimb.transform.root.GetComponentsInChildren<GripBehaviour>())
            {
                grip.CollidersToIgnore.Remove(GetComponent<Collider2D>());
            }
            StartCoroutine(ArmorCollision());
        }

        protected IEnumerator ArmorCollision()
        {
            yield return new WaitForSeconds(5f);
            if (attachedLimb != null)
            {
                PlusAPI.ToggleEntityCollision(GetComponent<Collider2D>(), limbColliders, false);
                foreach (GripBehaviour grip in attachedLimb.transform.root.GetComponentsInChildren<GripBehaviour>())
                {
                    grip.RefreshNoCollide(false);
                    grip.RefreshNoCollide(true);
                }
                attachedLimb = null;
            }
            equipped = false;

        }

        protected void OnDestroy()
        {
            if (attachedLimb != null)
                Destroy(attachedLimb.GetComponent(armorWearerType));
        }
    }

    public abstract class ArmorWearer : MonoBehaviour
    {
        protected internal bool armorExists = false;
        protected internal string armorName;
        public ArmorBehaviour armorObject;

        public virtual void Start()
        {
            if (armorExists)
                armorExists = false;
            else
            {
                CreationPlus.SpawnItem(ModAPI.FindSpawnable(armorObject.gameObject.name), transform.position);
                Destroy(this);
            }
        }
    }

    public class BodyArmorWearer : ArmorWearer
    {
    }

    public class ClothingWearer : ArmorWearer
    {
    }


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
            if (powerCreated)
            {
                //if (!limb.IsConsideredAlive && powerEnabled)
                //    TogglePower(false);
                //else if (limb.IsConsideredAlive && !powerEnabled)
                //    TogglePower(true);

                if (!person.transform.Find(LimbList.head).GetComponent<LimbBehaviour>().IsConsideredAlive && powerEnabled)
                    TogglePower(false);
                else if (person.transform.Find(LimbList.head).GetComponent<LimbBehaviour>().IsConsideredAlive && !powerEnabled)
                    TogglePower(true);
            }

            if (powerEnabled)
            {
                if (limb.Person.Consciousness < 0.8f && abilityEnabled)
                    ToggleAbility(false);
                else if (limb.Person.Consciousness >= 0.8f && !abilityEnabled)
                    ToggleAbility(true);
            }

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

