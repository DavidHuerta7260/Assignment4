/*
David Huerta
Challenge 3
Loops the scrolling background.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackgroundX : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    void Start()
    {
        startPos = transform.position;

        // Use the Renderer width; loop when we've moved past half the width
        Renderer r = GetComponent<Renderer>();
        if (r != null)
        {
            repeatWidth = r.bounds.size.x * 0.5f; // HALF of the background's width (important!)
        }
        else
        {
            // Fallback if no Renderer is present (you can tune this value)
            repeatWidth = 20f;
            Debug.LogWarning("RepeatBackgroundX: No Renderer found. Using fallback repeatWidth.");
        }
    }

    void Update()
    {
        // If background moves left by its repeat width, move it back to start position
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
