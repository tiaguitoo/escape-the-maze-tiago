using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class playSteps : MonoBehaviour
{
    PlayableDirector director;
    public List<Steps> steps;

    public TextMeshProUGUI objectivesText; 
    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    [System.Serializable]
    public class Steps{
        public String name;
        public float time;
        public bool hasPlayed = false;
        public string ObjectiveToBeUpdated = "";
    }

    public void PlayStepIndex(int index){
        Steps step = steps[index];
        if(!step.hasPlayed){
            step.hasPlayed = true;
            director.Stop();
            director.time = step.time;
            director.Play();
        }
    }
    public void updateObjectives(int index)
    {
        Steps step = steps[index];
        //if(!step.hasPlayed){
        objectivesText.text = step.ObjectiveToBeUpdated;
        Invoke("ClearObjectivesText", 12f); // Schedule to clear text after 12 seconds
        //}
    }

    private void ClearObjectivesText()
    {
        objectivesText.text = ""; // Clear the objectives text
    }
}
