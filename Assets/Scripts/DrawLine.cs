using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawLine : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
{
    [SerializeField] private LineRenderer _line;
    private bool _isCanDraw;
    private List<Vector3> _linePositions = new List<Vector3>();
    
    public static event Action OnMouseButtonUped;
    public static event Action<Vector3> OnMouseButtonEntered;

    private void Start()
    {
        _isCanDraw = false;
    }

    private void Update()
    {
        for (int i = 0; i < _line.positionCount; i++)
        {
            _line.SetPosition(i, new Vector3(_line.GetPosition(i).x, 
                _line.GetPosition(i).y, 
                Camera.main.transform.position.z + 10));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isCanDraw = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (_isCanDraw && Input.GetMouseButton(0))
        {
            Vector3 position = GetWorldPosition(Input.mousePosition);
            _line.positionCount++;
            _line.SetPosition(_line.positionCount - 1, position);
            OnMouseButtonEntered?.Invoke(position);
            _linePositions.Add(position);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnMouseButtonUped?.Invoke();
        _isCanDraw = false;
        _line.positionCount = 0;
        _linePositions.Clear();
    }
    
    private Vector3 GetWorldPosition(Vector3 mousePosition)
    {
        Vector3 currentPosition = new Vector3(mousePosition.x, mousePosition.y, 10);
        return Camera.main.ScreenToWorldPoint(currentPosition);
    }
}
