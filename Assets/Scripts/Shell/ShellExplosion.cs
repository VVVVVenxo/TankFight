using UnityEngine;
/// <summary>
/// 子弹逻辑(已给出定义,需补充)
/// </summary>
public class ShellExplosion : MonoBehaviour
{
    [SerializeField]
    private float m_MaxLifeTime = 2f;          //删除子弹之前的时间(以秒为单位)。
    [SerializeField]
    private GameObject shellExplosion;       //子弹爆炸特效
    [SerializeField]
    private float explosionRadius = 1f;          //子弹爆炸半径。
    [SerializeField]
    private LayerMask tankMask;

    [SerializeField] AudioSource explosionAudio;     //爆炸音效

    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime); // 子弹达到最大时长销毁自身
    }


    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankMask);  //生成一个球体，球心为子弹的位置
        foreach (Collider c in colliders)       //遍历球体内的所有物体
        {
            TankHealth tankHealth = c.gameObject.GetComponent<TankHealth>();
            tankHealth.Damage(10f);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            foreach (Collider c in colliders)       //遍历球体内的所有物体
            {
                TankHealth tankHealth = c.gameObject.GetComponent<TankHealth>();
                tankHealth.Damage(30f);
                Destroy(other.gameObject);
            }
        }


        ParticleSystem shellParticleSystem = shellExplosion.GetComponent<ParticleSystem>();
        shellParticleSystem.Play();

        shellExplosion.transform.parent = null;
        Destroy(shellExplosion, shellParticleSystem.main.duration);

        explosionAudio.Play();
        Destroy(gameObject); // 子弹碰撞到物体销毁自身
    }

    private float CalculateDamage(Vector3 targetPosition)       //根据目标的位置计算目标返回应该受到的伤害
    {
        return 0f;
    }
}