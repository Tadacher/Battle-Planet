using Zenject;
using UnityEngine;
namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        public Transform StartPoint, controllerParent;
        [SerializeField] GameObject 
            ShipPrefab, 
            uiControllerPrefab, 
            sfxControllerPrefab, 
            canvasPrefab, 
            cameraPrefab, 
            enemyspawnerPrefab, 
            planetprefab, 
            planetaryupgradesPrefab, 
            scoreControllPrefab;
        public Transform[] enemySpawnPoints;
        [SerializeField] AudioSource sfxSource;

        public override void InstallBindings()
        {
            BindShipcontrollAndShiphHp();
            BindLocInstaller();
            BindUiGameObject();
            BindSfxController();

            BindUiController();
            BindCamera();

            BindPlanetaryUpgrades();
            BindScoreControll();
            
            BindPlanetaryHitPoints();
            BindEnemySpawner();
           
        }
        private void BindLocInstaller()
        {
            Container.Bind<LocationInstaller>().FromInstance(this).AsSingle();
        }    
        private void BindScoreControll()
        {
            ScoreControll scoreControll = InstantiateMonobehaviourService<ScoreControll>(scoreControllPrefab);
            Container
                .Bind<ScoreControll>()
                .FromInstance(scoreControll)
                .AsSingle();
            Container.QueueForInject(scoreControll);
        }
        private void BindShipcontrollAndShiphHp()
        {
            Shipcontroll shipcontroll = InstantiateMonobehaviourService<Shipcontroll>(ShipPrefab);
            Container
                .Bind<Shipcontroll>()
                .FromInstance(shipcontroll)
                .AsSingle();
            Container
                .Bind<PlayerBehavoiur>()
                .FromInstance(shipcontroll.gameObject.GetComponent<PlayerBehavoiur>())
                .AsSingle();
            Container.QueueForInject(shipcontroll);
            Container.QueueForInject(shipcontroll.gameObject.GetComponent<PlayerBehavoiur>());
        }
        private void BindUiController()
        {
            UiController uiController = InstantiateMonobehaviourService<UiController>(uiControllerPrefab);
            Container
                .Bind<UiController>()
                .FromInstance(uiController)
                .AsSingle();
            Container.QueueForInject(uiController);
        }
        private void BindSfxController()
        {
            SfxService sfxController = InstantiateMonobehaviourService<SfxService>(sfxControllerPrefab);
            Container
                .Bind<SfxService>()
                .FromInstance(sfxController)
                .AsSingle();
           // sfxController._source = sfxSource;
            Container.QueueForInject(sfxController);
        }
        private void BindUiGameObject ()
        {
            UiDependenciesContainer uiHolder = InstantiateMonobehaviourService<UiDependenciesContainer>(canvasPrefab);
            Container
                .Bind<UiDependenciesContainer>()
                .FromInstance(uiHolder)
                .AsSingle();
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
        void BindEnemySpawner()
        {
            EnemySpawner enemySpawner = InstantiateMonobehaviourService<EnemySpawner>(enemyspawnerPrefab);
            Container
                .Bind<EnemySpawner>()
                .FromInstance(enemySpawner)
                .AsSingle();
            enemySpawner.spawnpoints = enemySpawnPoints;
            Container.QueueForInject(enemySpawner);
        }
        void BindPlanetaryHitPoints()
        {
            PlanetHitpoints planetHitpoints = InstantiateMonobehaviourService<PlanetHitpoints>(planetprefab);
            Container
                .Bind<PlanetHitpoints>()
                .FromInstance(planetHitpoints)
                .AsSingle();
            Container.QueueForInject(planetHitpoints);
        }
        void BindPlanetaryUpgrades()
        {
            PlanetaryUpgrades planetaryUpgrades = InstantiateMonobehaviourService<PlanetaryUpgrades>(planetaryupgradesPrefab);
            Container
                .Bind<PlanetaryUpgrades>()
                .FromInstance(planetaryUpgrades)
                .AsSingle();
            Container.QueueForInject(planetaryUpgrades);
        }
        public GameObject CreateEnemy(GameObject prefab, Transform spawnPos)
        {
            return Container.InstantiatePrefab(prefab, spawnPos.position, spawnPos.rotation, null);
        }
        public GameObject CreateUpgrade(GameObject prefab, Transform spawnPos)
        {
            return Container.InstantiatePrefab(prefab, spawnPos);
        }

        Tcomponent InstantiateMonobehaviourService<Tcomponent>(GameObject prefab) where Tcomponent : MonoBehaviour => 
            Instantiate(prefab, new Vector3(0f, 0f, -0f), Quaternion.identity, controllerParent).GetComponent<Tcomponent>();

    }
}