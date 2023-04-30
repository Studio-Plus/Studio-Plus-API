# StudioPlusAPI
## ChemistryPlus
### LiquidReaction() 
This method allows to add a liquid to an already existing item. Contains 3 overloads.<br/>
```cs
public static void LiquidReaction(string liquid1, string liquid2, string target, float ratePerSecond = 0.05f)

public static void LiquidReaction(string liquid1, string liquid2, string liquid3, string target, float ratePerSecond = 0.05f)

public static void LiquidReaction(Liquid[] ingredientLiquids, Liquid target, float ratePerSecond = 0.05f)
```
The most basic version is for mixing 2 liquids into one:
```cs
ChemistryPlus.LiquidReaction(LifeSyringe.LifeSerumLiquid.ID, Chemistry.Tritium.ID, DeathSyringe.InstantDeathPoisonLiquid.ID);
```
Again, when working with liquids it's better to use a reference to the ID string rather than a plain string (More on that in AddLiquidToItem().md)

You can also mix 3 liquids into one:
```cs
ChemistryPlus.LiquidReaction(Sugar.ID, Spice.ID, EverythingNice.ID, ThePerfectLittleGirl.ID);
```
For advanced users, you can use an array to mix as many liquids into one as you want. It is not possible to mix e.g. 4 liquids into 2 with 1 API method, although it shouldn't be hard to implement something like this.
```cs
ChemistryPlus.LiquidReaction(
    new Liquid[]
    {
        Liquid.GetLiquid(Sugar.ID),
        Liquid.GetLiquid(Spice.ID),
        Liquid.GetLiquid(EverythingNice.ID),
        Liquid.GetLiquid(Chemistry.Tritium.ID)
    }, 
    Liquid.GetLiquid(PowerpuffGirls.ID)
)
//If you're wondering, no, I'm not a Powerpuff Girls fan, it's just a fun example.
```