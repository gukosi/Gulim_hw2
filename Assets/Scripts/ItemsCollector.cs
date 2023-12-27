using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemsCollector : MonoBehaviour
{
    private int cherryCount = 0;

    [SerializeField] private TextMeshProUGUI cherriesCountText;

    [SerializeField] private AudioSource collectSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry")) //check for froggy collision with object tagged 'cherry'
        {
            collectSound.Play();
            Destroy(collision.gameObject); //destroy item
            cherryCount++;
            cherriesCountText.text = "Cherries: " + cherryCount;
        }
    }
}
