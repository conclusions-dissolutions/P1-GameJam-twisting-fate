using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("Layout/Text Box Sizer")]
public class TextBoxSizer : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public float _offsetHeight = 0;

    /// <summary>
    /// 
    /// </summary>
    public float _offsetWidth = 0;

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
    /// 
    /// </summary>
    [SerializeField]
    private TMPro.TextMeshProUGUI _textMeshProUGUI;

    /// <summary>
    /// 
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
    /// 
    /// </summary>
    private RectTransform recTransform;

    /// <summary>
    /// 
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
    /// 
    /// </summary>
    private RectTransform _tmpRecTransform;

    /// <summary>
    /// 
    /// </summary>
    public RectTransform TMPRectTransform { get { return _tmpRecTransform; } }

    /// <summary>
    /// 
    /// </summary>
    private float _preferredHeight;

    /// <summary>
    /// 
    /// </summary>
    public float PreferredHeight { get { return _preferredHeight; } }

    /// <summary>
    /// 
    /// </summary>
    private void SetHeight()
    {
        if (TextMeshPro == null) return;

        Rect.sizeDelta = new Vector2(Rect.sizeDelta.x, TextMeshPro.preferredHeight + _offsetHeight);
        _tmpRecTransform.sizeDelta = new Vector2(Rect.sizeDelta.x, TextMeshPro.preferredHeight);
    }


    /// <summary>
    /// 
    /// </summary>
    private float _preferredWidth;

    /// <summary>
    /// 
    /// </summary>
    public float PreferredWidth { get { return _preferredWidth; } }

    /// <summary>
    /// 
    /// </summary>
    private void SetWidth()
    {
        if (TextMeshPro == null) return;

        Rect.sizeDelta = new Vector2(TextMeshPro.preferredWidth + _offsetWidth, Rect.sizeDelta.y);
        _tmpRecTransform.sizeDelta = new Vector2(TextMeshPro.preferredWidth, Rect.sizeDelta.y);
    }

    /// <summary>
    /// 
    /// </summary>
    private void resizeBox()
    {
        SetHeight();
        SetWidth();
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        resizeBox();
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        resizeBox();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (_preferredHeight != TextMeshPro.preferredHeight + _offsetHeight || _preferredWidth != TextMeshPro.preferredWidth + _offsetWidth)
        {
            resizeBox();
        }
    }
}
