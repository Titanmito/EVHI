using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    public float speed;

    void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody.velocity = new Vector2(0, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
