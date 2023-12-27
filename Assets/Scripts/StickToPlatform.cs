using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Froggy") //if Froogy is on platform
        {
            collision.gameObject.transform.SetParent(transform); //platform is set to be parent of Froggy; their positions change together
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Froggy") 
        {
            collision.gameObject.transform.SetParent(null); //platform is no longer parent of Froggy; unbounding
        }
    }
}
