/*
David Huerta
Challenge 3
Spawns bombs and money randomly.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    private float spawnDelay = 2f;
    private float spawnInterval = 1.5f;

    private PlayerControllerX playerControllerScript;

    void Start()
    {
        // SPELLING MATTERS: must match method name below
        InvokeRepeating(nameof(SpawnObjects), spawnDelay, spawnInterval);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Spawn obstacles
    void SpawnObjects()
    {
        if (playerControllerScript != null && !playerControllerScript.gameOver)
        {
            Vector3 spawnLocation = new Vector3(30f, Random.Range(5f, 15f), 0f);
            int index = Random.Range(0, objectPrefabs.Length);
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }
}
