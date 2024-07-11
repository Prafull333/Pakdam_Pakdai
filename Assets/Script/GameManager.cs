using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform[] crowd_Location;
    public GameObject[] crowd_persons;
    public List<GameObject> crowd_personsInStadium;

    public List<GameObject> Players;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
       // SetCrowd();
    }



    void SetCrowd()
    {
        foreach (Transform t in crowd_Location)
        {
            GameObject g = Instantiate(crowd_persons[UnityEngine.Random.Range(0, crowd_persons.Length)],
                t.position, t.rotation);

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
}
