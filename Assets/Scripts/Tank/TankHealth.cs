using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
/// <summary>
/// 坦克生命值类(已给出部分代码,需补充)
/// </summary>
public class TankHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider; //血条

    [SerializeField] private GameObject tankExplosion; //坦克爆炸特效
    [SerializeField] AudioSource explosionAudio; //音效组件

    private float healthScore = 100; //血量

    private float healAmount = 20f; //加血量


    void Start()
    {
        SetHealthUI(healthScore);
    }

    void SetHealthUI(float score)
    {
        healthSlider.value = score;
    }

    public void Damage(float amount)    //调整坦克的当前健康状况，根据新健康状况更新UI并检查坦克是否已死亡。
    {
        healthScore -= amount;
        if (healthScore <= 0)
        {
            OnDeath();
            healthScore = 0;
        }
        SetHealthUI(healthScore);
    }

    private void OnDeath()
    {
        Debug.Log("This tank is dead!");// 发挥坦克死亡的影响并将其停用。
        GameObject tankExplosionInstance = Instantiate(tankExplosion);  //将爆炸动画实例化
        tankExplosionInstance.transform.position = transform.position;  //将爆炸动画的位置设置为坦克的位置
        ParticleSystem tankExplosionParticleSystem = tankExplosionInstance.GetComponent<ParticleSystem>();  //获取爆炸动画的粒子系统

        tankExplosionParticleSystem.Play(); //播放爆炸动画
        Destroy(tankExplosionInstance, tankExplosionParticleSystem.main.duration);  //销毁爆炸动画

        explosionAudio.Play();  //播放爆炸音效

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)     //检测坦克是否碰到了加血道具
    {
        if (other.gameObject.CompareTag("HealthPowerUp"))
        {
            HealthPowerUp();
            Destroy(other.gameObject);
        }
    }

    private void HealthPowerUp()       //加血
    {
        healthScore += healAmount;
        if (healthScore > 100)
            healthScore = 100;
        SetHealthUI(healthScore);
    }

    public void ResetHealth()    //重置血量
    {
        healthScore = 100;
        SetHealthUI(healthScore);
    }
}