using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeOut : MonoBehaviour {
    public float fadeSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Color color = this.GetComponent<SpriteRenderer>().color;
        color.a -= Time.deltaTime * fadeSpeed;
        this.GetComponent<SpriteRenderer>().color = color;
        if(color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
    

}
