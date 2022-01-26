using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using static Extensions.Convenience;

public class CaretTracker : MonoBehaviour
{
    [SerializeField]
    RectTransform _rectTransform;

    public RectTransform RectTransform => _rectTransform;

    [SerializeField]
    TextMeshProUGUI docuContent;

    [SerializeField]
    TMP_InputField _inputField;

    [SerializeField]
    int _cursorPos;

    [SerializeField]
    int _linePos;

    public int CursorPos
    {
        get
        {
            return _cursorPos;
        }
        set
        {
            _cursorPos = value;
            onCursorMove.Invoke();
        }
    }

    [SerializeField]
    int _anchoredSelectPos;

    [SerializeField]
    int previousCursorPos;


    [SerializeField]
    int selectionLength;

    [SerializeField, TextArea(10, 20)]
    string highlightedString;

    public string HighlightedString => highlightedString;

    public float InitYPosition { get; private set; }
    bool initYSet = false;
    int _currentLineIndex = 0;
    int _previousLineCount;

    [SerializeField]
    int _lengthOfPreviousLines = 0;

    public int LengthOfPreviousLines => _lengthOfPreviousLines;
    public int CurrentLineIndex => _currentLineIndex;


    bool onFocus => _inputField.isFocused;

    Queue<Action> queuedCalls = new Queue<Action>();
    public int PreviousLineCount
    {
        get
        {
            return _previousLineCount;
        }
        set
        {
            if (_previousLineCount != value)
            {
                _previousLineCount = value;
                onNewLine.Invoke();
            }
        }
    }

    Sign sign;

    bool isHighlighting = false;

    public Action refocusCall;

    public UnityEvent onCursorMove;

    public UnityEvent onNewLine;

    [SerializeField]
    string[] contentDataList;

    public string[] ContentDataList => contentDataList;

    //Cycles
    WaitForSeconds contentDataListUpdateDelay;



    private void Awake()
    {
        contentDataListUpdateDelay = new WaitForSeconds(0.05f);
        StartCoroutine(DataListUpdateCycle());
    }

    public void Update()
    {

        GetCurrentLine();

        UpdateXPosition();

        //Update Cursor Position
        CursorPos = _inputField.caretPosition;

        //Update Previous Line Count (Reactive)
        _previousLineCount = docuContent.textInfo.lineCount;

        //Update AnchoredSelectPosition for highlighting purposes
        //as well as the selection length
        _anchoredSelectPos = _inputField.selectionAnchorPosition;
        selectionLength = Mathf.Abs(_anchoredSelectPos - _inputField.selectionFocusPosition);

        //To know how to grab the substring of the text, we use the sign 
        //of the direction of highlighting.
        sign = (_inputField.selectionFocusPosition - _anchoredSelectPos).ToSign();

        if (isHighlighting && onFocus)
            GetHighlightedString();
        else if (!isHighlighting && onFocus)
            highlightedString = string.Empty;

        CalculateSumOfPreviousLineLengths();


    }

    internal void QueueForUpdate(Action p)
    {
        queuedCalls.Enqueue(p);
    }

    public void CalculateSumOfPreviousLineLengths()
    {
        int length = 0;
        for(int i = 0; i < _currentLineIndex + 1; i++)
        {
            length += contentDataList[i].Length;
        }

        _lengthOfPreviousLines = length;
    }

   

    /// <summary>
    /// The cycle in which updated new
    /// data content for things like indentions,
    /// lists, etc.
    /// </summary>
    /// <returns></returns>
    IEnumerator DataListUpdateCycle()
    {
        while (true)
        {
            UpdateList();

            yield return contentDataListUpdateDelay;

            if (queuedCalls.Count > 0)
            {
                var call = queuedCalls.Dequeue();
                call.Invoke();
            }
        }
    }

    void UpdateList()
    {
        //Splits entire string into a DataList
        var newDataList = docuContent.text.Substring(0, docuContent.text.Length - 1).Split('\n');

        //Update only when necessary
        if (contentDataList != newDataList)
            contentDataList = newDataList;
    }

    /// <summary>
    /// The X position will update based on the farthest character on the TMP component
    /// </summary>
    void UpdateXPosition()
    {
        //Despite BottomLeft of a character doesn't account for padding
        //it seems to be the best method for knowing what position
        //a character is in local space.
        var position = docuContent.textInfo.characterInfo[CursorPos].bottomLeft;

        //Have the tracker go to the character position
        _rectTransform.anchoredPosition = position;

        //If the anchor is not equal to the focus
        //(start and end positions of highlighting),
        //we are currently highlighting a word.
        if (_inputField.selectionAnchorPosition != _inputField.selectionFocusPosition)
            isHighlighting = true;
        else
        {
            isHighlighting = false;
            highlightedString = string.Empty;
        }
    }

    /// <summary>
    /// Replace the highlighted text.
    /// </summary>
    /// <param name="v"></param>
    internal void UpdateHighlightedLine(string v)
    {
        _inputField.text = _inputField.text.Replace(highlightedString, v);
        UpdateList();
    }

    /// <summary>
    /// Get the text that is currently highlighted.
    /// </summary>
    void GetHighlightedString()
    {
        refocusCall.Invoke();
        highlightedString = docuContent.text.Substring(sign == Sign.Negative ? CursorPos : _anchoredSelectPos, Mathf.Abs(_anchoredSelectPos - CursorPos));
        UpdateList();
        GetCurrentLine();
    }

    /// <summary>
    /// Replace the current line.
    /// </summary>
    /// <param name="v"></param>
    internal void UpdateLine(string v)
    {
        if (contentDataList[_currentLineIndex] == string.Empty)
        {
            _inputField.text = _inputField.text.Insert(CursorPos, v);
            return;
        }
        _inputField.text = ReplaceLastOccurrence(_inputField.text,contentDataList[_currentLineIndex], v);
        GetCurrentLine();
    }

    public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
    {
        int Place = Source.LastIndexOf(Find);
        string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
        return result;
    }

    internal void ShiftCursor(int distance)
    {
        var shift = _inputField.caretPosition + distance;
        _inputField.caretPosition = shift;
        GetCurrentLine();
    }

    /// <summary>
    /// Returns the last character position from the text.
    /// This is used for scrolling.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetLastCharacterPosition() => docuContent.textInfo.characterInfo[docuContent.text.Length - 1].bottomLeft;

    /// <summary>
    /// returns the first character position from the text.
    /// This is used for scrolling.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetFirstCharacterPosition() => docuContent.textInfo.characterInfo[0].bottomLeft;

    /// <summary>
    /// Return the Input Field
    /// </summary>
    /// <returns></returns>
    public TMP_InputField GetInputField() => _inputField;

    /// <summary>
    /// Get the text of the current line.
    /// </summary>
    /// <returns></returns>
    public string GetCurrentLine()
    {
        contentDataList = docuContent.text.Substring(0, docuContent.text.Length - 1).Split('\n');
        var line = docuContent.text.Substring(0, CursorPos).Split('\n').Length - 1;
        _currentLineIndex = line;
        return contentDataList[_currentLineIndex] ?? string.Empty;
    }

    internal string GetPreviousLine()
    {
        return contentDataList[_currentLineIndex-1] ?? string.Empty;
    }
}
