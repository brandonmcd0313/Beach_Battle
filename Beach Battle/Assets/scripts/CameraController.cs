using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private List<AudioClip> deadList; //list of death noises
    public float deadVol;
    public float reset, increment, backlength; //x position to reset on
    public float speed = 2f;
    public GameObject background, eventManager;
    GameObject recent; Vector3 newPos;
    public static float camMoving = 1;
    bool finished = false;
    // Use this for initialization
    void Start()
    {//set everything static to default
        camMoving = 1;
        increment = reset;
        recent = background;
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraController.camMoving == 1)
        {
            //slowly scroll to the side
            transform.position += new Vector3(
                speed * Time.deltaTime, 0);

            //if we are above the reset value, create new backround at postion
            if (transform.position.x >= reset)
            {
                //get a position for the new backround
                newPos = recent.transform.position;
                newPos += new Vector3(backlength, 0);
                recent = Instantiate(background);
                recent.transform.position = newPos;
                reset += increment; //set next reset point
            }
        }
        else
        {
            //lock the y pos
            if (transform.position.y != 0)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }

        }


    }

    //remove parent so crab can move and dance
    public void unParent()
    {
        transform.parent = null;
    }

    public void deathNoise()
    {
        int rand = Random.Range(0, deadList.Count);
        GetComponent<AudioSource>().PlayOneShot(deadList[rand], deadVol);
    }
}

