using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f; // 调整旋转速度以适应你的需求

    // 每帧调用一次Update方法
    void Update()
    {
        // 绕着Y轴平滑旋转游戏物体
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
