using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileRightMove : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private float _rightMove = 0f;
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
        _rightMove = 1f;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        _rightMove = 0f;
    }

    public float GetRightMove()
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
