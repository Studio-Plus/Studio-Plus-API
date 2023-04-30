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
    public struct CreationPlus
    {
        public static GameObject SpawnItem(SpawnableAsset item, Transform parent, Vector3 position = default, bool spawnSpawnParticles = false)
        {
            Quaternion rotation = parent.lossyScale.x < 0f ? Quaternion.Euler(0f, 0f, 180f) * parent.rotation : parent.rotation;
            Vector3 newPosition = parent.position + rotation * position;

            GameObject spawnedItem = UnityEngine.Object.Instantiate(item.Prefab, newPosition, rotation);
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
            PhysicalBehaviour phys = spawnedItem.GetComponent<PhysicalBehaviour>();
            phys.SpawnSpawnParticles = spawnSpawnParticles;
            spawnedItem.AddComponent<AudioSourceTimeScaleBehaviour>();
            spawnedItem.AddComponent<TexturePackApplier>();
            spawnedItem.AddComponent<SerialiseInstructions>().OriginalSpawnableAsset = item;
            spawnedItem.name = item.name;
            CatalogBehaviour.PerformMod(item, spawnedItem);
            return spawnedItem;
        }

        /*
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
        */


        public static void CreateFixedJoint(GameObject main, GameObject other)
        {
            FixedJoint2D joint = main.AddComponent<FixedJoint2D>();
            joint.dampingRatio = 1;
            joint.frequency = 0;
            joint.connectedBody = other.GetComponent<Rigidbody2D>();
        }

        public static void CreateFixedJoint(GameObject main, GameObject other, Vector2 position = default)
        {
            FixedJoint2D joint = main.AddComponent<FixedJoint2D>();
            joint.dampingRatio = 1;
            joint.frequency = 0;
            joint.connectedBody = other.GetComponent<Rigidbody2D>();
            joint.anchor = position;
        }


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
    }
}

