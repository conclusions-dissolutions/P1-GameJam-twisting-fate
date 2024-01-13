using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxTyper : MonoBehaviour
{

    /// <summary>
    /// Destroy the game object after a set number of seconds have passed.
    /// -1 means it's disabled.
    /// </summary>
    public float destroyAfter = -1;

    /// <summary>
    /// Number of seconds before it starts typing
    /// -1 means it's disabled.
    /// </summary>
    public float delayStart = -1;

    /// <summary>
    /// Before we clear and start typing the next one
    /// -1 means it's disabled.
    /// </summary>
    public float delayBetweenText = -1;

    /// <summary>
    /// Float that is reused to keep track of time between text printed.
    /// </summary>
    private float delayTimerBeetweenText;

    /// <summary>
    /// What will be displayed char by char
    /// </summary>
    private string textToPrint;

    /// <summary>
    /// List of every line of text that needs to be printed
    /// </summary>
    public List<string> listOfTextToPrint;

    [Header("Events")]

    /// <summary>
    /// The event to fire off when the text box is destroyed.
    /// </summary>
    public GameEvent onTextBoxDestroy;

    /// <summary>
    /// tracker for which text we should be printing
    /// </summary>
    private int textIndex = 0;

    /// <summary>
    /// Seconds till the next character
    /// </summary>
    public float speed;

    /// <summary>
    /// indicates if the text has been fully printed
    /// </summary>
    private bool isTyping = true;

    /// <summary>
    /// interface for isTyping
    /// </summary>
    public bool IsTyping { get { return isTyping; } }

    /// <summary>
    /// Holds how many seconds have passed from when the last char was printed
    /// </summary>
    private float secFromLast;

    /// <summary>
    /// Test renderer for the text box
    /// </summary>
    [SerializeField]
    private TMPro.TextMeshProUGUI _textMeshProUGUI;

    /// <summary>
    /// Gets and sets the render and the transforms of the children
    /// </summary>
    public TMPro.TextMeshProUGUI TextMeshPro
    {
        get
        {
            if (_textMeshProUGUI == null && transform.GetComponentInChildren<TMPro.TextMeshProUGUI>())
            {
                _textMeshProUGUI = transform.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            }

            return _textMeshProUGUI;
        }
    }

    /// <summary>
    /// Called on Enabled ;)
    /// </summary>
    private void OnEnable()
    {
        if (TextMeshPro == null) return; // ERROR IF TRUE!

        _textMeshProUGUI.text = "";
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        if (TextMeshPro == null) return; // ERROR IF TRUE!
        _textMeshProUGUI.text = "";

        //TODO: HACK!
        if (!PersistentVariables.isFreshStart)
        {
            listOfTextToPrint = new List<string>();
            return;
        }



        delayTimerBeetweenText = delayBetweenText;
        textToPrint = listOfTextToPrint[0];
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (_textMeshProUGUI == null || listOfTextToPrint.Count == 0) return; // ERROR IF TRUE!

        if (delayStart > 0)
        {
            delayStart -= Time.deltaTime;
            return;
        }

        if (!isTyping)
        {
            if (textIndex < listOfTextToPrint.Count)
            {
                delayTimerBeetweenText -= Time.deltaTime;
                if (delayTimerBeetweenText < 0)
                {
                    isTyping = true;
                    _textMeshProUGUI.text = "";
                    textToPrint = listOfTextToPrint[textIndex];
                }

                return;
            }

            /// IFF we have a delay timer destroy after that time has passed
            if (destroyAfter > 0)
            {
                destroyAfter -= Time.deltaTime;
                if (destroyAfter < 0)
                {
                    if (onTextBoxDestroy != null)
                    {
                        onTextBoxDestroy.Raise(this, GameState.mainMenu);
                    }
                    Destroy(gameObject);
                }
            }
            else
            {
                return;
            }
        }

        secFromLast += Time.deltaTime;
        if (secFromLast > speed)
        {
            secFromLast = 0;

            // Update text
            string currentText = _textMeshProUGUI.text;

            // Would like something more hard stop
            if (currentText.Length == textToPrint.Length)
            {
                isTyping = false;
                textIndex++;
                delayTimerBeetweenText = delayBetweenText;
                return;
            }
            currentText += textToPrint[currentText.Length];
            _textMeshProUGUI.text = currentText;
        }
    }
}
