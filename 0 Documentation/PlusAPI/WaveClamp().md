# StudioPlusAPI
## PlusAPI
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
In this sexample, myValue will first be 0 and will start going upwards, then after 2 seconds it will be 1 and will start to go down. After 2 more seconds (4 total since this code started running) myValue will be back at 0 and so on. Why 2 seconds? Because the timer measures time in seconds, and the period between the extrema (in this case, 0 and 1) is set to 2f, so 2 seconds.<br/>
The property of it interchanging between 2 different values at a fixed speed could be extremely useful in certain scenarios.
#### WaveClamp()
This is the generic method containing the 2 remaining overloads.<br/>
Overload 1:
```cs
public static float WaveClamp(float num, float period, float maxNum)
```
Similar to WaveClamp01(), but it  will interchange between 0 and a specified maxNum instead.<br/> 
MaxNum parameter cannot be 0 and the method will throw an exception in that case. If maxNum is negative, the method will automatically convert it to a positive value.<br/>
But wait, why does maxNum here come first? It's because the mathematical function of this overload is way easier than the function of the other overload so it comes first.

Overload 2:
```cs
public static float WaveClamp(float num, float period, float maxNum, float minNum)
```
Similar to the 1st WaveClamp() overload, but you can also specify the minimum value, so it will interchange between minNum and MaxNum. When num is 0, minNum will be returned.<br/>
minNum and maxNum can't be equal and the method will throw an exception if they are. If minNum is larger than maxNum, the values will be flipped around, so minNum will actually be maxNum in the mathematical function and vice versa.
