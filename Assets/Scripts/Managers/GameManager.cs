using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 游戏管理类 (已给出部分代码,需补充)
/// </summary>*
public class GameManager : MonoBehaviour
{
    [SerializeField] private Tank[] tanks; //坦克数组
    [SerializeField] private GameObject tankPrefab; //坦克预制体
    [SerializeField] private Text messageText; //引用覆盖文本以显示获奖文本等。
    [SerializeField] private float gameStartDelay = 2f;
    [SerializeField] private float gameEndDelay = 3f;

    [SerializeField] private int gameRoundCount = 5; //游戏回合数

    [SerializeField] private TextMeshProUGUI countText1; //玩家1分数
    [SerializeField] private TextMeshProUGUI countText2; //玩家2分数
    private int count1 = 0; //玩家1分数
    private int count2 = 0; //玩家2分数

    public static GameManager Instance { get; private set; } //单例模式
    private TankManager _tankManager;

    public int m_NumRoundsToWin = 5; //单个玩家必须赢得的回合数才能赢得游戏。
    public float m_EndDelay = 3f;
    private int m_RoundNumber; //当前正在进行哪一轮比赛。
    private WaitForSeconds m_StartWait; //在回合开始时的延迟。 

    private WaitForSeconds m_EndWait; //在回合或比赛结束时会有延迟。


    private void Awake()
    {
        SpawnTanks(); //生成坦克

        m_StartWait = new WaitForSeconds(gameStartDelay); //初始化延迟
        m_EndWait = new WaitForSeconds(gameEndDelay);
    }

    public void Start()
    {
        StartCoroutine(GameLoop()); //开始游戏循环,协程,相当于启动了一个线程

        SetCountText1();
        SetCountText2();
    }

    private void SpawnTanks() //生成坦克
    {
        _tankManager = new TankManager(gameRoundCount);

        for (int i = 0; i < tanks.Length; i++) //遍历坦克数组
        {
            Tank tank = tanks[i];
            GameObject tankGameObject = Instantiate(tankPrefab, tank.spawnPoint.position, Quaternion.identity);     //实例化坦克预制体

            tank.Setup(tankGameObject, i + 1); //调用Tank脚本中的Setup方法

            tankGameObject.SetActive(true);

            _tankManager.Addtank(tank);
        }
    }

    public void StartGame() //开始游戏
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop() //游戏循环，协程
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());
    }

    private IEnumerator RoundStarting()
    {
        messageText.text = "Round Starting";
        _tankManager.ResetTanks();  //重置坦克
        _tankManager.EnableTanks(false);    //禁用坦克

        yield return new WaitForSeconds(gameStartDelay);    //延迟
    }

    private IEnumerator RoundPlaying()      //回合进行中
    {
        messageText.text = "";

        _tankManager.EnableTanks(true);

        yield return new WaitUntil(_tankManager.IsOneTankLeft);
    }

    private IEnumerator RoundEnding()   //回合结束
    {
        messageText.text = "Round Ending";

        _tankManager.EnableTanks(false);

        Tank roundWinner = _tankManager.GetRoundWinner();
        roundWinner.IncreaseRoundWinnerCount();

        messageText.text = "Round Winner: Tank " + roundWinner.playerNumber;

        yield return new WaitForSeconds(gameEndDelay);      //延迟

        Tank gameWinner = _tankManager.GetGameWinner();

        // 更新计数器文本
        if (roundWinner.playerNumber == 1)
        {
            Debug.Log("player1 win");
            count1++;
            SetCountText1();
        }
        else if (roundWinner.playerNumber == 2)
        {
            Debug.Log("player2 win");
            count2++;
            SetCountText2();
        }

        // 如果有玩家赢得了游戏，那么就结束游戏
        if (gameWinner != null)
        {
            messageText.text = "Game Winner: Tank " + gameWinner.playerNumber;
        }
        else
        {
            messageText.text = "Need another Round !";
            StartCoroutine(GameLoop());
        }
    }

    void SetCountText1()    //设置玩家1分数
    {
        countText1.text = "Score: " + count1.ToString();
    }

    void SetCountText2()    //设置玩家2分数
    {
        countText2.text = "Score: " + count2.ToString();
    }
}