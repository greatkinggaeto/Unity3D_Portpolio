using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class BgController : MonoBehaviour
{
    [SerializeField]
    private float _bgSpeed = 1.0f;

    [SerializeField]
    private float _bgPosX = -56.0f;

    private Camera _mainCamera;
    private float _backgroundWidth;

    private void Awake()
    {
        _mainCamera = Camera.main;

        // 배경의 너비를 계산합니다.
        //_backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;

    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * _bgSpeed * Time.deltaTime);
        // 여기서  _mainCamera.orthographicSize * _mainCamera.aspect) 는 카메라의 가로사이즈를 말한다.
        if (transform.position.x < _bgPosX)
        {

            // 배경을 오른쪽으로 배경 너비의 세 배만큼 이동
            transform.transform.localPosition = Vector3.zero;
          // transform.position = new Vector3(transform.position.x + _backgroundWidth * 3, transform.position.y, transform.position.z);
        }


    }
}
