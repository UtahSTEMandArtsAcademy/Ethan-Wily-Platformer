using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AItype
{
    snek
}

public enum States
{
    idle,
    attacking
}

public class enemyAI : MonoBehaviour
{
    public States state;
    public AItype type;
    private Rigidbody rb;
    public float speed = 1;
    public int health;
    public bool AIStart;
    public GameObject player;
    public PlayerMovement playerScript;
    // DA BASICS (for now)
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {

        switch (type)
        {
            case AItype.snek:
                SnekAI(state, speed);
                break;
        }

    }

    void SnekAI(States state, float multiplier)
    {
        switch (state)
        {
            case States.attacking:
                rb.AddForce((player.transform.position - transform.position).normalized * multiplier * Time.deltaTime);
                
                break;

            case States.idle:
                Vector3 vel = rb.velocity;
                vel.x /= 1.05f;
                rb.velocity = vel;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerScript.StartDeath(1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = States.attacking;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = States.idle;
        }
    }

}
