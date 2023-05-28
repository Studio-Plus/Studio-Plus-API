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
    public static class CreationPlus
    {
        public static GameObject SpawnItem(SpawnableAsset item, Transform parent, Vector3 position = default, bool spawnSpawnParticles = false)
        {
            Quaternion vectorRotation = parent.lossyScale.x < 0f ? Quaternion.Euler(0f, 0f, 180f) * parent.rotation : parent.rotation;
            Vector3 newPosition = parent.position + vectorRotation * Vector2.Scale(position, parent.localScale.GetAbs());

            GameObject spawnedItem = UnityEngine.Object.Instantiate(item.Prefab, newPosition, parent.rotation);
            spawnedItem.transform.localScale = parent.lossyScale;
            PhysicalBehaviour phys = spawnedItem.GetComponent<PhysicalBehaviour>();
            phys.SpawnSpawnParticles = spawnSpawnParticles;
            spawnedItem.AddComponent<AudioSourceTimeScaleBehaviour>();
            spawnedItem.AddComponent<TexturePackApplier>();
            spawnedItem.AddComponent<SerialiseInstructions>().OriginalSpawnableAsset = item;
            spawnedItem.name = item.name;
            CatalogBehaviour.PerformMod(item, spawnedItem);
            return spawnedItem;
        }

        public static GameObject SpawnItemAsChild(SpawnableAsset item, Transform parent, Vector3 position = default, bool spawnSpawnParticles = false)
        {
            GameObject spawnedItem = UnityEngine.Object.Instantiate(item.Prefab, parent.position, parent.rotation);
            spawnedItem.transform.SetParent(parent);
            spawnedItem.transform.localPosition = position;
            spawnedItem.transform.localScale = Vector2.one;
            PhysicalBehaviour phys = spawnedItem.GetComponent<PhysicalBehaviour>();
            phys.SpawnSpawnParticles = spawnSpawnParticles;
            spawnedItem.AddComponent<AudioSourceTimeScaleBehaviour>();
            spawnedItem.AddComponent<TexturePackApplier>();
            spawnedItem.AddComponent<SerialiseInstructions>().OriginalSpawnableAsset = item;
            spawnedItem.name = item.name;
            CatalogBehaviour.PerformMod(item, spawnedItem);
            return spawnedItem;
        }

        [Obsolete]
        public static GameObject SpawnItemStatic(SpawnableAsset item, Vector2 position = default, bool spawnSpawnParticles = false)
        {
            GameObject spawnedItem = UnityEngine.Object.Instantiate(item.Prefab, position, Quaternion.identity);
            PhysicalBehaviour phys = spawnedItem.GetComponent<PhysicalBehaviour>();
            phys.SpawnSpawnParticles = spawnSpawnParticles;
            spawnedItem.AddComponent<AudioSourceTimeScaleBehaviour>();
            spawnedItem.AddComponent<TexturePackApplier>();
            spawnedItem.AddComponent<SerialiseInstructions>().OriginalSpawnableAsset = item;
            spawnedItem.name = item.name;
            CatalogBehaviour.PerformMod(item, spawnedItem);
            return spawnedItem;
        }


        public static FixedJoint2D CreateFixedJoint(this GameObject main, GameObject other)
        {
            FixedJoint2D joint = main.AddComponent<FixedJoint2D>();
            joint.dampingRatio = 1;
            joint.frequency = 0;
            joint.connectedBody = other.GetComponent<Rigidbody2D>();
            return joint;
        }

        public static FixedJoint2D CreateFixedJoint(this GameObject main, GameObject other, Vector2 position)
        {
            FixedJoint2D joint = main.AddComponent<FixedJoint2D>();
            joint.dampingRatio = 1;
            joint.frequency = 0;
            joint.connectedBody = other.GetComponent<Rigidbody2D>();
            joint.anchor = position;
            return joint;
        }


        public static HingeJoint2D CreateHingeJoint(this GameObject main, GameObject other, Vector2 position, float minDeg, float maxDeg)
        {
            HingeJoint2D joint = main.AddComponent<HingeJoint2D>();
            joint.connectedBody = other.GetComponent<Rigidbody2D>();
            joint.anchor = position;
            JointAngleLimits2D limits = joint.limits;
            limits.min = minDeg;
            limits.max = maxDeg;
            joint.limits = limits;
            joint.useLimits = true;
            return joint;
        }


        public static GameObject CreateDebris(string name, Transform parent, Sprite sprite, Vector2 position)
        {
            Quaternion vectorRotation = parent.lossyScale.x < 0f ? Quaternion.Euler(0f, 0f, 180f) * parent.rotation : parent.rotation;
            GameObject myGameObject = ModAPI.CreatePhysicalObject(name, sprite);
            myGameObject.transform.position = parent.position + vectorRotation * Vector2.Scale(position, parent.localScale.GetAbs());
            myGameObject.transform.rotation = parent.rotation;
            myGameObject.transform.localScale = parent.lossyScale;
            parent.gameObject.GetOrAddComponent<DestroyShardsController>().childrenObjects.Add(myGameObject);
            myGameObject.AddComponent<DebrisComponent>();
            return myGameObject;
        }


        public static ParticleSystem CreateParticles(GameObject item, Transform parent, Vector2 position, Quaternion rotation)
        {
            GameObject particleObject = UnityEngine.Object.Instantiate(item, parent.position, rotation);
            particleObject.transform.SetParent(parent);
            particleObject.transform.localPosition = position;
            particleObject.transform.localScale = parent.localScale.GetAbs();
            UnityEngine.Object.Destroy(particleObject.GetComponent<DestroyWhenAllChildrenDestroyed>());
            foreach (Transform myTransform in particleObject.GetComponentsInChildren<Transform>())
            {
                if (myTransform != particleObject.transform)
                    UnityEngine.Object.Destroy(myTransform.gameObject);
            }
            ParticleSystem particles = particleObject.GetComponent<ParticleSystem>();
            UnityEngine.Object.Destroy(particleObject, particles.main.duration);
            return particles;
        }

        public class DestroyShardsController : MonoBehaviour
        {
            public List<GameObject> childrenObjects = new List<GameObject>();
            protected int count;

            protected void FixedUpdate()
            {
                foreach (GameObject myObject in childrenObjects)
                {
                    if (myObject == null)
                    {
                        childrenObjects.Remove(myObject);
                        if (childrenObjects.Count == 0)
                            Destroy(gameObject);
                        break;
                    }
                }
            }

            protected void OnDestroy()
            {
                foreach (GameObject myObject in childrenObjects)
                {
                    Destroy(myObject);
                }
            }
        }
    }
}

