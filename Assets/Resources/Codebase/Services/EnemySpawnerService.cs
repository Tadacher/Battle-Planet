using System.Collections;
using UnityEngine;
using Zenject;
using Infrastructure;


public class EnemySpawnerService
{
    private float[] _enemyCosts = new float[4]; //where index is the same as enemytypes id;
    private float _stdTimeTillNextWave;
    private bool _isCoroutineOn;

    private enum EnemyTypes {mosqito, canoneer, assaulteer, missile};
    private int mosquitoTraj;
    private enum CurveTypes {straight, circle};


    private float enemyStrenght, enemystrenghtmod =1f;
    private float timetillnextWave;

   
    Transform currentSpawnPos;

    public Transform[] spawnpoints;
    public GameObject[] enemy;
    public struct Wave
    {
        public short count;
        public int type;
        public float betweenDelay;
        public short spawnedEnemies;

        
        public Wave(short enemycount, int enemytype, int spawnpointID)
        {

            count = enemycount;
            type = enemytype;
            spawnedEnemies = 0;
            count = enemycount;
            betweenDelay = GenerateBetweenDelay(enemytype);
        }

        /// <summary>
        /// generates wave, decreases enemystrenght by generated wave cost;
        /// </summary>
        /// <param name="enemystrenght"></param>
        /// <param name="enemycosts"></param>
        public Wave (ref float enemystrenght, float[] enemycosts, float maxValue)
        {
            float waveCost;

            //init type

            if (maxValue < enemystrenght) type = Random.Range(0, System.Enum.GetNames(typeof(EnemyTypes)).Length);
            else type = 0;
            
            //generate mosqito fleet count
            if (type == 0)
            {
                if (enemystrenght < 10 * enemycosts[0]) waveCost = Random.Range(0f, enemystrenght);
                else waveCost = Random.Range(0f, 10 * enemycosts[0]);
            }
            else waveCost = enemycosts[type];
            enemystrenght -= waveCost;
            //init
            count = (short)(waveCost/ enemycosts[type]);
            if (count == 0) count++;
            spawnedEnemies = 0;
            betweenDelay = GenerateBetweenDelay(type);

            if (count == 0) Debug.LogWarning("zero enemies in a wave");
        }
        static float  GenerateBetweenDelay(int type)
        {
            if (type == 0) return Random.Range(0.15f, 0.3f);
            else return Random.Range(5f, 10f);
        }
    }
    Wave wave;
    public IEnumerator WaveLauncher()
    {
        while (wave.spawnedEnemies < wave.count)
        {
           
                wave.spawnedEnemies++;
                GameObject enemyGo = _locationInstaller.CreateEnemy(enemy[wave.type], currentSpawnPos);

                enemyGo.GetComponent<EnemyBehaviour>().player = _shipcontroll.gameObject.transform;

                if (wave.type == 0)
                {
                    MosquitoFleet mosquitoFleet = enemyGo.GetComponent<MosquitoFleet>();
                    mosquitoFleet.trajType = (MosquitoFleet.TrajType)mosquitoTraj;
                    if (mosquitoTraj == 3)
                    {
                        mosquitoFleet.InitAi();
                    }
                    else if (mosquitoTraj == 2) mosquitoFleet.SetCircleArguments();
                }
                Debug.Log(wave.spawnedEnemies + " out of " + wave.count);
                yield return new WaitForSeconds(wave.betweenDelay);     
        }
        if (wave.spawnedEnemies == wave.count)
        {
            Debug.Log("Coro stopped");
            yield break;
        }
        else
        {
            Debug.LogWarning("spawner bug");
            yield break;
        }
    }

    TickDelegate tickDelegate;
    //toinject
    ShipBehaviour _shipcontroll;
    LocationInstaller _locationInstaller;
    CoroutineProcessorService _coroutinePrecessor;
    [Inject]
    void Construct(ShipBehaviour _shipControll, LocationInstaller loc, CoroutineProcessorService coroutineProcessor, TickableService tickableService, EnemySpawnPositionsContainer enemySpawnPositionsContainer)
    {
        _shipcontroll = _shipControll;
        _locationInstaller = loc;
        _coroutinePrecessor = coroutineProcessor;
        spawnpoints = enemySpawnPositionsContainer.spawnPoints;
        timetillnextWave = _stdTimeTillNextWave;
        enemyStrenght = 1f;
        currentSpawnPos = RandomSpawnPos();
        tickDelegate += Tick;
        tickableService.AddToTick(tickDelegate);
    }

    private void Tick()
    {
        enemyStrenght += Time.deltaTime * enemystrenghtmod;
        enemystrenghtmod *= 1.0001f;
        timetillnextWave -= Time.deltaTime;
        if (!_isCoroutineOn && timetillnextWave <= 0)
        {
            currentSpawnPos = RandomSpawnPos();
            mosquitoTraj = Random.Range(0, 4);
            wave = GenerateWave();
            _coroutinePrecessor.StartCoroutineProcess(WaveLauncher());
            SetTillWaveTimer(); 
        }
        if (Input.GetKeyDown(KeyCode.F)) _locationInstaller.CreateEnemy(enemy[2], currentSpawnPos);
    }
    Wave GenerateWave() => new Wave(ref enemyStrenght, _enemyCosts, FindEnemyMaxValue());
    public Wave[] waves = new Wave[2];
    float FindEnemyMaxValue()
    {
        float cost = 0;
        for (int i = 0; i < _enemyCosts.Length; i++)
        {
            if (cost < _enemyCosts[i]) cost = _enemyCosts[i];
        }
        return cost;
    }

    void SetTillWaveTimer()
    {
        if (enemyStrenght < _stdTimeTillNextWave) timetillnextWave = _stdTimeTillNextWave;
        else
        {
            enemyStrenght -= _stdTimeTillNextWave;
            timetillnextWave = _stdTimeTillNextWave / 2;
        }
    }

    public Transform RandomSpawnPos()
    {
        if (spawnpoints.Length > 0) return spawnpoints[Random.Range(0, spawnpoints.Length)];
        else
        {
            Debug.Log("no spawnpoints");
            return null;
        }
    }
}
