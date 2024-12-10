using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance; // 싱글톤 패턴 사용

    void Awake()
    {
        // 이 오브젝트가 이미 존재하면 새로 생성된 것을 파괴 (중복 방지)
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // 첫 번째로 생성된 MusicManager를 유지
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 이동 시 파괴되지 않도록 설정
        }
    }
}
