# StudioPlusAPI
## CreationPlus (REQUIRES PlusAPI)
### CreateParticles()
Creates particles.<br/>
```cs
public static ParticleSystem CreateParticles(GameObject item, Transform parent, Vector2 position = default, Quaternion rotation = default)
```
This is actually very useful unlike the last entry. You know how plates create cool custom particles and sound? What this method does is that it takes the broken plate prefab and removes any GameObjects so that only the particles remain. Here is how you do it for the previously mentioned example:
```cs
ParticleSystem particles = CreationPlus.CreateParticles(ModAPI.FindSpawnable("Plate").Prefab.GetComponent<DestroyableBehaviour>().DebrisPrefab, Instance.transform);
```
If positon parameter is left empty, the particles will play at the center of the object.<br/>
The rotation parameter is a modifier, if left empty the rotation will simply equal the rotation of the parent transform.

The method returns the ParticleSystem component in case you have to modify the particles in any way.