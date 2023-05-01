# StudioPlusAPI
## PowerPlus (PlusAPI HIGHLY recommended)
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
- **CreatePower()** is called when the class is first added by default in the Start() method. If you don't wish for the power to be immediately added when created, override Start() without adding base.Start() anywhere.<br/> 
  Within it you should add things like creating any light sprites, adding immunities or abilities (See Ability documentation), etc., basically anything meant to be permanent.<br/>
  Here is an example from PowerPlus [MOD]:
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
      eyeLight = new GameObject("Light"),
      Limb.transform.root.transform.Find(LimbList.head),
      UniversalAssets.eyeLight,
      new Vector2(2.5f, 1.5f) * ModAPI.PixelSize,
      powerColor,
      eyeGlow = TexturePlus.InstantiateLight(eyeLight.transform)
  );
  eyeLight.SetActive(eyeActive);

  abilities.Add(LimbList.FindLimb(Limb.transform.root, LimbList.lowerArmFront).gameObject.GetOrAddComponent<FireTouch>());
  abilities.Add(LimbList.FindLimb(Limb.transform.root, LimbList.lowerArmBack).gameObject.GetOrAddComponent<FireTouch>());
        }
  ```
- **TogglePower(bool toggle)** toggles the power as a whole. In this state, the human should look as if he has no powers (ignoring e.g. immunities, but that's up to you to decide). **Calls ToggleAbility(bool toggle) by default.**
  For Fire Human from Power Plus [MOD] it simply disables eye glow:
  ```cs
  protected override void TogglePower(bool toggled)
  {
      eyeLight.SetActive(toggled);
      eyeActive = toggled;
  }
  ```
- **ToggleAbility(bool toggle)** toggles ability instead. The class itself deals with this function rather effectively, so the toggle of abilities should be limited to the Ability class (See documentation on Ability). That is the case for Fire Human Power, which is why no example can be provided
- **DeletePower()** is called when the class is removed. Should undo everything that CreatePower() does. If your power is not meant to be removable in that way, you don't have to add anything to it.

There are also 2 additional classes that you should know about:
```cs
public void ForceTogglePower(bool toggled)

public void ForceToggleAbility(bool toggled)
```
So like the name implies, it forces Powers or Abilities to be toggled on and off right? Well, sort of.<br/>
When you toggle Power/Abilities to false by this method, they will be permanently disabled without considering the Power & Ability Toggle Conditions (More about that later). However when toggled to true by this method, it will enable it once and then subject it to the previously mentioned Power & Ability Toggle Conditions, so you will have to use this method with that in mind.<br/> 
Here is a quick example of it being taken in mind correctly:
```cs
public void Use(ActivationPropagation a)
{
    if (PowerActive)
        ForceTogglePower(false); //If Power is toggled on, toggle it off
    else if (Person.transform.Find(LimbList.head).GetComponent<LimbBehaviour>().IsConsideredAlive)
        ForceTogglePower(true); //If Power is toggled off and Entity with power is still alive, re-enable powers
    else return;
}
```

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
    if (Person.Consciousness < 0.8f && AbilityEnabled)
        ToggleAbilityInt(false);
    else if (Person.Consciousness >= 0.8f && !AbilityEnabled && PowerEnabled)
        ToggleAbilityInt(true);
}

if (PowerActive)
{
    if (!Person.transform.Find(LimbList.head).GetComponent<LimbBehaviour>().IsConsideredAlive && PowerEnabled)
        TogglePowerInt(false);
    else if (Person.transform.Find(LimbList.head).GetComponent<LimbBehaviour>().IsConsideredAlive && !PowerEnabled)
        TogglePowerInt(true);
}
```
I'll quickly summarize it in  wors here:

If both Power And Abilities are toggled off, do nothing


Else, if Ability is toggled on:
- If person is unconscious and Ability wasn't disabled yet, disable them
- Else if person is conscious and Ability wasn't enabled yet and Power is enabled, enable them

If Power is toggled on:
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
    switch (toggled)
    {
        case true:
            AbilityEnabled = toggled;
            foreach (Ability ability in abilities)
            {
                ability.enabled = toggled;
            }
            Debug.Log("Abilities Enabled!");
            break;
        case false:
            AbilityEnabled = toggled;
            foreach (Ability ability in abilities)
            {
                ability.enabled = toggled;
            }
            Debug.Log("Abilities Disabled!");
            break;
    }
    ToggleAbility(toggled);
}
```
As you can see, the ToggleAbilityInt on its own changes the enabled variable in the Ability class. This is acknowledged in the Ability class itself and is meant to be the way you handle toggling powers on and off. (See Ability Documentation)