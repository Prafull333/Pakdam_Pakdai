using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        left_Hand = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftHand).gameObject;
        right_Hand = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand).gameObject;
        GameManager.Instance.Players.Add(this.gameObject);

        createRaiderEvent.AddListener(() => createRaider());
        createRaiderEvent.AddListener(() => GameManager.Instance.clapCrowd());
        looseRaiderEvent.AddListener(() => looseRaider());
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
        if(raiderSignal != null)
        {
            raiderSignal.SetActive(true);
        }
    }

    public void looseRaider()
    {
        isRaider = false;
        if (raiderSignal != null)
        {
            raiderSignal.SetActive(false);
        }
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
