using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer sr;
    private CharacterController cc;

    public float walkSpeed, runSpeed, unladenSpeed, jump, grav = -9.81f, yVar, interval, railSpeed, SOUPERJUMP, xVar;
    private float speed, timer, lastDir;
    public bool canDash, canMove;
    public IntDaa lives;
    public Vector3Data respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        sr = GetComponent<SpriteRenderer>();
        speed = walkSpeed;
        timer = interval;
    }

    // Update is called once per frame
    void Update()
    {
        yVar += grav * Time.deltaTime;
        
        xVar += -xVar * Time.deltaTime;
        Vector2 move = new Vector2(xVar, yVar);
        if(move.x > 0)
        {
            lastDir = 1;
        }
        else
        {
            if(move.x < 0)
            {
              lastDir = -1;
            }
            
        }
        if (cc.isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            yVar = 0f;
            yVar += jump;
        }
        else
        {
            if (cc.isGrounded && yVar < -1f)
            {
                yVar = -0.5f;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && canDash == true)
        {
            speed += runSpeed;
            timer = interval;
            canDash = false;
        }
        else
        {
            if (cc.isGrounded && canDash == true)
            {
                speed = walkSpeed;
            }
            else
            {
                if (canDash == true)
                {
                    speed = unladenSpeed;
                }

            }
        }
        if (timer <= 0)
        {
            canDash = true;
            timer = interval;
        }
        timer -= Time.deltaTime;
        if (canMove == true)
        {
            xVar += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            cc.Move(move * Time.deltaTime);
        }

    }

    public IEnumerator DEATH(float wait)
    {
        timer = wait;
        if(lives.daa >= 1)
        {
            sr.enabled = false;
            lives.daa--;
            cc.enabled = false;
            transform.position = respawnPoint.location;
            canMove = false;
            cc.enabled = true;
            yield return new WaitForSeconds(wait);
            sr.enabled = true;
            canMove = true;
        }
        yield return null;
    }
    public void StartDeath(float wait)
    {
        StartCoroutine(DEATH(wait));
    }

    public void ChangeSpawnX(float x) 
    {
        respawnPoint.location.x = x;
    }
    public void ChangeSpawnY(float y)
    {
        respawnPoint.location.y = y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("JumpPad"))
        {
            yVar += SOUPERJUMP;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Rail"))
        {
            canMove = false;
            speed = railSpeed;
            xVar = lastDir * speed;
            Vector2 rail = new Vector2(xVar, yVar);
            cc.Move(rail * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Rail"))
        {
            canMove = true;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.normal.x < -0.8 || hit.normal.x > 0.8)
        {
            xVar = 0;
        }

        if (hit.normal.y == -1)
        {
            yVar = -0.5f;
        }
    }
}
