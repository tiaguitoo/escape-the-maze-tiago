using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls; // 0 - Up 1 -Down 2 - Right 3- Left
    public GameObject[] doors;
    public GameObject pointLight;
    public Renderer lightBulb;
    public Material onLight, offLight;

    public GameObject ScaryVoiceTrigger;
    public SummonZombie summonZombie;

    public void UpdateRoom(bool[] status, bool lightStatus, GameObject zombie, bool scaryVoice)
    {
        if(summonZombie != null)
        {
            summonZombie.zombie = zombie;
        }

        if(scaryVoice && ScaryVoiceTrigger){
            ScaryVoiceTrigger.SetActive(true);
        }

        if (lightStatus) {
            pointLight.SetActive(true);
            lightBulb.material = onLight;
        } else {
            pointLight.SetActive(false);
            lightBulb.material = offLight;
        }

        for (int i = 0; i < status.Length; i++)
        {
            if(doors.Length > 0) {
                doors[i].SetActive(status[i]);
            }
            walls[i].SetActive(!status[i]);
        }
    }
}