using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public GameObject left_Hand;
    public GameObject right_Hand;

    public UnityEvent createRaiderEvent;
    public UnityEvent looseRaiderEvent;
    public GameObject raiderSignal;
    public bool isRaider;

    public string nameString;


    private void OnEnable()
    {
        left_Hand = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftHand).gameObject;
        right_Hand = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand).gameObject;

        createRaiderEvent.AddListener(() => createRaider());
        createRaiderEvent.AddListener(() => GameManager.Instance.clapCrowd());
        looseRaiderEvent.AddListener(() => looseRaider());
    }

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        CreateandCheckCollision(left_Hand.transform);
        CreateandCheckCollision(right_Hand.transform);
    }

    public void createRaider()
    {
        
        isRaider = true;
        raiderSignal.SetActive(true);
        GameManager.Instance.raiderText.text = $"Raider :- {nameString}";
        GameManager.Instance.raiderPlayer = this.gameObject;
    }

    public void looseRaider()
    {
        isRaider = false;
        raiderSignal.SetActive(false);
    }
    


    void CreateandCheckCollision(Transform t)
    {
        Collider[] handCollider = Physics.OverlapSphere(t.position, 0.2f);

        foreach (Collider c in handCollider)
        {
            if(c.gameObject != this.gameObject && isRaider && c.GetComponent<Player>())
            {
                Debug.Log("Point");
                c.GetComponent<Player>().createRaiderEvent.Invoke();
                looseRaiderEvent.Invoke();
            }
        }

    }
}
