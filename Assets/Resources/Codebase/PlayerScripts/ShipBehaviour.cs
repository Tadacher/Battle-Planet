using System.Collections;
using UnityEngine;
using Zenject;
/// <summary>
/// ship controll logic, ship stats
/// </summary>
public class ShipBehaviour : MonoBehaviour
{
    [SerializeField] private float 
        speed = 20, 
        shootInterval, 
        currentShootInterval, 
        crushRotaionSpeed;
    [SerializeField] private float 
        turnFactor, 
        multargX, 
        multArgY,
        maxSpeedMod, 
        minSpeedMod;
    [SerializeField] private int currentWeapon = 0;

    [SerializeField] private ShipHitpointComponent _shipHpComponent;
    [SerializeField] private ControllLayout controlllayout;
    [SerializeField] private Transform projectileSpawner;
    [SerializeField] private GameObject[] projectiles;
    //original* variables are being used to add percented values from upgrades and to draw stat (readonly)
    //or just to remember original stat values
    public float OriginalSpeed {get; private set;}
    public float respawnTime;
    public float OriginalRespawnTime { get; private set; }
    public float thrustSpeed;
    public bool isCrushing, crushed;

    public ProjectileData[] projectileInfos = new ProjectileData[1];

    private float 
        originalMultargX, 
        originalMultargY;

    private ProjectileFactory projectileFactory;

    //to inject
    private SfxService _sfxService;
    private UiService _uiController;

    [Inject]
    private void Construct(SfxService sfxService, UiService uiController)
    {
        _sfxService = sfxService;
        _uiController = uiController;
        
        projectileFactory = new ProjectileFactory();
        
        _shipHpComponent = GetComponent<ShipHitpointComponent>();

        projectileInfos[0] = new ProjectileData(1, projectiles[0], 0, 0.4f);
        shootInterval = projectileInfos[currentWeapon].shotInterval;

        CacheOriginalStatValues();
        _shipHpComponent = gameObject.AddComponent<ShipHitpointComponent>();
        _shipHpComponent.Construct(this, uiController);
    }

    private void Update()
    {
        turnFactor += speed * Time.deltaTime;
        CalculateAndSetNewPosition();
        if (currentShootInterval >= 0) currentShootInterval -= Time.deltaTime;
    }
 
    public void UpdateShootInterval() => shootInterval = projectileInfos[currentWeapon].shotInterval;
    public void ShootIfPossible()
    {
        if (currentShootInterval <= 0 && !isCrushing)
        {
            ProjectileData projectileInfo = projectileInfos[0];
            projectileFactory.SpawnBullet(projectileInfo, projectileSpawner);
            _sfxService.PlayPlayerShotSound(projectileInfo.shotSoundId);
            currentShootInterval = shootInterval;
        }
    }  

    public void StartCrush() 
    {
        if (!isCrushing) 
            StartCoroutine(Crush());
    }

    public void Accelerate() => speed += Time.deltaTime * thrustSpeed;
    public void Decelerate() => speed -= Time.deltaTime * thrustSpeed;
    public void RotateToPointer(Vector3 mousepos)
    {
        float angle = Mathf.Atan2(mousepos.y - transform.position.y, mousepos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 270);
    }
    private void CalculateAndSetNewPosition()
    {
        float x = Mathf.Cos(turnFactor) * multargX;
        float y = Mathf.Sin(turnFactor) * multArgY;
        transform.position = new Vector3(x, y, 0);
    }
    private void CacheOriginalStatValues()
    {
        originalMultargX = multargX;
        originalMultargY = multArgY;
        OriginalRespawnTime = respawnTime;
    }

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
            multargX += Time.deltaTime * speed;
            multArgY += Time.deltaTime * speed;
            yield return null;
        }
        isCrushing = false;
        _shipHpComponent.ResetHP();
    }
}
