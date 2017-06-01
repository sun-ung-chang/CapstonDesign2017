using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GoldenKeyManager : MonoBehaviour
{
    static List<Transform> GoldenShow;
    static public Transform GroundDone;
    static public Transform GroundDoned;
            
    static public Transform ChooseEscape;

    void Start()
    {
        GoldenShow = new List<Transform>();

        for (int i = 0; i < 9; ++i)
        {
            Debug.Log(i + ":" + (GoldenKeyState)i);
            GoldenShow.Insert(i,transform.Find(((GoldenKeyState)i).ToString()));
        }

        GroundDone = transform.Find("GroundDone");
        GroundDoned = transform.Find("GroundDoned");
        ChooseEscape = transform.Find("ChooseEscape");
    }

    public static Transform GetGoldenObj(GoldenKeyState state)
    {
        return GoldenShow[(int)state];
    }

}
