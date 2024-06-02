using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeController : MonoBehaviour
{
   
    private Skul _playerTransform;
    private float _speed_1 = 1.0f;
    private float _speed_2 = 0.5f;
    
    private void Start()
    {

        _playerTransform = GameManager.Instance.Player.GetComponent<Skul>();
    }


    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.tag == "1")
        {
            CompareTargetMove(1);
        }
        else if (gameObject.transform.tag == "2")
        {
            CompareTargetMove(2);
        }

    }

    private void CompareTargetMove(int i)
    {
        float speed = 0;

        // i 값에 따라 속도 변수를 선택합니다.
        if (i == 1)
        {
            speed = _speed_1;
        }
        else if (i == 2)
        {
            speed = _speed_2;
        }
        if (_playerTransform.GetVelocity().x > 0)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (_playerTransform.GetVelocity().x < 0)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        //-horizontal 값을 가져와서 넣어주자.
    }
}
