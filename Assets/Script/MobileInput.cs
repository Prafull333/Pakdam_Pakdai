using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInput : MonoBehaviour
{
    public HoverButton jumpButton;
    public HoverButton runButton;
    public Joystick js;


    public void jumpButtonClick()
    {
        if(GameManager.Instance.mainPlayer != null)
        GameManager.Instance.mainPlayer.GetComponent<ThirdPersonController>().mobileJumpAndGravity();
    }
}
