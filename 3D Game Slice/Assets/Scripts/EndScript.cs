using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public GameObject GameOverText;
    public bool endReach = false; //Grabbing objects
    public GameObject Spawner;

    private void Start()
    {
        Spawner.SetActive(false); //Setting both objects to be deactivated on play
    GameOverText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            endReach = true;  //Letting the player trigger the end
        }
    }

    private void Update()
    {
        if (endReach == true)
        {
            Spawner.SetActive(true); //Activating both objects as the player reaches the objective
            GameOverText.SetActive(true);
        }
    }
}
