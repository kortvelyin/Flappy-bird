using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;

    [Header("Variables")]
    public float jumpforce;

    [Header("Audio")]
    [SerializeField] private AudioSource aS;
    [SerializeField] private AudioClip flapS;
    [SerializeField] private AudioClip hitS;
   



    private void Awake()
    {
        rb=GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
 

    public void Jumping()
    {

        aS.PlayOneShot(flapS);
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpforce);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        aS.PlayOneShot(hitS);
       
            SceneManager.LoadScene("Game");
        
    }
}
