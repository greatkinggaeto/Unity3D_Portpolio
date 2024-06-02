using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigdrasilController : MonoBehaviour
{
    public enum BossState
    {
        Idle,
        Pattern_1,
        Pattern_2,
        Pattern_3
    }
    private WaitForSeconds _setStateInterval;
    public BossState _state = BossState.Idle;

    private GameObject _head;
    private GameObject _leftHand;
    private GameObject _righthand;

    private void Awake()
    {
       
        _setStateInterval = new WaitForSeconds(7.0f);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private IEnumerator BossAI()
    {
      
        while (true)
        {


            
            yield return _setStateInterval;
        }
    }

    public BossState GetState() { return _state; }


}
