using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceTrigger : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private PlayerAppearanceChanger playerAppearanceChanger;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerAppearanceChanger = other.GetComponent<PlayerAppearanceChanger>();
            Debug.Log("F를 눌러 외형을 바꿔보세요");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            playerAppearanceChanger = null;
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            // 외형 선택 UI 또는 번호 키로 선택 가능
            Debug.Log("외형 선택: 1~4번 키 중 하나를 누르세요.");
        }

        if (isPlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                playerAppearanceChanger?.ChangeAppearance(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                playerAppearanceChanger?.ChangeAppearance(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                playerAppearanceChanger?.ChangeAppearance(2);
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                playerAppearanceChanger?.ChangeAppearance(3);
        }
    }
}
