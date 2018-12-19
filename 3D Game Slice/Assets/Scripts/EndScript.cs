using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public GameObject GameOverText;
    public bool endReach = false;
    public GameObject Spawner;

    private void Start()
    {
        Spawner.SetActive(false);
    GameOverText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            endReach = true;
        }
    }

    private void Update()
    {
        if (endReach == true)
        {
            Spawner.SetActive(true);
            GameOverText.SetActive(true);
        }
    }
}
