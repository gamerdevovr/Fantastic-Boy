using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJump : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private bool _jumpState = false;

    public virtual void OnPointerDown(PointerEventData ped)
    {
        _jumpState = true;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        _jumpState = false;
    }

    public bool Jump()
    {
        return Input.GetButtonDown("Jump") || _jumpState;
    }

}
