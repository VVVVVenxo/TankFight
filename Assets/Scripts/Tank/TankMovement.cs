using System;
using UnityEngine;
/// <summary>
/// 坦克移动类(已给出部分代码,需补充)
/// </summary>
public class TankMovement : MonoBehaviour
{
    [SerializeField]    //序列化字段，使其在Inspector面板中显示
    private float moveSpeed = 10f;   //坦克移动速度
    [SerializeField]
    private float turnSpeed = 200f;   //坦克转向速度
    [SerializeField]
    private int m_PlayerNumber = 1;              //区分玩家
    private Rigidbody _rb;              //移动Tank的引用
    private float _turnInputValue;             //左右输入
    private float _movementInputValue;         //前后输入

    [SerializeField] private AudioClip idleClip;         //音效组件
    [SerializeField] private AudioClip movingClip;            //坦克待机音效
    [SerializeField] private AudioSource engineAudioSources;           //坦克运行音效

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();    //获取坦克的刚体
    }

    private void Update()   //update里获取输入
    {
        _turnInputValue = Input.GetAxis("Horizontal" + m_PlayerNumber); //获取玩家左右输入
        _movementInputValue = Input.GetAxis("Vertical" + m_PlayerNumber);   //获取玩家前后输入

        // 存储玩家的输入并确保引擎的音频正在播放。
    }

    private void FixedUpdate()
    {
        Move();
        Turn();

        engineAudio();
        // 移动并转动坦克。
    }

    private void engineAudio()
    {
        if (!Mathf.Approximately(_movementInputValue, 0f) || !Mathf.Approximately(_turnInputValue, 0f))
        {
            if (engineAudioSources.clip == idleClip)
            {
                engineAudioSources.clip = movingClip;
                engineAudioSources.Play();
            }
        }
        else
        {
            if (engineAudioSources.clip == movingClip)
            {
                engineAudioSources.clip = idleClip;
                engineAudioSources.Play();
            }
        }
    }

    private void Move() // 根据玩家的输入调整坦克的位置。
    {   
        //forward是坦克的前方，position是坦克的位置
        Vector3 position = transform.forward * _movementInputValue * Time.deltaTime * moveSpeed;
        _rb.MovePosition(_rb.position + position);
    }


    private void Turn() // 根据玩家的输入调整坦克的旋转。
    {
        float turn = _turnInputValue * Time.deltaTime * turnSpeed;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        _rb.MoveRotation(_rb.rotation * turnRotation);
    }

    public void SetPlayerNumber(int playerNumber)   //设置玩家编号
    {
        TankMovement tankMovement = GetComponent<TankMovement>();   //获取坦克移动组件
        tankMovement.m_PlayerNumber = playerNumber;
    }
}