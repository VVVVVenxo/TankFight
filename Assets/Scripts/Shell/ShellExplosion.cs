using UnityEngine;

/// <summary>
/// 子弹逻辑(已给出定义,需补充)
/// </summary>
public class ShellExplosion : MonoBehaviour
{
    [SerializeField]
    private float m_MaxLifeTime = 2f; //删除子弹之前的时间(以秒为单位)。

    [SerializeField]
    private GameObject shellExplosion; //子弹爆炸特效

    [SerializeField]
    private float explosionRadius = 1f; //子弹爆炸半径。

    [SerializeField]
    private LayerMask tankMask;

    [SerializeField]
    AudioSource explosionAudio; //爆炸音效

    // public enum ShellType
    // {
    //     Normal,
    //     PowerUp
    // }
    // [SerializeField]
    // private ShellType shellType = ShellType.Normal;
    //
    [SerializeField]
    private int damageValue;

    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime); // 子弹达到最大时长销毁自身
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankMask); //生成一个球体，球心为子弹的位置
        foreach (Collider c in colliders)
        {
            TankHealth tankHealth = c.gameObject.GetComponent<TankHealth>();
            tankHealth.Damage(damageValue);
        }

        ParticleSystem shellParticleSystem = shellExplosion.GetComponent<ParticleSystem>();
        shellParticleSystem.Play();

        shellExplosion.transform.parent = null;
        Destroy(shellExplosion, shellParticleSystem.main.duration);

        explosionAudio.Play();
        TriggerScreenShake(); // 在这里调用触发屏幕抖动的方法
        Destroy(gameObject); // 子弹碰撞到物体销毁自身
    }

    private float CalculateDamage(Vector3 targetPosition) //根据目标的位置计算目标返回应该受到的伤害
    {
        return 0f;
    }

    public void TriggerScreenShake()
    {
        Debug.Log("TriggerScreenShake");

        CameraShake leftCameraShake = GameObject
            .FindWithTag("MainCamera1")
            .GetComponentInChildren<CameraShake>(true); // 获取左相机上的 CameraShake 组件
        CameraShake rightCameraShake = GameObject
            .FindWithTag("MainCamera2")
            .GetComponentInChildren<CameraShake>(true); // 获取右相机上的 CameraShake 组件

        // 分别触发左右相机的抖动效果
        if (leftCameraShake != null)
        {
            leftCameraShake.ShakeCamera();
        }

        if (rightCameraShake != null)
        {
            rightCameraShake.ShakeCamera();
        }
    }
}
