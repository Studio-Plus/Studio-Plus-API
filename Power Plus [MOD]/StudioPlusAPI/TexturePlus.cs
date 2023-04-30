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

//This is a PUBLIC version of StudioPlusAPI by Studio Plus
//Based on version v3.0.0
//The mod author might have added, changed or removed features or might have even changed the name of it.
//StudioPlusAPI is an open-source project gifted to the community, meaning that you can do anything with it
//As long as you don't claim it as your own creation, shown by this mod creator leaving this comment in here.
//Link to the original repository: https://github.com/Studio-Plus/Studio-Plus-API
//API DEPENDENCIES: None

namespace StudioPlusAPI
{
    public struct TexturePlus
    {
        public static void CreateLightSprite(GameObject lightObject, Transform parentObject, Sprite sprite, Vector2 position, Color color)
        {
            lightObject.transform.SetParent(parentObject);
            lightObject.transform.rotation = parentObject.rotation;
            lightObject.transform.localPosition = position;
            lightObject.transform.localScale = Vector2.one;

            var lightSprite = lightObject.AddComponent<SpriteRenderer>();
            lightSprite.sprite = sprite; //THIS SPRITE NEEDS TO BE FULLY WHITE OR ELSE YOU'RE GONNA GET WEIRD EFFECTS
            lightSprite.material = ModAPI.FindMaterial("VeryBright");

            lightSprite.color = color;
            lightSprite.sortingLayerName = parentObject.GetComponent<SpriteRenderer>().sortingLayerName;
            lightSprite.sortingOrder = parentObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        
        public static void CreateLightSprite(GameObject lightObject, Transform parentObject, Sprite sprite, Vector2 position, Color color, LightSprite glow, float radius = 5f, float brightness = 1.5f)
        {
            lightObject.transform.SetParent(parentObject);
            lightObject.transform.rotation = parentObject.rotation;
            lightObject.transform.localPosition = position;
            lightObject.transform.localScale = Vector2.one;

            var lightSprite = lightObject.AddComponent<SpriteRenderer>();
            lightSprite.sprite = sprite; //THIS SPRITE NEEDS TO BE FULLY WHITE OR ELSE YOU'RE GONNA GET WEIRD EFFECTS
            lightSprite.material = ModAPI.FindMaterial("VeryBright");

            lightSprite.color = color;
            lightSprite.sortingLayerName = parentObject.GetComponent<SpriteRenderer>().sortingLayerName;
            lightSprite.sortingOrder = parentObject.GetComponent<SpriteRenderer>().sortingOrder + 1;

            glow.transform.localPosition = Vector3.zero;
            glow.Color = ConvertToGlowColor(color);
            glow.Radius = radius;
            glow.Brightness = brightness;
        }

        public static LightSprite InstantiateLight(Transform parent)
        {
            var component = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/ModLightPrefab"), parent).GetComponent<LightSprite>();
            return component;
        }


        public static void ChangeLightColor(GameObject lightObject, LightSprite glow, Color newColor)
        {
            lightObject.GetComponent<SpriteRenderer>().color = newColor;
            glow.Color = ConvertToGlowColor(newColor);
        }

        public static void ChangeLightColor(GameObject lightObject, Color newColor)
        {
            lightObject.GetComponent<SpriteRenderer>().color = newColor;
        }

        public static Color ConvertToGlowColor(Color color)
        {
            return new Color(color.r, color.g, color.b, 1f);
        }


        public static void ReplaceItemSprite(string item, Sprite replaceTexture)
        {
            ModAPI.FindSpawnable(item).Prefab.GetComponent<SpriteRenderer>().sprite = replaceTexture;
        }

        public static void ReplaceItemSprite(string item, string childObject, Sprite childReplaceTexture)
        {
            ModAPI.FindSpawnable(item).Prefab.transform.Find(childObject).GetComponent<SpriteRenderer>().sprite = childReplaceTexture;
        }

        public static void ReplaceItemSprite(string item, Sprite replaceTexture, string childObject, Sprite childReplaceTexture)
        {
            ReplaceItemSprite(item, replaceTexture);
            ReplaceItemSprite(item, childObject, childReplaceTexture);
        }

        public static void ReplaceItemSprite(string item, string[] childObjects, Sprite[] childReplaceSprites)
        {
            if (childObjects.Length != childReplaceSprites.Length)
            {
                Debug.LogError("StudioPlusAPI ReplaceItemSprite: Amount of child objects does not match replace textures");
                return;
            }
            for (int i = 0; i < childObjects.Length; i++)
            {
                ReplaceItemSprite(item, childObjects[i], childReplaceSprites[i]);
            }
        }

        public static void ReplaceItemSprite(string item, Sprite replaceSprite, string[] childObjects, Sprite[] childReplaceSprites)
        {
            if (childObjects.Length != childReplaceSprites.Length)
            {
                Debug.LogError("StudioPlusAPI ReplaceItemSprite: Amount of child objects does not match replace textures");
                return;
            }
            ReplaceItemSprite(item, replaceSprite);
            for (int i = 0; i < childObjects.Length; i++)
            {
                ReplaceItemSprite(item, childObjects[i], childReplaceSprites[i]);
            }
        }


        public static void ReplaceViewSprite(string item, Sprite replaceSprite)
        {
            ModAPI.FindSpawnable(item).ViewSprite = replaceSprite;
        }
    }
}

