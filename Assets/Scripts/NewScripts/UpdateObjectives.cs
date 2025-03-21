using UnityEngine;
using TMPro;

public class UpdateObjectives : MonoBehaviour
{
    public TextMeshProUGUI objectivesText; // Assign your Text component here

    public string ObjectiveToBeUpdated = "";
    public void updateObjectives()
    {
        objectivesText.text = ObjectiveToBeUpdated;
        Invoke("ClearObjectivesText", 12f); // Schedule to clear text after 12 seconds

    }

    private void ClearObjectivesText()
    {
        objectivesText.text = ""; // Clear the objectives text
    }
}
