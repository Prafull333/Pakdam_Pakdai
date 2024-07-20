using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data manager", menuName = "ScriptableObjects/Data Manager")]
public class DefaultData : ScriptableObject
{
    public string playerName;
    public string[] defaultName = new string[] { "Aanya", "Aaradhya", "Aarohi", "Aakil", "Aaryan", "Arjun",
    "Bhavana", "Charvi", "Dia", "Dhruv","Ishaan","Kabir","Ela", "Ganga","Gauri", "Neel", "Shlok","Tanay", 
        "Aakshya", "Aaloka", "Aaria", "Ahi", "Bhaanu", "Bhavya", "Chaaru", "Chanchal", "Chitra","Deepu",
    "Devasri", "Dhanshree", "Gulab"};

    public GameObject[] aiPlayer;
    public GameObject[] crowdPeople;

    public GameObject playerChar;
    public int avalableGaneshCoin = 0;

    public int selectedSceneIndex = 1;
}
