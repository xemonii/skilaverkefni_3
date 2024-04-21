// Written by Belistov
// Designed to be a universal gun script

using UnityEngine;
using System.Collections;

public class ShooterCode : MonoBehaviour
{
    [Header("< Main Cam / Player >")]
    [SerializeField] private mouseLook cam;
    private float originalSensitivity;

    [SerializeField] private PlayerMovement _speed;
    private float originalSpeed;

    [SerializeField] private PlayerMovement _SprintSpeed;
    private float originalSprintSpeed;

    [Header("< Gun Settings >")]
    public bool is_auto;
    [Header("")]
    public float damage = 10f;
    public float fireRate = 15f;
    public float impactForce = 10f;
    [Header("")]
    public float aimDist = 30f;
    public float aimSens = 10f;
    public float aimWalkSpeed = 12f;
    public float aimSprintSpeed = 12f;
    public float range = 100f;
    [Header("")]
    public float maxAmmo = 10f;
    public float currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    [Header("< Gun Components >")]
    public GameObject muzzle;
    public Camera fpsCam;
    public AudioSource shoot_SFX;
    private float nextTimeToFire = 0f;

    void Start()
    {
        shoot_SFX = GetComponent<AudioSource>();
        shoot_SFX.Pause();

        originalSensitivity = cam.mouseSensitivity;
        originalSpeed = _speed.speed;
        originalSprintSpeed = _SprintSpeed.sprintSpeed;
        currentAmmo = maxAmmo;

    }

    void OnEnable()
    {
        isReloading = false;
    }

    void Update()
    {
        if (isReloading)
        {
            StartCoroutine(Reload());
            return;
        }
        if (currentAmmo <= 0 || Input.GetKeyDown("r"))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Aim();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            noAim();
        }

        if (is_auto == true)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
                shoot_SFX.Play(0);
            }
        }
        if (is_auto == false)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
                shoot_SFX.Play(0);
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        float elapsedTime = 0f;
        

        while (elapsedTime < reloadTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        noAim();


        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            healthCode target = hit.transform.GetComponent<healthCode>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }


            // Optionally add force to a rigidbody
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }

    void Aim()
    {
        fpsCam.fieldOfView = aimDist;
        cam.mouseSensitivity = aimSens;
        _speed.speed = aimWalkSpeed;
        _SprintSpeed.sprintSpeed = aimSprintSpeed;
    }

    void noAim()
    {
        fpsCam.fieldOfView = 60;
        cam.mouseSensitivity = originalSensitivity;
        _speed.speed = originalSpeed;
        _SprintSpeed.sprintSpeed = originalSprintSpeed;
    }
}
