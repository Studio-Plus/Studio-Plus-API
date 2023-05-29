# StudioPlusAPI
## ChemistryPlus
### AddBottleOpening()
This method allows you to add a bottle opening to any container.
```cs
public static PointLiquidTransferBehaviour AddBottleOpening(this BloodContainer container, Vector2 position, Space outerSpace = Space.Self)
```
It's important to note that BloodContainer class is the base class for all liquid containers, so you can also call it from FlaskBehaviour, CirculationBehaviour, etc.<br/>
Returns PointLiquidTransferBehaviour in case you have to make any additional modifications.

The argument outerSpace determines whether to use relative or absolute position for Space.Self and Space.World respectively. You will almost always want to use relative position so it defaults to Space.Self but what do I know about what kind of item you want to make?<br/>
Here is an example of how to use it:
```cs
PointLiquidTransferBehaviour.bottleOpening = GetComponent<BloodContainer>().AddBottleOpening(new Vector2(-1f, 4.5f) * ModAPI.PixelSize);
```