using UnityEngine;
using System.Collections;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shotLight;
    public GameObject shotDark;
    public Transform shotSpawnLight;
    public Transform shotSpawnDark;

    public float fireRate;
    private float nextFire;
    private AudioSource audioSource;
    private bool shield;

    private Rigidbody rb;
    private Transform shotSpawn;
    private GameObject shot;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        shield = true;
        shotSpawn = shotSpawnLight;
        shot = shotLight;
    }

    private void Update()
    {
        //creates shots
        if ((Input.GetButton("Fire1") || Input.GetButton("Jump")) && Time.time > nextFire) 
        {
            nextFire = Time.time + fireRate;
            GameObject clone =
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }

       //swithes the barrier
        if ((Input.GetKeyDown("left shift")))
        {
            Barrier();
        }
    }

    void FixedUpdate()
    {

        //moves the player and sets the boundaries
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    //switches the barriers
    void Barrier ()
    {
        if (shield == true)
        {
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(true);
            shotSpawn = shotSpawnDark;
            shot = shotDark;
            shield = false;
            return;
        }

        if (shield == false)
        {
            transform.GetChild(4).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
            shotSpawn = shotSpawnLight;
            shot = shotLight;
            shield = true;
            return;
        }
    }
}