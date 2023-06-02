# StudioPlusAPI
## ChemistryPlus
### LiquidReaction() 
This collection of methods allows you to create custom liquid mixes. Contains 5 overloads.<br/>
```cs
public static void LiquidReaction(string input1, string input2, string target, float ratePerSecond = 0.05f)

public static void LiquidReaction(string input1, string input2, string input3, string target, float ratePerSecond = 0.05f)

public static void LiquidReaction(Liquid[] inputs, Liquid target, float ratePerSecond = 0.05f)

public static void LiquidReaction(Liquid[] inputs, Liquid[] targets, float ratePerSecond = 0.05f)

public static void LiquidReaction(Liquid[] inputs, Liquid[] targets, int[] ratios, float ratePerSecond = 0.05f)
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
For advanced users, you can use an array to mix as many liquids into one as you want:
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
For even more advanced users the 4th overload allows you to mix multiple input liquids into multiple output liquids
```cs
ChemistryPlus.LiquidReaction(
    new Liquid[]
    {
        Liquid.GetLiquid(Sugar.ID),
        Liquid.GetLiquid(Spice.ID),
        Liquid.GetLiquid(EverythingNice.ID),
        Liquid.GetLiquid(Chemistry.Tritium.ID)
    }, 
    new Liquid[]
    {
        Liquid.GetLiquid(Blossom.ID),
        Liquid.GetLiquid(Bubbles.ID),
        Liquid.GetLiquid(Buttercup.ID)
    },
    0.06f
)
```
These liquids will be created in equal ratios. However, if you want unique ratios, use the 5th overload:
```cs
ChemistryPlus.LiquidReaction(
    new Liquid[]
    {
        Liquid.GetLiquid(Sugar.ID),
        Liquid.GetLiquid(Spice.ID),
        Liquid.GetLiquid(EverythingNice.ID),
        Liquid.GetLiquid(Chemistry.Tritium.ID)
    }, 
    new Liquid[]
    {
        Liquid.GetLiquid(Blossom.ID),
        Liquid.GetLiquid(Bubbles.ID),
        Liquid.GetLiquid(Buttercup.ID)
    },
    new int[]
    {
        2,
        3,
        4
    },
    0.09f
)
```
I'm going to explain how these ratios work:<br/>
First of all, the number of ratios and outputs must match exactly, otherwise you will get an error.<br/>
When the mixing process takes place, a total of 9 (sum of all numbers in the ratios array) parts of liquid will be made. 2 parts will be Blossom, 3 parts will be Bubbles and 4 parts will be ButterCup.

You might have noticed that for the last 2 overloads I specifically set a ratePerSecond to 0.06f and 0.09f respectively. This is very important so read carefully:<br/>
Because PPG code is garbage, if a given liquid in a container is below 0.02 units, it gets removed. The way these 2 overloads work is that they divide the inputed ratePerSecond, which could result in the liquid reaction not working properly.

To save you the headache of having to adjust these values yourself, the methods will automatically calculate the lowest ratePerSecond that will work and adjust your inputted ratePerSecond accordingly. If you're curious how those values are calculated, the general formula is
```cs
float minRate = 0.02f * divisor / Mathf.Min(ratios);
```
Divisor here is the sum of all ratios.<br/>
For the 4th overload, the divisor is simply the number of output liquids and the minimum value is 1 (Because what the 4th overload does is basically a 1 : 1 : ... ratio) so the formula is:
```cs
float minRate = 0.02f * targets.Length;
```
For the 4th overload example, the minimum value is 0.02f * 3 = 0.06f, and for the 5th overload example the minimum value is 0.02f * 9 / 2 = 0.09f.

The larger the ratePerSecond is, the more inaccurate does the mix become so approach the last 2 overloads with caution.