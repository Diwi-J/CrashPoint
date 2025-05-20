using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] private GameObject footStepsObject; 

    void Start()
    {
        if (footStepsObject != null)
            footStepsObject.SetActive(false);
    }

    void Update()
    {
        //Activate footsteps when any movement key is pressed
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (footStepsObject != null)
                footStepsObject.SetActive(true);
        }

        // Deactivate footsteps when any movement key is released
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            if (footStepsObject != null)
                footStepsObject.SetActive(false);
        }
    }
}
