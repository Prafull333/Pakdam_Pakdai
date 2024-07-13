using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data manager", menuName = "ScriptableObjects/Shop")]
public class ShopData : ScriptableObject
{
    public GameObject[] playableObjects; 
}
