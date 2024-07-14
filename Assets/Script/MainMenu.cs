using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public DefaultData data;
    public ShopData shop;

    [Header("Menu and CameraPos")]
    public GameObject mainmenuCameraPos;
    public GameObject shopmenuCameraPos;
    public GameObject avtarSelectCameraPos;
    public GameObject mainMenuPanal;
    public GameObject shopmenuPanal;
    public GameObject avtarSelectMenuPanal;

    [Header("Select Charcter")]
    public int selectAvtarIndex = 0;


    [Header("Shop")]
    public int shopItemIndex = 0;
    public Transform shopPlayerLocation;
    public Button previousShopButton;
    public Button nextShopButton;

    public void ClickPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ActiveShopPanal()
    {
        mainmenuCameraPos.gameObject.SetActive(false);
        shopmenuCameraPos.gameObject.SetActive(true);
        avtarSelectCameraPos.gameObject.SetActive(false);
        mainMenuPanal.gameObject.SetActive(false);
        shopmenuPanal.gameObject.SetActive(true);
        avtarSelectMenuPanal.gameObject.SetActive(false);
    }

    public void ActiveCharcterSelectPanal()
    {
        mainmenuCameraPos.gameObject.SetActive(false);
        shopmenuCameraPos.gameObject.SetActive(false);
        avtarSelectCameraPos.gameObject.SetActive(true);
        mainMenuPanal.gameObject.SetActive(false);
        shopmenuPanal.gameObject.SetActive(false);
        avtarSelectMenuPanal.gameObject.SetActive(true);
    }

    #region (Shop)

    public void ClickShopButton()
    {
        shopItemIndex = 0;
        showsShopItem();
    }

    public void ClickNextShopButton()
    {
        shopItemIndex++;
        showsShopItem();
    }

    public void ClickPreviousShopButton()
    {
        shopItemIndex--;
        showsShopItem();
    }

    void showsShopItem()
    {
        if(shopItemIndex == 0)
            previousShopButton.interactable = false;
        else if((shopItemIndex + 1) == shop.allPlayerinShop.Length)
            nextShopButton.interactable = false;
        else
        {
            previousShopButton.interactable = true;
            nextShopButton.interactable = true;
        }

        foreach(Transform t in shopPlayerLocation)
        {
            t.gameObject.SetActive(false);
        }

        shopPlayerLocation.GetChild(shopItemIndex).gameObject.SetActive(true);
    }
    #endregion

}
