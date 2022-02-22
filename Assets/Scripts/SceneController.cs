using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Enemy EnemyPrefab;
    public Enemy BossPrefab;
    public PlayerBase BasePrefab;
    public GameObject EnemiesRoot;
    public GameObject BasesRoot;
    public int Score = 0;
    public int EnemiesAlive;

    private Enemy[] _enemies;
    private PlayerBase[] _bases;
    private Enemy _boss;
    [SerializeField]
    private Player _player = null;
    private List<Enemy> _enemiesActive;
    
    
    private const int BOSS_NUM = 16; // When _enemiesAlive == this number, activate Boss
    private const int ENEMY_ROW = 4;
    private const int ENEMY_COL = 8;
    private const int BASE_NUM = 4;

    // Start is called before the first frame update
    void Start()
    {
        _enemies = new Enemy[ENEMY_ROW * ENEMY_COL];
        _bases = new PlayerBase[BASE_NUM];
        _enemiesActive = new List<Enemy>();
        EnemiesAlive = ENEMY_ROW * ENEMY_COL;

        InitBases();
        InitEnemies();
        SetEnemiesType();

        StartCoroutine(EnemiesMovement());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator EnemiesMovement()
    {
        int steps = 5;

        while (EnemiesAlive > 0 && _player.Lives > 0)
        {
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < ENEMY_ROW; i++)
                {
                    yield return new WaitForSeconds(1);
                    
                    EnemiesShoot();

                    for (int j = 0; j < ENEMY_COL; j++)
                    {
                        if (_enemies[i * ENEMY_COL + j] != null)
                        {
                            _enemies[i * ENEMY_COL + j].Move();
                        }
                    }
                }
            }

            yield return new WaitForSeconds(1);

            foreach (Enemy en in _enemies)
            {
                if (en != null)
                {
                    if (steps > 0)
                    {
                        en.MoveForward();
                    }

                    en.ChangeDirection();
                }
            }

            if (steps > 0)
            {
                steps--;
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void InitBases()
    {
        const float BASE_OFFSET_X = -7;
        const float BASE_OFFSET_Y = -3;

        float basePosX = 0;

        for (int i = 0; i < _bases.Length; i++)
        {
            basePosX = i * 4 + BASE_OFFSET_X;
            _bases[i] = Instantiate(BasePrefab);
            _bases[i].transform.parent = BasesRoot.transform;
            _bases[i].transform.position = new Vector3(basePosX, BASE_OFFSET_Y, -1);
        }
    }

    private void InitEnemies()
    {
        const float ENEMY_OFFSET_X = -8;
        const float ENEMY_OFFSET_Y = 4;

        float enemyPosX = 0;
        float enemyPosY = 0;

        for (int i = 0; i < _enemies.Length; i++)
        {
            enemyPosX = (i % ENEMY_COL) * 2 + ENEMY_OFFSET_X;
            enemyPosY = -(i / ENEMY_COL) + ENEMY_OFFSET_Y;

            _enemies[i] = Instantiate(EnemyPrefab);
            _enemies[i].transform.parent = EnemiesRoot.transform;
            _enemies[i].transform.position = new Vector3(enemyPosX, enemyPosY, -1);
            _enemies[i].Controller = this;
            _enemies[i].Index = i;

            if ((i / ENEMY_COL) == ENEMY_ROW - 1)
            {
                _enemies[i].IsActive = true;
                _enemiesActive.Add(_enemies[i]);
            }
        }
    }

    void SetEnemiesType()
    {
        for(int i = 0; i < _enemies.Length; i++)
        {
            int type = i / ENEMY_COL;
            switch(type)
            {
                case 0:
                    _enemies[i].Type = EnemyType.Hard;
                    break;
                case 1:
                    _enemies[i].Type = EnemyType.Medium;
                    break;
                case 2:
                case 3:
                    _enemies[i].Type = EnemyType.Easy;
                    break;
            }
        }
    }
    public void ActivateEnemy(int index)
    {
        int activateIndex = index - ENEMY_COL;
        EnemiesAlive--;

        if (EnemiesAlive == BOSS_NUM)
        {
            ActivateBoss();
        }

        _enemiesActive.Remove(_enemies[index]);

        if (activateIndex >= 0 && _enemies[activateIndex] != null)
        {
            _enemies[activateIndex].IsActive = true;
            _enemiesActive.Add(_enemies[activateIndex]);
        }
    }

    void EnemiesShoot()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        if (_enemiesActive.Count > 0)
        {
            _enemiesActive[Random.Range(0, _enemiesActive.Count)].Shoot();
        }
    }

    void ActivateBoss()
    {
        Vector3 startPosition = new Vector3(15, 4, -1);
        _boss = Instantiate(BossPrefab);
        _boss.transform.parent = EnemiesRoot.transform;
        _boss.transform.position = startPosition;
        _boss.Type = EnemyType.Special;
        _boss.Controller = this;

        StartCoroutine(BossMove());

    }

    IEnumerator BossMove()
    {
        int i = 0;
        int shootingFrequency = 10;
        const float LEFT_BOUNDARY = -16.0F;
        float speed = -2;

        do
        {
            i++;
            if(i % shootingFrequency == 0 && _boss != null)
            {
                _boss.Shoot();
            }

            Vector3 BossPosition = _boss.transform.localPosition;
            BossPosition.x += speed * Time.deltaTime;
            _boss.transform.localPosition = BossPosition;

            yield return null;

        } while (_boss != null && _boss.transform.localPosition.x > LEFT_BOUNDARY);
        
        if (_boss != null)
        {
            _boss.Destroy();
        }
    }
}
