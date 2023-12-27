using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBetweenPoints : MonoBehaviour
{
    [SerializeField] private GameObject[] stoppoints; //make it array so that the num of points is not fixed; useful for reusing the code
    private int currentStoppointIndex = 0; //indexes start from 0

    [SerializeField] private float speed = 3.5f; //speed of platform

    private void Update()
    {
        if (Vector2.Distance(stoppoints[currentStoppointIndex].transform.position, transform.position) < .1f)
        {
            currentStoppointIndex++;
            if (currentStoppointIndex >= stoppoints.Length)
            {
                currentStoppointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, stoppoints[currentStoppointIndex].transform.position, Time.deltaTime * speed);
    }
}
