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
            BindLocInstaller();
            BindUiGameObject();
            BindSfxController();

            BindUiController();
            BindCamera();

            BindPlanetaryUpgrades();
            BindScoreControll();
            BindShipcontrollAndShiphHp();
            BindPlanetaryHitPoints();
            BindEnemySpawner();
           
        }
        private void BindLocInstaller()
        {
            Container.Bind<LocationInstaller>().FromInstance(this).AsSingle();
        }    
        private void BindScoreControll()
        {
            ScoreControll scoreControll = Container.InstantiatePrefabForComponent<ScoreControll>(scoreControllPrefab, StartPoint.position, Quaternion.identity, controllerParent);
            Container
                .Bind<ScoreControll>()
                .FromInstance(scoreControll)
                .AsSingle();
        }
        private void BindShipcontrollAndShiphHp()
        {
            Shipcontroll shipcontroll = Container.InstantiatePrefabForComponent<Shipcontroll>(ShipPrefab, StartPoint.position, Quaternion.identity, null);
            Container
                .Bind<Shipcontroll>()
                .FromInstance(shipcontroll)
                .AsSingle();
            Container
                .Bind<PlayerBehavoiur>()
                .FromInstance(shipcontroll.gameObject.GetComponent<PlayerBehavoiur>())
                .AsSingle();
        }
        private void BindUiController()
        {
            UiController uiController = Container.InstantiatePrefabForComponent<UiController>(uiControllerPrefab, StartPoint.position, Quaternion.identity, controllerParent);
            Container
                .Bind<UiController>()
                .FromInstance(uiController)
                .AsSingle();
        }
        private void BindSfxController()
        {
            SfxController sfxController = Container.InstantiatePrefabForComponent<SfxController>(sfxControllerPrefab, StartPoint.position, Quaternion.identity, controllerParent);
            Container
                .Bind<SfxController>()
                .FromInstance(sfxController)
                .AsSingle();
            sfxController.source = sfxSource;
        }
        private void BindUiGameObject ()
        {
            UiDependenciesContainer uiHolder = Container.InstantiatePrefabForComponent<UiDependenciesContainer>(canvasPrefab, StartPoint.position, Quaternion.identity, null);
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
        }
        void BindEnemySpawner()
        {
            EnemySpawner enemySpawner = Container.InstantiatePrefabForComponent<EnemySpawner>(enemyspawnerPrefab, new Vector3(0f, 0f, -1f), Quaternion.identity, controllerParent);
            Container
                .Bind<EnemySpawner>()
                .FromInstance(enemySpawner)
                .AsSingle();
            enemySpawner.spawnpoints = enemySpawnPoints;
        }
        void BindPlanetaryHitPoints()
        {
            PlanetHitpoints planetHitpoints = Container.InstantiatePrefabForComponent<PlanetHitpoints>(planetprefab, new Vector3(0f, 0f, -0f), Quaternion.identity, null);
            Container
                .Bind<PlanetHitpoints>()
                .FromInstance(planetHitpoints)
                .AsSingle();
        }
        void BindPlanetaryUpgrades()
        {
            PlanetaryUpgrades planetaryUpgrades = Container.InstantiatePrefabForComponent<PlanetaryUpgrades>(planetaryupgradesPrefab, new Vector3(0f, 0f, -0f), Quaternion.identity, controllerParent);
            Container
                .Bind<PlanetaryUpgrades>()
                .FromInstance(planetaryUpgrades)
                .AsSingle();
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