using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance;

    [SerializeField] private GameObject InteractionTextUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowMessage()
    {
        InteractionTextUI.SetActive(true);
    }

    public void HideMessage()
    {
        InteractionTextUI.SetActive(false);
    }
}

