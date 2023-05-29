# StudioPlusAPI
## RegenerationPlus (PlusAPI HIGHLY recommended)
### RegenerateLimb()
A quick way to write all the regenration methods as one method.
```cs
public static void RegenerateLimb(this LimbBehaviour myLimb, float regenSpeed, float acidSpeed, float burnSpeed, float rottenSpeed, float woundsEfficiency, bool regenWhenDead = false)
```
This regeneration automatically stops when the limb dies, unless regenWhenDead is set to true, which by default is not.<be/>
If any of the floats are set to equal 0, their respective heal method will not run.

I'm going to explain how to input regenSpeed, acidSpeed, burnSpeed and rottenSpeed here:<br/>
When any of these is set to 1, the entity will fully regenerate the respective value (health & acid, burn or rotting damage) in one second. Setting the value to greater than 1 will make the process faster, setting it to less than 1 makes the process slower.<br/>
This is very confusing and will cause madness if you dare to try and figure out the respective values for regeneration as a simple float, so it's simpler to simply write the speeds as 1f/x, where x stands for seconds. This is also known as an inverse of a number, and PlusAPI module contains a method for inversing floats! Here is an example from evil mod:
```cs
limbs.RegenerateLimb(
    PlusAPI.Inv(300f),
    PlusAPI.Inv(600f),
    PlusAPI.Inv(1200f),
    PlusAPI.Inv(150f),
    0.375f
);
```
I would go mad if I tried to input the actual plain values here so I just used inverses. Note that these values are very low, for example the 1st one is 0.0033333, but this is because 1f means that regenerating from 100% damage to 0% damage takes 1 second. I took the value I saw as 'fitting' and then multiplied it by 5 to get the end result. You are of course free to experiment.

There is also woundsEfficiency, but I'm going to explain it in its own entry because it'll fit better there

### HealAcid()
Heals acid damage. The way it's healed is explained in RegenerateLimb() entry.
```cs
public static void HealAcid(this LimbBehaviour myLimb, float speed = 1f)
{
    if (myLimb.SkinMaterialHandler.AcidProgress > 1f)
        myLimb.SkinMaterialHandler.AcidProgress = 1f;
    else if (myLimb.SkinMaterialHandler.AcidProgress > 0f)
        myLimb.SkinMaterialHandler.AcidProgress -= Time.deltaTime * speed;
}
```
I'm only going to mention it here, but the method automatically clamps the acid, burn and rotten damage to 1f, because zooi apparently didn't make it an integral property of these values to be clamped between 0 and 1.<br/>
Another thing I'll only mention here and that applies to every method in this module, this is how you apply it:
```cs
LimbBehaviour limb;
public void Start()
{
    limb = GetComponent<LimbBehaviour>();
}
public void FixedUpdate()
{
    limb.HealAcid(PlusAPI.Inv(60f));
}
```

### HealBurn()
Heals burn damage. The way it's healed is explained in RegenerateLimb() entry.
```cs
public static void HealBurn(this LimbBehaviour myLimb, float speed = 1f)
```

### HealRotten()
Heals rotten damage. The way it's healed is explained in RegenerateLimb() entry.
```cs
public static void HealRotten(this LimbBehaviour myLimb, float speed = 1f)
```

### HealWounds()
Heals wounds. 'Wounds' here means stuff like bullet holes, stab holes, skin damage, etc., basically anything that isn't acid, burn or rotten damage
```cs
public static void HealWounds(this LimbBehaviour myLimb, float efficiency = 1f)
```
Efficiency here is a value clamped between 0 and 1. When set to 1, all wounds will be healed immediately, if set to 0... I think you can deduce what will happen when it's set to 0. For values in-between, I'm not exactly sure how it works but a factor is calculated and it reduces wounds by that factor. The important thing is that it works.