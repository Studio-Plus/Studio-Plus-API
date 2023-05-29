# StudioPlusAPI
## PowerPlus (REQUIRES PlusAPI)
### public abstract class PowerPlus : MonoBehaviour
Allows you to gift humans with power.<br/>
This class is a beautiful and perfect mesh of being simple for you, the modder, to use, yet also being the most advanced piece of code I've written up to date, so this documentation will be split into 2 parts:
- How to use?
- How does it work?

#### How to use?
So first you must know that there are 3 protected virtual methods in this class (That is, methods that you're allowed to override in your subclass).
```cs
protected virtual void Awake()

protected virtual void Start()

protected virtual void FixedUpdate()
```
It's important to get that out of the way because I will be mentioning them throughout this section of the documentation

The class is abstract because you must make a subclass for it, and it contains 4 abstract methods that you have to override:
```cs
protected abstract void CreatePower();

protected abstract void TogglePower(bool toggled);

protected abstract void ToggleAbility(bool toggled);

protected abstract void DeletePower();
```

##### CreatePower()
It's called when the class is first added by default in the Start() method. If you don't wish for the power to be immediately added when created, override Start() without adding base.Start() anywhere.<br/> 
Within it you should add things like creating any light sprites, adding immunities or abilities (See Ability documentation), etc., basically anything meant to be permanent.<br/>
Here is an example from Power Plus [MOD]:
```cs
protected override void CreatePower()
{
    foreach (var limbs in Person.Limbs)
    {
        var phys = limbs.PhysicalBehaviour;

        limbs.DiscomfortingHeatTemperature = float.PositiveInfinity;
        phys.SimulateTemperature = false;

        phys.Properties = UniversalAssets.fireHumanProperties;
    }

    TexturePlus.CreateLightSprite(
        out eyeLight,
        Limb.transform.root.transform.Find(LimbList.head),
        UniversalAssets.eyeLight,
        new Vector2(2.5f, 1.5f) * ModAPI.PixelSize,
        powerColor,
        out eyeGlow
    );
    eyeLight.SetActive(eyeActive);

    abilities.Add(LimbList.FindLimb(Limb.transform.root, LimbList.lowerArmFront).gameObject.GetOrAddComponent<FireTouch>());
    abilities.Add(LimbList.FindLimb(Limb.transform.root, LimbList.lowerArmBack).gameObject.GetOrAddComponent<FireTouch>());
}
```

##### TogglePower(bool toggle)
It toggles the power as a whole. In this state, the human should look as if he has no powers (ignoring e.g. immunities, but that's up to you to decide). **Calls ToggleAbility(bool toggle) by default.**
For Fire Human from Power Plus [MOD] it simply toggles eye glow:
```cs
protected override void TogglePower(bool toggled)
{
    eyeLight.SetActive(toggled);
}
```

##### ToggleAbility(bool toggle)
It toggles ability instead. The class itself deals with this function rather effectively, so the toggle of abilities should be limited to the Ability class (See documentation on Ability). That is the case for Fire Human Power, which is why no example can be provided

##### DeletePower()
Called when the class is removed. Should undo everything that CreatePower() does. If your power is not meant to be removable in that way, you don't have to add anything to it. It already handles removing abiilities by itself so you don't have to worry about that

##### IsCreated { get; protected set; }
Turns true when power is first created with CreatePower(). Not meant to be turned false.<br/>
It should be only used when modifying values by the means of arithmetic operators. Here is an example:
```cs
protected override void CreatePower()
{
    foreach (LimbBehaviour limbs in Person.Limbs)
    {
        limbs.DiscomfortingHeatTemperature = 2000f; //This sets the value to a specific value each time, so it can run 1000 times and it will work just fine
        if (!IsCreated) //This ensures that the code in the curly brackets only runs when this is false, in other words exactly once, considreing the value of IsCreated is later set to true and it never turns back to false.
        {
            limbs.BreakingThreshold *= 2f; //If left outside of the if statement, the breaking threshold will be doubled each time the entity is copied
            //This would be bad, so you ensure with the if statement that this is only run once.
        }        
    }   
}
```
If you are not sure if the method runs correctly, the Debug Log "Power created!" is set up to only run once with the same thought process, so if it only does print out exactly once, you must have done something wrong instead of the API being broken (probably, maybe) 

##### Other stuff
There are also 2 additional methods that you should know about:
```cs
public void ForceTogglePower(bool toggled)

public void ForceToggleAbility(bool toggled)
```
So like the name implies, it forces Powers or Abilities to be toggled on and off right? Well, sort of.<br/>
When you toggle Power/Abilities to false by this method, they will be permanently disabled without considering the Power & Ability Toggle Conditions (More about that later). However when toggled to true by this method, it will only subject it to the previously mentioned Power & Ability Toggle Conditions, so if the entity is dead anyway it actually won't enable it for a frame to have the class then immediately disable it by default.<br/>
Additionally, if power is already off and you use the method to forecully turn it off, it won't turn it off again since it's already turned off. That's an optimization thing, or whatever.

#### How does it work?
I will just go over every method that does not relate to 'how to use' here:

##### protected virtual void Awake()
Defines the following properties thusly:
```cs
/* earlier...
public LimbBehaviour Limb { get; protected set; }
public PersonBehaviour Person { get; protected set; }
*/
protected virtual void Awake()
{
    Limb = GetComponent<LimbBehaviour>();
    Person = Limb.Person;
}
```

##### protected virtual void Start()
Calls PowerCreateInt()<br/>
Woah hold on! What the heck is PowerCreateInt()? Just give me a moment okay?

##### protected virtual void FixedUpdate()
Contains the so-called "Power & Abilities Toggle Conditions".<br/>
Here they are!
```cs
if (!PowerActive && !AbilityActive)
    return;

if (AbilityActive)
{
    if (AbilityEnabled && !Person.FindLimb(LimbList.head).IsCapable)
        ToggleAbilityInt(false);
    else if (!AbilityEnabled && Person.FindLimb(LimbList.head).IsCapable && PowerEnabled)
        ToggleAbilityInt(true);
}

if (PowerActive)
{
    if (!Person.FindLimb(LimbList.head).IsConsideredAlive && PowerEnabled)
        TogglePowerInt(false);
    else if (Person.FindLimb(LimbList.head).IsConsideredAlive && !PowerEnabled)
        TogglePowerInt(true);
}
```
I'll quickly summarize how it works here:

If both Power And Abilities are inactive, do nothing

Else...<br/>
if Ability is active:
- If person is unconscious and Ability wasn't disabled yet, disable them
- Else if person is conscious and Ability wasn't enabled yet and Power is enabled, enable them

If Power is active:
- If the head is dead and power wasn't disabled yet, disable it.
- Else if the head is alive and power wasn't enabled yet, enable it.

##### protected void CreatePowerInt(), protected void TogglePowerInt(), protected void ToggleAbilityInt()
We finally get to these guys. 'Int' here stands for 'Internal', and that's because this is the stuff that happens internally in the class. Crazy naming, right?<br/>
They do the necessary behind-the-scenes stuff to make those methods work and is what's actually called every time power is made or toggled. The things that you put into the abstract classes get executed by the Internal classes though so don't worry.

The details of how they exactly work and what they do is kinda boring and unnecessary, say for one:
```cs
//This class turns *abilities* on or off. Main use for when the one with power is knocked unconcsious
protected void ToggleAbilityInt(bool toggled)
{
    AbilityEnabled = toggled;
    foreach (Ability ability in Abilities)
    {
        ability.enabled = toggled;
    }
    string toggledString = toggled ? "Enabled" : "Disabled";
    Debug.Log($"Abilities {toggledString}!");
    ToggleAbility(toggled);
}
```
As you can see, the ToggleAbilityInt on its own changes the enabled variable in the Ability class. This is acknowledged in the Ability class itself and is meant to be the way you handle toggling powers on and off. (See Ability Documentation)

##### protected void OnDestroy()
You might wonder why there is no 'DeletePowerInt()' method. That is because OnDestroy does what it would do:
```cs
protected void OnDestroy()
{
    foreach (Ability ability in Abilities)
    {
        Destroy(ability);
    }
    DeletePower();
}
```
The only thing that needs to be done when Power is deleted is deleting the ability classes. Everything else is defined by the abstract class.