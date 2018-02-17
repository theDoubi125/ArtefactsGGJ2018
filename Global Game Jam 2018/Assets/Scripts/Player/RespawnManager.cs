using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public RespawnLocation[] respawnLocations;
    private List<Transform> playersToRespawn = new List<Transform>();
    private List<float> respawnDelays = new List<float>();

    public void AddPlayer(Transform player, float repawnDelay)
    {
        playersToRespawn.Add(player);
        respawnDelays.Add(repawnDelay);
        player.SetParent(transform);
        player.gameObject.SetActive(false);
    }

    void Start()
    {
        respawnLocations = Object.FindObjectsOfType<RespawnLocation>();
    }

    void Update()
    {
        for(int i=0; i<respawnDelays.Count; i++)
        {
            respawnDelays[i] -= Time.deltaTime;
            if(respawnDelays[i] < 0)
            {
                playersToRespawn[i].SetParent(null);
                playersToRespawn[i].position = respawnLocations[(int)(Random.value * respawnLocations.Length)].transform.position;
                playersToRespawn[i].gameObject.SetActive(true);
                respawnDelays.RemoveAt(i);
                playersToRespawn.RemoveAt(i);
                i--;
            }
        }
    }
}
