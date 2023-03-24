using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    bool canShoot, isShoot, canNet;
    public GameObject bullet, net, mc, newNet;
    public float shootCool, netCool;
    Animator anim;
    public Slider NetSlider;
    public RawImage netTut, shootTut;
    public float shotVol, netVol;
    //player will manage the score + levle
    public Text scoreText, level;
    public static int score = 0;
    public float sliderSpeed, sliderAmount;
    int shoots = 0;
    AudioSource ass;
    //audio stuff
    [SerializeField] private List<AudioClip> shotList; //list of shot noises
    [SerializeField] private List<AudioClip> netList; //list of net shot noises
   
    // Use this for initialization
    void Start() {
        //set everything static to default
        ass = GetComponent<AudioSource>();
        netTut.enabled = false;
        score = 0;
        mc = GameObject.Find("Main Camera");
        anim = GetComponent<Animator>();
        isShoot = true; canShoot = true;
        canNet = false;
        StartCoroutine(netCooldown());
    }
    // Update is called once per frame
    void Update() {
        scoreText.text = "Score: " + score;
        level.text = "Level: " + EndlessEnemy.speed;
        if (isShoot)
        {
            if (Input.GetKeyDown(KeyCode.K) && canShoot)
            {
                anim.Play("shoot");
                Instantiate(bullet, new Vector3(transform.position.x + 1, transform.position.y + 1),
                    Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
                StartCoroutine(shootCooldown());
                shoots++;
                int rand = Random.Range(0, shotList.Count);
                ass.PlayOneShot(shotList[rand], shotVol);
            }

            if (Input.GetKeyDown(KeyCode.D) && canNet)
            {
                anim.Play("shoot");
                newNet = Instantiate(net, new Vector3(mc.transform.position.x -1.6f,
                    mc.transform.position.y -1.00f),
                    Quaternion.Euler(new Vector3(0, 0, 0)));
                newNet.transform.parent = gameObject.transform;
                StartCoroutine(netCooldown());
                NetSlider.transform.rotation =
                    Quaternion.Euler(new Vector3(0, 0,0));
                int rand = Random.Range(0, netList.Count);
                ass.PlayOneShot(netList[rand], netVol);
                netTut.enabled = false;
            }

            if(canNet)
            {
                //bar is full so shake it
                //makes it clear to player that net time has come
                NetSlider.transform.rotation =
                    Quaternion.Euler(new Vector3(0,0, 
                    Mathf.Sin(Time.time * sliderSpeed) * sliderAmount));
                netTut.enabled = true;

            }
            
        }
        if(shoots >= 5)
        {
            shootTut.enabled = false;
        }
    }

    IEnumerator netCooldown()
    {
        canNet = false;
        NetSlider.maxValue = (netCool * 2);
        for(int i = 0; i < (netCool * 2); i++)
        {
            //set value to remaining time
            NetSlider.value = (netCool * 2) - i;
            yield return new WaitForSeconds(0.5f);

        }
        NetSlider.value = 0;
        canNet = true;
    }


    IEnumerator shootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCool);
        canShoot = true;
    }
}


