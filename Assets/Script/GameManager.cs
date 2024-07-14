using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Space(10)]
    [Header("Instance")]
    public static GameManager Instance;

    public TextMeshProUGUI raiderText;

    [Space(10)]
    [Header("Scriptable Object")]
    public DefaultData data;

    [Space(10)]
    [Header("Crowd")]
    public Transform[] crowd_Location;
    public List<GameObject> crowd_personsInStadium;

    [Space(10)]
    [Header("Player List")]
    public List<GameObject> Players;
    public GameObject raiderPlayer;

    [Space(10)]
    [Header("Camera")]
    public CinemachineVirtualCamera playerFollowCamera;

    private void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        GeneratePlayersForAIMode();
        createRandamRaider();

        SetCrowd();
    }
     

    void createRandamRaider()
    {
        Players[Random.Range(0, Players.Count)].GetComponent<Player>().createRaiderEvent.Invoke();

        Debug.Log(Players.Count);
    }

    void GeneratePlayersForAIMode()
    {
        GenerateMainPlayer();
        for (int i = 0; i < 9; i++)
        {
            Vector3 loc = new Vector3(UnityEngine.Random.Range(-30, 30), 0, UnityEngine.Random.Range(-30, 30));
            GameObject p = Instantiate(data.aiPlayer[UnityEngine.Random.Range(0,data.aiPlayer.Length)], loc, Quaternion.identity);

            p.GetComponent<Player>().nameString = data.defaultName[Random.Range(0,data.defaultName.Length)].ToString();
            Players.Add(p);
        }
    }

    void GenerateMainPlayer()
    {
        Vector3 loc = new Vector3(UnityEngine.Random.Range(-30, 30), 0, UnityEngine.Random.Range(-30, 30));
        GameObject p = Instantiate(data.playerChar, loc, Quaternion.identity);
        p.GetComponent<Player>().nameString = data.playerName;
        Debug.Log($"Is game manager : {p.GetComponent<Player>().nameString}");

        playerFollowCamera.Follow = p.GetComponent<ThirdPersonController>().CinemachineCameraTarget.transform;
        playerFollowCamera.LookAt = p.GetComponent<ThirdPersonController>().CinemachineCameraLookAt.transform;
        Players.Add(p);
    }


    void SetCrowd()
    {
        foreach (Transform t in crowd_Location)
        {
            GameObject g = Instantiate(data.crowdPeople[UnityEngine.Random.Range(0, 
                data.crowdPeople.Length)],t.position, t.rotation);

            crowd_personsInStadium.Add(g);
        }
    }

    public void clapCrowd()
    {
        foreach(GameObject g in crowd_personsInStadium)
        {
            g.GetComponent<Animator>().SetTrigger("Clap");
        }
    }

    public void DeletePlayer()
    {
        raiderPlayer.GetComponent<Player>().looseRaiderEvent.Invoke();
        Players.Remove(raiderPlayer);
        foreach(GameObject p in Players)
        {
            if(p.GetComponent<AiPlayer>())
            {
                if(p.GetComponent<AiPlayer>().nearestPlayer == raiderPlayer)
                {
                    p.GetComponent<AiPlayer>().nearestPlayer = null;
                    p.GetComponent<AiPlayer>().nearestPlayerDistance = Mathf.Infinity;
                }
            }
        }

        raiderPlayer.SetActive(false);

        Destroy(raiderPlayer,1f);
        createRandamRaider();
    }
}
