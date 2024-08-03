using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject mainPlayer;

    [Space(10)]
    [Header("Camera")]
    public CinemachineFreeLook playerFollowCamera;


    public string firstPlace_PlayerName;
    public string secondPlace_PlayerName;
    public string thirdPlace_PlayerName;

    [Space(10)]
    [Header("UI")]
    public TextMeshProUGUI Ist_Winner;
    public TextMeshProUGUI IInd_Winner;
    public TextMeshProUGUI IIIrd_Winner;
    public GameObject winScreen;
    public GameObject gameplayScreen;
    public GameObject eliminatedScreen;
    public GameObject mobileScreen;
    public TextMeshProUGUI rewardText;

    private void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        GeneratePlayersForAIMode();
        createRandamRaider();

       // SetCrowd();

        eliminatedScreen.SetActive(false);
        winScreen.SetActive(false);
        gameplayScreen.SetActive(true);
        rewardText.gameObject.SetActive(false);

        if(SystemInfo.deviceType != DeviceType.Desktop)
        {
            mobileScreen.SetActive(true);
        }
        else
        {
            mobileScreen.SetActive(false);
        }
    }
     

    void createRandamRaider()
    {
        Players[Random.Range(0, Players.Count)].GetComponent<Player>().createRaiderEvent.Invoke();

        Debug.Log(Players.Count);
    }

    public void setCameraforRandamPlayer()
    {
        GameObject p = Players[Random.Range(0, Players.Count)];
        playerFollowCamera.Follow = p.GetComponent<ThirdPersonController>().CinemachineCameraTarget.transform;
        playerFollowCamera.LookAt = p.GetComponent<ThirdPersonController>().CinemachineCameraLookAt.transform;
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

        mainPlayer = p;
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

        if (playerFollowCamera.Follow ==
          raiderPlayer.GetComponent<ThirdPersonController>().CinemachineCameraTarget.transform)
        {
           setCameraforRandamPlayer();
        }

        raiderPlayer.SetActive(false);


        if(raiderPlayer == mainPlayer)
        {
            rewardText.gameObject.SetActive(true);

            if (Players.Count != 1)
            {
                GameManager.Instance.eliminatedScreen.gameObject.SetActive(true);
            }

            rewardText.color = Color.green;

            switch(Players.Count)
            {
                case 1: 
                    {
                        int winAount = data.bet * 3;
                        rewardText.text = $"You Win : {(winAount)}";
                        data.avalableGaneshCoin += winAount;
                    }  break;

                case 2:
                    {
                        int winAount = data.bet * 2;
                        rewardText.text = $"You Win : {winAount}";
                        data.avalableGaneshCoin += winAount;
                    } break;

                default:
                    {
                        rewardText.text = $"You Loss";
                        rewardText.color = Color.red;
                    }
                    break;
            }


        }

        Destroy(raiderPlayer,1f);
        createRandamRaider();
    }

    public void setWinners(string name, int pos)
    {
        switch(pos)
        {
            case 1: firstPlace_PlayerName = name; break;
            case 2: secondPlace_PlayerName = name; break;
            case 3: thirdPlace_PlayerName = name; break;
        }

        if (pos == 1)
        {
            GetComponent<Timer>().StopTimer();
            Players[0].GetComponent<ThirdPersonController>().enabled = false;
            Players[0].GetComponent<Player>().enabled = false;

            Ist_Winner.text = firstPlace_PlayerName;
            IInd_Winner.text = secondPlace_PlayerName;
            IIIrd_Winner.text = thirdPlace_PlayerName;
            gameplayScreen.SetActive(false);
            winScreen.SetActive(true);
            eliminatedScreen.SetActive(false);
        }

        if(mainPlayer != null && Players.Count == 1 && Players[0] == mainPlayer)
        {
            rewardText.color = Color.green;
            rewardText.gameObject.SetActive(true);
            int winAount = data.bet * 4;
            rewardText.text = $"You Win : {winAount}";
            data.avalableGaneshCoin += winAount;
        }

    }




    public void HomeButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    public void ReplaySceneButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
