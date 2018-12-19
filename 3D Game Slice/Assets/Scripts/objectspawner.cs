using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectspawner : MonoBehaviour {

    [SerializeField] string[] tag;

    objectpooler objectPooler;

    private void Start()
    {
        objectPooler = objectpooler.Instance;
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        GameObject go = objectPooler.SpawnFromPool(tag[Random.Range(0, tag.Length)], transform.position, transform.rotation);
    }
}
