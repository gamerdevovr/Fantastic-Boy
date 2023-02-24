using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileRightMove : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private float _rightMove = 0f;

    public virtual void OnPointerDown(PointerEventData ped)
    {
        _rightMove = 1f;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        _rightMove = 0f;
    }

    public float GetLeftMove()
    {
        if (_rightMove == 0f)
        {
            return Input.GetAxis("Horizontal");
        }
        else
        {
            return _rightMove;
        }
    }

}
