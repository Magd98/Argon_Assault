using UnityEngine;
using UnityEngine.EventSystems;

public class DragLabel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 _initialVectorDirection;
    private CanvasGroup _canvasGroup;
    private bool _dropped;
    private Vector3 _initialPosition;
    private void Awake()
    {
        _dropped = false;
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        _canvasGroup.alpha = .6f;
        _canvasGroup.blocksRaycasts = false;
        var position = transform.position;
        _initialVectorDirection = Vector2ToVector3(eventData.position) - position;
        _initialPosition = position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        transform.position = Vector2ToVector3(eventData.position) - _initialVectorDirection;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        Invoke(nameof(Reposition), 0.1f);
        

    }
    
    
    private void Reposition()
    {
        if (_dropped == false)
        {
            transform.position = _initialPosition;      
            transform.parent.parent.parent.parent.GetComponent<TableQuestion>().CheckAllAnswers();
        }
        else
        {
            _dropped = false;
        }

    }
    private Vector3 Vector2ToVector3(Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0);
    }
    public void SetDropped(bool dropped)
    {
        _dropped = dropped;
    }
    
}
