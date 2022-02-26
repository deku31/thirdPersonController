using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerbody;
    public float xRotate;
    public float kecepatanbelok;
    [SerializeField] private Transform kamera;
    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        kamera.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * kecepatanbelok * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * kecepatanbelok * Time.deltaTime;

        xRotate -= mouseX;
        xRotate = Mathf.Clamp(xRotate, -90f, 90f);
        //transform.localRotation = Quaternion.Euler(xRotate, 0, 0);
        playerbody.Rotate(Vector3.up * mouseX);
    }
   
    private void LateUpdate()
    {
        kamera.LookAt(target);
    }

}
