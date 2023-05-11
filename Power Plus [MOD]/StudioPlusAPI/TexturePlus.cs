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
            glow.Color = ChangeAlpha(color);
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
            glow.Color = ChangeAlpha(newColor);
        }

        public static void ChangeLightColor(GameObject lightObject, Color newColor)
        {
            lightObject.GetComponent<SpriteRenderer>().color = newColor;
        }

        public static Color ChangeAlpha(Color color, float alpha = 1f)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        public static Color32 ChangeAlpha(Color32 color, byte alpha = 255)
        {
            return new Color32(color.r, color.g, color.b, alpha);
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
                throw new ArgumentException("ReplaceItemSprite's amount of child objects does not match replace textures");
            for (int i = 0; i < childObjects.Length; i++)
            {
                ReplaceItemSprite(item, childObjects[i], childReplaceSprites[i]);
            }
        }

        public static void ReplaceItemSprite(string item, Sprite replaceSprite, string[] childObjects, Sprite[] childReplaceSprites)
        {
            if (childObjects.Length != childReplaceSprites.Length)
                throw new ArgumentException("ReplaceItemSprite's amount of child objects does not match replace textures");
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


        public static float ToFloat(byte value)
        {
            float newValue = (float)value;
            float returnValue = newValue / 255f;
            return Mathf.Clamp01(returnValue);
        }

        public static byte ToByte(float value)
        {
            float newValue = Mathf.Clamp01(value) * 255f;
            return (byte)newValue;
        }
    }
}

