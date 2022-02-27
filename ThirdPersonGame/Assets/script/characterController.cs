using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    CharacterController Controller;

    public float Speed;
    [SerializeField]private float kecepatanlari;
    [SerializeField]private float kecepatanjalan;

    public float jumpPower;

    public Transform Cam;

    public Animator anim;

    public bool isgrounded;
    Vector3 velocity;
    public float grafityScale = -9.81f;

    [SerializeField] private Transform cektanah;
    [SerializeField] private float jaraktanah = 0.4f;
    [SerializeField] private LayerMask masktanah;

    //animation controller
    public bool berdiri2;
    public bool Jalankiri;
    public bool Jalankanan;
    public bool lari;
    public bool Lompat;

    // Start is called before the first frame update
    void Start()
    {
        Speed = kecepatanjalan;
        Controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        //cek tanah
        isgrounded = Physics.CheckSphere(cektanah.position, jaraktanah, masktanah);

        //kondisi untuk lari atau jalan
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = kecepatanlari;
            lari = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = kecepatanjalan;
            lari = false;
        }
        //akhir dari kondisi lari atau jalan

        float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
        Movement.y = 0f;
        gravity();

        animasiCOntroller();
        Controller.Move(Movement);

        //kondisi lompat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isgrounded)
            {
                if (lari==true)
                {
                    Lompat = true;
                }
                lompat();
            }
           
        }
        else
        {
            Lompat = false;
        }
        //akhir kondisi lompat
        if (Movement.magnitude != 0f)
        {
            //kondisi jalan kekanan atau kekiri
            if (Horizontal > 0)
            {
                Jalankanan = true;
            }
            else if (Horizontal < 0)
            {
                Jalankiri = true;
            }
            //akhir kondisi jalan kekanan atau kekiri

            //kondisi jalan maju atau mundur
            if (Vertical!=0)/*untuk sementara != 0 kila sudah ada animasi jalan mundur ubah menjadi >0*/ 
            {
                Jalankanan = false;
                Jalankiri = false;
                if (lari==false)
                {
                    anim.SetBool("jalan", true);
                    anim.SetBool("lari", false);
                }
                else if (lari==true)
                {
                    anim.SetBool("lari", true);
                    //anim.SetBool("jalan", false);
                }
            }
            //else if (Vertical<0)//uncoment ketika mendapatkan animasi mundur
            //{
            //untuk animasi jalan mundur
            //}
            //akhir kondisi jalan maju atau mundur
           
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Cam.GetComponent<camera>().sensivity * Time.deltaTime);

            Quaternion CamRotation = Cam.rotation;
            CamRotation.x = 0f;
            CamRotation.z = 0f;
            print(Horizontal);
            transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);

        }
        else
        {
            Jalankanan = false;
            Jalankiri = false;
            anim.SetBool("Jalankanan", false);
            anim.SetBool("Jalankiri", false);
            anim.SetBool("jalan", false);
        }

    }
    private void lompat()
    {
        velocity.y += jumpPower+(Speed/kecepatanjalan);
        Controller.Move(velocity);
    }
    private void gravity()
    {
        if (isgrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += grafityScale * Time.deltaTime;
        Controller.Move(velocity);
    }
    private void animasiCOntroller()
    {
        //indle animation
        if (berdiri2 == false)
        {
            anim.SetBool("idle2", false);
        }
        else
        {
            anim.SetBool("idle2", true);
        }
        //walking animation
        if (Jalankanan==true)
        {
            anim.SetBool("Jalankanan", true);
            anim.SetBool("Jalankiri", false);
        }
        else if (Jalankiri==true)
        {
            anim.SetBool("Jalankanan", false);
            anim.SetBool("Jalankiri", true);
        }
        else
        {
            anim.SetBool("Jalankanan", false);
            anim.SetBool("Jalankiri", false);
        }
        //jump animation
        if (Lompat==true)
        {
            anim.SetBool("lompat", true);
        }
        else
        {
            anim.SetBool("lompat", false);
        }
    }
}
