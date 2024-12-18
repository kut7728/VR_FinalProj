using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public Transform cannonMuzzle; // 포구 위치
    public bool isLoaded = false;  // 장전 상태

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cannon"))
        {
            // 포탄을 포구 위치로 이동
            transform.position = cannonMuzzle.position;
            transform.rotation = cannonMuzzle.rotation;
            GetComponent<Rigidbody>().isKinematic = true;
            isLoaded = true; // 장전 상태 활성화
        }
    }
}
