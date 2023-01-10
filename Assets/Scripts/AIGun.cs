using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIGun : MonoBehaviour
{
    public GameObject Player;
    public float Distance;

    public bool isAngered;

    public NavMeshAgent _agent;

    [Header("Gun Settings")]
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public float range = 100;
    public float impactForce = 30f;
    public float damage = 10;
    public int reservedAmmoCapacity = 270;

    public Camera fpsCam;
    public GameObject impactEffect;

    //Variables that change thrue out the code
    bool _canShoot;
    int _currentAmmoInClip;
    int _ammoInReserve;

    //Muzzle Flash
    //public Image muzzleFlashImage;
    //public Sprite[] flashes;

    //Aiming
    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;

    public float aimSmoothing = 10;

    //WeaponRecoil
    public bool randomizeRecoil;
    public Vector2 randomRecoilConstraints;
    //You only need to assigns this if randomize recoil is off
    public Vector2 recoilPattern;


    void Start()
    {
        _currentAmmoInClip = clipSize;
        _ammoInReserve = reservedAmmoCapacity;
        _canShoot = true;
    }

    void Update()
    {
        Distance = Vector3.Distance(Player.transform.position, this.transform.position);

        if (Distance <= 50)
        {
            isAngered = true;
        }

        if (Distance > 50f)
        {
            isAngered = false;
        }

        if (isAngered)
        {
            _agent.isStopped = false;
            _agent.SetDestination(Player.transform.position);

            DetermineAim();
            if (Input.GetMouseButton(0) && _canShoot && _currentAmmoInClip > 0)
            {
                _canShoot = false;
                _currentAmmoInClip--;
                StartCoroutine(ShootGun());
            }
            else if (Input.GetKeyDown(KeyCode.R) && _currentAmmoInClip < clipSize && _ammoInReserve > 0)
            {
                int amountNeeded = clipSize - _currentAmmoInClip;
                if (amountNeeded >= _ammoInReserve)
                {
                    _currentAmmoInClip += _ammoInReserve;
                    _ammoInReserve -= amountNeeded;
                }
                else
                {
                    _currentAmmoInClip = clipSize;
                    _ammoInReserve -= amountNeeded;
                }
            }

        }

        if (!isAngered)
        {
            _agent.isStopped = true;
        }
    }

    void DetermineAim()
    {
        Vector3 target = normalLocalPosition;
        if (Input.GetMouseButton(1)) target = aimingLocalPosition;

        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);

        transform.localPosition = desiredPosition;
    }

    void DetermineRecoil()
    {
        transform.localPosition -= Vector3.forward * 0.1f;
    }

    IEnumerator ShootGun()
    {
        DetermineRecoil();
        //StartCoroutine(MuzzleFlash());

        RayCastForEnemy();

        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }

    /*IEnumerator MuzzleFlash()
    {
        muzzleFlashImage.sprite = flashes[Random.Range(0, flashes.Length)];
        muzzleFlashImage.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        muzzleFlashImage.sprite = null;
        muzzleFlashImage.color = new Color(0, 0, 0, 0);
    }*/
    void RayCastForEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            //___________________________________________________
            AIHealth target2 = hit.transform.GetComponent<AIHealth>();
            if (target2 != null)
            {
                target2.TakeDamage(damage);
            }
        }
    }
}
