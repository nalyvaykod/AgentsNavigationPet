using ProjectDawn.Navigation.Hybrid;
using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace ProjectDawn.Navigation.Sample.Mass
{
    public class PlayerSpawner : MonoBehaviour
    {
        public GameObject prefabToSpawn;
        public NavMeshSurface navMeshSurface;
        public Transform DestinationPlayer;
        public bool DestinationDeff = true;
        public int Count;
        public int deadCount;
        public GameObject enemy;
        public Text CounterText;
        public Text DeadText;


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;



                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 spawnPosition = hit.point;

                    NavMeshHit navMeshHit;

                    if (NavMesh.SamplePosition(spawnPosition, out navMeshHit, 1.0f, NavMesh.AllAreas))
                    {
                        var agent = GameObject.Instantiate(prefabToSpawn, navMeshHit.position, Quaternion.identity);



                        if (DestinationPlayer != null)
                        {
                            var agentComp = agent.GetComponent<AgentAuthoring>();
                            if (DestinationDeff)
                            {
                                agentComp.SetDestination(DestinationPlayer.position);

                            }
                            else
                            {
                                agentComp.SetDestination(DestinationPlayer.position);
                            }
                        }
                        StartCoroutine(AddIfDestroyed(agent));

                    }

                    else
                    {
                        Debug.Log("Spawn position is not on NavMesh!");
                    }



                }
                IEnumerator AddIfDestroyed(GameObject agent)
                {
                    yield return new WaitForSeconds(5f);
                    Destroy(agent);
                    deadCount++;
                    Count--;
                    CounterPl();
                }

                Count++;
            }


            CounterPl();





        }

        void CounterPl()
        {
            CounterText.text = Count.ToString();
            DeadText.text = deadCount.ToString();
        }
    }
}
