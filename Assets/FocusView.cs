using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FocusView : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTransform;

    [SerializeField]
    RectTransform _inputRectTransform;

    [SerializeField]
    Scrollbar verticalScrollBar, horizontalScrollBar;

    [SerializeField]
    CaretTracker tracker;

    [SerializeField]
    TextMeshProUGUI textContent;

    [SerializeField]
    bool focusingOnCaret = true;

    private void Start()
    {
        tracker.refocusCall = Refocus;
    }

    public void Refocus()
    {
        if (focusingOnCaret == false)
        {
            focusingOnCaret = true;
            TryPosition();
        }
    }

    private void Update()
    {
        if(focusingOnCaret)
            UpdatePosition();   
    }

    public void UpdatePosition()
    {
        rectTransform.anchoredPosition = tracker.RectTransform.anchoredPosition;
    }

    internal void InterpolateHorizontally(float value)
    {
        if (focusingOnCaret)
            focusingOnCaret = false;

        if (focusingOnCaret == false)
            rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(_inputRectTransform.rect.size.x, tracker.GetFirstCharacterPosition().x, value), rectTransform.anchoredPosition.y);
    }

    public void InterpolateVertically(float value)
    {
        if (focusingOnCaret)
            focusingOnCaret = false;

        if (focusingOnCaret == false)
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, Mathf.Lerp(tracker.GetLastCharacterPosition().y, tracker.GetFirstCharacterPosition().y, value));
    }

    public void TryPosition()
    {
        if (focusingOnCaret == false) return;

        if ((tracker.GetLastCharacterPosition().y - tracker.GetFirstCharacterPosition().y) == 0
            || (_inputRectTransform.rect.size.x - tracker.GetFirstCharacterPosition().x == 0))
            return;

        verticalScrollBar.value = 1 - (Mathf.Abs((rectTransform.anchoredPosition.y - tracker.GetFirstCharacterPosition().y))
        / Mathf.Abs((tracker.GetLastCharacterPosition().y - tracker.GetFirstCharacterPosition().y)));

        horizontalScrollBar.value = 1 - (Mathf.Abs((rectTransform.anchoredPosition.x - tracker.GetFirstCharacterPosition().x))
            / Mathf.Abs((_inputRectTransform.rect.size.x - tracker.GetFirstCharacterPosition().x)));
    }
}