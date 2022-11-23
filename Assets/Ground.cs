using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public Material Active;
    public Material NonActive;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Debug.Log("hit player");
            transform.GetComponent<Renderer>().material = Active;
        }
    }
}
