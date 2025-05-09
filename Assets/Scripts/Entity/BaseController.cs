using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ĳ������ �⺻ ������, ȸ��, �˹� ó���� ����ϴ� ��� Ŭ����
public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // �̵��� ���� ���� ������Ʈ
    
    [SerializeField] private SpriteRenderer characterRenderer; // �¿� ������ ���� ������
    [SerializeField] private Transform weaponPivot; // ���⸦ ȸ����ų ���� ��ġ

    protected Vector2 movementDirection = Vector2.zero; // ���� �̵� ����
    public Vector2 MovementDirection{get{return movementDirection;}}
    
    protected Vector2 lookDirection = Vector2.zero; // ���� �ٶ󺸴� ����
    public Vector2 LookDirection{get{return lookDirection;}}

    private Vector2 knockback = Vector2.zero; // �˹� ����
    private float knockbackDuration = 0.0f; // �˹� ���� �ð�
    
    protected AnimationHandler animationHandler;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void Start()
    {
        
    }
    
    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
    }
    
    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
        if(knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime; // �˹� �ð� ����
        }
    }
    
    protected virtual void HandleAction()
    {
        
    }

    private void Movment(Vector2 direction)
    {
        direction = direction * 5; // �̵� �ӵ�
        
        // �˹� ���̸� �̵� �ӵ� ���� + �˹� ���� ����
        if(knockbackDuration > 0.0f)
        {
            direction *= 0.2f; // �̵� �ӵ� ����
            direction += knockback; // �˹� ���� �߰�


        }
        //�ִϸ��̼� ó��
        _rigidbody.velocity = direction; //_�̰� ����
        animationHandler.Move(direction);

        // ���� ���� �̵�
        _rigidbody.velocity = direction;
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;
        
        // ��������Ʈ �¿� ����
        characterRenderer.flipX = isLeft;
        
        if (weaponPivot != null)
        {
		        // ���� ȸ�� ó��
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
    
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        // ��� ������ �ݴ�� �о
        knockback = -(other.position - transform.position).normalized * power;
    }    
}
