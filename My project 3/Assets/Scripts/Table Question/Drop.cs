
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Drop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler 
{
    
    private Image _image;
    private Color _highlightColor;
    private TableQuestion _tableQuestion;
    private void Awake()
    {
        _tableQuestion = transform.parent.parent.parent.parent.GetComponent<TableQuestion>();
        _highlightColor = _tableQuestion.GetHighlightColor();
        _image = GetComponentInChildren<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        // eventData.pointerDrag.GetComponent<RectTransform>().transform.position = GetComponent<RectTransform>().transform.position;
        eventData.pointerDrag.GetComponent<DragLabel>().SetDropped(true);
        var eventSiblingIndex = eventData.pointerDrag.transform.GetSiblingIndex();
        var eventParentTransform = eventData.pointerDrag.transform.parent.transform;
        
        
        eventData.pointerDrag.transform.SetParent(transform.parent.transform);
        eventData.pointerDrag.transform.SetSiblingIndex(transform.GetSiblingIndex());
        
        transform.SetParent(eventParentTransform);
        transform.SetSiblingIndex(eventSiblingIndex);
        _image.color = Color.white;
        transform.parent.parent.parent.parent.GetComponent<TableQuestion>().CheckAllAnswers();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null)
            return;
        _image.color = _highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null)
            return;
        _image.color = Color.white;
    }
}
