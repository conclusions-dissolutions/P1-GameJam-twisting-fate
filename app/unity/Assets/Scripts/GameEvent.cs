using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
How To:

To add an event dispatcher, add this to your class

//// As a Prop

[Header("Events")]
public GameEvent onNameEvent;

//// Later In code

// Be sure to add your data when needed
onNameEvent.Rase(this, 0);

 */

/// <summary>
/// Custom class that is used to hold all those who listen for a given event.
/// </summary>
[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    /// <summary>
    /// Holds all registered listeners
    /// </summary>
    public List<GameEventListener> listeners = new List<GameEventListener>();

    // Raise event through different methods signatures

    /// <summary>
    /// Call listeners that are registered
    /// </summary>
    public void Raise()
    {
        Raise(null, null);
    }

    /// <summary>
    /// Call listeners that are registered
    /// </summary>
    public void Raise(Component sender)
    {
        Raise(sender, null);
    }

    /// <summary>
    /// Call listeners that are registered
    /// </summary>
    public void Raise(object data)
    {
        Raise(null, data);
    }

    /// <summary>
    /// Call listeners that are registered
    /// </summary>
    public void Raise(Component sender, object data)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised(sender, data);
        }
    }

    // Manage Listeners

    /// <summary>
    /// New listener to be added to the list
    /// </summary>
    /// <param name="listener"></param>
    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    /// <summary>
    /// listener to be removed from the list
    /// </summary>
    /// <param name="listener"></param>
    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }


}
