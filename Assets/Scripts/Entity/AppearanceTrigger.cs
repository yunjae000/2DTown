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
            Debug.Log("F�� ���� ������ �ٲ㺸����");
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
            // ���� ���� UI �Ǵ� ��ȣ Ű�� ���� ����
            Debug.Log("���� ����: 1~4�� Ű �� �ϳ��� ��������.");
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
