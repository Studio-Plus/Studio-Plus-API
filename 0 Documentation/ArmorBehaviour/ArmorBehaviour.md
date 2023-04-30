# StudioPlusAPI
## ArmorBehaviour (REQUIRES CreationPlus and PlusAPI)
### public class ArmorBehaviour
This class must be added to the object you want to make into armor in order for it to be armor.

But simply adding the ArmorBehaviour is not enough. In order for it to work, you must add in one of the default 3 **armor constructor methods**.<br/>

#### Armor constructor method
Unlike the name suggests, those are not actual constructors but methods that essentially do what a constructor would. The behaviour by default contains Body Armor and Clothing, with each of them having their unique constructor:
```cs
public void CreateBodyArmor(string newLimbType, float newStabResistance)

public void CreateClothing(string newLimbType)
```
For clothing you simply have to provide the string of the limb path in the transform hierarchy. For this you can simply use LimbList (See documentation on LimbList).<br/>
For body armor however, you also must provide a stab resistance. This value is clamped between 0 and 1, where 0 means no protection and 1 means full protection. If it's set to 0.75f, there is a 75% chance that the armor will be protected from being stabbed, so 25% chance that human will be stabbed. It's purely based on chance instead of taking factors like velocity into account. I do realize this. Some might consider me lazy for writing this like that, in which case they're right. I was lazy when writing this. I might or might not fix it in a future update to the API.<br/>
For clothing, stab resistance is always 0.

Both of these methods are derived and draw from the actual armor constructor method
```cs
public void CreateCustom<T>(string newLimbType, float newStabResistance, int newArmorSorOrd) where T : ArmorWearer
```
This contains the additional setting of armor sorting order, which defines how many sorting orders above the limb the armor will show.<br/>
For clothing, this number is 2. For body armor, this number is 3. This basically means that body armor will always be displayed above clothing for the limb. There does exist the "head armor visual glitch", but more on that shortly.<br/>
While in theory this number can also be 1, for Studio Plus Mods that sorting order is reserved for light sprites so that cny clothing covering those sprites up will always cover up the light sprite as well, so Clothing uses the next available lowest number 2.<br/>
I already implied this, but this number shouldn't be 0 or a  negative number because then armor will not be shown above the limb.

Also, it's very important that ArmorBehaviour must be added via GetOrAddComponent to ensure a 99.9% bug-free experience, so:
```cs
Instance.GetOrAddComponent<ArmorBehaviour>().CreateBodyArmor(LimbList.head, 0.5f);
```

To end this boring documentation entry, here is a full example of an armor piece being created from ArmorBehaviour [MOD]:
```cs
ModAPI.Register(
    new Modification()
    {
        OriginalItem = ModAPI.FindSpawnable("Rod"),
        NameOverride = "Helmet" + tag,
        NameToOrderByOverride = "Z2",
        DescriptionOverride = "For bike rides and in case of nuclear explosion!",
        CategoryOverride = ModAPI.FindCategory("Entities"),
        ThumbnailOverride = ModAPI.LoadSprite("Textures/Views/Helmet View.png"),
        AfterSpawn = (Instance) =>
        {
            Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Textures/Entities/Helmet.png");
            Instance.FixColliders();
            var phys = Instance.GetComponent<PhysicalBehaviour>();
            phys.Properties = ModAPI.FindPhysicalProperties("Bowling pin"); //Bad code, search ArmorBehaviour [MOD] source code for how to write this better
            Instance.GetOrAddComponent<ArmorBehaviour>().CreateBodyArmor(LimbList.head, 0.5f);
        }
    }
);
```
#### Head armor visual glitch
Imagine sorting orders like putting transparent pieces of paper with things drawn on them in a specific order. Now, imagine you creating multiple such stacks, compressing them into 1 piece of transparent paper and then putting those in order. This is the most basic explanation of sorting layers in unity.<br/>
The back arm/leg and front arm/leg are both on a different sotring layers when compared to the rest of the limbs (Background and Foreground respectively) which allows ArmorBehaviour to exploit sortinh order to sort armor pieces the way they are. That is, except for the head which for some reason is also on the Foreground layer with the front leg/arm.<br/>
This causes any armor piece that's placed on the head and in the sorting order 2 or above to display above the front arm, which obviously doesn't look good so we need a solution.

One of the solutions would be to put all Armor on the sorting order 1, but that's impossible with the way the ArmorBehaviour system is set up.

I have long dealt with this issue by not dealing with it, the one slight imperfection of my ArmorBehaviour shadowing me every day... What a failure I am because I can't perfect my ArmorBehaviour. How can I even call myself a programmer?<br/>
Luckily, I recently found a solution: Simply forcing the heads of humans and androids to the default sorting layer at the beginning of the code.
```cs
namespace Mod
{
    public class Mod : MonoBehaviour
    {
        public static void Main()
        {
            ModAPI.FindSpawnable("Human").Prefab.transform.Find("Head").GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            ModAPI.FindSpawnable("Android").Prefab.transform.Find("Head").GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            //Mandatory Studio Plus disclaimer: These 2 things are here so that ArmorBehaviour isn't a mess with head pieces, you're welcome
            //"Might have some unintended side effects that manifest as unexplainable bugs later"
            //-zooi

            //Your other code here...
        }
    }
}
```
While this is not the perfect solution like zooi mentioned, I can finally sleep peacefully at night, knowing that one of the biggest failures of this API has been corrected.

#### ???
Not sure what else I could add. I could go over the other 157 lines of code detailing how the Armor works and interacts with the world, but not sure if that's really needed.

Well I can at least tell you how Armor collides with Other Armor. Basically, if:
- Other Armor is of the same type as Armor
- Other Armor connects to the same limb as Armor
- Armor is equipped

The armor pieces will not collide. The last condition is in place to make placing armor on an entity that already has armor easier.