# StudioPlusAPI
## ChemistryPlus
### AddLiquidToItem() 
This method allows to add a liquid to an already existing item. Contains 2 overloads.<br/>
```cs
public static void AddLiquidToItem(string item, string newLiquidID, float amount)

public static void AddLiquidToItem(string item, string newLiquidID, float amount, float capacity)
```
By default capacity is adjusted to the amount of liquid you add:
```cs
ChemistryPlus.AddLiquidToItem("Rotor", Oil.ID, 1.4f);

//ChemistryPlus.AddLiquidToItem("Rotor", "OIL", 1.4f);
```
While using simple strings for the IDs of liquids is possible, using the ID stored in the actual class is recommended as it will be more resistant to breaking with updates to the game.<br/>
For your convinience, a list of all liquid IDs will be provided in this folder.

If you so wish, you can provide a capacity for the container (capacity must be larger than amount):
```cs
ChemistryPlus.AddLiquidToItem("Rotor", Oil.ID, 1.4f, 2.8f);
```
This is basically useless, I know.