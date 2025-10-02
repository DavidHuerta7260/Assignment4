/*
 David Huerta
 Prototype 3
 Adds triggerzone that allows score to go up
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterAddScore : MonoBehaviour
{

    private UIManager uIManager;

    private bool triggered = false;

    void Start()
    {
        uIManager = GameObject.FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            uIManager.score++;
        }
    }
}
