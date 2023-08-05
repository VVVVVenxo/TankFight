using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform cameraTransform; // 相机的Transform组件
    private float shakeDuration = 0.2f; // 抖动的持续时间
    private float shakeAmount = 0.1f; // 抖动的幅度

    private void Start()
    {
        cameraTransform = GetComponent<Transform>(); // 获取相机的Transform组件
    }

    public void ShakeCamera()
    {
        StartCoroutine(DoShake());
    }

    private IEnumerator DoShake()
    {
        Debug.Log("Shake");
        Vector3 originalPosition = cameraTransform.localPosition;

        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            // 随机生成抖动的偏移量
            Vector3 randomOffset = Random.insideUnitSphere * shakeAmount;
            cameraTransform.localPosition = originalPosition + randomOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 抖动结束后将相机位置重置
        cameraTransform.localPosition = originalPosition;
    }
}
