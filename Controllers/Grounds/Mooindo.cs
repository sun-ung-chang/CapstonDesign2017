using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mooindo : GroundCtr
{
    public static Text mooindotext;
    public static Image mooindoimage;

    void Start()
    {
        mooindotext = GameObject.Find("MooindoText").GetComponent<Text>();
        mooindoimage = GameObject.Find("MooindoImage").GetComponent<Image>();
        mooindotext.gameObject.SetActive(false);
        mooindoimage.gameObject.SetActive(false);
    }

    public override void Act(PlayerCtr player, Action _End)
    {
        player.Mooindo_count = 3;
        StartCoroutine(ShowMooindoAsync(player.Mooindo_count,_End) );                
    }

    static public IEnumerator ShowMooindoAsync(int value,Action End)
    {
        
        mooindotext.gameObject.SetActive(true);
        mooindoimage.gameObject.SetActive(true);
        mooindotext.text = "<" + value.ToString() + "턴 후에 진행합니다.>";

        yield return new WaitForSeconds(2);

        mooindotext.gameObject.SetActive(false);
        mooindoimage.gameObject.SetActive(false);
        End();
    }
}
