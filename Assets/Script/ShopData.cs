using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data manager", menuName = "ScriptableObjects/Shop")]
public class ShopData : ScriptableObject
{
    [System.Serializable]
    public struct playerInShopData
    {
        public string name;
        public GameObject playablePlayer;
        public GameObject onlyShopPlayer;
        public int price;
    }

    public playerInShopData[] allPlayerinShop;
    public List<playerInShopData> purchasedPlayer;

    private void OnEnable()
    {
        if(purchasedPlayer.Count == 0 && allPlayerinShop.Length > 0)
        purchasedPlayer.Add(allPlayerinShop[0]);
    }
}
