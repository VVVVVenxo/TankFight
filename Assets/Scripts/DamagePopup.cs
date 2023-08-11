using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePopup : MonoBehaviour
{
    public Text damageText;
    public float moveSpeed = 1f;
    public float duration = 1.5f;
    public Color textColor = Color.red;

    private float elapsedTime = 0f;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // Move the popup upwards over time
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // Fade out the text color over time
        Color newColor = damageText.color;
        newColor.a = Mathf.Lerp(1f, 0f, elapsedTime / duration);
        damageText.color = newColor;

        // Destroy the popup when the duration is reached
        if (elapsedTime >= duration)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(int damageValue)
    {
        damageText.text = "-" + damageValue.ToString();
        damageText.color = textColor;
    }
}