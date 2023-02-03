using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
/// <summary>
/// ship controll logic, ship stats
/// </summary>
public class Shipcontroll : MonoBehaviour
{
    [SerializeField]
    float speed = 20, shootInterval, currentShootInterval, crushRotaionSpeed;
    [SerializeField]
    ControllLayout controlllayout;
    //original* variables are being used to add percented values from upgrades and to draw stat (readonly)
    //or just to remember original stat values
    public float originalSpeed {get; private set;}
    public float respawnTime;
    public float originalRespawnTime { get; private set; }
    public float thrustSpeed;
    public bool isCrushing, crushed;


    float originalMultargX, originalMultargY;

    PlayerBehavoiur playerBehaviour;

    [SerializeField]
    Transform projectileSpawner;
    [SerializeField]
    GameObject[] projectiles;
    [SerializeField]
    float turnFactor, multargX, multArgY, maxSpeedMod, minSpeedMod;
    [SerializeField]
    int currentWeapon = 0;
    //to inject
    SfxController sfx;
    UiController uiController;
    //

    public struct ProjectileInfo
    {
        public int damage;
        /// <summary>
        /// start projectile damage (read only)
        /// </summary>
        public int originalDamage { private set; get; }
        public GameObject projectilePrefab;
        public int shotSoundId;
        public float shotInterval;
        /// <summary>
        /// start projectile shotCooldown (read only)
        /// </summary>
        public float originalInterval { private set; get; }
        public ProjectileInfo (int dmg, GameObject projPrefab, int shotSnd, float shotIntervl)
            {
                damage = dmg;
                originalDamage = damage;
                projectilePrefab = projPrefab;
                shotSoundId = shotSnd;
                shotInterval = shotIntervl;
                originalInterval = shotInterval;
            }
        }

    public ProjectileInfo[] projectileInfos = new ProjectileInfo[1];

    IEnumerator crush, respawnCount, respawn;
    private IEnumerator Crush()
    {
        while (true)
        {
            isCrushing = true;
            transform.eulerAngles += new Vector3(0f, 0f, crushRotaionSpeed * Time.deltaTime);
            multargX -= Time.deltaTime * speed;
            multArgY -= Time.deltaTime * speed;
            if (multargX <= 0.2)
            {
                sfx.PlayPlayerCrushSound(1);
                crushed = true;
                StartCoroutine(respawnCount);
                Debug.Log("crushCoroStopped");
                StopCoroutine(crush);
            }
            //yield return new WaitForEndOfFrame();
            yield return null;
        }
    }
    private IEnumerator RespawnCount()
    {
        while (true)
        {
            crushed = false;
            uiController.SetRespawnTimer(respawnTime);
            yield return new WaitForSeconds(respawnTime);
            if (!crushed)
            {
                StopCoroutine(respawnCount);
                Debug.Log("RespawnCount stopped");
                sfx.PlayPlayerCrushSound(2);
                sfx.PlayPlayerCrushSound(3);
                StartCoroutine(respawn);
                //breal cycle??
            }
        }
        
    }
    private IEnumerator Respawn()
    {
        while (true)
        {
            Debug.Log("RespawnCoroStep");
            multargX += Time.deltaTime * speed;
            multArgY += Time.deltaTime * speed;

            if (multargX >= originalMultargX)
            {
                isCrushing = false;
                playerBehaviour.ResetHP();
                Debug.Log("respawn coro stopped");
                StopCoroutine(respawn);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    [Inject]
    private void Construct(SfxController sfxController, UiController uiContr)
    {
        sfx = sfxController;
        uiController = uiContr;
    }
    void Start()
    {
        ControllSetup();
        crush = Crush();
        respawnCount = RespawnCount();
        respawn = Respawn(); 

        projectileInfos[0] = new ProjectileInfo(1, projectiles[0], 0, 0.4f);
        shootInterval = projectileInfos[currentWeapon].shotInterval;
        playerBehaviour = GetComponent<PlayerBehavoiur>();

        originalMultargX = multargX;
        originalMultargY = multArgY;
        originalRespawnTime = respawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        turnFactor += speed * Time.deltaTime;
        CalculatePosition();
        if (currentShootInterval >= 0) currentShootInterval -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (!isCrushing) RotateToMouse();
    }

    void ControllSetup()
    {
        controlllayout = new ControllLayout();
        controlllayout.Enable();
        controlllayout.Player.shoot.performed += context => Shoot(projectileInfos[0]);
        controlllayout.Player.slowdown.performed += Context => Slowdown();
        controlllayout.Player.Speeedup.performed += Context => SpeedUp();
    }
    public void UpdateShootInterval()
    {
        shootInterval = projectileInfos[currentWeapon].shotInterval;
    }

    void RotateToMouse()
    {
        Vector3 mousepos = GetMousePos();
        float angle = Mathf.Atan2(mousepos.y - transform.position.y, mousepos.x - transform.position.x)  * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 270);
    }

    Vector3 GetMousePos()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 0;
        return mousepos;
    }
    void Shoot(ProjectileInfo projectileInfo)
    {
        if (currentShootInterval <= 0 && !isCrushing)
        {
            GameObject bullet = Instantiate(projectileInfo.projectilePrefab, projectileSpawner.position, projectileSpawner.rotation, null);
            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 10, ForceMode2D.Impulse);
            sfx.PlayPlayerShotSound(projectileInfo.shotSoundId);

            Projectile projScript = bullet.GetComponent<Projectile>();
            projScript.damage = projectileInfo.damage;
            currentShootInterval = shootInterval;
        }
    }
    void SpeedUp()
    {
        speed += Time.deltaTime * thrustSpeed;
    }
    void Slowdown()
    {
        speed -= Time.deltaTime * thrustSpeed;
    }

    void CalculatePosition()
    {
        float x = Mathf.Cos(turnFactor) * multargX;
        float y = Mathf.Sin(turnFactor) * multArgY;
        transform.position = new Vector3(x, y, 0);
    }

    public void IfDamageRecieved(int hp)
    {
        if (hp <= 0 && !isCrushing)
        {
            StartCoroutine(crush);
            sfx.PlayPlayerCrushSound(0);
        }
    }

}
