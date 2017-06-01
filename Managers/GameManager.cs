using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Select_Turn = 0,
        Roll_Dice_Turn = 1,
        Wait_Dice_Turn = 2,
        Move = 3,
        SelectUI =4,
        GameOver =5,
    }

    [Header("Player")]
    public PlayerUiCtr[] PlayerUi;

    public GameObject[] PlayerPrefab;
    public GameObject[] PlayerCamera;

    [Header("Dice")]
    public Button RollButton;
    public DiceManager DiceM;
    public Image DiceTimeImage;

    [Header("UI")]
    public GameObject SelectMooindo;

    public GameObject SelectTurn;
    [Header("Stage")]
    static public GroundCtr[] grounds;

    static GameState state;

    static public List<PlayerCtr> Players;

    static public PlayerCtr FindPlayer(int id)
    {
        return Players.Find(x => x.Id == id);
    }

    static public int My_id;
    int NowPlayer_Id;


    #region INIT

    void Start()
    {
        Init();
    }

    void Init()
    {
        GroundSet();
        StartSelectTurn();
    }

    
    void PlayerSetting(bool first)
    {
        My_id = 1004;

        PlayerCtr player = PlayerPrefab[0].AddComponent<PlayerCtr>();
        player.Id = My_id;
        player.SetHp(1000);
        player.HairC = Color.red;
        player.Name = "빨간머리";
        player.Ui = PlayerUi[0];
        player.Escape = false;

        PlayerCtr AI = PlayerPrefab[1].AddComponent<AI>();
        AI.Id = 777;
        AI.SetHp(1000);
        AI.HairC = Color.yellow;
        AI.Name = "노랑머리";
        AI.Ui = PlayerUi[1];
        AI.Escape = false;

        Players = new List<PlayerCtr>();

        player.Now_Ground_Id = 0;
        player.Mooindo_count = 0;
        AI.Now_Ground_Id = 0;
        AI.Mooindo_count = 0;

        player.Next_Player_Id = AI.Id;
        AI.Next_Player_Id = player.Id;

        if (first)
        {
            NowPlayer_Id = player.Id;

            Players.Insert(0, player);
            Players.Insert(1, AI);

            player.SetUI(true);
            AI.SetUI(false);
        }
        else
        {
            NowPlayer_Id = AI.Id;

            Players.Insert(0, AI);
            Players.Insert(1, player);

            player.SetUI(false);
            AI.SetUI(true);
        }

        SelectTurn.SetActive(false);

        StartRollDiceTurn();
    }

    #endregion INIT

    #region SelectTurn

    void StartSelectTurn()
    {
        state = GameState.Select_Turn;

        Button first = SelectTurn.transform.GetChild(0).GetComponent<Button>();

        Button Sec = SelectTurn.transform.GetChild(1).GetComponent<Button>();

        SetButtons(first, Sec);

        SelectTurn.SetActive(true);
    }

    void SetButtons(Button Fir, Button Sec)
    {
        Fir.onClick.RemoveAllListeners();
        Fir.onClick.AddListener(SetFirst);
        Sec.onClick.RemoveAllListeners();
        Sec.onClick.AddListener(SetSec);
    }

    void SetFirst()
    {
        PlayerSetting(true);
    }

    void SetSec()
    {
        PlayerSetting(false);
    }

    #endregion SelectTurn

    #region Dice

    void EscapeMooindo(PlayerCtr player)
    {
        player.Mooindo_count = 0;
        player.Escape = false;

        state = GameState.Roll_Dice_Turn;

        if (player.GetType() == typeof(AI))
        {
            RollingDices();
        }
        else
        {
            StartCoroutine(TimeStop(10));
        }
    }

    void StartRollDiceTurn()
    {
        PlayerCtr nowplayer = FindPlayer(NowPlayer_Id);

        
        DiceTimeImage.gameObject.SetActive(true);

        if (NowPlayer_Id == My_id)
        {
            RollButton.gameObject.SetActive(true);
        }

        if (FindPlayer(NowPlayer_Id).Mooindo_count != 0)
        {
            DiceTimeImage.gameObject.SetActive(false);
            RollButton.gameObject.SetActive(false);
            if (nowplayer.Escape)
            {
                if (nowplayer.GetType() == typeof(AI))
                {
                    EscapeMooindo(nowplayer);
                }
                else
                {
                    state = GameState.SelectUI;
                    SelectMooindo.SetActive(true);
                }
            }
            else
            {
                if (--FindPlayer(NowPlayer_Id).Mooindo_count == 0)
                {
                    state = GameState.Roll_Dice_Turn;
                    RollButton.gameObject.SetActive(true);
                    DiceTimeImage.gameObject.SetActive(true);
                    if (nowplayer.GetType() == typeof(AI))
                    {
                        RollButton.gameObject.SetActive(false);
                        DiceTimeImage.gameObject.SetActive(false);
                        RollingDices();
                    }
                    else
                    {
                        StartCoroutine(TimeStop(10));
                    }
                }
                else
                    StartCoroutine(Mooindo.ShowMooindoAsync(FindPlayer(NowPlayer_Id).Mooindo_count, NextTurn));
                return;
            }
        }

        state = GameState.Roll_Dice_Turn;

        if (nowplayer.GetType() == typeof(AI))
        {
            RollingDices();
        }
        else
        {
            StartCoroutine(TimeStop(10));
        }
    }

    IEnumerator TimeStop(float time)
    {
        float max = time;

        while(state == GameState.Roll_Dice_Turn)
        {
            time -= Time.deltaTime;

            float amount = time / max;

            DiceTimeImage.fillAmount = amount;

            if (time < 0)
            {
                time = 0;
                DiceTimeImage.fillAmount = 0;
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }

        RollingDices();
    }

    public void RollingDices()
    {
        if(state !=GameState.Roll_Dice_Turn)
            return;

        state = GameState.Move;

        DiceM.RollDices(EndRollDice);
    }

    static public void Move(PlayerCtr player,int value, Action end)
    {
        int now_gid = player.Now_Ground_Id;

        Vector2[] poss = new Vector2[Mathf.Abs(value)];

        int healpos = -1;

        List<Action<float>> Anis = new List<Action<float>>();

        for (int i = 0; i < Mathf.Abs(value); ++i)
        {
            if (value > 0)
            {
                SetGroundId(now_gid, 1,out now_gid);
            }
            else
            {
                SetGroundId(now_gid, -1, out now_gid);
            }
            if (grounds[now_gid].GetType() == typeof(StartGround))
            {
                healpos = i;
            }
            Action<float> act = grounds[now_gid].Ani;

            Anis.Add(act);

            poss[i] = GetGroundPos(now_gid);
        }

        player.Heal(healpos);

        player.Now_Ground_Id = now_gid;

        player.Move(end, Anis, poss);
    }

    void EndRollDice(int value)
    {
        RollButton.gameObject.SetActive(false);
        DiceTimeImage.gameObject.SetActive(false);

        PlayerCtr nowp = FindPlayer(NowPlayer_Id);

        int now_gid = nowp.Now_Ground_Id;

        Vector2[] poss = new Vector2[Mathf.Abs(value)];

        int healpos = -1;

        List<Action<float>> Anis = new List<Action<float>>();

        for (int i=0; i< Mathf.Abs(value); ++i)
        {
            if (value > 0)
            {
                SetGroundId(now_gid, 1, out now_gid);
            }
            else
            {
                SetGroundId(now_gid, -1, out now_gid);
            }

            if (grounds[now_gid].GetType() == typeof(StartGround))
            {
                healpos = i;
            }
            Action<float> act = grounds[now_gid].Ani;

            Anis.Add(act);

            poss[i] = GetGroundPos(now_gid);
        }

        nowp.Heal(healpos);

        nowp.Now_Ground_Id = now_gid;

        nowp.Move(StartGroundAct, Anis,poss);
    }

    #endregion Dice

    #region Ground

    public void ChooseEscape()
    {
        EscapeMooindo(FindPlayer(NowPlayer_Id));
        SelectMooindo.SetActive(false);
        state = GameState.Roll_Dice_Turn;
    }

    public void StillHere()
    {
        StartCoroutine(Mooindo.ShowMooindoAsync(FindPlayer(NowPlayer_Id).Mooindo_count, NextTurn));
        --FindPlayer(NowPlayer_Id).Mooindo_count;
        SelectMooindo.SetActive(false);
    }

    void GroundSet()
    {
        StartCoroutine(DiceM.GetOutDice(0));

        Transform Boxes = GameObject.Find("Ground").transform;

        Debug.Log(Boxes);

        grounds = new GroundCtr[28];

        for (int i = 1; i <= 28; ++i)
        {
            string name = "Box";
            if (i < 10)
            {
                name += "00" + i.ToString();
            }
            else
            {
                name += "0" + i.ToString();
            }

            grounds[i - 1] = Boxes.Find(name).GetComponent<GroundCtr>();
        }

        for (int i = 0; i < grounds.Length; ++i)
        {
            grounds[i].Belong = false;
            grounds[i].Id = i;
        }
    }

    static public void SteelGround(int id,PlayerCtr player)
    {
        FindPlayer(player.Next_Player_Id).RemoveGround(id);
        grounds[id].SetBuilding(player);
    }

    static public void AniGround(int id)
    {

    }

    static void SetGroundId(int id, int plus, out int value)
    {
        value = id + plus;

        if (value >= grounds.Length)
        {
            value = 0;
        }
    }

    void StartGroundAct()
    {
        DiceM.SetNone();

        PlayerCtr nowp = FindPlayer(NowPlayer_Id);

        int g_id = nowp.Now_Ground_Id;

        grounds[g_id].Act(nowp, NextTurn);                
    }
    

    static Vector2 GetGroundPos(int id)
    {
        Vector2 pos = new Vector2(grounds[id].transform.localPosition.x, grounds[id].transform.localPosition.z);

        return pos;
    }

    #endregion Ground

    #region Flow

    static public void GameDone(int id)
    {
        state = GameState.GameOver;

        if (id == My_id)
        {
            GameObject.Find("GameOver").transform.GetComponent<GameOverCtr>().Lose.SetActive(true);
            //패배
        }
        else
        {
            GameObject.Find("GameOver").transform.GetComponent<GameOverCtr>().Win.SetActive(true);
            //승리
        }
    }

    void NextTurn()
    {
        FindPlayer(NowPlayer_Id).SetUI(false);        
        NowPlayer_Id = FindPlayer(NowPlayer_Id).Next_Player_Id;
        FindPlayer(NowPlayer_Id).SetUI(true);
        StartRollDiceTurn();
    }

    public void GoIntro()
    {
        PSceneManager.ChangeScene(SceneState.Character);
    }

    #endregion Flow
}

