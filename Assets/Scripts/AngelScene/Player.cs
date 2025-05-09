using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator = null; // �ִϸ����Ϳ� ������ٵ� ���� ����
    Rigidbody2D _rigidbody = null; // �÷��̾��� ������ٵ� (���� ���� ����)

    public float flapForce = 6f; // ���� ���� (�÷�)
    public float forwardSpeed = 3f; // ������ ������ �ӵ� (���� �̵�)
    public bool isDead = false; // �÷��̾ �׾����� Ȯ���ϴ� ����
    float deathCooldown = 0f; // ��� �� ������� ���� ��� �ð�

    bool isFlap = false; // ����(�÷�) ���� Ȯ���ϴ� ����

    public bool godMode = false; // �� ��� ���� (�浹 ����)

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        // �ִϸ����Ϳ� ������ٵ� ������Ʈ���� ã��
        animator = transform.GetComponentInChildren<Animator>();
        _rigidbody = transform.GetComponent<Rigidbody2D>();

        // �ִϸ����Ϳ� ������ٵ� ������ ���� ���
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
            // �׾��� �� ��� �ð�(`deathCooldown`)�� ������ ������� ���� �Է� �ޱ�
            if (deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame(); // ���� �����
                }
            }
            else
            {
                // ��� �ð� ����
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            // ���� (�÷�) �Է� ó��
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true; // ���� ����
            }
        }
    }

    // ���� ������Ʈ (������ �ð� �������� ȣ���)
    public void FixedUpdate()
    {
        if (isDead) // �׾����� ���� ���� ���� ����
            return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed; // ���� �ӵ��� �����ϰ� ���� (������ ��� �̵�)

        if (isFlap)
        {
            velocity.y += flapForce; // ���� ȿ�� (���� �ӵ� ����)
            isFlap = false; // ���� �Ϸ� �� �ʱ�ȭ
        }

        // ������ٵ� �ӵ� ������Ʈ
        _rigidbody.velocity = velocity;

        // ���� �� ���� ���� (���Ʒ��� ����)
        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        float lerpAngle = Mathf.Lerp(transform.rotation.eulerAngles.z, angle, Time.fixedDeltaTime * 5f);
        transform.rotation = Quaternion.Euler(0, 0, lerpAngle);
    }

    // �浹 ó��
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) // �� ����� ��� �浹 ����
            return;

        if (isDead) // �̹� �׾����� �浹 ó�� �� ��
            return;

        // ���� �ִϸ��̼� ����
        animator.SetInteger("IsDie", 1);
        isDead = true; // ���� ���·� ����
        deathCooldown = 1f; // ����� ��� �ð� 1�� ����

        gameManager.GameOver(); 
    }
}