using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerexplosion;
    public int scoreValue;

    private GameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }


    //Enemy Red Shield Blocks Red Shots
    //ENEMY SHIELDING IS WORKING. JUST NOT WHEN ATTATCHED TO THE ENEMY.
    //Blue shield was working for a while, suddenly both of them don't work. I forget what I changed.

    private void OnTriggerEnter(Collider other) { 

        //Things that objects with this script will pass through
        if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Light Shield" || other.tag == "Dark Shield" || other.tag == "Light Enemy Bolt" || other.tag == "Dark Enemy Bolt" )
        {
            return;
        }

        //a pass through specifically for lasers not colliding
        if (tag == "Light Enemy Bolt" || tag == "Dark Enemy Bolt")
        {
            if (other.tag == "Light Shot Player" || other.tag == "Dark Shot Player")
            {
                return;
            }
        }

        //anything else gets blown up when collision occurs
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.tag == "Player")
        {
            Instantiate(playerexplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
