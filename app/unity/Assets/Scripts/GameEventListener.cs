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
/// Used to intercept and direct Game Events
/// </summary>
public class GameEventListener : MonoBehaviour
{
    /// <summary>
    /// What event to listen for.
    /// </summary>
    public GameEvent gameEvent;

    /// <summary>
    /// Handles caring data to a target function.
    /// </summary>
    public CustomGameEvent response;

    /// <summary>
    /// Called when the component is enabled.
    /// </summary>
    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    /// <summary>
    /// Called when the component is disabled.
    /// </summary>
    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    /// <summary>
    /// Pass on info to the response.
    /// </summary>
    public void OnEventRaised(Component sender, object data)
    {
        response.Invoke(sender, data);
    }
}
