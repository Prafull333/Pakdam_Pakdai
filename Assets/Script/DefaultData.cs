using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data manager", menuName = "ScriptableObjects/Data Manager")]
public class DefaultData : ScriptableObject
{
    public string playerName;
    public string[] defaultName = new string[] { "Aanya", "Aaradhya", "Aarohi", "Aakil", "Aaryan", "Arjun",
    "Bhavana", "Charvi", "Dia", "Dhruv","Ishaan","Kabir","Ela", "Ganga","Gauri", "Neel", "Shlok","Tanay"};

    public GameObject[] aiPlayer;
    public GameObject[] crowdPeople;

    public GameObject playerChar;

    public int selectedSceneIndex = 1;
}