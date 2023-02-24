using UnityEngine;
using UnityEngine.EventSystems;

public class MobileLeftMove : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private float _leftMove = 0f;

    public virtual void OnPointerDown(PointerEventData ped)
    {
        _leftMove = -1f;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        _leftMove = 0f;
    }

    public float GetLeftMove()
    {
        if (_leftMove == 0f)
        {
            return Input.GetAxis("Horizontal");
        }
        else 
        {
            return _leftMove;
        }
    }

}
