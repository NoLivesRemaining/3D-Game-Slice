using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public GameObject GameOverText;
    public bool endReach = false;

    private void Start()
    {
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
        
            GameOverText.SetActive(true);
        }
    }
}
