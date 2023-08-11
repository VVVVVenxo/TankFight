using System;
using System.Collections;
using UnityEngine;

public class BombingArea : MonoBehaviour
{
    public float bombDisplayDuration = 3f;      // 造成伤害的间隔时间
    public int damageValue = 50;        //造成的伤害
    
    [SerializeField]
    AudioSource explosionAudio; //爆炸音效
    
    [SerializeField]
    private GameObject Explosion; //爆炸特效
    
    private bool isPlayerInCollider = false; //是否在碰撞体内
    private GameObject player=null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInCollider = true;
            player=other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInCollider = false;
            player = null;
        }
    }

    private void Damage()
    {
        TankHealth playerHealth = player.GetComponent<TankHealth>();
        if (playerHealth != null)
        {
            playerHealth.Damage(damageValue);
        }
    }
    private void Start()
    {
        StartCoroutine(DisplayBombingEffect());
    }

    private IEnumerator DisplayBombingEffect()
    {
        yield return new WaitForSeconds(3f);

        Debug.Log("正在轰炸");
        if (isPlayerInCollider)
        {
            Debug.Log("造成伤害");
            Damage();
        }
        
        ParticleSystem explosionParticleSystem = Explosion.GetComponent<ParticleSystem>();
        explosionParticleSystem.Play();
        
        Explosion.transform.parent = null;
        Destroy(Explosion, explosionParticleSystem.main.duration);
        
        explosionAudio.Play();
        
        yield return new WaitForSeconds(explosionParticleSystem.main.duration);
        Destroy(gameObject);
    }
    
}