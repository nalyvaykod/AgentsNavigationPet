using Unity.Mathematics;
using Random = Unity.Mathematics.Random;
using UnityEngine;
using ProjectDawn.Navigation.Hybrid;
using UnityEngine.UI;
using System.Collections;

namespace ProjectDawn.Navigation.Sample.Mass
{
    public class Spawner : MonoBehaviour
    {
        public GameObject Prefab;
        public float SpawnInterval = 10f;
        public int ObjectsPerSpawn = 2;
        public float3 Size = new float3(1, 0, 1);
        public int MaxCount = 300;
        public int Count;
        public Transform Destination;
        public bool DestinationDeferred = true;
        public Text textCounter;
        public Text diedCounter;

        private int TotalSpawned = 0;
        private int AliveCount = 0;
        private int diedCount = 0;
        private Random m_Random = new Random(1);

        private bool stopSpawning = false;

        void Start()
        {
            StartCoroutine(SpawnObj());
        }

        IEnumerator SpawnObj()
        {
            while (!stopSpawning && TotalSpawned < MaxCount)
            {
                for (int i = 0; i < ObjectsPerSpawn; i++)
                {
                    if (TotalSpawned >= MaxCount)
                    {
                        stopSpawning = true;
                        yield break;
                    }

                    float3 offset = m_Random.NextFloat3(-Size, Size);
                    float3 position = (float3)transform.position + offset;
                    GameObject unit = GameObject.Instantiate(Prefab, position, Quaternion.identity);
                    if (Destination != null)
                    {
                        var agent = unit.GetComponent<AgentAuthoring>();
                        if (DestinationDeferred)
                        {
                            agent.SetDestinationDeferred(Destination.position);
                        }
                        else
                        {
                            agent.SetDestination(Destination.position);
                        }
                    }
                    TotalSpawned++;
                    AliveCount++;
                    Counter();
                    StartCoroutine(DestroyAndAdd(unit));
                }
                yield return new WaitForSeconds(SpawnInterval);
            }
        }

        void Counter()
        {
            textCounter.text = AliveCount.ToString();
            diedCounter.text = diedCount.ToString();
        }

        IEnumerator DestroyAndAdd(GameObject unit)
        {
            yield return new WaitForSeconds(5f);
            Destroy(unit);
            diedCount++;
            AliveCount--;
            Counter();
        }
    }
}
