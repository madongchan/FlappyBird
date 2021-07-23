using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour
{
    public float jumpPower = 5f;
    public GameObject imageBird;
    public Vector3 lookDirection;
    public GameManager GM;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if(GM.end == false)
        {
            if (Input.GetMouseButtonDown(0) && transform.position.y < 4f)
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(0, jumpPower, 0, ForceMode.VelocityChange);
                GetComponent<AudioSource>().Play();
            }
            if(GM.ready == false)
            {
                lookDirection.z = rb.velocity.y * 10f + 20f;
            }
        }
        

        
        Quaternion R = Quaternion.Euler(lookDirection);
        imageBird.transform.rotation
           = Quaternion.RotateTowards(imageBird.transform.rotation, R, 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cactus"))
        {
            rb.velocity = new Vector3(0, -3, 0);
            lookDirection = new Vector3(0, 0, -90);
            GM.GameOver();
        }else if (other.CompareTag("goal") && GM.end != true)
        {
            GM.GetScore();
        }
    }
}
