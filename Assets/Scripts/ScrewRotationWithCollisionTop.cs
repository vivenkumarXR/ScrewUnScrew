using UnityEngine;
using System.Collections;

public class ScrewRotationWithCollisionTop : MonoBehaviour
{
    public bool isCompleted = true;
    public bool isScrewIn = false;
    public bool isScrewInFlag = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DrillBit") && isCompleted == false)
        {
            SceneManagerScript.Instance.ProcessStepHandler.CompleteStep();
            isCompleted = true;
        }
        else if (other.CompareTag("DrillBit") && isScrewIn == true && isScrewInFlag == false)
        {
            SceneManagerScript.Instance.ProcessStepHandler.xRSocketInteractorScrewTop_GO.transform.position = new Vector3(-1.0188f, 1.8159f, 1.586f);
            SceneManagerScript.Instance.ProcessStepHandler.CompleteStep();
            isScrewInFlag = true;
        }
    }
}



