using Zenject;
using UnityEngine;
namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        public Transform StartPoint, controllerParent;
        public GameObject ShipPrefab, uiControllerPrefab, sfxControllerPrefab, canvasPrefab, cameraPrefab, enemyspawnerPrefab, planetprefab, planetaryupgradesPrefab, scoreControllPrefab;
        public Transform[] enemySpawnPoints;
        [SerializeField]
        AudioSource sfxSource;

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
            ScoreControll scoreControll = Instantiate(scoreControllPrefab, StartPoint.position, Quaternion.identity, controllerParent).GetComponent<ScoreControll>();
            Container
                .Bind<ScoreControll>()
                .FromInstance(scoreControll)
                .AsSingle();
            Container.QueueForInject(scoreControll);
        }
        private void BindShipcontrollAndShiphHp()
        {
            Shipcontroll shipcontroll = Instantiate(ShipPrefab, StartPoint.position, Quaternion.identity, null).GetComponent<Shipcontroll>();
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
            UiController uiController = Instantiate(uiControllerPrefab, StartPoint.position, Quaternion.identity, controllerParent).GetComponent<UiController>(); ;
            Container
                .Bind<UiController>()
                .FromInstance(uiController)
                .AsSingle();
            Container.QueueForInject(uiController);
        }
        private void BindSfxController()
        {
            SfxController sfxController = Instantiate(sfxControllerPrefab, StartPoint.position, Quaternion.identity, controllerParent).GetComponent<SfxController>();
            Container
                .Bind<SfxController>()
                .FromInstance(sfxController)
                .AsSingle();
            sfxController.source = sfxSource;
            Container.QueueForInject(sfxController);
        }
        private void BindUiGameObject ()
        {
            UiDependenciesContainer uiHolder = Instantiate(canvasPrefab, StartPoint.position, Quaternion.identity, null).GetComponent<UiDependenciesContainer>();
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
            EnemySpawner enemySpawner = Instantiate(enemyspawnerPrefab, new Vector3(0f, 0f, -1f), Quaternion.identity, controllerParent).GetComponent<EnemySpawner>();
            Container
                .Bind<EnemySpawner>()
                .FromInstance(enemySpawner)
                .AsSingle();
            enemySpawner.spawnpoints = enemySpawnPoints;
            Container.QueueForInject(enemySpawner);
        }
        void BindPlanetaryHitPoints()
        {
            PlanetHitpoints planetHitpoints =Instantiate(planetprefab, new Vector3(0f, 0f, -0f), Quaternion.identity, null).GetComponent<PlanetHitpoints>();
            Container
                .Bind<PlanetHitpoints>()
                .FromInstance(planetHitpoints)
                .AsSingle();
            Container.QueueForInject(planetHitpoints);
        }
        void BindPlanetaryUpgrades()
        {
            PlanetaryUpgrades planetaryUpgrades = Instantiate(planetaryupgradesPrefab, new Vector3(0f, 0f, -0f), Quaternion.identity, controllerParent).GetComponent<PlanetaryUpgrades>(); ;
            Container
                .Bind<PlanetaryUpgrades>()
                .FromInstance(planetaryUpgrades)
                .AsSingle();
            Container.QueueForInject(planetaryUpgrades);
        }
        public GameObject EnemyFactory(GameObject prefab, Transform spawnPos)
        {
            return Container.InstantiatePrefab(prefab, spawnPos.position, spawnPos.rotation, null);
        }
        public GameObject UpgradeFactory(GameObject prefab, Transform spawnPos)
        {
            return Container.InstantiatePrefab(prefab, spawnPos);
        }

    }
}