using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jalan : MonoBehaviour
{
    [SerializeField] private CharacterController player;
    [SerializeField] private Animator anim;

    public float speed=12f;
    public bool berdiri2 = false;
    public bool jln;
    float x;
    float z;
    //grafity
    public float grafityScale = -9.81f;
    Vector3 velocity;

    //cek tanah
    [SerializeField]private Transform cektanah;
    [SerializeField] private float jaraktanah=0.4f;
    [SerializeField] private LayerMask masktanah;

    public bool isgrounded;
    public Transform pemain;
    public float powerJump=6;
    float xrotate;

    void Start()
    {
        anim = GameObject.Find("player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //cek tanah
        isgrounded = Physics.CheckSphere(cektanah.position, jaraktanah, masktanah);
        //walking
        x = Input.GetAxis("Horizontal");//tombol horizontal seperti a, d, kiri, kanan. 
        z = Input.GetAxis("Vertical");//tombol vertikal seperti w, s, atas, bawah.
        
        Vector3 move = transform.right * x + transform.forward * z;

        player.Move(move*speed*Time.deltaTime);//untuk membuat player dapat dijalankan
        gravity();
        //kondisi untuk menentukan player itu jalan atau tidak
        if (x != 0 || z != 0)
            jln = true;
        else
            jln = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isgrounded)
            {
                lompat();
            }
        }
        jalananim();//method yang bertujuan untuk mengakses animasi jalan
        berdiri();
    }

    //method animasi berjalan
    private void jalananim()
    {
        if (jln)
        {
            anim.SetBool("jalan", true);

        }
        else
        {
            anim.SetBool("jalan", false);
        }
    }

    //method animasi berdiri diam(idle)
    private void berdiri()
    {
        if (berdiri2 == false)
        {
            anim.SetBool("idle2", false);
        }
        else
        {
            anim.SetBool("idle2", true);
        }
    }
    //mengatur gravity pada player
    private void gravity()
    {
        if (isgrounded&&velocity.y<0)
        {
            velocity.y = -2f;
        }
        velocity.y += grafityScale * Time.deltaTime;
        player.Move(velocity);
    }
    private void lompat()
    {
        velocity.y += powerJump;
        player.Move(velocity);
    }
}
