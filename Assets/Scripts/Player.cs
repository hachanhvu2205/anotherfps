using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    // Object Static Variables
    private static int _maxHealth = 100;
    private static int _maxAmmo = 250;
    public static int maxHealth
    {
        get { return _maxHealth; }
    }
    public static int maxAmmo
    {
        get { return _maxAmmo; }
    }
    public static GameObject instance;

    // Reference Variables
    private AudioSource source;
    private CharacterController control;
    private Transform cam;

    // Public Reference Variables
    [HideInInspector]
    public Transform weaponSlot;
    public bool hasKey
    {
        get { return gotKey; }
    }
    public List<string> hasItems
    {
        get { return gotItems; }
    }

    // Object Variables
    private Transform spawnPoint;
    private Vector3 moveDirection;
    private Vector3 camRotation;
    private int curHealth = _maxHealth;
    private int curAmmo;
    private int kills;
    private List<string> gotItems = new List<string>(4);
    private bool gotWeapon;
    private bool gotKey;
    private bool paused;
    private float walkTime;
     private Animator mAnimator;
    // Events
    public delegate void HealthUpdateHandler(int curHealth);
    public static event HealthUpdateHandler HealthUpdate;
    public delegate void AmmoUpdateHandler(int curAmmo);
    public static event AmmoUpdateHandler AmmoUpdate;
    public delegate void ItemUpdateHandler(string itemName);
    public static event ItemUpdateHandler ItemUpdate;
    public delegate void PauseHandler(bool pause);
    public static event PauseHandler PauseEvent;
    public delegate void DeathHandler(bool dead);
    public static event DeathHandler DeathEvent;
    public delegate void KillHandler(int kills);
    public static event KillHandler KillUpdate;

    // Customizeable Variables
    public GameObject riflePrefab;
    public GameObject feedbackPrefab;
    public Transform bulletOrigin;    
    public float speed = 6;
    public float jumpSpeed = 1.5f;
    public float gravity = 0.5f;
    [Range(10, 200)]
    public int shootRange = 100;
    [Range(-45, -15)]
    public int minAngle = -30;
    [Range(30, 80)]
    public int maxAngle = 45;
    [Range(50, 500)]
    public int sensitivity = 200;
    [Range(5, 50)]
    public int healAmount = 25;
    [Range(20, 100)]
    public int ammoAmount = 40;
    public AudioClip walkSound;
    public AudioClip shootSound;
    public AudioClip dryFireSound;
    public AudioClip hitSound;
    public AudioClip pickAmmoSound;
    public AudioClip pickHealthSound;
    public AudioClip pickSound;
    
    // MonoBehaviour Functions
    private void Awake()
    {
        instance = gameObject;
        source = GetComponent<AudioSource>();
        control = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        weaponSlot = cam.GetChild(0);
    }

    private void Update()
    {
        
        if (DialogueManager.DialogueIsOpen || curHealth <= 0) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if (paused) return;
        Shoot();
        Rotate();
        Move();
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        Bypass();
        #endif
    }

    #if UNITY_EDITOR || DEVELOPMENT_BUILD
    private void Bypass()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                GetRifle();
                GameManager.Instance.updateGameState(GameState.FindGun);
            }
            else
            {
                gotKey = true;
                GameManager.Instance.updateGameState(GameState.FindKey);
            }
        }   
        else if (Input.GetKeyDown(KeyCode.F2))
            GetItem("Battery");
        else if (Input.GetKeyDown(KeyCode.F3))
            GetItem("SmallBattery");
        else if (Input.GetKeyDown(KeyCode.F4))
            GetItem("MediumBattery");
        else if (Input.GetKeyDown(KeyCode.F5))
            GetItem("GasCan");
        else if (Input.GetKeyDown(KeyCode.F6))
            GameManager.Instance.updateGameState(GameState.Start);
        else if (Input.GetKeyDown(KeyCode.F7))
            GameManager.Instance.updateGameState(GameState.Fight);
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                GameManager.Instance.updateGameState(GameState.Save);
            else
                GameManager.Instance.updateGameState(GameState.Escape);
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                UpdateSpawnpoint(GameObject.Find("SpawnPoint").transform);
            else 
                UpdateSpawnpoint(GameObject.Find("SpawnPoint (1)").transform);
            Respawn();
        }
        else if (Input.GetKeyDown(KeyCode.F10))
        {
            UpdateSpawnpoint(GameObject.Find("SpawnPoint (2)").transform);
            Respawn();
        }
        else if (Input.GetKeyDown(KeyCode.F11))
        {
            UpdateSpawnpoint(GameObject.Find("SpawnPoint (3)").transform);
            Respawn();
        }
        else if (Input.GetKeyDown(KeyCode.F12))
        {
            UpdateSpawnpoint(GameObject.Find("SpawnPoint (4)").transform);
            Respawn();
        }
    }
    #endif

    /// <summary>
    /// Controls camera rotation oriented by mouse position
    /// </summary>
    private void Rotate()
    {
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * Input.GetAxis("MouseX"));

        camRotation.x -= Input.GetAxis("MouseY") * sensitivity * Time.deltaTime;
        camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);

        cam.localEulerAngles = camRotation;
    }

    /// <summary>
    /// Controls movement with CharacterController
    /// </summary>
    private void Move()
    {
        if (control.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

            if (moveDirection.magnitude > 0.3f && walkTime > 0.4f)
            {
                walkTime = 0;
                source.pitch = Random.Range(0.8f, 1.2f);
                source.PlayOneShot(walkSound, 0.3f);
            }
            walkTime += Time.deltaTime;

            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpSpeed;            
        }

        moveDirection.y -= gravity * Time.deltaTime;
        control.Move(moveDirection * speed * Time.deltaTime);
    }

    /// <summary>
    /// Raycasts to the target position, and sends a message to the object if hit
    /// </summary>
    private void Shoot()
    {
        if (gotWeapon && Input.GetButtonDown("Fire"))
        {
            if (curAmmo > 0)
            {
                // Displaying Shoot Feedback
                source.pitch = Random.Range(0.8f, 1.2f);
                source.PlayOneShot(shootSound);
                curAmmo--;
                AmmoUpdate(curAmmo);
                camRotation.x -= 2;
                GameObject gunFlare = Instantiate(feedbackPrefab, bulletOrigin.position, Quaternion.identity) as GameObject;
                gunFlare.transform.SetParent(bulletOrigin);
                Destroy(gunFlare, 0.1f);
                // Checking if the shot hit 
                Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, shootRange));
                Vector3 dir = target - cam.position;
                Ray ray = new Ray(cam.position, dir.normalized);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit,shootRange))
                {
                    if(hit.transform.gameObject.tag=="Enemy")
                    {
                        // Destroy(hit.transform.gameObject);
                       AIHealth enemy = hit.transform.GetComponent<AIHealth>();
                        if(enemy == null) return;
                    //     if(enemy.currentHealth <=0){
                    //         hit.transform.GetComponent<Animator>().SetTrigger("Die");
                    //     }
                        enemy.GotDamage(20f);
                    //    hit.transform.gameObject.GetComponent<Animator>().SetBool("Idle",true);
                    //    hit.transform.GetComponent<Animator>().SetBool("Move",false);
                        // hit.transform.gameObject.GetComponent<Animator>().SetTrigger("Die");
                        // Destroy(hit.transform.gameObject,1f);
                        // mAnimator.SetTrigger("Die");
                        // hit.transform.parent.GetComponent<Soldier>().Hit();
                    }
                }
                
                if (Physics.Raycast(ray, out hit, shootRange))
                {
                   
                    hit.collider.SendMessage("TakeDamage", SendMessageOptions.DontRequireReceiver);
                    GameObject hitFlare = Instantiate(feedbackPrefab, hit.point, Quaternion.identity) as GameObject;
                    Destroy(hitFlare, 0.2f);
                }
            }
            else
            {
                source.pitch = Random.Range(0.8f, 1.2f);
                source.PlayOneShot(dryFireSound);
            }
        }
    }

    /// <summary>
    /// Updates the player spawnpoint by a given point
    /// </summary>
    /// <param name="newPoint">The new spawnpoint's transform</param>
    private void UpdateSpawnpoint(Transform newPoint)
    {
        spawnPoint = newPoint;
    }

    /// <summary>
    /// Takes a given amount of damage
    /// </summary>
    /// <param name="amount">The amount of damage to take</param>
    public void TakeDamage(int amount)
    {
        curHealth -= amount;        
        if (curHealth <= 0)
        {
            
            // transform.position = spawnPoint.position;
            // transform.rotation = spawnPoint.rotation;
            // curHealth = maxHealth;
            DeathEvent(true);
        }
        source.pitch = Random.Range(0.8f, 1.2f);
        source.PlayOneShot(hitSound);
        HealthUpdate(curHealth);
    }

    /// <summary>
    /// Recovers the defaul amount of damage
    /// </summary>
    private void RecoverHealth()
    {
        if (curHealth == _maxHealth)
            return;

        curHealth += healAmount;
        if (curHealth > _maxHealth)
            curHealth = _maxHealth;

        source.pitch = Random.Range(0.8f, 1.2f);
        source.PlayOneShot(pickHealthSound);
        HealthUpdate(curHealth);
    }

    /// <summary>
    /// Collects a given amount of ammunition
    /// </summary>
    /// <param name="amount">The amount of ammo to pick</param>
    private void TakeAmmo(int amount)
    {
        Debug.Log("Got " + ammoAmount + " Ammo");
        curAmmo += amount;
        if (curAmmo > _maxAmmo)
            curAmmo = _maxAmmo;

        source.pitch = Random.Range(0.8f, 1.2f);
        source.PlayOneShot(pickAmmoSound);
        AmmoUpdate(curAmmo);
    }

    /// <summary>
    /// Gets the rifle and equips it
    /// </summary>
    private void GetRifle()
    {
        Debug.Log("Got Rifle");
        source.pitch = Random.Range(0.8f, 1.2f);
        source.PlayOneShot(pickAmmoSound);
        GameObject rifle = Instantiate(riflePrefab, weaponSlot.position, Quaternion.identity) as GameObject;
        rifle.transform.SetParent(weaponSlot);        
        rifle.transform.localScale = Vector3.one * 0.2f;
        rifle.transform.localRotation = Quaternion.identity;
        rifle.transform.localPosition = Vector3.zero;
        gotWeapon = true;
        TakeAmmo(20);
    }

    /// <summary>
    /// Gets an item and process its required logic
    /// </summary>
    /// <param name="itemName">The item to get</param>
    private void GetItem(string itemName)
    {
        Debug.Log("Got " + itemName);
        switch (itemName)
        {
            case "Key":
                gotKey = true;
                source.pitch = Random.Range(0.8f, 1.2f);
                source.PlayOneShot(pickSound);
                break;
            case "Rifle":
                GetRifle();
                break;
            case "Health":
                RecoverHealth();
                break;
            case "Ammo":
                TakeAmmo(ammoAmount);
                break;
            default:
                gotItems.Add(itemName);
                source.pitch = Random.Range(0.8f, 1.2f);
                source.PlayOneShot(pickSound);
                ItemUpdate(itemName);
                break;
        }
    }

    public void Pause() {
        paused = Time.timeScale > 0 ? true : false;
        PauseEvent(paused);
    }

    public void Respawn() {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        curHealth = maxHealth;
        HealthUpdate(curHealth);
        GameObject.Find("HUD").transform.Find("Timer").GetComponent<Timer>().SetTimeRemaining(180);
        DeathEvent(false);
    }

    public void SetKills(int kills) {
        this.kills = kills; 
        KillUpdate(this.kills);
    }
    public void AddKill()
    {
        kills++;
        KillUpdate(kills);
    }
}
