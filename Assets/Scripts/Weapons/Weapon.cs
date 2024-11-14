using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;
    
    [Header("Bullets")]
    public Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> bulletPool;

    private float shootTimer;

    private void Start()
    {
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            collectionCheck: false,
            defaultCapacity: 30,
            maxSize: 100
        );
    }

    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        newBullet.gameObject.SetActive(false);
        return newBullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        DontDestroyOnLoad(bullet.gameObject);
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootIntervalInSeconds)
        {
            shootTimer = 0f;
            FireBullet();
        }
    }

    private void FireBullet()
    {
        Bullet bullet = bulletPool.Get();
    }
}
