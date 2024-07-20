using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public DefaultData data;
    public ShopData shop;

    public Button coinButton;

    [Header("Menu and CameraPos")]
    public GameObject mainmenuCameraPos;
    public GameObject shopmenuCameraPos;
    public GameObject avtarSelectCameraPos;
    public GameObject mainMenuPanal;
    public GameObject shopmenuPanal;
    public GameObject avtarSelectMenuPanal;

    public GameObject charcterBio;

    [Header("Select Charcter")]
    public int selectAvtarIndex = 0;
    public Transform selectCharcterPlayerLocation;
    public Button previousSC_Button;
    public Button nextShopSC_Button;
    public Button selectSC_Button;

    [Header("Shop")]
    public int shopItemIndex = 0;
    public Transform shopPlayerLocation;
    public Button previousShopButton;
    public Button nextShopButton;
    public Button buyButtonInShop;

    private void Start()
    {
        SetPurchesedCharcter();
        ActiveMainPanal();
        updateCoin();
    }

    public void updateCoin()
    {
        coinButton.GetComponentInChildren<TextMeshProUGUI>().text = data.avalableGaneshCoin.ToString();
    }

    public void ClickPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ActiveMainPanal()
    {
        charcterBio.SetActive(false);
        mainmenuCameraPos.gameObject.SetActive(true);
        shopmenuCameraPos.gameObject.SetActive(false);
        avtarSelectCameraPos.gameObject.SetActive(false);
        mainMenuPanal.gameObject.SetActive(true);
        shopmenuPanal.gameObject.SetActive(false);
        avtarSelectMenuPanal.gameObject.SetActive(false);
    }

    public void ActiveShopPanal()
    {
        charcterBio.SetActive(true);
        mainmenuCameraPos.gameObject.SetActive(false);
        shopmenuCameraPos.gameObject.SetActive(true);
        avtarSelectCameraPos.gameObject.SetActive(false);
        mainMenuPanal.gameObject.SetActive(false);
        shopmenuPanal.gameObject.SetActive(true);
        avtarSelectMenuPanal.gameObject.SetActive(false);
    }

    public void ActiveCharcterSelectPanal()
    {
        charcterBio.SetActive(true);
        mainmenuCameraPos.gameObject.SetActive(false);
        shopmenuCameraPos.gameObject.SetActive(false);
        avtarSelectCameraPos.gameObject.SetActive(true);
        mainMenuPanal.gameObject.SetActive(false);
        shopmenuPanal.gameObject.SetActive(false);
        avtarSelectMenuPanal.gameObject.SetActive(true);

        showsSC_Item();
    }

    #region Shop

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

    public void ClickBuyShopButton()
    {
        if(data.avalableGaneshCoin >= shop.allPlayerinShop[shopItemIndex].price)
        {
           data.avalableGaneshCoin -= shop.allPlayerinShop[shopItemIndex].price;
           shop.purchasedPlayer.Add(shop.allPlayerinShop[shopItemIndex]);
        }
        else
        {

        }

        SetPurchesedCharcter();
        updateCoin();
        showsShopItem();
    }

    void showsShopItem()
    {
        if (shopItemIndex == 0)
        {
            previousShopButton.interactable = false;
            nextShopButton.interactable = true;
        }
        else if ((shopItemIndex + 1) == shop.allPlayerinShop.Length)
        {
            nextShopButton.interactable = false;
            previousShopButton.interactable = true;
        }
        else
        {
            previousShopButton.interactable = true;
            nextShopButton.interactable = true;
        }

        foreach(Transform t in shopPlayerLocation)
        {
            t.gameObject.SetActive(false);
        }

        bool result = shop.isPurchesedPlayer(shop.allPlayerinShop[shopItemIndex]);

        buyButtonInShop.GetComponentInChildren<TextMeshProUGUI>().text = 
            shop.allPlayerinShop[shopItemIndex].price.ToString();

        buyButtonInShop.gameObject.SetActive(result == true ? false : true);

        shopPlayerLocation.GetChild(shopItemIndex).gameObject.SetActive(true);

        charcterBio.GetComponentInChildren<TextMeshProUGUI>().text =
            $"<B> Name :- </B>{shop.allPlayerinShop[shopItemIndex].name} \n" +
            $"<B> Bio :- </B> {shop.allPlayerinShop[shopItemIndex].description}";
    }
    #endregion


    #region Select Charcter

    public void ClickSCButton()
    {
        selectAvtarIndex = 0;
        showsSC_Item();
    }

    public void ClickNextSC_Button()
    {
        selectAvtarIndex++;
        showsSC_Item();
    }

    public void ClickPreviousSC_Button()
    {
        selectAvtarIndex--;
        showsSC_Item();
    }

    public void ClickSelectSC_Button()
    {
        data.playerChar = shop.purchasedPlayer[selectAvtarIndex].playablePlayer;
    }

    void SetPurchesedCharcter()
    {
        foreach(Transform t in selectCharcterPlayerLocation)
        { Destroy(t.gameObject); }

        for(int i = 0; i < shop.purchasedPlayer.Count;i++)
        {
             Instantiate(shop.purchasedPlayer[i].onlyShopPlayer, selectCharcterPlayerLocation);
        }
    }

    void showsSC_Item()
    {
        if (selectAvtarIndex == 0)
        {
            previousSC_Button.interactable = false;
            nextShopSC_Button.interactable = true;
        }
        else if ((selectAvtarIndex + 1) == shop.purchasedPlayer.Count)
        {
            previousSC_Button.interactable = true;
            nextShopSC_Button.interactable = false; 
        }

        else
        {
            previousSC_Button.interactable = true;
            nextShopSC_Button.interactable = true;
        }
      
        if(shop.purchasedPlayer.Count == 1)
        {
            previousSC_Button.interactable = false;
            nextShopSC_Button.interactable = false;
        }

        foreach (Transform t in selectCharcterPlayerLocation)
        {
            t.gameObject.SetActive(false);
        }

        selectCharcterPlayerLocation.GetChild(selectAvtarIndex).gameObject.SetActive(true);

        selectSC_Button.interactable = (shop.purchasedPlayer[selectAvtarIndex].playablePlayer
            != data.playerChar) ? true : false;

        charcterBio.GetComponentInChildren<TextMeshProUGUI>().text =
        $"<B> Name :- </B>{shop.allPlayerinShop[selectAvtarIndex].name} \n" +
        $"<B> Bio :- </B> {shop.allPlayerinShop[selectAvtarIndex].description}";

    }

    #endregion
}
