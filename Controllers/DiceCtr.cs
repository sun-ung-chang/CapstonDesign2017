using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


// 주사위 윗면 값
public class DiceCtr : MonoBehaviour
{
    int diceValue = 0;

    Quaternion PreRot;
    Vector3 PrePos;

    IEnumerator CheckDone(Action Done)
    {
        PreRot = transform.localRotation;
        PrePos = transform.localPosition;

        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            if (PreRot == transform.localRotation && (PrePos == transform.localPosition) )
            {
                Done();
                break;
            }

            PreRot = transform.localRotation;
            PrePos = transform.localPosition;

            yield return new WaitForSeconds(0.1f);
        }
    }
    
    public void RollDice(Vector3 Pos , Action Done)
    {
        transform.localPosition = Pos;
        int force = UnityEngine.Random.Range(0, 10);
        gameObject.GetComponent<Rigidbody>().AddForce(transform.up * force);
        force = UnityEngine.Random.Range(0, 10);
        gameObject.GetComponent<Rigidbody>().AddForce(transform.right * force);

        transform.rotation = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360)));

        StartCoroutine(CheckDone(Done));
    }

    public int GetDiceValue()
    {
        if (Vector3.Dot(transform.forward, Vector3.up) > 0.6f)
            diceValue = 1;
        else if (Vector3.Dot(-transform.up, Vector3.up) > 0.6f)
            diceValue = 2;
        else if (Vector3.Dot(-transform.right, Vector3.up) > 0.6f)
            diceValue = 3;
        else if (Vector3.Dot(transform.right, Vector3.up) > 0.6f)
            diceValue = 4;
        else if (Vector3.Dot(transform.up, Vector3.up) > 0.6f)
            diceValue = 5;
        else if (Vector3.Dot(-transform.forward, Vector3.up) > 0.6f)
            diceValue = 6;

        return diceValue;
    }

}