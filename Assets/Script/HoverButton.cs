using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool isHovering = false;
    public Color hoverColor;
    public UnityEvent onClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        GetComponent<Image>().color = hoverColor;
        transform.GetChild(0).GetComponent<Image>().color = hoverColor;
        Debug.Log("Button is being hovered over.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        GetComponent<Image>().color = Color.white;
        transform.GetChild(0).GetComponent<Image>().color = Color.white;
        Debug.Log("Button is no longer being hovered over.");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.white;
        transform.GetChild(0).GetComponent<Image>().color = Color.white;
        onClick.Invoke();
    }
}
