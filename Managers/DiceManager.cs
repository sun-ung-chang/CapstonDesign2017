using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public DiceCtr[] dices;

    public Text Vtext;

    int count;

    public void SetNone()
    {
        Vtext.text = "?";
    }

    public void RollDices(Action<int> end)
    {
        
        for (int i=0; i<dices.Length;++i)
        {
            dices[i].gameObject.SetActive(true);
        }

        Vector3 pos1 = new Vector3(-0.75f, -0.8f, -0.29f);
        Vector3 pos2 = new Vector3(1.75f, -0.78f, -0.22f);

        count = 0;

        dices[0].RollDice(pos1,DoneRoll);
        dices[1].RollDice(pos2, DoneRoll);

        StartCoroutine(CheckDone(end));
    }

    void DoneRoll()
    {
        ++count;
    }

    IEnumerator CheckDone(Action<int> end)
    {
        while(true)
        {
            if(count == 2)
            {
                int value = dices[0].GetDiceValue() + dices[1].GetDiceValue();
                Vtext.text = value.ToString();
                  
                end(value);
                break;
            }
            yield return new WaitForSeconds(0.03f);
        }

        StartCoroutine(GetOutDice(2));
    }

    public IEnumerator GetOutDice(float time)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < dices.Length; ++i)
        {
            if(count == 2)
            dices[i].gameObject.SetActive(false);
        }
    }

}
