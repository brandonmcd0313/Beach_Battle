using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessEnemy : MonoBehaviour
{
    //this script is on crab
    int location; public static float won = 0;
    public static float speed = 1;
    Vector3 target; bool moving = false;
    Animator anim; public GameObject maincamera; //use main cam for relative movement
    bool DANCIN = false;                                  // Use this for initialization
    void Start()
    {   
        
        EndlessManager.crabCount++; //this crab has been spawned
        maincamera = GameObject.Find("Main Camera");
        anim = GetComponent<Animator>();
        //set location based on current y cord
        if (transform.position.y == 3)
        {
            location = 1;
        }
        else if (transform.position.y == 0)
        {
            location = 0;
        }
        else if (transform.position.y == -3)
        {
            location = -1;
        }
        //start forward anim
        anim.Play("forward");

        //set a target to go towards to start movement
        target = new Vector3(maincamera.transform.position.x + 7.5f, transform.position.y);
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if have won
        if (EndlessEnemy.won == 1)
        {

            //goto win position
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, 
                new Vector3(-20f, -3.15f, 0f), Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, new Vector3(-20f, -3.15f, 0f)) < 0.001f)
            {
                maincamera.GetComponent<CameraController>().unParent();
                Destroy(this.gameObject);
            }
        }
       else  if (moving)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                anim.Play("forward");
                moving = false;
                move();
            }
        }


    }

    void move()
    {
        //if on last row
        if (transform.position.x < (maincamera.transform.position.x - 2.5f)
            || (speed == 1 && transform.position.x < (maincamera.transform.position.x - 1f)))
        {
            //if left
            if (location == -1)
            {
                //go right to center

                //go right towards center
                anim.Play("right");
                target = new Vector3(transform.position.x, 0);
                moving = true;
                location = 0;

            }


            //if right
            else if (location == 1)
            {
                //go left to center
                //go left towards center
                anim.Play("left");
                target = new Vector3(transform.position.x, 0);
                moving = true;
                location = 0;
            }

            //if center
            else
            {
                //go forward to attack!=lse
                anim.Play("forward");
                target = new Vector3(transform.position.x - 2.75f, transform.position.y);
                moving = true;
                location = 0;
            }
        }
        //if left
        else if (location == -1)
        {
            //go forward or right
            //70 - 30 chance
            int rand = Random.Range(0, 101);
            if (rand < 70)
            {
                //go right towards center
                anim.Play("right");
                target = new Vector3(transform.position.x, 0);
                moving = true;
                location = 0;
            }
            else
            {
                //forward
                anim.Play("forward");
                target = new Vector3(transform.position.x - 2.75f, transform.position.y);
                moving = true;
                location = -1;
            }
        }


        //if right
        else if (location == 1)
        {
            //go forward or left
            //70 - 30 chance
            int rand = Random.Range(0, 101);
            if (rand < 70)
            {
                //go left towards center
                anim.Play("left");
                target = new Vector3(transform.position.x, 0);
                moving = true;
                location = 0;
            }
            else
            {
                //forward
                anim.Play("forward");
                target = new Vector3(transform.position.x - 2.75f, transform.position.y);
                moving = true;
                location = 1;
            }
        }

        //if center
        else if (location == 0)
        {
            //left, right, or forward
            //40,40,20
            int rand = Random.Range(0, 101);
            if (rand < 40)
            {
                //go left
                anim.Play("left");
                target = new Vector3(transform.position.x, -3);
                moving = true;
                location = -1;

            }
            else if (rand < 80)
            {
                //go right
                anim.Play("right");
                target = new Vector3(transform.position.x, 3);
                moving = true;
                location = 1;
            }
            else
            {
                //go forward
                anim.Play("forward");
                target = new Vector3(transform.position.x - 2.75f, transform.position.y);
                moving = true;
                location = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if hit a bullet
        if (col.tag == "Bullet")
        {
            //destroy bullet
            Destroy(col.gameObject);
            //add this crabs score to player
            //score = crab speed
            PlayerController.score += (int)speed;
            //destroy crab
            EndlessManager.crabCount--; //this crab has been killed
            Destroy(this.gameObject);
        }


        //if hit castel
        if (col.tag == "Finish")
        {
            col.GetComponent<CastleEnd>().EndGame();
        }

        //if hit player
        if (col.tag == "Player")
        {
            //HIGH WAVE SIZE
            EndlessManager.waveSize = 100000;
            //super low crab count so a lot of crabs can go nuts
            EndlessManager.crabCount = -999999999;
            //set won to true 
            EndlessEnemy.won++;
            //destroy player
            Destroy(col.gameObject);
            //make cam a child of this object
            CameraController.camMoving--;
            maincamera.GetComponent<CameraController>().deathNoise();
            maincamera.transform.parent = gameObject.transform;
            speed *= 2;
        }
    }
    
    
}




