using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FroggyLife : MonoBehaviour
{
    private Rigidbody2D rigb;
    private Animator anim;

    [SerializeField] private AudioSource deathSound;

    private void Start()
    {
        rigb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision) //method to kill froggy when he collides with objects tagged 'Trap'
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die() //what happens when froggy dies
    {
        rigb.bodyType = RigidbodyType2D.Static; //prevent further movement
        deathSound.Play();
        anim.SetTrigger("death"); //show death animation
    }

    private void RestartLvl() //method to restart level, is called by 'Froggy_Death' animation
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
