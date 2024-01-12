using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("Layout/Text Box Sizer")]
public class TextBoxSizer : MonoBehaviour
{
    /// <summary>
    /// Extends the height of the background image past the text size
    /// </summary>
    public float backgroundHeightOffset = 0;

    /// <summary>
    /// Extends the width of the background image past the text size
    /// </summary>
    public float backgroundWidthOffset = 0;

    /// <summary>
    /// the heights the box will grow
    /// -1 means no limit
    /// </summary>
    public float maxHeight = -1;

    /// <summary>
    /// the widest the box will grow
    /// -1 means no limit
    /// </summary>
    public float maxWidth = -1;

    /// <summary>
    /// This forces the height of the text box to a fix value
    /// -1 means it will not force the value
    /// </summary>
    public float fixedHeight = -1;

    /// <summary>
    /// This forces the width of the text box to a fix value
    /// -1 means it will not force the value
    /// </summary>
    public float fixedWidth = -1;

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
        get {
            if (_textMeshProUGUI == null && transform.GetComponentInChildren<TMPro.TextMeshProUGUI>())
            {
                _textMeshProUGUI = transform.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                _tmpRecTransform = _textMeshProUGUI.rectTransform;
            }

            //_tmpRecTransform = transform.GetComponentInChildren<RectTransform>();
            return _textMeshProUGUI;
        }
    }

    /// <summary>
    /// Rectangle Transform of the current object this script is attached too
    /// </summary>
    private RectTransform recTransform;

    /// <summary>
    /// interface for recTransform
    /// </summary>
    public RectTransform Rect { 
        get { 

            if (recTransform == null)
            {
                recTransform = GetComponent<RectTransform>();
            }
            return recTransform;
        }
     }

    /// <summary>
    /// Rectangle Transform of the child TMP
    /// </summary>
    private RectTransform _tmpRecTransform;

    /// <summary>
    /// interface for _tmpRecTransform
    /// </summary>
    public RectTransform TMPRectTransform { get { return _tmpRecTransform; } }

    /// <summary>
    /// Holds the height that TMP thinks it should be
    /// </summary>
    private float _preferredHeight;

    /// <summary>
    /// Interface for _preferredHeight
    /// </summary>
    public float PreferredHeight { get { return _preferredHeight; } }

    /// <summary>
    /// Holds the width that TMP thinks it should be
    /// </summary>
    private float _preferredWidth;

    /// <summary>
    /// Interface for _preferredWidth
    /// </summary>
    public float PreferredWidth { get { return _preferredWidth; } }

    /// <summary>
    /// Sets both the Height and the Width of the Text box
    /// </summary>
    private void SetSize()
    {
        if (TextMeshPro == null) return;

        Rect.sizeDelta = new Vector2(TextMeshPro.preferredWidth + backgroundWidthOffset, TextMeshPro.preferredHeight + backgroundHeightOffset);
        _tmpRecTransform.sizeDelta = new Vector2(TextMeshPro.preferredWidth, TextMeshPro.preferredHeight);
    }

    /// <summary>
    /// Calls all functions needed to resize the box
    /// </summary>
    private void ResizeBox()
    {
        SetSize();
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        ResizeBox();
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        ResizeBox();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (
            _preferredHeight != TextMeshPro.preferredHeight + backgroundHeightOffset 
            || 
            _preferredWidth != TextMeshPro.preferredWidth + backgroundWidthOffset
        )
        {
            ResizeBox();
        }
    }
}
