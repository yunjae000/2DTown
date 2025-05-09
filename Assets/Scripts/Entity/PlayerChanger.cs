using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearanceChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer; // �÷��̾��� ��������Ʈ ������
    [SerializeField] private Sprite[] appearances; // ���� �ĺ� 4�� (0~3)

    // �ܺο��� ȣ�� �� �� �Լ��� ������ �ٲ�
    public void ChangeAppearance(int index)
    {
        if (index >= 0 && index < appearances.Length)
        {
            spriteRenderer.sprite = appearances[index];
        }
        else
        {
            Debug.LogWarning("�߸��� ���� �ε����Դϴ�.");
        }
    }
}

