using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Infrastructure;


public class EnemySpawner : MonoBehaviour
{

    public Transform[] spawnpoints;
    public GameObject[] enemy;
    public LocationInstaller locationInstaller;

    enum enemyTypes {mosqito, canoneer, assaulteer, missile};
    int mosquitoTraj;
    enum curveTypes {straight, circle};
  
    Transform currentSpawnPos;
    
    float enemyStrenght, enemystrenghtmod =1f; 
    float timetillnextWave;

    [SerializeField]
    float[] enemyCosts = new float[4]; //where indexator same with enemytypes id;
    [SerializeField]
    float stdTimeTillNextWave;  
    [SerializeField]
    bool coroutineIsOn;

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

            if (maxValue < enemystrenght) type = Random.Range(0, System.Enum.GetNames(typeof(enemyTypes)).Length);
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
        while (true)
        {
            if (wave.spawnedEnemies < wave.count)
            {
                wave.spawnedEnemies++;
                GameObject enemyGo = locationInstaller.EnemyFactory(enemy[wave.type], currentSpawnPos);

                enemyGo.GetComponent<EnemyBehaviour>().player = shipcontroll.gameObject.transform;

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
            else if (wave.spawnedEnemies == wave.count)
            {
                coroutineIsOn = false;
                Debug.Log("Coro stopped");
                StopCoroutine(waveLauncherExemplar);
            }
            else
            {
                Debug.LogWarning("spawner bug");
                coroutineIsOn = false;
                StopCoroutine(waveLauncherExemplar);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator waveLauncherExemplar;
    //toinject
    Shipcontroll shipcontroll;
    [Inject]
    void Construct(Shipcontroll _shipControll, LocationInstaller loc)
    {
        shipcontroll = _shipControll;
        locationInstaller = loc;
    }
    private void Start()
    {
        timetillnextWave = stdTimeTillNextWave;
        waveLauncherExemplar = WaveLauncher();
        enemyStrenght = 1f;
        currentSpawnPos = RandomSpawnPos();
    }
    private void Update()
    {
        enemyStrenght += Time.deltaTime * enemystrenghtmod;
        enemystrenghtmod *= 1.0001f;
        timetillnextWave -= Time.deltaTime;
        if (!coroutineIsOn && timetillnextWave <= 0)
        {
            currentSpawnPos = RandomSpawnPos();
            mosquitoTraj = Random.Range(0, 4);
            wave = GenerateWave();
            StartCoroutine(waveLauncherExemplar);
            coroutineIsOn = true;
            SetTillWaveTimer(); 
        }
        if (Input.GetKeyDown(KeyCode.F)) locationInstaller.EnemyFactory(enemy[2], currentSpawnPos);
    }
    Wave GenerateWave()
    {
        return new Wave(ref enemyStrenght, enemyCosts, FindEnemyMaxValue());
    }
    public Wave[] waves = new Wave[2];
    float FindEnemyMaxValue()
    {
        float cost = 0;
        for (int i = 0; i < enemyCosts.Length; i++)
        {
            if (cost < enemyCosts[i]) cost = enemyCosts[i];
        }
        return cost;
    }

    void SetTillWaveTimer()
    {
        if (enemyStrenght < stdTimeTillNextWave) timetillnextWave = stdTimeTillNextWave;
        else
        {
            enemyStrenght -= stdTimeTillNextWave;
            timetillnextWave = stdTimeTillNextWave / 2;
        }
    }

    public Transform RandomSpawnPos()
    {
        return spawnpoints [Random.Range(0, spawnpoints.Length)];
    }
}
