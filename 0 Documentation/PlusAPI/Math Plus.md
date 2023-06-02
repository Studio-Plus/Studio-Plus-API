# StudioPlusAPI
## PlusAPI
This is just a collection of multiple consecutive math functions that I gave the informal module name 'Math Plus.'
### WaveClamp()
A collection of a few special mathemagical functions that can be easily applied. We will be skipping over boring mathematical details and jump right into the mathemagic!<br/>
Contains a total of 3 overloads:
####  WaveClamp01()
```cs
public static float WaveClamp01(float num, float period)
```
This special overload that is actually its own method will interchangibly return a value between 0 and 1 given a periodic amount of time period and a self-incrementing value num.<br/>
I assume only 0.1% of readers will understand what I mean so a quick example:
```cs
float timer = 0f;
float myValue = 0f;

public void FixedUpdate()
{
    myValue = PlusAPI.WaveClamp01(timer, 2f);
    timer += Time.fixedDeltaTime;
}
```
This method is mostly meant for these kinds of scenarios<br/>
In this example, myValue will first be 0 and will start going upwards, then after 2 seconds it will be 1 and will start to go down. After 2 more seconds (4 total since this code started running) myValue will be back at 0 and so on. Why 2 seconds? Because the timer measures time in seconds, and the period between the extrema (in this case, 0 and 1) is set to 2f, so 2 seconds.<br/>
The property of it interchanging between 2 different values at a fixed speed could be extremely useful in certain scenarios.
#### WaveClamp0x()
This is another special overload that is its own method but still related to WaveClamp
```cs
public static float WaveClamp0x(float num, float period, float maxNum)
```
Similar to WaveClamp01(), but it  will interchange between 0 and a specified maxNum instead.<br/> 
MaxNum parameter cannot be 0 and the method will throw an exception in that case. If maxNum is negative, the method will automatically convert it to a positive value.<br/>
#### WaveClamp()
This is the generic method.<br/>
```cs
public static float WaveClamp(float num, float period, float minNum, float maxNum)
```
Similar to WaveClamp0x(), but you can also specify the minimum value, so it will interchange between minNum and MaxNum. When num is 0, minNum will be returned. For each multiple of period (1\*period, 2\*period, 3\*period, etc), it will return maxNum, minNum, maxNum, etc. respectively<br/>
minNum and maxNum can't be equal and the method will throw an exception if they are.<br/>
If minNum is greater than maxNum, it will still behave as expected but instead of starting at the minimum value it wil start at the maximum value.

### ToFloat()
Changes a byte (0 to 255) into its corresponding float. Clamped between 0f and 1f:
```cs
public static float ToFloat(this byte value)
{
    float newValue = value;
    float returnValue = newValue / 255f;
    return Mathf.Clamp01(returnValue);
}
```

### ToByte()
Inverse operation of ToFloat, also clamped between 0 and 255:
```cs
public static byte ToByte(this float value)
{
    float newValue = Mathf.Clamp01(value) * 255f;
    return (byte)newValue;
}
```

### Inv()
Returns the inverse of a given float.<br/>
```cs
public static float Inv(this float num)
{
    if (num == 0f) 
        return 0f;
    if (num == 1f)
        return 1f;
    return 1f / num;
}
```
In other words, if you input 2f in the method it will return 1/2, or 0.5f.
If num is equal to 1f it will return 1f, same for 0f.

### GetAbs() (Vector2/Vector3)
Returns the absolute value of a Vector
```cs
public static Vector2 GetAbs(this Vector2 originalVector)

public static Vector3 GetAbs(this Vector3 originalVector)
```

### Sum() (float/int)
Returns the sum of given floats or integers
```cs
public static float Sum(params float[] values)

public static int Sum(params int[] values)
```
You can input the values as either an array or individual values
```cs
float example1 = PlusAPI.Sum(1.9f, 2.1f, 4.3f, 6f);
float[] exampleArray = new float[]
{
    1.9f, 
    2.1f, 
    4.3f, 
    6f
};
float example2 = PlusAPI.Sum(exampleArray);
```
