using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skul : CharacterScript
{



    private bool _isCombo = false;
    private bool _isComboAttack = false;
    private float _dashInputTime = 0.0f;
    private GameObject _playerHpBar;
    protected Image _playerHpBar_front;

    // private Slider _hpSlider;

    // Jump했을때 y값을 기준으로 밑에있는애들 trigger켜주고 위에있으면 꺼주기

    private void Awake()
    {
        base.Awake();
        _curHp = _maxHp;
    }
    private void Start()
    {
        _playerHpBar = GameObject.Find("Player_HpBar_Back");
        _playerHpBar_front = _playerHpBar.transform.GetChild(0).GetComponent<Image>();
    }

    protected override  void  Update()
    {

        MoveControl();


        JumpControl();
        DashControl();
        AttackControl();
        HpControl();

        base.Update();

    }

    private void HpControl()
    {
        _playerHpBar_front.fillAmount = (float)_curHp / (float)_maxHp;
    }

    private void MoveControl()
    {
        _velocity.x = Input.GetAxis("Horizontal");

    }
    private void JumpControl()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < 2)
        {
            JumpStart();
        }
        
    }

    //이코드는 두번 눌렀을때 입력되는코드이다 즉 어택에 적용시켜서
    //일정시간안에 중복되었을때를 체크하면되는거다

    private void DashControl()
    {
        if (Input.GetKeyDown(KeyCode.C) &&_isDash==false)
        {
                DashStart();
        }
    }
    private void AttackControl()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            _velocity.x = 0;

            if (_isAttack)
            {
                if(_isCombo)
                {
                    _isComboAttack = true;
                }                
            }
            else
            {
                _isAttack = true;
                _animator.SetBool("IsAttack", true);
               
                Attack();
            }
        }
    }

    private void StartAttackCombo()
    {
        _isCombo= true;
        _isComboAttack = false;
    }
    private void EndAttackCombo()
    {
        _isCombo = false;
        //_animator.SetBool("IsAttackCombo", false);
    }

    private void EndAttack()
    {
        _animator.SetBool("IsAttack", false);
        _isAttack = false;
    }

    private void ComboAttack()
    {
        if(_isComboAttack)
        {
            if (_spriteRenderer.flipX)
            {
                transform.Translate(Vector2.left * 0.5f);
            }
            else if (!_spriteRenderer.flipX)
            {
                transform.Translate(Vector2.right * 0.5f);
            }

            _animator.SetTrigger("ComboAttack");
        }
    }

    private void JumpAttackEnd()
    {
        _velocity.y = 0.0f;
        _isAttack = false;

    }


    //특정위치 도달했을때 몬스터 소환
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpawnTrigger")
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.MonsterSpawn();
        }
        else if (collision.gameObject.tag == "BossSpawnTrigger")
        {
            Debug.Log("보스소환");
            collision.gameObject.SetActive(false);
            Vector2 pos = collision.transform.position;
            EnemyManager.Instance.BossSpawn(pos);
        }

    }



}
