using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private void Start()
    {
         Button button= GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnStartButtonClicked);
        }
    }

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveListener(OnStartButtonClicked);
        }
    }

    private void OnStartButtonClicked()
    {
        GameManager.Instance.StartGame();
    }
}
