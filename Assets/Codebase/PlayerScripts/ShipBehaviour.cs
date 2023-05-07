using Codebase.Infrastructure;
using System.Collections;
using UnityEngine;
using Zenject;
/// <summary>
/// ship controll logic, ship stats
/// </summary>
public class ShipBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 20, shootInterval, currentShootInterval, crushRotaionSpeed;
    [SerializeField] float turnFactor, multargX, multArgY, maxSpeedMod, minSpeedMod;
    [SerializeField] int currentWeapon = 0;

    [SerializeField] ShipHitpointComponent _shipHpComponent;
    [SerializeField] ControllLayout controlllayout;
    [SerializeField] Transform projectileSpawner;
    [SerializeField] GameObject[] projectiles;
    //original* variables are being used to add percented values from upgrades and to draw stat (readonly)
    //or just to remember original stat values
    public float OriginalSpeed {get; private set;}
    public float respawnTime;
    public float OriginalRespawnTime { get; private set; }
    public float thrustSpeed;
    public bool isCrushing, crushed;


    float originalMultargX, originalMultargY;

    PlayerInput _playerInput;

   
    //to inject
    SfxService _sfxService;
    UiService _uiController;
    //
    ProjectileFactory projectileFactory;

    public struct ProjectileInfo
    {
        public int damage;
        public int OriginalDamage { private set; get; }
        public GameObject projectilePrefab;
        public int shotSoundId;
        public float shotInterval;
        public float OriginalInterval { private set; get; }
        public ProjectileInfo (int dmg, GameObject projPrefab, int shotSnd, float shotIntervl)
            {
                damage = dmg;
                OriginalDamage = damage;
                projectilePrefab = projPrefab;
                shotSoundId = shotSnd;
                shotInterval = shotIntervl;
                OriginalInterval = shotInterval;
            }
    }

    public ProjectileInfo[] projectileInfos = new ProjectileInfo[1];
    private IEnumerator Crush()
    {
        Debug.Log("CrushStarted");
        _sfxService.PlayPlayerCrushSound(0);
        while (multargX >= 0.2)
        {
            isCrushing = true;
            transform.eulerAngles += new Vector3(0f, 0f, crushRotaionSpeed * Time.deltaTime);
            multargX -= Time.deltaTime * speed;
            multArgY -= Time.deltaTime * speed;
            //yield return new WaitForEndOfFrame();
            yield return null;
        }
        _sfxService.PlayPlayerCrushSound(1);
        crushed = true;
        StartCoroutine(RespawnCount());
    }
    private IEnumerator RespawnCount()
    {
        Debug.Log("respawnCountStarted");
        _uiController.SetRespawnTimer(respawnTime);
        yield return new WaitForSeconds(respawnTime);
        _sfxService.PlayPlayerCrushSound(2);
        _sfxService.PlayPlayerCrushSound(3);
        StartCoroutine(Respawn());
    }
    private IEnumerator Respawn()
    {
        while (multargX < originalMultargX)
        {
            Debug.Log("RespawnCoroStep");
            multargX += Time.deltaTime * speed;
            multArgY += Time.deltaTime * speed;
            yield return null;
        }
        isCrushing = false;
        _shipHpComponent.ResetHP();
    }
    
    
    [Inject]
    private void Construct(SfxService sfxService, UiService uiController, CoroutineProcessor coroutineProcessor, PlayerInput playerInput)
    {
        _sfxService = sfxService;
        _uiController = uiController;
        _playerInput = playerInput;
        
        projectileFactory = new ProjectileFactory();
        
        _shipHpComponent = GetComponent<ShipHitpointComponent>();

        projectileInfos[0] = new ProjectileInfo(1, projectiles[0], 0, 0.4f);
        shootInterval = projectileInfos[currentWeapon].shotInterval;

        CacheOriginalValues();
    }

    void Update()
    {
        turnFactor += speed * Time.deltaTime;
        CalculateAndSetNewPosition();
        if (currentShootInterval >= 0) currentShootInterval -= Time.deltaTime;
    }
 
    public void UpdateShootInterval() => shootInterval = projectileInfos[currentWeapon].shotInterval;

    

    public void Shoot(ProjectileInfo projectileInfo)
    {
        if (currentShootInterval <= 0 && !isCrushing)
        {
            projectileFactory.SpawnBullet(projectileInfo, projectileSpawner);
            _sfxService.PlayPlayerShotSound(projectileInfo.shotSoundId);
            currentShootInterval = shootInterval;
        }
    }

    

    public void StartCrush() 
    {
        if (!isCrushing) StartCoroutine(Crush());
    }

    public void Accelerate() => speed += Time.deltaTime * thrustSpeed;
    public void Decelerate() => speed -= Time.deltaTime * thrustSpeed;
    public void RotateToMouse(Vector3 mousepos)
    {
        float angle = Mathf.Atan2(mousepos.y - transform.position.y, mousepos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 270);
    }
    void CalculateAndSetNewPosition()
    {
        float x = Mathf.Cos(turnFactor) * multargX;
        float y = Mathf.Sin(turnFactor) * multArgY;
        transform.position = new Vector3(x, y, 0);
    }
    private void CacheOriginalValues()
    {
        originalMultargX = multargX;
        originalMultargY = multArgY;
        OriginalRespawnTime = respawnTime;
    }
}
