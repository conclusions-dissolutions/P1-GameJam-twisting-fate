using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object>{}

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
