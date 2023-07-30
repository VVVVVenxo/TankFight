using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // public Transform cameraTransform;
    [HideInInspector] public Transform player; // 当前激活的目标
    public int m_playerNumber;

    public float distance = 5f; // 相机相对于目标的水平参数
    public float height = 2f; // 相机相对于目标的竖直参数
    public float smoothSpeed = 10f; // 相机移动的平滑速度
    public Vector3 offset = new Vector3(0f, 1f, 0.5f); // 相机与目标之间的偏移量
    public float radius = 0.5f; // 球形射线的半径
    public float maxDistance = 10f; // 球形射线的最大距离
    public float minDistance = 1f; // 相机与目标的最小距离

    private void LateUpdate()
    {
        // 计算相机的目标位置
        Vector3 targetPosition = player.position + player.up * (height + offset.y) - player.forward * distance +
                                 player.right * offset.z;

        // 检测相机与目标之间的遮挡物
        RaycastHit hit;
        if (Physics.SphereCast(player.position, radius, targetPosition - player.position, out hit, maxDistance))
        {
            // 如果有遮挡物，将相机位置拉近遮挡物表面
            float adjustedDistance = Mathf.Clamp(hit.distance - minDistance, 0f, maxDistance);
            targetPosition = player.position + player.up * (height + offset.y) - player.forward * adjustedDistance +
                             player.right * offset.z;
        }

        // 使用平滑的插值移动相机
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // 相机朝向目标
        transform.LookAt(player.position + player.up * offset.y);
    }
}