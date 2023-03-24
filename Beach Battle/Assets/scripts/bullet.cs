using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
    [SerializeField] private List<Sprite> bulletList; //list of bullet sprites
    SpriteRenderer sr; public float speed = 20;
    public GameObject maincamera; //public Rigidbody2D rb2d;
    float rotSpeed;
    // Use this for initialization
    void Start () {
       // rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        maincamera = GameObject.Find("Main Camera");
        //pick one of four random sprites
        int rand = Random.Range(0, bulletList.Count);
        sr.sprite = bulletList[rand];
        rotSpeed = Random.Range(-5f, 5f);
	}
	
	// Update is called once per frame
	void Update () {
        //rotate the bullet for AESTICS
        transform.Rotate(Vector3.forward, rotSpeed);
        //move bullet forward
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        if (transform.position.x > maincamera.transform.position.x + 10) //if out of cam view
        {
             Destroy(this.gameObject);
        }
    }
}
