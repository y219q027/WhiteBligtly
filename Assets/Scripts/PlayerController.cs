using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float x_val;
    private float speed;
    public float jumpingPower;
    public float inputSpeed;
    private Animator anim;
    private float x_valold;
    private string dire;
    public ParticleSystem LightAttack;
    public ParticleSystem LeftAttack;
    public GameObject Attackface;
    public GameObject Sadface;
    bool jump = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Attackface.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        x_val = Input.GetAxisRaw("Horizontal");
        //Debug.Log(x_val);

        if (x_val == 0)
        {
            speed = 0;
            anim.SetBool("Run", false);
            anim.SetBool("LeftRun", false);
            anim.SetBool("LeftStand", false);
        }
        else if(x_val == 0 && dire == "hidari")
        {
            speed = 0;
            anim.SetBool("Run", false);
            anim.SetBool("LeftRun", false);
            //左の立ち再生
            anim.SetBool("LeftStand", true);
        }
        //右に移動
        else if (x_val >= 1)
        {
            speed = inputSpeed;
            //右方向を向く

            anim.SetBool("Run", true);
            anim.SetBool("LeftRun", false);
            //anim.SetTrigger("RightRunTrigger");
        }
        //左に移動
        else if (x_val <= -1)
        {

            speed = inputSpeed * -1;
            //左方向を向く
            anim.SetBool("LeftRun", true);
            anim.SetBool("Run", false);
            //anim.SetTrigger("LeftRunTrigger");
        }

        //Debug.Log(x_val);
        if (x_val != x_valold)
        {
            x_valold = x_val;
            if (x_val <= -1)
            {
                dire = "hidari";

            }
            if (x_val >= 1)
            {
                dire = "migi";
            }
        }

        //transform.position += transform.right * speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        
        //攻撃アクション
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if (dire == "migi")
            {
                GetComponent<Animator>().Play("Attack");
                LightAttack.Play();
                Attackface.gameObject.SetActive(true);
                Invoke("Nomalface", 1);
            }
            else if(dire == "hidari")
            {
                GetComponent<Animator>().Play("Attackleft");
                LeftAttack.Play();
                Attackface.gameObject.SetActive(true);
                Invoke("Nomalface", 1);
            }

        }
        // キャラクターを移動 Vextor2(x軸スピード、y軸スピード(元のまま))
        //rb.velocity = new Vector3(speed, rb.velocity.y);

        //キャラクター移動
        //Debug.Log(speed); 

        transform.Translate(speed, 0,0);
        //transform.position += transform.right * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            //ジャンプ
            rb.AddForce(Vector3.up * jumpingPower);
            jump = true;
        }

    }

    void Nomalface()
    {
        Attackface.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        //ジャンプ一回
        if(collision.gameObject.CompareTag("Stage"))
        {
            jump = false;
        }
    }
}