using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum StageType
    {
        NormalStage,
        BossStage
    }

    [SerializeField]
    private StageType _currentStageType;

    //외부에서 정보 변경
    private int _enemyPoolSize = 5;
    [SerializeField]
    private CinemachineVirtualCamera _playerVirtualCamera;

    public static GameManager Instance;

    private GameObject _player;
    public GameObject Player
    {
        get { return _player; }
    }

    // hpPanel정보
    [SerializeField]
    private Transform _hpBarPanel;

    public Transform HpBarPanel
    {
        get { return _hpBarPanel; }
    }

    // private int _Stage = 1;

    private bool _isTriggerOneTime = false;

    // 스폰위치 잡아주기
    private Vector2 _spawnPos_1_Left = new Vector2(-9.5f,-3.0f);
    private Vector2 _spawnPos_1_Right = new Vector2(5.5f, -3.0f);
    private Vector2 _spawnPos_2_Left = new Vector2(-4.5f, 0.3f);
    private Vector2 _spawnPos_2_Right = new Vector2(0.7f, 0.3f);
    private Transform _playerSpawnPos;

    // enemy 카운트확인
    private int _killCount = 0;




    private void Awake()
    {
        Instance = this;
       // _player = GameObject.Find("Player").GetComponent<Skul>();
        DataManager.Instance.LoadData();

        // 플레이어프리팹 넣어둬서 스폰위치 정해두기
        _playerSpawnPos = GameObject.FindGameObjectWithTag("PlayerSpawnPos").transform;
         GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
         _player = Instantiate(playerPrefab, _playerSpawnPos.transform.position, Quaternion.identity);

        if (_currentStageType == StageType.NormalStage)
        {
            //캔버스안에 있는 판넬 불러오기
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                _hpBarPanel = canvas.transform.Find("HpBarPanel").transform;
            }
            //적군미리 생성
            EnemyManager.Instance.CreateEnemies(_enemyPoolSize);
        }
        else if(_currentStageType == StageType.BossStage)
        {
            EnemyManager.Instance.CreatrBoss();
        }


    }

    private void Start()
    {
        _playerVirtualCamera.Follow = _player.transform;
    }


    private void Update()
    {
       
    }

    public void MonsterSpawn()
    {
        if(!_isTriggerOneTime)
        {
            for (int i = 0; i < _enemyPoolSize; i++)
            {
                int random;

                random = Random.Range(0, 2);
                if (random == 0)
                {
                    float RandomX = Random.Range(_spawnPos_1_Left.x, _spawnPos_1_Right.x);
                    float RandomY = Random.Range(_spawnPos_1_Left.y, _spawnPos_1_Right.y);
                    Vector2 randomSpawnPos = new Vector2(RandomX, RandomY);
                    EnemyManager.Instance.Spawn(randomSpawnPos);
                }
                else if (random == 1)
                {
                    float RandomX = Random.Range(_spawnPos_2_Left.x, _spawnPos_2_Right.x);
                    float RandomY = Random.Range(_spawnPos_2_Left.y, _spawnPos_2_Right.y);
                    Vector2 randomSpawnPos = new Vector2(RandomX, RandomY);
                    EnemyManager.Instance.Spawn(randomSpawnPos);
                }

            }
            print("OneTimeSpawn");
            _isTriggerOneTime = true;
        }
       
    }

    // 적을 잡았을떄 오르는 카운트
    public int GetkillCount()
    {
        return _killCount;
    }
    public void IncreaseKillCount()
    {
        _killCount++;
    }
    public int GetSpawnEnemyCount()
    {
        return _enemyPoolSize;
    }


}
