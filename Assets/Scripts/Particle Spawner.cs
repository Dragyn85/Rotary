using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particles;
    [SerializeField] Vector3          minPosition    = new Vector3(-10f, -5f, 0);
    [SerializeField] Vector3          maxPosition    = new Vector3(10f, 5f, 0);
    
    private void Awake()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
        foreach (var particle in particles)
        {
            particle.gameObject.SetActive(false);
        }
    }

    public IEnumerator Spawn()
    {
        WaitForSeconds                               waitForSeconds = new WaitForSeconds(0.2f);
        foreach (var particle in particles)
        {
            var randomPosition = new Vector3(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y), 0);
            particle.transform.localPosition = randomPosition;
            particle.gameObject.SetActive(true);
            yield return waitForSeconds;
        }
        
        
    }
}
