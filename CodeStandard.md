# Code Standard

## When to document

Rule of thumb - Continuous Documentation

BUT! Visual studio can do a lot of it for you!

You start by typing `///` above where you want to put the code comment.

So if you add code comments in when you about to go into review, that's A-OK. I wont know ;) but git-history will.

## Other Notes

1. Rule of thumb - Continuous Documentation.
2. Be sure to always keep a space between props and functions.
3. In function comments are with two slashes. `//`
4. No Magic Numbers: Values that don't have an obvious meaning

## Example Class

```C#
/// <summary>
/// Add a summary of the Class
/// </summary>
public class ClassName : ParentClassName
{

    /// <summary>
    /// Add a summary of the prop
    /// </summary>
    int examplePropOne;

    /// <summary>
    /// Add a summary of the prop
    /// </summary>
    int examplePropTwo;

    /// <summary>
    /// Add a summary of the function
    /// </summary>
    /// <param name="example">Description of the prop</param>
    /// <returns>Description of what is returned</returns>
    public int FunctionName(int exampleParam)
    {
        // In function comments should only be two slashes
        return 1;
    }

}
```
