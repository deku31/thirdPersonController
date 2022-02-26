using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    CharacterController Controller;

    public float Speed;

    public Transform Cam;

    public Animator anim;

    public bool isgrounded;
    Vector3 velocity;
    public float grafityScale = -9.81f;

    [SerializeField] private Transform cektanah;
    [SerializeField] private float jaraktanah = 0.4f;
    [SerializeField] private LayerMask masktanah;
    // Start is called before the first frame update
    void Start()
    {

        Controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
        Movement.y = 0f;
        gravity();


        Controller.Move(Movement);

        if (Movement.magnitude != 0f)
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Cam.GetComponent<camera>().sensivity * Time.deltaTime);
            anim.SetBool("jalan", true);

            Quaternion CamRotation = Cam.rotation;
            CamRotation.x = 0f;
            CamRotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);

        }
        else
        {
            anim.SetBool("jalan", false);
        }
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
}
