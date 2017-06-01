using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum GoldenKeyState
{
    GetGround = 0,
    HpUp = 1,
    HpDown = 2,
    EHpUp = 3,
    EHpDown = 4,
    MoveBack = 5,
    MoveFront = 6,
    BackStart = 7,
}

public class GoldenKey : GroundCtr
{
    IEnumerator ShowGroundGet(int id)
    {
        if (id == GameManager.My_id)
        {
            GoldenKeyManager.GroundDone.gameObject.SetActive(true);
        }
        else
        {
            GoldenKeyManager.GroundDoned.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1);

        if (id == GameManager.My_id)
        {
            GoldenKeyManager.GroundDone.gameObject.SetActive(false);
        }
        else
        {
            GoldenKeyManager.GroundDoned.gameObject.SetActive(false);
        }
    }

    IEnumerator ShowKey(PlayerCtr player, int value, GoldenKeyState state, Action _End)
    {
        GoldenKeyManager.GetGoldenObj(state).gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        GoldenKeyManager.GetGoldenObj(state).gameObject.SetActive(false);

        GameManager.Move(player, value, _End);
    }

    IEnumerator ShowKey(GoldenKeyState state, int id ,Action _End)
    {
        GoldenKeyManager.GetGoldenObj(state).gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        GoldenKeyManager.GetGoldenObj(state).gameObject.SetActive(false);

        if (state == GoldenKeyState.GetGround)
        {
            yield return StartCoroutine(ShowGroundGet(id));
        }

        _End();
    }

    IEnumerator ShowKey(GoldenKeyState state, Action _End)
    {
        GoldenKeyManager.GetGoldenObj(state).gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        GoldenKeyManager.GetGoldenObj(state).gameObject.SetActive(false);

        _End();
    }

    public override void Act(PlayerCtr player, Action _End)
    {
        GoldenKeyState state = (GoldenKeyState)UnityEngine.Random.Range((int)0,(int)8);

      //  state = GoldenKeyState.MoveBack;

        Debug.Log(state);

        PlayerCtr enemy = GameManager.FindPlayer(player.Next_Player_Id);

        switch (state)
        {
            case GoldenKeyState.GetGround:
   
                List<int> enemygroundlist = enemy.GetGroundIds();
                int ran = 0;// UnityEngine.Random.Range(0, enemygroundlist.Count);
                int id = 0;// enemygroundlist[ran];

                if (enemygroundlist == null)
                {
                    StartCoroutine(ShowKey(state, id, _End));
                    break;
                }
                else if(enemygroundlist.Count == 0)
                {
                    StartCoroutine(ShowKey(state, id, _End));
                    break;
                }
                else
                {
                    ran = UnityEngine.Random.Range(0, enemygroundlist.Count);
                    id = enemygroundlist[ran];
                    GameManager.SteelGround(id, player);
                    StartCoroutine(ShowKey(state, id, _End));
                }

                               
                break;

            /*case GoldenKeyState.EscapeMooindo:
                player.Escape = true;

                StartCoroutine(ShowKey(state, _End));
                break;*/

            case GoldenKeyState.HpUp:
                player.GetDamaged(-200);
                player.SetUI(true);

                StartCoroutine(ShowKey(state, _End));
                break;

            case GoldenKeyState.HpDown:
                player.GetDamaged(200);
                player.SetUI(true);

                StartCoroutine(ShowKey(state, _End));
                break;

            case GoldenKeyState.EHpUp:
                enemy.GetDamaged(-200);
                enemy.SetUI(false);

                StartCoroutine(ShowKey(state, _End));
                break;

            case GoldenKeyState.EHpDown:
                enemy.GetDamaged(200);
                enemy.SetUI(false);

                StartCoroutine(ShowKey(state, _End));
                break;

            case GoldenKeyState.MoveFront:
                StartCoroutine( ShowKey(player,3, state, _End));
                break;

            case GoldenKeyState.MoveBack:
                StartCoroutine(ShowKey(player, -3, state, _End));                
                break;

            case GoldenKeyState.BackStart:
               StartCoroutine( ShowKey(player, GameManager.grounds.Length - player.Now_Ground_Id, state, _End));
                break;

            default:
                //Debug.Log("에러");
                break;
        }

    }

}
