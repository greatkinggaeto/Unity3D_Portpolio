using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update

    //킬카운트== 스폰숫자일때 문이 열린다.
    private Animator _animator;
    private BoxCollider2D _collider;
    private bool _isOpen = false;
    private bool _isInRange = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        OpenDoorTrigger();
        ChangeScene();
    }

    private void OpenDoorTrigger()
    {
        if (GameManager.Instance.GetkillCount() == GameManager.Instance.GetSpawnEnemyCount())
        {
            if(!_isOpen)
            {
                _animator.SetTrigger("OpenTrigger");
                _isOpen = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _isInRange = true;
            Debug.Log("닫힌문 Player 충돌");
            if (_isOpen==true)
            {
                Debug.Log("열린문 Player 충돌");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isInRange = false;
            Debug.Log("Player left the trigger area of the door");
        }
    }

    private void ChangeScene()
    {
        if (_isOpen && _isInRange)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene("BossStage");
            }
        }
    }

}
