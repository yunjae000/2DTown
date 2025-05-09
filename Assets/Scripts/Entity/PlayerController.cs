using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera camera; // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ�ϱ� ���� ���� ī�޶� ����
    private Rigidbody2D rb;

    // �̵� �ӵ��� ������ ���� (Inspector���� ���� ����)
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        // Rigidbody2D ������Ʈ�� ��������
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    protected override void HandleAction()
    {
        // Ű���� �Է��� ���� �̵� ���� ��� (��/��/��/��)
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D �Ǵ� ��/��
        float vertical = Input.GetAxisRaw("Vertical"); // W/S �Ǵ� ��/��

        // ���� ���� ����ȭ (�밢���� �� �ӵ� ����)
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // ���콺 ��ġ�� ȭ�� ��ǥ �� ���� ��ǥ�� ��ȯ
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        // ���� ��ġ�κ��� ���콺 ��ġ������ ���� ���
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