using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileJump : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private bool _jumpState = false;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();

        #if UNITY_ANDROID
            image.enabled = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        #else
            _image.enabled = false;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        #endif
    }

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
