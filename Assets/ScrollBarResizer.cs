using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarResizer : MonoBehaviour, IDragHandler
{
    [SerializeField]
    Scrollbar _verticalScrollBar;

    [SerializeField]
    CaretTracker _tracker;

    [SerializeField]
    FocusView _focusView;

    //InputField init size
    public float fieldInitSize = 0;

    RectTransform inputFieldRect;

    private void Start()
    {
        inputFieldRect = _tracker.GetInputField().GetComponent<RectTransform>();
    }

    public void OffsetView()
    {
        _focusView.Interpolate(_verticalScrollBar.value);
    }

    public void Resize()
    {
        if (fieldInitSize == 0)
            fieldInitSize = inputFieldRect.rect.size.y;

        //Todo: Resize based on inputField size
        _verticalScrollBar.size = fieldInitSize / inputFieldRect.rect.size.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        OffsetView();
    }
}
