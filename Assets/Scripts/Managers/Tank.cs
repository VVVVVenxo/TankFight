using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Tank //存储坦克信息的类
{
    public Color skinColor; //坦克皮肤颜色
    public Transform spawnPoint; //坦克生成点
    public GameObject tankGameObject; //坦克游戏对象
    public int playerNumber; //玩家编号
    public Camera tankCamera; //坦克对应的摄像机
    private int roundWinnerCount = 0; //回合胜利者

    private void SetTankColor(Color color)
    {
        MeshRenderer[] renderers = tankGameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers) //遍历坦克的所有子物体
        {
            renderer.material.color = color;
        }
    }

    private void SetTankPlayerNumber(int playerNumber) //设置坦克的玩家编号
    {
        TankMovement tankMovement = tankGameObject.GetComponent<TankMovement>(); //获取坦克的移动脚本
        tankMovement.SetPlayerNumber(playerNumber);

        TankShooting tankShooting = tankGameObject.GetComponent<TankShooting>(); //获取坦克的射击脚本
        tankShooting.SetPlayerShootingNumber(playerNumber);

        ThirdPersonCamera thirdPersonCamera = tankCamera.GetComponent<ThirdPersonCamera>(); //获取坦克的摄像机脚本
        thirdPersonCamera.player = tankGameObject.transform;
    }

    public void Setup(GameObject tankGameObject, int number) //设置坦克信息
    {
        this.tankGameObject = tankGameObject; //设置坦克游戏对象
        this.playerNumber = number;

        SetTankColor(this.skinColor); //设置坦克的皮肤颜色
        SetTankPlayerNumber(this.playerNumber); //设置坦克的玩家编号
    }

    public GameObject GetTankGameObject() //获取坦克游戏对象
    {
        return tankGameObject;
    }

    public int GetRoundWinnerCount() //获取回合胜利者
    {
        return roundWinnerCount;
    }

    public void IncreaseRoundWinnerCount() //增加回合胜利者
    {
        roundWinnerCount++;
    }

    public void Reset()
    {
        tankGameObject.SetActive(true);
        tankGameObject.transform.position = spawnPoint.position;
        tankGameObject.transform.rotation = spawnPoint.rotation;
        
        TankHealth tankHealth = this.tankGameObject.GetComponent<TankHealth>();
        tankHealth.ResetHealth();
    }

    public void Enable(bool enable)
    {
        TankShooting tankShooting = this.tankGameObject.GetComponent<TankShooting>();
        tankShooting.enabled = enable;

        TankMovement tankMovement = this.tankGameObject.GetComponent<TankMovement>();
        tankMovement.enabled = enable;
    }
}