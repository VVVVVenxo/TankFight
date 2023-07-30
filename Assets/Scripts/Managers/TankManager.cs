using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 坦克管理类(已给出，无需修改)
/// </summary>
[Serializable] //序列化 具体可查看Unity API中关于序列化
public class TankManager
{
    // 该类用于管理坦克上的各种设置
    // 它与GameManager类一起控制坦克的行为
    // 以及玩家是否能够控制他们的坦克
    // 游戏的不同阶段
    public ArrayList tanks = new ArrayList();
    private int gameRoundCount = 0;

    public TankManager(int gameRoundCount)
    {
        this.gameRoundCount = gameRoundCount;
    }

    public void Addtank(Tank tank)
    {
        tanks.Add(tank);
    }

    public Transform[] GetTanksTransforms()
    {
        Transform[] targets = new Transform[tanks.Count];
        for (int i = 0; i < tanks.Count; i++)
        {
            Tank tank = (Tank)tanks[i];
            targets[i] = tank.GetTankGameObject().transform;
        }
        return targets;
    }

    public bool IsOneTankLeft()
    {
        int numbTankLeft = 0;
        foreach (Tank tank in tanks)
        {
            if (tank.GetTankGameObject().activeSelf)
            {
                numbTankLeft++;
            }
        }
        return numbTankLeft <= 1;
    }

    public Tank GetRoundWinner()
    {
        foreach (Tank tank in tanks)
        {
            if (tank.GetTankGameObject().activeSelf)
            {
                return tank;
            }
        }

        return null;
    }

    public Tank GetGameWinner()
    {
        foreach (Tank tank in tanks)
        {
            if (tank.GetRoundWinnerCount() == gameRoundCount)
            {
                return tank;
            }
        }
        return null;
    }

    public void EnableTanks(bool enable)
    {
        foreach (Tank tank in tanks)
        {
            tank.Enable(enable);
        }
    }

    public void ResetTanks()
    {
        foreach (Tank tank in tanks)
        {
            tank.Reset();
        }
    }
}