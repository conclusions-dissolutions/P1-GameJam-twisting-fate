// A script that plays audio clips when a Button is clicked or hovered over.
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This script adds audio sounds to a Button.
/// </summary>
[RequireComponent(typeof(Button))]
public class ButtonSounds : MonoBehaviour
{
    /// <summary>
    /// The sound to play when the mouse hovers over the button.
    /// </summary>
    public AudioClip hoverSound;

    /// <summary>
    /// The sound to play when the button is clicked.
    /// </summary>
    public AudioClip clickSound;

    /// <summary>
    /// The sound to play when the button is clicked but not enabled.
    /// </summary>
    public AudioClip errorSound;

    /// <summary>
    /// The AudioSource component used to play audio.
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// The EventTrigger component used to handle events.
    /// </summary>
    private EventTrigger _eventTrigger;

    /// <summary>
    /// The Button component that is being controlled by the script.
    /// </summary>
    private Button _button;

    /// <summary>
    /// Called when the game starts or when the MonoBehaviour is enabled.
    /// </summary>
    void Start()
    {
        // Initialize the AudioSource and EventTrigger components
        _audioSource = gameObject.AddComponent<AudioSource>();
        _eventTrigger = gameObject.AddComponent<EventTrigger>();
        _button = GetComponent<Button>();

        // Prevent audio from playing automatically
        _audioSource.playOnAwake = false;

        // Add event triggers for clicking and hovering
        _eventTrigger.triggers.Add(new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick,
            callback = new EventTrigger.TriggerEvent()
        });
        _eventTrigger.triggers.Add(new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter,
            callback = new EventTrigger.TriggerEvent()
        });

        // Add event listeners for the click and hover events
        _eventTrigger.triggers[0].callback.AddListener((data) => OnClick());
        _eventTrigger.triggers[1].callback.AddListener((data) => OnHover());
    }

    /// <summary>
    /// Called when the button is clicked.
    /// </summary>
    private void OnClick()
    {
        // Play the appropriate sound based on the button's interactable state
        if (_button.interactable)
        {
            _audioSource.PlayOneShot(clickSound);
        }
        else
        {
            _audioSource.PlayOneShot(errorSound);
        }
    }

    /// <summary>
    /// Called when the mouse hovers over the button.
    /// </summary>
    private void OnHover()
    {
        // Play the hover sound
        _audioSource.PlayOneShot(hoverSound);
    }
}
