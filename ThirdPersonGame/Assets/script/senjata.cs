using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class senjata : MonoBehaviour
{
    public Transform tangan;
    public Transform bahu;
    [SerializeField] private characterController jlnscrpt;
    void Start()
    {
        jlnscrpt = FindObjectOfType<characterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //ganti senjata ke bahu saat tekan q
        if (Input.GetKey(KeyCode.Q))
        {
            senjatabahu();
        }
        //ganti senjata ketangan
        else if (Input.GetKey(KeyCode.R))
        {
            pegangsenjata();
        }
    }
    //menaruh senjata dibelakang punggung
    public void senjatabahu ()
    {
        transform.SetParent(bahu);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        jlnscrpt.berdiri2 = true;
    }
    //mengambil senjata ketangan
    public void pegangsenjata()
    {
        transform.SetParent(tangan);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        jlnscrpt.berdiri2 = false;
    }
}
