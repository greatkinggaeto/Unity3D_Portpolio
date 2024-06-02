using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("EnemyManager");
                _instance = obj.AddComponent<EnemyManager>();
            }

            return _instance;
        }
    }
    // enemy_1 갯수와 프리팹 저장
    private GameObject _enemyPrefab;
    private List<GameObject> enemies = new List<GameObject>();

    //  보스몬스터의 프리팹 저장
    private GameObject _bossPrefab;
    private GameObject _boss;

    private void Awake()
    {
        _enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy/Melee/Enemy_1");
        _bossPrefab = Resources.Load<GameObject>("Prefabs/Enemy/Boss/Boss");
        
    }
  



    public void CreateEnemies(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(_enemyPrefab, transform);
            obj.SetActive(false);
            enemies.Add(obj);
        }
    }

    public void CreatrBoss()
    {
        GameObject boss = Instantiate(_bossPrefab, transform);
        boss.SetActive(false);
        _boss = boss;
    }

    public void Spawn(Vector3 pos)
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf == false)
            {
                enemy.SetActive(true);
                enemy.transform.position = pos;
                break;
            }
        }
    }
    public void BossSpawn(Vector3 pos)
    {
        if(_boss.activeSelf == false)
        {
            _boss.SetActive(true);
            _boss.transform.position = pos;
        }
    }


  
}
