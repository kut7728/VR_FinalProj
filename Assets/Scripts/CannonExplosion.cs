using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerParticleEffect : MonoBehaviour
{
    [Header("Particle Settings")]
    public ParticleSystem particleEffect; // 실행할 Particle System
    public Transform effectSpawnPoint; // 효과가 생성될 위치 (옵션)

    private InputAction triggerButtonAction; // 오른쪽 Trigger 버튼 액션

    private void Awake()
    {
        // 오른쪽 Trigger 버튼 Input Action 생성
        triggerButtonAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{RightHand}/trigger" // 오른쪽 Trigger 버튼 매핑
        );
        triggerButtonAction.Enable(); // Input Action 활성화
    }

    private void Update()
    {
        // Trigger 버튼 입력 확인
        if (triggerButtonAction.IsPressed())
        {
            PlayParticleEffect(); // Trigger 버튼을 누르고 있을 때 파티클 실행
        }
        else
        {
            StopParticleEffect(); // Trigger 버튼을 놓았을 때 파티클 중지
        }
    }

    private void PlayParticleEffect()
    {
        if (particleEffect != null && !particleEffect.isPlaying)
        {
            if (effectSpawnPoint != null)
            {
                // 지정된 위치에서 파티클 실행
                particleEffect.transform.position = effectSpawnPoint.position;
                particleEffect.transform.rotation = effectSpawnPoint.rotation;
            }
            particleEffect.Play(); // 파티클 실행
            Debug.Log("Particle effect playing.");
        }
    }

    private void StopParticleEffect()
    {
        if (particleEffect != null && particleEffect.isPlaying)
        {
            particleEffect.Stop(); // 파티클 중지
            Debug.Log("Particle effect stopped.");
        }
    }

    private void OnDestroy()
    {
        // Input Action 비활성화
        triggerButtonAction.Disable();
    }
}
