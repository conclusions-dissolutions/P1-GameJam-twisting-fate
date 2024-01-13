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
    /// What will be displayed char by char
    /// </summary>
    public string textToPrint;

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
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (_textMeshProUGUI == null) return; // ERROR IF TRUE!

        if (!isTyping)
        {
            /// IFF we have a delay timer destroy after that time has passed
            if (destroyAfter > 0)
            {
                destroyAfter -= Time.deltaTime;
                if (destroyAfter < 0)
                {
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
                return;
            }
            currentText += textToPrint[currentText.Length];
            _textMeshProUGUI.text = currentText;
        }
    }
}
