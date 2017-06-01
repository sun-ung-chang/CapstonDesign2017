using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCtr : MonoBehaviour
{
    List<int> Grounds;

    public List<int> GetGroundIds() { return Grounds; }



    public void RemoveGround(int id)
    {
        if(Grounds.Contains(id))
        {
            Grounds.Remove(id);
        }
    }



    public bool DoYouHaveGround(int id)
    {
        if (Grounds == null)
            Grounds = new List<int>();
        
        return Grounds.Contains(id);
    }

    public void AddGround(int id)
    {
        if (Grounds == null)
            Grounds = new List<int>();

        Grounds.Add(id);
    }

    int Hp { get; set; }
    public int Id { get; set; }
    public int Now_Ground_Id { get; set; }
    public int Next_Player_Id { get; set; }
    public int Mooindo_count { get; set; }

    public bool Escape { get; set; }

    public Color HairC { get; set; }

    public string Name { get; set; }

    public PlayerUiCtr Ui;

    public void SetUI(bool my)
    {
        Ui.SetUI(my, Hp.ToString(), Name);
    }

    public void Init(Vector2 Pos)
    {
        transform.localPosition = new Vector3(Pos.x+2f, 9.06f, Pos.y+2f);//pos
    }

    public void SetHp(int value)
    {
        Hp = value;
    }

    public void GetDamaged(int value)
    {

        Hp -= value;

        if(Hp<=0)
        {
            GameManager.GameDone(Id);
        }
    }

    public void Move(Action End, List<Action<float>> Ani,params Vector2[] Pos)
    {
        StartCoroutine(GoMove(End, Ani, Pos));
    }

    float healnum;

    public void Heal(int index)
    {
        healnum = index;
    }

    IEnumerator GoMove(Action End, List<Action<float>> GAni, Vector2[] Pos)
    {
        for (int i = 0; i < Pos.Length; ++i)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Moving(Pos[i]));
            //
            Ani(0.3f);
            GAni[i](0.3f);

            if (i == healnum)
            {
                Hp += 100;
                SetUI(true);
            }
        }
        yield return new WaitForSeconds(0.5f);
        End();
    }

    IEnumerator Moving(Vector2 End)
    {

        transform.localPosition = new Vector3(End.x, transform.localPosition.y, End.y);//좌표
        yield return null;
    }

    public void Ani(float value)
    {
        Debug.Log(Id);
        StartCoroutine(StartUpDown(value));
    }

    IEnumerator StartUpDown(float value)
    {
        float deltime = 0;
        Vector3 Startpos, Endpos;
        Startpos = gameObject.transform.localPosition;
        Endpos = Startpos;
        Endpos.y -= value;

        while (true)
        {
            if (deltime >= 1)
            {
                gameObject.transform.localPosition = Endpos;
                break;
            }

            deltime += Time.deltaTime * 4;
            gameObject.transform.localPosition = Vector3.Lerp(Startpos, Endpos, deltime);
            yield return new WaitForSeconds(0.001f);
        }

        deltime = 0;
        while (true)
        {
            if (deltime >= 1)
            {
                gameObject.transform.localPosition = Startpos;
                break;
            }
            deltime += Time.deltaTime * 4;
            gameObject.transform.localPosition = Vector3.Lerp(Endpos, Startpos, deltime);

            yield return new WaitForSeconds(0.001f);
        }

    }

}
