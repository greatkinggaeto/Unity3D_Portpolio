using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private float _changePatternInterVal = 10.0f;
    private float _shakeSpeed = 0.5f;


    private WaitForSeconds _setStateInterval;
    private WaitForSeconds _changeUpAndDownInterVal;

    protected bool _isUp = false;
    protected WigdrasilController _parent;
    protected WigdrasilController.BossState _state;
    protected Vector2 _velocity;



    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;


    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _parent = transform.GetComponentInParent<WigdrasilController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _setStateInterval = new WaitForSeconds(0.1f);
        _changeUpAndDownInterVal = new WaitForSeconds(0.5f);
    }

    protected virtual void Start()
    {
        StartCoroutine(ActionBossAI());
        StartCoroutine(SetUpAndDownState());
    }

    // switch문을 쓰자 //Ienumlator 써서 5초마다 한번씩 진행 코루틴으로 만들기
    //
    protected virtual void Update()
    {
        _state = _parent.GetComponent<WigdrasilController>().GetState();


    }


    protected IEnumerator ActionBossAI()
    {
        while (true)
        {
            switch (_state)
            {
                case WigdrasilController.BossState.Idle:
                    {
                        IdleControl();
                    }
                    break;
                case WigdrasilController.BossState.Pattern_1:
                    {
                        Pattern_1Control();
                    }
                    break;
                case WigdrasilController.BossState.Pattern_2:
                    {
                        Pattern_2Control();
                    }
                    break;
                case WigdrasilController.BossState.Pattern_3:
                    {
                        Pattern_3Control();
                    }
                    break;

            }
            yield return null;
        }
    }
    private IEnumerator SetUpAndDownState()
    {
        while (true)
        {
            _isUp = !_isUp;
            yield return _changeUpAndDownInterVal;
        }
    }

    protected virtual void IdleControl()
    {
        _velocity.x = 0;
        _velocity.y = _isUp ? 1.0f : -1.0f;

        transform.Translate(_velocity* _shakeSpeed * Time.deltaTime);
    }

    protected virtual void Pattern_1Control()
    {

    }
    protected virtual void Pattern_2Control()
    {

    }
    protected virtual void Pattern_3Control()
    {

    }


}
