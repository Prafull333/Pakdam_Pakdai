using StarterAssets;
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

    // Update is called once per frame
    void Update()
    {
        CreateandCheckCollision(left_Hand.transform);
        CreateandCheckCollision(right_Hand.transform);
    }

    public void createRaider()
    {
        if(GameManager.Instance.Players.Count == 1)
        {
            GetComponent<Animator>().SetBool("Win", true);
            return;
        }
        
        isRaider = true;
        raiderSignal.SetActive(true);

        GameManager.Instance.raiderText.text = $"Reider :- {nameString}";
        GameManager.Instance.raiderPlayer = this.gameObject;
    }

    public void looseRaider()
    {
        isRaider = false;
        raiderSignal.SetActive(false);

        if(GetComponent<AiPlayer>())
        {
            GetComponent<AiPlayer>().nearestPlayer = null;
            GetComponent<AiPlayer>().nearestPlayerDistance = Mathf.Infinity;
        }
    }
    


    void CreateandCheckCollision(Transform t)
    {
        Collider[] handCollider = Physics.OverlapSphere(t.position, 0.2f);

        foreach (Collider c in handCollider)
        {
            if(c.gameObject != this.gameObject && isRaider && c.GetComponent<Player>())
            {
                c.GetComponent<Player>().createRaiderEvent.Invoke();
                looseRaiderEvent.Invoke();
            }
        }

    }

    private void OnDestroy()
    {
        if(GameManager.Instance.Players.Count == 2)
        {
            GameManager.Instance.setWinners(nameString, 3);
            return;
        }

        if (GameManager.Instance.Players.Count == 1)
        {
            GameManager.Instance.setWinners(nameString, 2);
            GameManager.Instance.setWinners(GameManager.Instance.Players[0]
                .GetComponent<Player>().nameString, 1);
            return;
        }
        
        //if(GameManager.Instance.playerFollowCamera.Follow == 
        //    GetComponent<ThirdPersonController>().CinemachineCameraTarget.transform)
        //{
        //    GameManager.Instance.setCameraforRandamPlayer();
        //}


    }
}
