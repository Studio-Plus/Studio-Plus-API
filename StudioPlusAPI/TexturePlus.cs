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
using System.Reflection;

//StudioPlusAPI is an API for the game people playground, created by Dawid23 Gamer and Studio Plus. It allows for modders to program mods for the game more easily, or at least that's the idea. 
//This API is released under the zlib license, by using it for your mod and/or downloading it you confirm that you read and agreed to the terms of said license.
//Link to the original repository: https://github.com/Studio-Plus/Studio-Plus-API
//API DEPENDENCIES: None

namespace StudioPlusAPI
{
    public static class TexturePlus
    {
        public static void CreateLightSprite(out GameObject lightObject, Transform parentObject, Sprite sprite, Vector2 position, Color color, bool activate = true)
        {
            lightObject = new GameObject("Light");
            lightObject.transform.SetParent(parentObject);
            lightObject.transform.rotation = parentObject.rotation;
            lightObject.transform.localPosition = position;
            lightObject.transform.localScale = Vector2.one;

            var lightSprite = lightObject.AddComponent<SpriteRenderer>();
            lightSprite.sprite = sprite; //This sprite should be a shade of white, or else the re-coloring will act weird
            lightSprite.material = ModAPI.FindMaterial("VeryBright");

            lightSprite.color = color;
            lightSprite.sortingLayerName = parentObject.GetComponent<SpriteRenderer>().sortingLayerName;
            lightSprite.sortingOrder = parentObject.GetComponent<SpriteRenderer>().sortingOrder + 1;

            lightObject.SetActive(activate);
        }
        
        public static void CreateLightSprite(out GameObject lightObject, Transform parentObject, Sprite sprite, Vector2 position, Color color, out LightSprite glow, float radius = 5f, float brightness = 1.5f, bool activate = true)
        {
            lightObject = new GameObject("Light");
            lightObject.transform.SetParent(parentObject);
            lightObject.transform.rotation = parentObject.rotation;
            lightObject.transform.localPosition = position;
            lightObject.transform.localScale = Vector2.one;

            var lightSprite = lightObject.AddComponent<SpriteRenderer>();
            lightSprite.sprite = sprite; //This sprite should be a shade of white, or else the re-coloring will act weird
            lightSprite.material = ModAPI.FindMaterial("VeryBright");

            lightSprite.color = color;
            lightSprite.sortingLayerName = parentObject.GetComponent<SpriteRenderer>().sortingLayerName;
            lightSprite.sortingOrder = parentObject.GetComponent<SpriteRenderer>().sortingOrder + 1;

            glow = ModAPI.CreateLight(lightObject.transform, color.ChangeAlpha(255), radius, brightness);
            glow.transform.localPosition = Vector3.zero;

            lightObject.SetActive(activate);
        }

        public static void ChangeLightColor(GameObject lightObject, LightSprite glow, Color newColor)
        {
            lightObject.GetComponent<SpriteRenderer>().color = newColor;
            glow.Color = newColor.ChangeAlpha(1f);
        }

        public static void ChangeLightColor(GameObject lightObject, Color newColor)
        {
            lightObject.GetComponent<SpriteRenderer>().color = newColor;
        }

        public static void ReplaceItemSprite(this SpawnableAsset item, Sprite replaceTexture)
        {
            item.Prefab.GetComponent<SpriteRenderer>().sprite = replaceTexture;
        }

        public static void ReplaceItemSprite(this SpawnableAsset item, string childObject, Sprite childReplaceTexture)
        {
            item.Prefab.transform.Find(childObject).GetComponent<SpriteRenderer>().sprite = childReplaceTexture;
        }

        public static void ReplaceItemSprite(this SpawnableAsset item, Sprite replaceTexture, string childObject, Sprite childReplaceTexture)
        {
            item.ReplaceItemSprite(replaceTexture);
            item.ReplaceItemSprite(childObject, childReplaceTexture);
        }

        public static void ReplaceItemSprite(this SpawnableAsset item, string[] childObjects, Sprite[] childReplaceSprites)
        {
            if (childObjects.Length != childReplaceSprites.Length)
                throw new ArgumentException("ReplaceItemSprite: Amount of child objects does not match replace textures");
            for (int i = 0; i < childObjects.Length; i++)
            {
                item.ReplaceItemSprite(childObjects[i], childReplaceSprites[i]);
            }
        }

        public static void ReplaceItemSprite(this SpawnableAsset item, Sprite replaceSprite, string[] childObjects, Sprite[] childReplaceSprites)
        {
            if (childObjects.Length != childReplaceSprites.Length)
                throw new ArgumentException("ReplaceItemSprite: Amount of child objects does not match replace textures");
            ReplaceItemSprite(item, replaceSprite);
            for (int i = 0; i < childObjects.Length; i++)
            {
                item.ReplaceItemSprite(childObjects[i], childReplaceSprites[i]);
            }
        }


        public static void ReplaceViewSprite(this SpawnableAsset item, Sprite replaceSprite)
        {
            item.ViewSprite = replaceSprite;
        }

        public static void SetBodyTextures(this PersonBehaviour person, Texture2D[] textures, float scale = 1f, int offset = 0)
        {
            if (textures.Length < 3)
                throw new ArgumentException("SetBodyTexturesArray: Too few body textures in array!");
            if (offset < 0)
                throw new ArgumentOutOfRangeException("SetBodyTexturesArray: Offset cannot be less than 0!");
            if (textures.Length % 3 != 0)
                throw new ArgumentOutOfRangeException("SetBodyTexturesArray: Amount of textures in array must be a multiple of 3! (Offset empty slots with null elements?)");
            int trueOffset = offset * 3;
            person.SetBodyTextures(textures[0 + trueOffset], textures[1 + trueOffset], textures[2 + trueOffset], scale);
        }


        public static void SetHealthBarColors(this PersonBehaviour person, Color color)
        {
            foreach (LimbBehaviour limbs in person.Limbs)
            {
                limbs.SetHealthBarColor(color);
            }
        }

        public static void ResetHealthBarColors(this PersonBehaviour person)
        {
            person.SetHealthBarColors(new Color32(55, 255, 0, 255));          
        }

        public static void SetHealthBarColor(this LimbBehaviour limb, Color color)
        {
            var myStatus = (GameObject) typeof(LimbBehaviour).GetField("myStatus", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(limb);
            myStatus.transform.Find("bar").GetComponent<SpriteRenderer>().color = color;
        }

        public static void ResetHealthBarColor(this LimbBehaviour limb)
        {
            limb.SetHealthBarColor(new Color32(55, 255, 0, 255));
        }
    }
}

