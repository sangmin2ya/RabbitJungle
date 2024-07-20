using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // 파티클 시스템이 재생을 멈췄을 때 오브젝트를 삭제합니다.
        if (particleSystem && !particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}