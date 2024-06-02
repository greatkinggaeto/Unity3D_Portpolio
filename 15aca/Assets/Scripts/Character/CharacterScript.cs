using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{


    //잔상 
    public Ghost _ghost;
    //캐릭터데이터
    [SerializeField]
    protected int _characterKey;
    //공격 콜라이더 위치&사이즈
    [SerializeField]
    protected Vector2 _attackOffset = new Vector2(0.5f, 0);
    [SerializeField]
    protected Vector2 _attackSize = new Vector2(1, 1);
   
    //리지드바디를 활용하지않은 중력처리
    protected float _gravityScale = 5.0f;

    protected DataManager.CharacterTableData _characterData;


    protected float _dashPower = 10.0f;
    protected bool _isDash = false;
    protected bool _isAttack = false;
    protected bool _isJump = false;
    protected bool _isDead = false;
    protected bool _isAir = true;
    protected int _jumpCount = 0;
    protected Vector2 _velocity;

    protected float _curHp; //* 현재 체력
    protected float _maxHp;//* 최대 체력

    protected int _floorLayer;


    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    protected BoxCollider2D _boxCollider2D;


    protected virtual void Awake()
    {

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        _characterData = DataManager.Instance.GetCharacterData(_characterKey);
       
        _maxHp = _characterData.MaxHp;
        _floorLayer = LayerMask.GetMask("Floor");

        if (_ghost == null)
            return;
    }


    protected virtual void Update()
    {
        Dash();
        CheckBottom();
    }
    protected virtual void FixedUpdate()
    {
        Jump();
        Move();
    }

    protected void Attack()
    {
        _animator.SetTrigger("Attack");

        if(gameObject.transform.tag =="Player")
        {
            if (_velocity.x < 0.0f)
            {
                print("left");
                transform.Translate(Vector2.left * 0.5f);
            }
            else if (_velocity.x > 0.0f)
            {
                print("right");
                transform.Translate(Vector2.right * 0.5f);
            }
        }

    }


    protected void Dash()
    {
        if (_isDash)
        {
            if (_spriteRenderer.flipX)
                //좌
                transform.Translate(Vector2.left * _dashPower * Time.deltaTime);
            else
                //우
                transform.Translate(Vector2.right * _dashPower * Time.deltaTime);
        }
    }

    protected void DashEnd()
    {
        _isDash = false;
        _ghost._isOnGhost = false;
    }

    protected void DashStart()
    {
        _animator.SetTrigger("Dash");
        _ghost._isOnGhost= true;
        _isDash = true;
    }

    protected void JumpStart()
    {
        _velocity.y = _characterData.JumpPower * 0.9f;
        _jumpCount++;
        _isJump = true;
    }

    protected void Jump()
    {
        if (_isAttack) return;

        //중력처리 
        _animator.SetFloat("Velocity", _velocity.y);
        _velocity.y -= _gravityScale * Time.fixedDeltaTime;
    }


    protected void Move()
    {
        if (_isDash) return;
        if (_isAttack) return;
        if (_isDead) return;

        _animator.SetFloat("Speed", Mathf.Abs(_velocity.x) * _characterData.Speed);

        if(_velocity.x==float.NaN) return;
        //왼
        if (_velocity.x < 0.0f)
        {
            _spriteRenderer.flipX = true;
        }
        //오
        else if (_velocity.x > 0.0f)
        {
            _spriteRenderer.flipX = false;
        }
        //Vector3 moveVector =;
        transform.Translate(_velocity * _characterData.Speed * Time.fixedDeltaTime);
    }

    public void Land(Collider2D collision)
    {
        if (_velocity.y > 0.0f) return;
        if (collision.bounds.min.y > _boxCollider2D.bounds.min.y + 0.1f) return;

        //_animator.SetBool("IsAir", false);
        _jumpCount = 0;
        _velocity.y = 0.0f;
        _isJump = false;

        //BoxCollider2D tileCollider = collision.GetComponent<BoxCollider2D>();             
        Vector2 pos = transform.position;
        pos.y = collision.bounds.max.y + _boxCollider2D.size.y * 0.5f - _boxCollider2D.offset.y;
        transform.position = pos;
    }

    //안쓰는코드
   
    public void Hurt(float damage)
    {
        _curHp -= damage;

        _animator.SetTrigger("Hurt");

        print("HP : " + _curHp);

    }
    protected void SetHp(float amount)
    {
        _maxHp = amount;
        _curHp = _maxHp;
    }
    
    private void CheckBottom()
    {
        Vector2 pos = new Vector2(transform.position.x,
            _boxCollider2D.bounds.min.y);
        Vector2 size = new Vector2(_boxCollider2D.size.x,
            0.5f);

        Collider2D hitBox = Physics2D.OverlapBox(pos, size, 0.0f, _floorLayer);

        if(hitBox != null)
        {
            _isAir = false;
        }
        else
        {
            _isAir = true;
        }
        

        _animator.SetBool("IsAir", _isAir);
    }
    protected void OnDrawGizmos()
    {
        // 바텀콜라이더 기즈모 형셩
        if (_boxCollider2D == null) return;

        Gizmos.color = Color.green;
        Vector2 pos = new Vector2(transform.position.x,
            _boxCollider2D.bounds.min.y);
        Vector2 size = new Vector2(_boxCollider2D.size.x,
            0.1f);

        Gizmos.DrawCube(pos, size);

        // 공격콜라이더 기즈모 형셩
        if (!_isAttack) return;

        Gizmos.color = Color.red;
        pos = _boxCollider2D.bounds.center;
        if (_spriteRenderer.flipX)
            pos -= _attackOffset;
        else
            pos += _attackOffset;

        Gizmos.DrawCube(pos, _attackSize);
    }

    virtual protected void PushWall(Collider2D collision)
    {
        Vector2 pos = transform.position;

        Vector2 direction = transform.position - collision.bounds.center;

        if (direction.x > 0.0f)
            pos.x = collision.bounds.max.x + _boxCollider2D.size.x * 0.5f - _boxCollider2D.offset.x;
        else
            pos.x = collision.bounds.min.x - _boxCollider2D.size.x * 0.5f - _boxCollider2D.offset.x;

        transform.position = pos;
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Tile")
        {
            Land(collision);
        }
        else if (collision.tag == "Wall")
        {
            PushWall(collision);
        }
    }
    protected void SendDamage()
    {
        Vector2 pos = _boxCollider2D.bounds.center;
        if (_spriteRenderer.flipX)
            pos -= _attackOffset;
        else
            pos += _attackOffset;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos, _attackSize, 0.0f);

        if(gameObject.CompareTag("Player"))
        {
            foreach (Collider2D collider in colliders)
            {
                Enemy monster = collider.GetComponent<Enemy>();

                if (monster == null) continue;

                monster.Hurt(_characterData.Power);
                monster.HitControl();
                Debug.Log("Hit");
            }
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            foreach (Collider2D collider in colliders)
            {
                Skul player = collider.GetComponent<Skul>();

                if (player == null) continue;

                player.Hurt(_characterData.Power);
            }
        }
    }
   
   

    public Vector2 GetVelocity() { return _velocity; }

}
