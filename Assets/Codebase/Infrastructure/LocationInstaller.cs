using Zenject;
using UnityEngine;
using System;
using Codebase.Infrastructure;

namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        public Transform StartPoint, controllerParent;
        [SerializeField] GameObject
            shipControllPrefab,
            UiContainerPrefab,
            cameraPrefab,
            planetprefab,
            coroutineProcessorPrefab,
            enemySpawnPositionsPrefab;
        public Transform[] enemySpawnPoints;
        [SerializeField] AudioSource sfxSource;
        [SerializeField] AudioSource musicSource;
        [SerializeField] SfxSetup sfxSetup;
        [SerializeField] MusicSetup musicSetup;

        public override void InstallBindings()
        {
            BindLocInstaller();
            BindCamera();
            BindItickable();

            BindScriptableObject<SfxSetup>(sfxSetup);
            BindScriptableObject<MusicSetup>(musicSetup);

            BindMonobehaviourService<EnemySpawnPositionsContainer>(enemySpawnPositionsPrefab);
            BindMonobehaviourService<PlanetHitpoints>(planetprefab);
            BindMonobehaviourService<ShipBehaviour>(shipControllPrefab);
            BindMonobehaviourService<UiDependenciesContainer>(UiContainerPrefab);
            BindMonobehaviourService<CoroutineProcessor>(coroutineProcessorPrefab);

            BindNonMonobehService<PlayerInput>(true);
            BindNonMonobehService<PlanetaryUpgrades>(true);
            BindNonMonobehService<EnemySpawner>(true);  
            BindNonMonobehService<UiService>(true);
            BindNonMonobehService<ScoreControll>(false);
            BindNonMonobehService<SfxService>(true);
            BindNonMonobehService<MusicService>(true);

            BindNonMonobehService<Game>(true);

        }

        private void BindItickable()
        {
            TickableService tickableService = new TickableService();
            Container.Bind<ITickable>().To<TickableService>().FromInstance(tickableService);
            Container.Bind<TickableService>().FromInstance(tickableService);
        }

        private void BindLocInstaller()
        {
            Container.Bind<LocationInstaller>().FromInstance(this).AsSingle();
            Container.QueueForInject(this);
        }

        private void BindCamera()
        {
            Camera camera = Container.InstantiatePrefabForComponent<Camera>(cameraPrefab, new Vector3(0f,0f,-1f), Quaternion.identity, null);
            Container
                .Bind<Camera>()
                .FromInstance(camera)
                .AsSingle();
            Container.QueueForInject(camera);
        }
       
        public GameObject CreateEnemy(GameObject prefab, Transform spawnPos)
        {
            return Container.InstantiatePrefab(prefab, spawnPos.position, spawnPos.rotation, null);
        }
        public GameObject CreateUpgrade(GameObject prefab, Transform spawnPos)
        {
            return Container.InstantiatePrefab(prefab, spawnPos);
        }

        Tcomponent InstantiateMonobehaviourServiceForComponent<Tcomponent>(GameObject prefab) where Tcomponent : MonoBehaviour => 
            Instantiate(prefab, new Vector3(0f, 0f, -0f), Quaternion.identity, controllerParent).GetComponent<Tcomponent>();
           
        private void BindMonobehaviourService<TComponentService>(GameObject prefab) where TComponentService : MonoBehaviour
        {
            TComponentService service = InstantiateMonobehaviourServiceForComponent<TComponentService>(prefab);
            Container.Bind<TComponentService>()
                .FromInstance(service)
                .AsSingle();
            Container.QueueForInject(service);
        }
        private void BindNonMonobehService<Tservice>(bool injectNeeded) where Tservice : new()
        {
            Tservice tservice = new Tservice();
            Container.Bind<Tservice>()
                .FromInstance(tservice)
                .AsSingle();
           Container.QueueForInject(tservice);
        }
        private void BindScriptableObject<TscriptableObject>(ScriptableObject scriptableObject) where TscriptableObject : ScriptableObject
        {
            Container.Bind<TscriptableObject>()
                .FromScriptableObject(scriptableObject)
                .AsSingle();
        }
    }
} 