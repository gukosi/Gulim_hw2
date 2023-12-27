using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform froggy;

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(froggy.position.x, froggy.position.y, transform.position.z); //using Vector3 cuz camera is further then the GameScene; x & y follow froggy
    }
}
