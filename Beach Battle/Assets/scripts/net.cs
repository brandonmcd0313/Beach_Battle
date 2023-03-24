using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class net : MonoBehaviour {
    Vector3 endPoint; GameObject mc;
    Collider2D col; Animator anim; public float speed;
	// Use this for initialization
	void Start () {
        mc = GameObject.Find("Main Camera");
        //net launcehd based on cam pos!
        endPoint = new Vector3(mc.transform.position.x + 3.2f,
           mc.transform.position.y - 1.6f, 0);
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();//turn off death
        col.enabled = false;
        anim.enabled = false;
        StartCoroutine(endOfAnim());e
        
	}
	
	// Update is called once per frame
	void Update () {
        endPoint = new Vector3(mc.transform.position.x + 3.2f,
           mc.transform.position.y - 1.6f, 0); //update end point
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPoint, Time.deltaTime * speed);
    }

    IEnumerator endOfAnim()
    {

        yield return new WaitForSeconds(0.3f);
        anim.enabled = true;
        //wait till end of animaton
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length 
            + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        //wait till in pos
        //print("ANIM END");
        //turn on death
        col.enabled = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
