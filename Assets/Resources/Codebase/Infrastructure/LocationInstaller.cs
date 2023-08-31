using Zenject;
using UnityEngine;
using System;
using Services;

namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        public Transform 
            _startPoint, 
            _monobehaviourServicesParent;
       
        [SerializeField] GameObject
            shipControllPrefab,
            UiContainerPrefab,
            cameraPrefab,
            planetprefab,
            coroutineProcessorPrefab,
            enemySpawnPositionsPrefab;
        
        public Transform[] _enemySpawnPoints;
       
        [SerializeField] private SfxSetup _sfxSetup;
        [SerializeField] private MusicSetup _musicSetup;
        public override void InstallBindings()
        {
            BindCamera();
            BindItickable();

            BindScriptableObject<SfxSetup>(_sfxSetup);
            BindScriptableObject<MusicSetup>(_musicSetup);

            BindNonMonobehService<PlayerInputService>();
            BindNonMonobehService<PlanetaryUpgradesService>();
            BindNonMonobehService<EnemySpawnerService>();
            BindNonMonobehService<UiService>();
            BindNonMonobehService<ScoreService>();
            BindNonMonobehService<SfxService>();
            BindNonMonobehService<MusicService>();

            BindMonobehaviourService<EnemySpawnPositionsContainer>(enemySpawnPositionsPrefab);
            BindMonobehaviourService<PlanetHitpoints>(planetprefab);
            BindMonobehaviourService<ShipBehaviour>(shipControllPrefab);
            BindMonobehaviourService<UiDependenciesContainer>(UiContainerPrefab);
            BindMonobehaviourService<CoroutineProcessorService>(coroutineProcessorPrefab);
            
           

           // BindNonMonobehService<Game>();
        }

        private void BindItickable()
        {
            TickableService tickableService = new TickableService();
            Container.Bind<ITickable>().To<TickableService>().FromInstance(tickableService);
            Container.Bind<TickableService>().FromInstance(tickableService);
        }

       
        private void BindCamera()
        {
            Camera camera = GameObject.Instantiate(cameraPrefab, new Vector3(0f,0f,-1f), Quaternion.identity, null).GetComponent<Camera>();
            Container
                .Bind<Camera>()
                .FromInstance(camera)
                .AsSingle();
            Container.QueueForInject(camera);
        }

        public GameObject CreateEnemy(GameObject prefab, Transform spawnPos) =>
            Container.InstantiatePrefab(prefab, spawnPos.position, spawnPos.rotation, null);
        public GameObject CreateUpgrade(GameObject prefab, Transform spawnPos) => 
            Container.InstantiatePrefab(prefab, spawnPos);

        Tcomponent InstantiateMonobehaviourServiceForComponent<Tcomponent>(GameObject prefab) where Tcomponent : MonoBehaviour => 
            Instantiate(prefab, new Vector3(0f, 0f, -0f), Quaternion.identity, _monobehaviourServicesParent).GetComponent<Tcomponent>();
           
        private void BindMonobehaviourService<TMonobehaviourService>(GameObject prefab) where TMonobehaviourService : MonoBehaviour
        {
            TMonobehaviourService service = InstantiateMonobehaviourServiceForComponent<TMonobehaviourService>(prefab);
            Container.Bind<TMonobehaviourService>()
                .FromInstance(service)
                .AsSingle();
            Container.QueueForInject(service);
        }
        private void BindNonMonobehService<Tservice>() => Container.Bind<Tservice>().AsSingle();
        private void BindScriptableObject<TscriptableObject>(ScriptableObject scriptableObject) where TscriptableObject : ScriptableObject => 
            Container.Bind<TscriptableObject>()
                .FromScriptableObject(scriptableObject)
                .AsSingle();
    }
} 