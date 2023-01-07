using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public enum ScrollDirection
{
    Vertical,
    Horizontal
}

public class ScrollBarResizer : MonoBehaviour, IDragHandler
{
    public ScrollDirection scrollDirection = ScrollDirection.Vertical;

    [SerializeField]
    Scrollbar _verticalScrollBar, _horizontalScrollBar;

    [SerializeField]
    CaretTracker _tracker;

    [SerializeField]
    FocusView _focusView;

    //InputField init size
    public float fieldInitSizeX = 0;
    public float fieldInitSizeY = 0;

    RectTransform inputFieldRect;

    private void Start()
    {
        inputFieldRect = _tracker.GetInputField().GetComponent<RectTransform>();
    }

    public void OffsetViewVertical()
    {
        _focusView.InterpolateVertically(_verticalScrollBar.value);
    }

    public void OffsetViewHorizontal()
    {
        _focusView.InterpolateHorizontally(_horizontalScrollBar.value);
    }

    public void ResizeY()
    {
        if (scrollDirection == ScrollDirection.Horizontal) return;

        if (fieldInitSizeY == 0)
            fieldInitSizeY = inputFieldRect.rect.size.y;

        //Todo: Resize based on inputField size
        _verticalScrollBar.size = fieldInitSizeY / inputFieldRect.rect.size.y;
    }

    public void ResizeX()
    {
        if (scrollDirection == ScrollDirection.Vertical) return;

        if (fieldInitSizeX == 0)
            fieldInitSizeX = _tracker.GetFirstCharacterPosition().x;

        _horizontalScrollBar.size = Mathf.Abs(fieldInitSizeX / (fieldInitSizeX * 2f));
    }

    public void OnDrag(PointerEventData eventData)
    {
        switch (scrollDirection)
        {
            case ScrollDirection.Vertical:
                OffsetViewVertical();
                break;

            case ScrollDirection.Horizontal:
                OffsetViewHorizontal();
                break;
        }
    }
}
