using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FocusView : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTransform;

    [SerializeField]
    Scrollbar scrollBar;

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

    public void Interpolate(float value)
    {
        if (focusingOnCaret)
            focusingOnCaret = false;

        if (focusingOnCaret == false)
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, Mathf.Lerp(tracker.GetLastCharacterPosition().y, tracker.GetFirstCharacterPosition().y, value));
    }

    public void TryPosition()
    {
        if (focusingOnCaret == false) return;

        if ((tracker.GetLastCharacterPosition().y - tracker.GetFirstCharacterPosition().y) == 0)
            return;

        var value = 

        scrollBar.value = 1 - (Mathf.Abs((rectTransform.anchoredPosition.y - tracker.GetFirstCharacterPosition().y))
        / Mathf.Abs((tracker.GetLastCharacterPosition().y - tracker.GetFirstCharacterPosition().y)));

    }
}