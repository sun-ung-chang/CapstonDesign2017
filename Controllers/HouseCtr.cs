using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseCtr : MonoBehaviour
{
    public GameObject Roof;

    public void SetHouse(Color RoofC)
    {
        Roof.GetComponent<MeshRenderer>().material.color = RoofC;
        gameObject.SetActive(true);
    }

}
