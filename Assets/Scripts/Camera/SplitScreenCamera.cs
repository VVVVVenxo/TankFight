using UnityEngine;

public class SplitScreenCamera : MonoBehaviour
{
    public Transform PlayerNumber1; // 玩家1的Transform组件
    public Transform PlayerNumber2; // 玩家2的Transform组件

    public Camera leftCamera; // 左边相机
    public Camera rightCamera; // 右边相机

    private Vector3 offset1; // 玩家1相机的偏移量
    private Vector3 offset2; // 玩家2相机的偏移量

    void Start()
    {
        // 计算相机与玩家之间的初始偏移量
        offset1 = leftCamera.transform.position - PlayerNumber1.position;
        offset2 = rightCamera.transform.position - PlayerNumber2.position;
    }

    void LateUpdate()
    {
        // 更新左边相机的位置
        leftCamera.transform.position = PlayerNumber1.position + offset1;

        // 更新右边相机的位置
        rightCamera.transform.position = PlayerNumber2.position + offset2;
    }
}
