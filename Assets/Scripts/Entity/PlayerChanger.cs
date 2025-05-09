using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearanceChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer; // 플레이어의 스프라이트 렌더러
    [SerializeField] private Sprite[] appearances; // 외형 후보 4개 (0~3)

    // 외부에서 호출 시 이 함수로 외형을 바꿈
    public void ChangeAppearance(int index)
    {
        if (index >= 0 && index < appearances.Length)
        {
            spriteRenderer.sprite = appearances[index];
        }
        else
        {
            Debug.LogWarning("잘못된 외형 인덱스입니다.");
        }
    }
}

