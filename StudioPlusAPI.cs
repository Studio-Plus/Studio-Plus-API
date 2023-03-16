using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.Events;

//This is the OFFICIAL version of StudioPlusAPI by Studio Plus, used by all Studio Plus Mods
//Current API version: v2.0.0
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
        //StudioPlusAPI.AddLiquidToItem("Rotor","OIL",0.28f);


        public static void LiquidReaction(string FirstLiquid, string SecondLiquid, string TargetLiquid, float ratePerSecond = 0.05f)
        {
            var mixer = new LiquidMixInstructions(
                Liquid.GetLiquid(FirstLiquid),
                Liquid.GetLiquid(SecondLiquid),
                Liquid.GetLiquid(TargetLiquid),
                ratePerSecond);

            LiquidMixingController.MixInstructions.Add(mixer);
        }
        //StudioPlusAPI.LiquidReaction("LIFE SERUM", "TRITIUM", "INSTANT DEATH POISON");

        //Alternatively:
        //ChemistryAPI.LiquidReaction(LifeSyringe.LifeSerumLiquid.ID, Chemistry.Tritium.ID, DeathSyringe.InstantDeathPoisonLiquid.ID);

        public static void TripleLiquidReaction(string FirstLiquid, string SecondLiquid, string ThirdLiquid, string TargetLiquid, float ratePerSecond = 0.05f)
        {
            var mixer = new LiquidMixInstructions(
                Liquid.GetLiquid(FirstLiquid),
                Liquid.GetLiquid(SecondLiquid),
                Liquid.GetLiquid(ThirdLiquid),
                Liquid.GetLiquid(TargetLiquid),
                ratePerSecond);

            LiquidMixingController.MixInstructions.Add(mixer);
        }
        //ChemistryAPI.TripleLiquidReaction("SUGAR","SPICE","EVERYTHING NICE","THE PERFECT LITTLE GIRL");

        //Alternatively:
        //ChemistryAPI.TripleLiquidReaction(Sugar.ID, Spice.ID, EverythingNice.ID, PerfectLittleGirl.ID);

        public static void InfiniteLiquidReaction(Liquid[] IngredientLiquids, Liquid TargetLiquid, float ratePerSecond = 0.05f)
        {
            var mixer = new LiquidMixInstructions(IngredientLiquids, TargetLiquid, ratePerSecond);
            LiquidMixingController.MixInstructions.Add(mixer);
        }
        //StudioPlusAPI.InfiniteLiquidReaction(
        //    new Liquid[]
        //    {
        //        Liquid.GetLiquid("SUGAER"),
        //        Liquid.GetLiquid("SPICE"),
        //        Liquid.GetLiquid("EVERYTHING NICE"),
        //        Liquid.GetLiquid(Chemistry.Tritium.ID)
        //    }, 
        //    Liquid.GetLiquid("POWERPUFF GIRLS")
        //);
    }

    public struct TexturePlus
    {
        public static void CreateLightSprite(GameObject LightObject, Transform parentObject, Sprite lightSprite, Vector2 position, Color color, float scale = 1f)
        {
            LightObject.transform.SetParent(parentObject);
            LightObject.transform.rotation = parentObject.rotation;
            LightObject.transform.localPosition = position;
            LightObject.transform.localScale = new Vector3(scale, scale); //Generally you don't have to touch this one but I give you this option in case you have to for some reason.

            var LightSprite = LightObject.AddComponent<SpriteRenderer>();
            LightSprite.sprite = lightSprite; //THIS SPRITE NEEDS TO BE FULLY WHITE OR ELSE YOU'RE GONNA GET WEIRD EFFECTS
            LightSprite.material = ModAPI.FindMaterial("VeryBright");

            LightSprite.color = color;
            LightSprite.sortingLayerName = parentObject.GetComponent<SpriteRenderer>().sortingLayerName;
            LightSprite.sortingOrder = parentObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        //StudioPlusAPI.CreateLightSprite(
        //    new GameObject("Glow"),
        //    Instance.transform,
        //    ModAPI.LoadSprite("Textures/SomeLightSpriteLOL.png"),
        //    Vector2.zero,
        //    new Color(1f, 0f, 0f, 0.5f)
        //);
        //This will create a red glowing shape (defined by your light sprite) at half brightness in the middle of your item

        //Alternatively:
        //StudioPlusAPI.CreateLightSprite(
        //    new GameObject("Glow"),
        //    Instance.transform,
        //    ModAPI.LoadSprite("Textures/SomeLightSpriteLOL.png"),
        //    Vector2.zero,
        //    new Color32(255, 0, 0, 127)
        //);

        public static void ReplaceItemSprite(string ItemToReplace, Sprite ReplaceTexture)
        {
            ModAPI.FindSpawnable(ItemToReplace).Prefab.GetComponent<SpriteRenderer>().sprite = ReplaceTexture;
        }
        //StudioPlusAPI.ReplaceItemSpriteOfChild("Sword", ModAPI.LoadSprite("Upscaled Sword.png"));

        public static void ReplaceItemSpriteOfChild(string ItemToReplace, string ChildObject, Sprite ReplaceTexture)
        {
            ModAPI.FindSpawnable(ItemToReplace).Prefab.transform.Find(ChildObject).GetComponent<SpriteRenderer>().sprite = ReplaceTexture;
        }
        //This one is specifically for e.g. axe:
        //StudioPlusAPI.ReplaceItemSpriteOfChild("Axe","Axe handle/Axe head", ModAPI.LoadSprite("Futuristic Axe Head.png"));
        //Where do you get this path from? From here: https://www.studiominus.nl/ppg-modding/gameAssets.html

        public static void ReplaceViewSprite(string ItemToReplace, Sprite ReplaceTexture)
        {
            ModAPI.FindSpawnable(ItemToReplace).ViewSprite = ReplaceTexture;
        }
        //StudioPlusAPI.ReplaceItemSpriteOfChild("Sword", ModAPI.LoadSprite("Upscaled Sword View.png"));

        public static void ReplaceSprites(string ItemToReplace, Sprite ItemReplaceTexture, Sprite ViewReplaceTexture)
        {
            ModAPI.FindSpawnable(ItemToReplace).Prefab.GetComponent<SpriteRenderer>().sprite = ItemReplaceTexture;
            ModAPI.FindSpawnable(ItemToReplace).ViewSprite = ViewReplaceTexture;
        }
        //Does ReplaceItemSprite and ReplaceViewSprite at once:
        //StudioPlusAPI.ReplaceItemSpriteOfChild("Sword", ModAPI.LoadSprite("Upscaled Sword.png"), ModAPI.LoadSprite("Upscaled Sword View.png"));
    }


    public struct CreationPlus
    {
        public static void SpawnItem(SpawnableAsset item, Vector2 position = default(Vector2), bool spawnSpawnParticles = false)
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
        //StudioPlusAPI.CreateHingeJoint(gameObject, myObject, new Vector2(0f, -4.5f), -45f, 45f);


        public static void CreateFixedJoint(GameObject main, GameObject other)
        {
            FixedJoint2D joint = main.AddComponent<FixedJoint2D>();
            joint.dampingRatio = 1;
            joint.frequency = 0;
            joint.connectedBody = other.GetComponent<Rigidbody2D>();
        }
        //StudioPlusAPI.CreateFixedJoint(gameObject, myObject);
    }


    public struct PlusAPI
    {
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

    public class ArmorBehaviour : MonoBehaviour
    {
        LimbBehaviour limb;
        PhysicalBehaviour phys;
        public string limbType;
        bool equipped = false;
        public float stabResistance;
        public int defaultSortingOrder;
        public int armorSortingOrder;
        Transform parent;
        Collider2D[] limbColliders;
        List<Collider2D> otherArmorColliders = new List<Collider2D>();
        GameObject attachedLimb;
        Type armorWearerType;
        public bool wasSpawned = false;

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

        //What follows this are 120 lines of delicate code that if altered could break everything.
        //Change anything at your own risk
        //(There is still stuff you can mess with down below though)
        public void Start()
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

        public void Update()
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

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent<LimbBehaviour>(out limb))
            {
                foreach (ArmorBehaviour armor in transform.root.GetComponentsInChildren<ArmorBehaviour>())
                {
                    if (armor.equipped == true || limb.transform.root.Find(armor.limbType).gameObject.GetComponent(armorWearerType))
                        return;
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

        public void OnTriggerExit2D(Collider2D other)
        {
            GetComponent<Collider2D>().isTrigger = false;
        }

        public void Attach(GameObject limbObject)
        {
            phys = GetComponent<PhysicalBehaviour>();
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
            armorWearer.armorObject = gameObject;

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

        private System.Collections.IEnumerator ArmorCollision() 
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

        public void OnDestroy()
        {
            if(attachedLimb != null)
                Destroy(attachedLimb.GetComponent(armorWearerType));
        }
    }

    public class ArmorWearer : MonoBehaviour
    {
        public bool armorExists = false;
        public string armorName;
        public GameObject armorObject;

        public virtual void Start()
        {
            if (armorExists)
                armorExists = false;
            else
            {
                CreationPlus.SpawnItem(ModAPI.FindSpawnable(armorName), transform.position);
                Destroy(this);
            }
        }
    }
    //IMPORTANT: ALL OF YOUR ARMORWEARER CLASSES MUST BE DERIVED FROM ArmorWearer, DO NOT USE IT DIRECTLY UNLESS YOU KNOW WHAT YOU'RE DOING

    public class BodyArmorWearer : ArmorWearer
    {
    }

    public class ClothingWearer : ArmorWearer
    {
    }
}

