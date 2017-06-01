using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUiCtr : MonoBehaviour
{
    public Text HpText;
    public Text NameText;
    public GameObject Myturn;

    public void SetUI(bool my, string hp)
    {
        if (my) Myturn.SetActive(true);
        else Myturn.SetActive(false);

        HpText.text = hp;
    }

    public void SetUI(bool my, string hp,string name)
    {
        if (my) Myturn.SetActive(true);
        else Myturn.SetActive(false);

        HpText.text = hp;
        NameText.text = name;
    }
}
