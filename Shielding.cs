using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielding : MonoBehaviour
{
    public string shot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == shot)
        {
            Destroy(other.gameObject); 
        }
    }
}
