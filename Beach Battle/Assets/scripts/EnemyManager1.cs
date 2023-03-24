using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager1 : MonoBehaviour {
    public GameObject enemy;
    public GameObject maincamera;
    bool running;
	// Use this for initialization
	void Start () {
        running = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void startWave(int amt, float spd)
    {
        if (!running)
        {
            StartCoroutine(summonWave(amt, spd));
        }

    }
    IEnumerator summonWave(int amount, float speed)
    {
        running = true;
        //for loop amount
        for(int i = 0; i < amount; i++)
        {
            //summon a crab 
            int preRand = Random.Range(-1, 2 );
            print(preRand);
            GameObject newEnemy = Instantiate(enemy, new Vector3(maincamera.transform.position.x + 10,( preRand * 3)), enemy.transform.rotation);
            //at cam x pos + 10, y = -3, 0 or, 3
            //wait for speed
            //for variation in time
            float rand = Random.Range(-0.5f, 0.5f);
            yield return new WaitForSeconds(speed + rand);
        }
        running = false;
    }
    }


