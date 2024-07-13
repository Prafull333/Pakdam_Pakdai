using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public DefaultData data;
    public ShopData shop;

    public void ClickPlayButton()
    {
        SceneManager.LoadScene(1);
    }
}
