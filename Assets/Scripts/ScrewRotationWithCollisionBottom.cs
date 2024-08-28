using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewRotationWithCollisionBottom : MonoBehaviour
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
        else if (other.CompareTag("DrillBit") && isScrewIn == true && isScrewInFlag==false)
        {
            SceneManagerScript.Instance.ProcessStepHandler.xRSocketInteractorScrewBottom_GO.transform.position = new Vector3(-1.0188000202178956f, 1.5199999809265137f, 1.585800051689148f);
            SceneManagerScript.Instance.ProcessStepHandler.CompleteStep();
            isScrewInFlag = true;
        }
    }

}
