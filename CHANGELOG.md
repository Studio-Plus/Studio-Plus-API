# STUDIO PLUS API CHANGELOG

## v3.1.1 (2nd May 2023)
- Added PlusAPI.liter and a few others (See documentation)

## v3.1.0 (1st May 2023)
- Some updates to PowerPlus
  - Added an Ability class which surprisingly is not totally useless like I thought previously
  - Made PowerPlus more of an abstract class
  - Generally changed the structure of the class (See documentation)
- PowerPlus now has a documentation entry
- Any public fields are now public properties instead.
- Fixed up PowerPlus, specifically PhysicalProperties (I implore you, check the comment in PowerPlus [MOD]/Code/Main.cs)
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