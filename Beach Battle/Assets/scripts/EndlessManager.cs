using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour {
    public GameObject enemy, maincamera;
    public static int crabCount = 0;
    bool running = false; bool started = false;
    public static int waveSize = 10;
    public AudioClip walkin;
    AudioSource ass;
    // Use this for initialization
    void Start() {
        ass = GetComponent<AudioSource>();
        //set everything static to default
        EndlessEnemy.speed = 1; EndlessEnemy.won = 0;
        crabCount = 0; waveSize = 10;
        StartCoroutine(startUp());
    }

    // Update is called once per frame
    void Update() {
        // print("Crabs: " + crabCount);
        if (!running && crabCount <= 0 && started)
        {
            ass.enabled = false;
            print("NEW WAVE");
            EndlessEnemy.speed++;
            print(EndlessEnemy.speed);
            waveSize = (int)(waveSize * 1.25);
            
            StartCoroutine(summonWave(waveSize, 1));
        }
        else //crabs exist
        {
            ass.enabled = true;
        }
    }



    IEnumerator summonWave(int amount, float speed)
    {
        running = true;
        //for loop amount
        for (int i = 0; i < amount; i++)
        {
            //summon a crab 
            int preRand = Random.Range(-1, 2);
            //print(preRand);
            GameObject newEnemy = Instantiate(enemy, new Vector3(maincamera.transform.position.x + 10, (preRand * 3)), enemy.transform.rotation);
            //at cam x pos + 10, y = -3, 0 or, 3
            //wait for speed
            //for variation in time
            float rand = Random.Range(-0.5f, 0.5f);
            yield return new WaitForSeconds(speed + rand);
        }
        running = false;
    }

   IEnumerator startUp()
   {
        //wait for loadtime
   yield return new WaitForSeconds(2f);
        ///begin the battle
     StartCoroutine(summonWave(waveSize, 1));
        started = true;
    }

    public void killAud()
    {
        //destroy the audiosorce component
        ass.volume = 0;
    }
}
