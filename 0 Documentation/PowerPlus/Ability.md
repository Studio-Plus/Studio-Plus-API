# StudioPlusAPI
## PowerPlus (PlusAPI HIGHLY recommended)
### public abstract class Ability : MonoBehaviour
This is a class for all abilities added by PowerPlus class.

For starters, tbe PowerPlus class has a list of abilities:
```cs
public List<Ability> abilities = new List<Ability>();
```
These should be added in the abstract method **CreatePower()**, preferably at the end of said method, like here:
```cs
protected override void CreatePower()
{
    //Other code here...

    abilities.Add(LimbList.FindLimb(Limb.transform, LimbList.lowerArmFront).gameObject.GetOrAddComponent<MyAbility>());
    abilities.Add(LimbList.FindLimb(Limb.transform, LimbList.lowerArmBack).gameObject.GetOrAddComponent<MyAbility>());
}
```
It's important that you do it like that by finding the appropriate limb then adding the Ability via GetOrAddComponent to make it work properly.

To spare the explaining of every method (and because the class is short enough) I will put it here as reference:
```cs
public abstract class Ability : MonoBehaviour
{
    [SkipSerialisation]
    public LimbBehaviour Limb { get; protected set; }
    [SkipSerialisation]
    public PersonBehaviour Person { get; protected set; }

    protected virtual void Awake()
    {
        Limb = GetComponent<LimbBehaviour>();
        Person = Limb.Person;
    }

    public virtual void FixedUpdate()
    {
        if (enabled && (!Limb.NodeBehaviour.IsConnectedToRoot || !Limb.IsConsideredAlive))
            enabled = false;
        else if (!enabled && Limb.IsConsideredAlive)
            enabled = true;
    }

    public abstract void OnEnable();

    public abstract void OnDisable();
}
```
As you can see, like PowerPlus it has Limb and Person and immediately assigns values for them.<br/>
It also contains the last of Power & Abilities Toggle Conditions: When the limb is separated from the body or if it's dead, it disables the class.

The class works the way it works mostly because of that 1 line of code.<br/>
It also contains the 2 absract methods OnEnable() and OnDisable(), in which you should define what happens when this ability is toggled on and off respectively.

Note that enabled simply being set to false doesn't automatically make other behaviours of the class stop, so you will have to adjust whatever you write with that in mind:
```cs
public class FireTouch : Ability
{
    [SkipSerialisation]
    public GameObject armLight;
    [SkipSerialisation]
    public LightSprite armGlow;
    [SkipSerialisation]
    public Color powerColor = new Color32(255, 127, 0, 31);

    public void Start()
    {
        TexturePlus.CreateLightSprite(
            out armLight,
            transform,
            UniversalAssets.powerLight,
            Vector2.zero,
            powerColor,
            out armGlow,
            5f,
            0.75f
        );
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (enabled && gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= 1f) //If statement must check if enabled is true or else this will trigger regardless of whether the ability is toggled on or off
        {
            var phys = other.transform.GetComponent<PhysicalBehaviour>();
            phys.Temperature += 200f;
            phys.Ignite();
            if (phys.OnFire)
            {
                phys.BurnIntensity = 1f;
            }
        }
    }

    public override void OnEnable()
    {
        //This question mark ensures that this is only run if armLight isn't null to ensure no NullReferenceException errors
        armLight?.SetActive(true);
    }

    public override void OnDisable()
    {
        //Same here
        armLight?.SetActive(false);
    }
    }
```