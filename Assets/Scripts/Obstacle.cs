using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Variables")]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MoveObstacles", .00f, 0.01f* Time.deltaTime);
    }


    void MoveObstacles()
    {
            transform.position -= transform.right * Time.deltaTime * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
