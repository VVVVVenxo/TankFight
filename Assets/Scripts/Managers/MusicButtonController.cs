using UnityEngine;
using UnityEngine.UI;

public class MusicButtonController : MonoBehaviour
{
    public AudioSource musicSource;
    public Image buttonImage;
    public Sprite playSprite;
    public Sprite pauseSprite;

    private bool isPlaying = true;

    private void Start()
    {
        // 设置按钮的默认状态为播放状态
        buttonImage.sprite = pauseSprite;
    }

    public void ToggleMusic()
    {
        if (isPlaying)
        {
            // 停止音乐播放
            musicSource.Stop();
            buttonImage.sprite = playSprite;
        }
        else
        {
            // 播放音乐
            musicSource.Play();
            buttonImage.sprite = pauseSprite;
        }

        isPlaying = !isPlaying;
    }
}
