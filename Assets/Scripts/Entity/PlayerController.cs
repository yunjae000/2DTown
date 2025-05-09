using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera camera; // 마우스 위치를 월드 좌표로 변환하기 위한 메인 카메라 참조
    private Rigidbody2D rb;

    // 이동 속도를 조절할 변수 (Inspector에서 조정 가능)
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        // Rigidbody2D 컴포넌트를 가져오기
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    protected override void HandleAction()
    {
        // 키보드 입력을 통해 이동 방향 계산 (좌/우/상/하)
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D 또는 ←/→
        float vertical = Input.GetAxisRaw("Vertical"); // W/S 또는 ↑/↓

        // 방향 벡터 정규화 (대각선일 때 속도 보정)
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // 마우스 위치를 화면 좌표 → 월드 좌표로 변환
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        // 현재 위치로부터 마우스 위치까지의 방향 계산
        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }

    public void FixedUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input = input.normalized;

        rb.MovePosition(rb.position + input * moveSpeed * Time.fixedDeltaTime);
    }
}