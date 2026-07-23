using UnityEngine;
using UnityEngine.EventSystems;

public class UIChangeCursorOnOver : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CustomCursorBehavior.Instance.SetCursor(CursorType.Hover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CustomCursorBehavior.Instance.SetCursor(CursorType.Normal);
    }

}
