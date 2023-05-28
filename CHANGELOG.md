# STUDIO PLUS API CHANGELOG

## v4.0.0 (28th May 2023)
### General
- Added a new module: RegenerationPlus
- All structs now changed to static classes because...
- ...StudioPlusAPI now uses extension methods. This has some consequences:
  - Lots of methods got moved into PlusAPI because they became too general for their module (for example ChangeAlpha())
  - Some methods (most notably FindLimb) got changed severly to fit as an extension method, so some methods from v3 may not be compatible with methods from v3, but new edition number should probably be warning enough that these 2 versons may not be compatible. Check documentation for changes.
  - Any changes related to extensions will mostly not be mentioned. Refer to documentation to figure out how to use new methods
- Method errors are now all done by throw new Exception() and the message has following format: "MethodName: This and that won't work because of so and so"
### ChemistryPlus:
- Added ChemistryPlus.AddBottleOpening(this BloodContainer container) [Check documentation]
### TexturePlus:
- Added TexturePlus.SetBodyTexturesArray(this PersonBehaviour) _("this" means that it's an extension method of in this case PersonBehaviour)_ [Check documentation]
- Added TexturePlus.SetHealthBarColor(this LimbBehaviour & this PersonBehaviour) and TexturePlus.ResetHealthBarColor(this LimbBehaviour & this PersonBehaviour) [Check documentation]
- Slightly modified TexturePlus.CreateLightSprite() [Check documentation]
- Removed TexturePlus.InstantiateLight() while I have an excuse to (edition number change, non-compatibility is not an issue)
- Generalized TexturePlus.ChangeAlpha(), thus it's now PlusAPI.ChangeAlpha()
### CreationPlus:
- Added CreationPlus.CreateDebris() [Check documentation]
- Addec CreationPlus.CreateParticles() [Check documentation]
- Finally fully fixed CreationPlus.SpawnItem()
### PlusAPI:
- Added PlusAPI.Inv(this float) (Inverse, inverses numbers, e.g. 10 becomes 1/10) [Check documentation]
- Polished PlusAPI.ToFloat() and PlusAPI.ToByte()
- Added PlusAPI.GetAbs(this Vector2 & this Vector3) [Check documentation]
- Added PlusAPI.Talk(this PhysicalBehaviour) [Check documentation]
- Radically changed LimbList.FindLimb(this PersonBehaviour) [Check documentation]
### PowerPlus:
- Made PowerPlus class way less confusing and adjusted it to v4 method changes
### ArmorBehaviour:
- Surprisingly, ArmorBehaviour remains unchanged

## v3.2.1 (12th May 2023)
- For PowerPlus, if a limb in the Ability list is dead, its powers will turn off (see documentation for details)

## v3.2.0 (12th May 2023)
- Fixed CreateLightSprite, now it doesn't require you to initialize the GameObject and LightSprite variables within the method (See documentation for further details). Finally
  - Since InstantiateLight lost purpose, it is now marked as obsolete
- Uncommented CreateItemStatic, but marked it as obsolete
- If FindLimb is supplied with a GameObject, it will return a GameObject instead of a Transform
- Added FindLimbBeh (Basically FindLimb but returns LimbBehaviour instead)
- Added FindLimbComp (See documentation)
- Fixed some minor bugs with PowerPlus
  - As part of fixing minor bugs, since this updated breaks CreateLightSprite (Hence it being an update, not a patch), those were fixed as well. All mods in this repository are up-to-date with the API usually. Can't say that about the steam workshop...
- Fiksed som gramatikal mistejks in te dokjumentary

## v3.1.3 (9th May 2023)
- Added WaveClamp01() and WaveClamp() (See documentation)
- Added TexturePlus.ToFloat and TexturePlus.ToByte (Again, see documentation)
- Updated the way Advanced Texture Pack System throws exceptions. Now they're actually exceptions.
- Ability class's Awake() is now a virtual void.

## v3.1.2 (3rd May 2023)
- Changes to PowerPlus:
  - Made Abilities list into a properrty
  - Added 'IsCreated' (See documentation)
  - Changed ForceAbilityToggle and ForcePowerToggle (Again, see documentation)
- Modified the PowerPlus documentation a bit while I'm on it.
- Changed ConvertToGlowColor() to ChangeAlpha() (See documentation)
- Other minor changes/fixes to the documentation

## v3.1.1 (2nd May 2023)
- Added PlusAPI.liter and a few others (See documentation)

## v3.1.0 (1st May 2023)
- Some updates to PowerPlus
  - Added an Ability class which surprisingly is not totally useless like I thought previously
  - Made PowerPlus more of an abstract class
  - Generally changed the structure of the class (See documentation)
- PowerPlus now has a documentation entry
- Any public fields are now public properties instead.
- Fixed up Power Plus [MOD], specifically PhysicalProperties (I implore you, check the comment in PowerPlus [MOD]/Code/Main.cs)
- Slight modifications to the ArmorBehaviour documentation entry
- Slightly modified Limblist.FindLimb();

## v3.0.0 (30th Apr 2023)
- Revamped most of the API yet again:
  - From now on the API is modular (Any dependencies on other API files are now listed in both the documentation and at the beginning of each file)
  - Overloaded methods are now a thing, so a lot of changes will be 'X methods are now an overloaded Y method'
  - Won't affect you, but all methods and stuff use correct C# naming style
- Created a documentation instead of it all being in comments
- Changed the comment at the beginning of each file
- Updated ChemistryPlus.AddLiquidToItem, now by default the container capacity will be the amount of liquid added (overridable)
- All ChemistryPlus LiquidReaction methods are now an overloaded 'LiquidReaction' method
- TexturePlus.CreateLightSprite() has now an overload for adding glow
- Added InstantiateLight()
- Added ChangeLightColor()
- Added ConvertToGlowColor()
- Changed the entirety of the advanced texture pack system (See documentation)
- CreationPlus.SpawnItem() and CreationPlus.SpawnItemAsChild() were revamped and generally made more useful (See documentation)
- Slight modifications to CreateFixedJoint() and CreateHingeJoint() (See documentation)
- Added a PlusAPI.kilogram and PlusAPI.ton constant. Have the values for a PPG ton and kilogram (based on 1000kg weight of 25)
- Changed the name of IgnoreCollision() and IgnoreEntityCollision() to more accurately depict their relation to the ignColl bool
- Created LimbList
  - It contains a list of every huamn/android child path
  - Contains LimbList.FindLimb() (See documentation)
- Updated ArmorBehaviour
  - most values are now 'protected'
  - Added 'public bool isAttached', it only indicates if armor is attached to limb or not, unlike equipped, which tells other armor/limb pieces how to interact with it.
  - Removed defaultSortingOrd because it's useless
  - Added some little optimizations
  - Updated the mod accordingly
- Added Power Plus expansion
  - Showcase of Power Plus Expansion with PowerPlus [Mod]

## v2.0.0 (15th Mar 2023)
- Added 4 Texture Pack Methods, basically improved version of the 'Basic texture pack system' found on the ppg modding website (https://www.studiominus.nl/ppg-modding/snippets/texturePackSystem.html), you may call this the'Advanced texture pack system`
- Removed StudioPlusAPI.SpawnGameObject because it seems to be completely useless
- ArmorWearer now contains a variable that stores the armor game object that essentially allows you to modify an armor piece when it's put on an entity
- Restructured the entire API: 
    - Now you have to add 'using StudioPlusAPI' to each file
    - There are now multiple different sub-APIs: ChemistryPlus, TexturePlus, CreationPlus, PlusAPI (Misc.)

## v1.1.2 (15th Dec 2022)
- Made buttons of ArmorBehaviour consistent with vanilla buttons, I hate small inconsistencies.