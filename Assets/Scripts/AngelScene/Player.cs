using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator = null; // 애니메이터와 리지드바디 변수 선언
    Rigidbody2D _rigidbody = null; // 플레이어의 리지드바디 (물리 엔진 적용)

    public float flapForce = 6f; // 점프 강도 (플랩)
    public float forwardSpeed = 3f; // 앞으로 나가는 속도 (수평 이동)
    public bool isDead = false; // 플레이어가 죽었는지 확인하는 변수
    float deathCooldown = 0f; // 사망 후 재시작을 위한 대기 시간

    bool isFlap = false; // 점프(플랩) 여부 확인하는 변수

    public bool godMode = false; // 신 모드 여부 (충돌 무시)

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        // 애니메이터와 리지드바디를 컴포넌트에서 찾기
        animator = transform.GetComponentInChildren<Animator>();
        _rigidbody = transform.GetComponent<Rigidbody2D>();

        // 애니메이터와 리지드바디가 없으면 에러 출력
        if (animator == null)
        {
            Debug.LogError("Not Founded Animator");
        }

        if (_rigidbody == null)
        {
            Debug.LogError("Not Founded Rigidbody");
        }
    }

    void Update()
    {
        if (isDead)
        {
            // 죽었을 때 대기 시간(`deathCooldown`)이 끝나면 재시작을 위한 입력 받기
            if (deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame(); // 게임 재시작
                }
            }
            else
            {
                // 대기 시간 감소
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            // 점프 (플랩) 입력 처리
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true; // 점프 시작
            }
        }
    }

    // 물리 업데이트 (고정된 시간 간격으로 호출됨)
    public void FixedUpdate()
    {
        if (isDead) // 죽었으면 물리 연산 하지 않음
            return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed; // 수평 속도는 일정하게 유지 (앞으로 계속 이동)

        if (isFlap)
        {
            velocity.y += flapForce; // 점프 효과 (수직 속도 증가)
            isFlap = false; // 점프 완료 후 초기화
        }

        // 리지드바디 속도 업데이트
        _rigidbody.velocity = velocity;

        // 점프 시 각도 조정 (위아래로 기울기)
        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        float lerpAngle = Mathf.Lerp(transform.rotation.eulerAngles.z, angle, Time.fixedDeltaTime * 5f);
        transform.rotation = Quaternion.Euler(0, 0, lerpAngle);
    }

    // 충돌 처리
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) // 신 모드일 경우 충돌 무시
            return;

        if (isDead) // 이미 죽었으면 충돌 처리 안 함
            return;

        // 죽음 애니메이션 실행
        animator.SetInteger("IsDie", 1);
        isDead = true; // 죽음 상태로 변경
        deathCooldown = 1f; // 재시작 대기 시간 1초 설정

        gameManager.GameOver(); 
    }
}