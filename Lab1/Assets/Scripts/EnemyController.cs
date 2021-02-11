using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject m_enemies; // GameObject = ghouls
    public Vector3 m_spawnValues; // Allows us to assign spawn values in editor

    public int m_hazardCount;
    public float m_spawnTimer;
    public float m_startWait;
    public float m_waveWait;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(m_startWait);
        while (true)
        {
            for (int i = 0; i < m_hazardCount; i++)
            {
                Vector3 m_spawnPosition = new Vector3(Random.Range(m_spawnValues.x, m_spawnValues.x + 5.0f), m_spawnValues.y, m_spawnValues.z);
                Quaternion m_spawnRotation = Quaternion.identity;
                GameObject clone;
                clone = Instantiate(m_enemies, m_spawnPosition, m_spawnRotation);
                yield return new WaitForSeconds(m_spawnTimer);
            }
            yield return new WaitForSeconds(m_waveWait);
        }
    }
}
