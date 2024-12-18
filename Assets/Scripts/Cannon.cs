using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform firePoint;         // 발사 위치
    public GameObject cannonBallPrefab; // 발사할 포탄 프리팹
    public ParticleSystem fireEffect;   // 발사 이펙트
    public float fireForce = 1000f;     // 발사 힘

    private bool isLoaded = false;

    private void Update()
    {
        if (isLoaded && Input.GetKeyDown(KeyCode.Space)) // Trigger 입력으로 변경 필요
        {
            Fire();
        }
    }

    public void LoadCannon()
    {
        isLoaded = true;
    }

    private void Fire()
    {
        GameObject cannonBall = Instantiate(cannonBallPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * fireForce);

        if (fireEffect != null)
        {
            fireEffect.Play();
        }

        isLoaded = false;
    }
}
