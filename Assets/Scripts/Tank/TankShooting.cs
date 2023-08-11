using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 坦克攻击类(已给出部分代码,需补充)
/// </summary>
///
public enum BulletType //子弹类型，添加新的子弹类型应在此处添加。
{
    Regular, //普通子弹
    PowerUp, //高伤害子弹
}

public class TankShooting : MonoBehaviour
{
    private BulletType currentBulletType = BulletType.Regular; //当前子弹类型

    // private float regularDamage = 10f;   //普通子弹伤害
    // private float powerUpDamage = 30f;   //高伤害子弹伤害

    [SerializeField] GameObject regularShellPrefab; //普通子弹预制体
    [SerializeField] GameObject powerUpShellPrefab; //高伤害子弹预制体
    [SerializeField] GameObject currentShellPrefab; //当前子弹预制体

    [SerializeField] private Transform fireTransform; //子弹发射位置
    [SerializeField] public float m_MaxLaunchForce = 20f; //最大发射力
    [SerializeField] public float m_MinLaunchForce = 10f; //最小发射力
    private float fireForce = 0; //发射力
    [SerializeField] public int m_PlayerNumber = 1; //区分玩家
    [SerializeField] private float fireLoadingTime = 0.3f; //发射间隔时间
    [SerializeField] private Slider m_AimSlider; //蓄力条

    [SerializeField] AudioSource audioSource; //音效组件
    [SerializeField] AudioClip fireClip; //发射音效
    [SerializeField] AudioClip loadPowerClip; //蓄力音效

    private int m_PowerUpleft = 0; //剩余多少个高伤害子弹

    private void Start()
    {
        currentShellPrefab = regularShellPrefab; // 初始时使用普通子弹预制体
    }


    private void Update() //侦测用户是否点击fire键
    {
        if (Input.GetButtonDown("Fire" + m_PlayerNumber))
        {
            fireForce = m_MinLaunchForce;
        }
        else if (Input.GetButton("Fire" + m_PlayerNumber))
        {
            if (audioSource.clip != loadPowerClip || !audioSource.isPlaying) //如果音效不是蓄力音效或者音效没有播放
            {
                audioSource.clip = loadPowerClip; //设置音效为蓄力音效
                audioSource.Play(); //播放音效
            }

            fireForce += Time.deltaTime * (m_MaxLaunchForce - m_MinLaunchForce) / fireLoadingTime; //根据力度增加发射力
            if (fireForce >= m_MaxLaunchForce)
            {
                Fire();
                fireForce = m_MinLaunchForce;
            }
        }
        else if (Input.GetButtonUp("Fire" + m_PlayerNumber))
        {
            Fire();
            fireForce = m_MinLaunchForce;
        }

        //跟踪发射按钮的当前状态，并根据当前发射力量做出决定。
        UpdateSlider();
    }

    private void UpdateSlider() //更新蓄力条
    {
        float sliderValue = (fireForce - m_MinLaunchForce) / (m_MaxLaunchForce - m_MinLaunchForce);
        m_AimSlider.value = sliderValue;
    }


    private void Fire() //动态创建一个游戏对象，实例化并启动子弹。
    {
        if (m_PowerUpleft > 0)
        {
            ChangeBulletType(BulletType.PowerUp);
            m_PowerUpleft--;
        }
        else
        {
            ChangeBulletType(BulletType.Regular);
        }

        GameObject Shell =
            Instantiate(currentShellPrefab, fireTransform.position, fireTransform.rotation); //发射的预制体，发射的位置，发射的旋转
        Rigidbody rb = Shell.GetComponent<Rigidbody>(); //获取子弹的刚体组件
        rb.velocity = fireTransform.forward * fireForce; //获取子弹的刚体组件，设置子弹的速度
        audioSource.clip = fireClip; //设置音效
        audioSource.Play(); //播放音效
    }

    public void SetPlayerShootingNumber(int playerNumber) //设置玩家编号
    {
        m_PlayerNumber = playerNumber;
    }

    private TankHealth GetOtherTankHealth() //获取另一个坦克的血量
    {
        TankHealth[] tankHealths = FindObjectsOfType<TankHealth>();
        foreach (TankHealth tankHealth in tankHealths)
        {
            if (tankHealth.gameObject != gameObject)
            {
                return tankHealth;
            }
        }

        return null;
    }

    public void ChangeBulletType(BulletType bulletType)
    {
        currentBulletType = bulletType;
        switch (currentBulletType)
        {
            case BulletType.PowerUp:
                currentShellPrefab = powerUpShellPrefab;
                break;
            case BulletType.Regular:
                currentShellPrefab = regularShellPrefab;
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUpPickUp"))
        {
            Debug.Log("吃到了高伤害子弹");
            if (m_PowerUpleft < 3) // 只在数量小于 3 时增加
            {
                m_PowerUpleft += 1; // 每次增加一发
            }

            Destroy(other.gameObject);
        }
    }
}