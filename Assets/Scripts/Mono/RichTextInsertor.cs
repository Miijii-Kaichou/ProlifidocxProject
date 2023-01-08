using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RichTextInsertor : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textContentTMP;

    [SerializeField]
    CaretTracker caretTracker;

    //Rich Text Insertor Tags
    string[] rtfTagsNames =
    {
        "align",
        "alpha",
        "color",
        "b",
        "i",
        "cspace",
        "font",
        "indent",
        "line-height",
        "line-indent",
        "link",
        "lowercase",
        "uppercase",
        "smallcaps",
        "margin",
        "mark",
        "mspace",
        "noparse",
        "nobr",
        "page",
        "pos",
        "size",
        "space",
        "sprite",
        "s",
        "u",
        "style",
        "sub",
        "sup",
        "voffset",
        "width",
    };

    public enum RTFTags
    {
        Align,
        Alpha,
        Color,
        Bold,
        Italize,
        CSpace,
        Font,
        Indent,
        LineHeight,
        LineIndent,
        Link,
        Lowercase,
        Uppercase,
        Smallcaps,
        Margin,
        Mark,
        MSpace,
        NoParse,
        NoBreak,
        Page,
        Pos,
        Size,
        Space,
        Sprite,
        Strikethrough,
        Underline,
        Style,
        Subscript,
        Superscript,
        VerticalOffset,
        Width
    }

    string _tagStartFormat = "<{0}>";
    string _tagEndFormat = "</{0}>";

    //Bullet Stats
    [SerializeField]
    bool isBulletListing = false;

    private void Start()
    {

    }

    #region Styling Commands
    public void Bold()
    {

    }

    public void BulletList()
    {
        if (caretTracker.HighlightedString != string.Empty)
        {
            var lines = caretTracker.HighlightedString.Split('\n');
            string newString = string.Empty;
            for (int i = 0; i < lines.Length; i++)
            {
                newString += "\t• " + lines[i] + (i < lines.Length - 1 ? "\n" : "");
            }
            caretTracker.UpdateHighlightedLine(newString);
            isBulletListing = true;

            caretTracker.QueueForUpdate(() =>
            {
                caretTracker.ShiftCursor((caretTracker.LengthOfPreviousLines + caretTracker.CurrentLineIndex) - caretTracker.CursorPos);
            });
        }
        else
        {
            var line = caretTracker.GetCurrentLine();
            line = "\t• " + line;
            caretTracker.UpdateLine(line);

            caretTracker.QueueForUpdate(() =>
            {
                caretTracker.ShiftCursor((caretTracker.LengthOfPreviousLines + caretTracker.CurrentLineIndex) - caretTracker.CursorPos);
            });
        }
    }
    #endregion

    public void UpdateBulletListingStatus()
    {
        isBulletListing = caretTracker.GetCurrentLine().Contains("•");
    }

    public void AddNewItem()
    {
        var line = caretTracker.GetCurrentLine();
        line = "\t•   " + line;
        caretTracker.UpdateLine(line);
    }

    public string SurroundWithTag(RTFTags tag, int start, int length)
    {
        return string.Format(_tagStartFormat, rtfTagsNames[(int)tag]) + textContentTMP.text.Substring(start, length) + string.Format(_tagEndFormat, rtfTagsNames[(int)tag]);
    }
}
