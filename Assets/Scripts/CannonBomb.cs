using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CannonballDestroyOnCannonCollision : MonoBehaviour
{
    [Header("References (Assign in Inspector)")]
    public XRGrabInteractable cannonball;  // XRGrabInteractable가 붙은 대포알
    public GameObject cannon;              // 대포 오브젝트

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 인스펙터에서 지정한 cannon인지 확인
        if (collision.gameObject == cannon)
        {
            // cannonball이 잡혀있는 상태인지 확인
            if (cannonball != null && cannonball.isSelected)
            {
                // 대포알 삭제
                Destroy(cannonball.gameObject);
            }
        }
    }
}
