using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
How To:

Add this to the game object that will be listening to for an event then set it to a public function with in that object to call.

That public function should be like this.

```C#
public void FuncName(Component sender, object data)
{
    //DoStuff
}
```

You will need to determine what Data is and then act on it accordingly

Extra:
Component can allow you to listen in to only pay attention to a given component.
*/

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object>{}

/// <summary>
/// 
/// </summary>
public class GameEventListener : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public GameEvent gameEvent;

    /// <summary>
    /// 
    /// </summary>
    public CustomGameEvent response;

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnDisable()
    {
        gameEvent.RegisterListener(this);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnEventRaised(Component sender, object data)
    {
        response.Invoke(sender, data);
    }
}
