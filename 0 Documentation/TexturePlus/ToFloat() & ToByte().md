# StudioPlusAPI
## TexturePlus
### ToFloat()
Changes a byte color (0 to 255) into its corresponding float. Clamped between 0f and 1f:
```cs
public static float ToFloat(byte value)
{
    float newValue = (float)value;
    float returnValue = newValue / 255f;
    return Mathf.Clamp01(returnValue);
}
```

### ToByte()
Inverse operation of ToFloat, also clamped between 0 and 255:
```cs
public static byte ToByte(float value)
{
    float newValue = Mathf.Clamp01(value) * 255f;
    return (byte)newValue;
}
```