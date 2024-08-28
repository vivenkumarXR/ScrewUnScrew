using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using static UnityEngine.Rendering.DebugUI;

public class ProcessStepHandler : MonoBehaviour
{

    public delegate void StepCompleted();
    public event StepCompleted OnStepCompleted;

    private int currentStepIndex = 0;
    private List<System.Action> steps = new List<System.Action>();


    [SerializeField]
    private bool IsTriggerPressedFirstTime = false;
    [SerializeField]
    private bool IsGripPressedFirstTime = false;


    public XRGrabInteractable grabInteractableDrillBody;
    public XRGrabInteractable grabInteractableScrew_Cross_Top;
    public XRGrabInteractable grabInteractableScrew_Cross_Bottom;
    public XRGrabInteractable grabInteractableDoor;

    
    public GameObject grabInteractableScrew_Cross_Top_GO;
    public GameObject grabInteractableScrew_Cross_Bottom_GO;
    public GameObject grabInteractableDoor_GO;

    public XRSocketInteractor xRSocketInteractorDoor;
    public XRSocketInteractor xRSocketInteractorScrewTop;
    public XRSocketInteractor xRSocketInteractorScrewBottom;

    public GameObject xRSocketInteractorDoor_GO;
    public GameObject xRSocketInteractorScrewTop_GO;
    public GameObject xRSocketInteractorScrewBottom_GO;

    public bool TakeOutDoor = true;
    public bool TakeOutTopScrew = true;
    public bool TakeOutBottomScrew = true;
    public bool PlaceBackDoor=false;
    public bool PlaceBackTopScrew = false;
    public bool PlaceBackBottomScrew = false;



    void Awake()
    {
        
        // Get the XRGrabInteractable component attached to this GameObject
        grabInteractableDrillBody = GameObject.FindWithTag("DrillBody").GetComponent<XRGrabInteractable>();
        grabInteractableScrew_Cross_Top = GameObject.FindWithTag("Screw_Cross_Top").GetComponent<XRGrabInteractable>();
        grabInteractableScrew_Cross_Bottom = GameObject.FindWithTag("Screw_Cross_Bottom").GetComponent<XRGrabInteractable>();
        grabInteractableDoor = GameObject.FindWithTag("Door").GetComponent<XRGrabInteractable>();

        grabInteractableScrew_Cross_Top_GO = grabInteractableScrew_Cross_Top.gameObject;
        grabInteractableScrew_Cross_Bottom_GO = grabInteractableScrew_Cross_Bottom.gameObject;
        grabInteractableDoor_GO = grabInteractableDoor.gameObject;


        // Check if the component exists
        if (grabInteractableDrillBody == null || grabInteractableScrew_Cross_Top == null || grabInteractableScrew_Cross_Bottom == null || grabInteractableDoor == null)
        {
            Debug.LogError("XRGrabInteractable component not found on this GameObject.");
            return;
        }

        xRSocketInteractorDoor = GameObject.FindWithTag("Socket Interactor Door").GetComponent<XRSocketInteractor>();
        xRSocketInteractorScrewTop = GameObject.FindWithTag("Socket Interactor Screw Top").GetComponent<XRSocketInteractor>();
        xRSocketInteractorScrewBottom = GameObject.FindWithTag("Socket Interactor Screw Bottom ").GetComponent<XRSocketInteractor>();

        xRSocketInteractorDoor_GO = xRSocketInteractorDoor.gameObject;
        xRSocketInteractorScrewTop_GO = xRSocketInteractorScrewTop.gameObject;
        xRSocketInteractorScrewBottom_GO = xRSocketInteractorScrewBottom.gameObject;

      

}


private void OnEnable()
    {
        // Define steps, each step is tied to a specific method
        steps.Add(Step1);
        steps.Add(Step2);
        steps.Add(Step3);
        steps.Add(Step4);
        steps.Add(Step5);
        steps.Add(Step6);
        steps.Add(Step7);
        steps.Add(Step8);
        steps.Add(Step9);
        steps.Add(Step10);
        steps.Add(Step11);
        steps.Add(Step12);
        steps.Add(Step13);
        steps.Add(Step14);
        steps.Add(Step15);

        
        // Subscribe to the select and activate events
        grabInteractableDrillBody.selectEntered.AddListener(OnSelectEnteredgrabInteractableDrillBody);
        grabInteractableDrillBody.activated.AddListener(OnActivatedTriggerInteractableDrillBody);
        grabInteractableDrillBody.deactivated.AddListener(OnDeActivatedTriggerInteractableDrillBody);
        grabInteractableScrew_Cross_Top.selectEntered.AddListener(OnSelectEnteredgrabInteractableScrew_Cross_Top);
        grabInteractableScrew_Cross_Bottom.selectEntered.AddListener(OnSelectEnteredgrabInteractableScrew_Cross_Bottom);
        grabInteractableDoor.selectEntered.AddListener(OnSelectEnteredgrabInteractableDoor);
        


        //Deactivate On Start
        grabInteractableScrew_Cross_Top.enabled = false;
        grabInteractableDoor.enabled = false;
        grabInteractableScrew_Cross_Bottom.enabled = false;
    }

    private void OnDestroy()
    {
        OnStepCompleted -= ExecuteNextStep;

        // Unsubscribe from the events when this object is destroyed
        if (grabInteractableDrillBody == null || grabInteractableScrew_Cross_Top == null 
            || grabInteractableScrew_Cross_Bottom == null || grabInteractableDoor == null)

        {
            grabInteractableDrillBody.selectEntered.AddListener(OnSelectEnteredgrabInteractableDrillBody);
            grabInteractableDrillBody.activated.AddListener(OnActivatedTriggerInteractableDrillBody);
            grabInteractableDrillBody.deactivated.AddListener(OnDeActivatedTriggerInteractableDrillBody);
            grabInteractableScrew_Cross_Top.selectEntered.AddListener(OnSelectEnteredgrabInteractableScrew_Cross_Top);
            grabInteractableScrew_Cross_Bottom.selectEntered.AddListener(OnSelectEnteredgrabInteractableScrew_Cross_Bottom);
            grabInteractableDoor.selectEntered.AddListener(OnSelectEnteredgrabInteractableDoor);

        }
    }

    


void Start()
    {

        OnStepCompleted += ExecuteNextStep;

        // Start the process with the first step
        ExecuteNextStep();
    }

    public void ExecuteStep(string instructionKey)
    {
        Debug.Log($"Executing step: {instructionKey}");
        SceneManagerScript.Instance.InstructionManager.UpdateInstruction( instructionKey );
        SceneManagerScript.Instance.SoundManager.audioSourceCorrect.Play();
        steps[currentStepIndex]?.Invoke();
    }

    void ExecuteNextStep()
    {
        Debug.Log("All steps Nunber :----------------------" + currentStepIndex);
        if (currentStepIndex < steps.Count)
        {
            ExecuteStep($"Inst{currentStepIndex + 1}"); // Assuming instruction keys are "Inst1", "Inst2", etc.
            currentStepIndex++;
        }
        else
        {
            Debug.Log("All steps completed!");
        }
    }

    // Method to invoke OnStepCompleted event
    public void CompleteStep()
    {
        OnStepCompleted?.Invoke();
    }







    // Individual step methods
    void Step1()
    {
        Debug.Log("Performing Step 1 actions.");
        
        // Calling OnSelectEnteredgrabInteractableDrillBody
    }

    // This method is called when the object is grabbed/selected
    private void OnSelectEnteredgrabInteractableDrillBody(SelectEnterEventArgs args)
    {
        if (!IsGripPressedFirstTime)
        {
            Debug.Log("DrillGripPressedFirstTime");
            IsGripPressedFirstTime = true;
            // Notify that step 1 is complete
            CompleteStep();
        }
        
    }

    void Step2()
    {
        Debug.Log("Performing Step 2 actions.");
        // Put your specific logic for Step 2 here

    }

    // This method is called when the object is activated (typically when the trigger is pressed)
    private void OnActivatedTriggerInteractableDrillBody(ActivateEventArgs args)
    {
        SceneManagerScript.Instance.DrillBitRotationManager.EnableDrill(true);
        SceneManagerScript.Instance.SoundManager.DrillSound(true);
        if (!IsTriggerPressedFirstTime)
        {
            SceneManagerScript.Instance.ScrewRotationWithCollisionTop.isCompleted = false;
            Debug.Log("TriggerPressedFirstTime");
            IsTriggerPressedFirstTime = true;
            // Notify that step 2 is complete
            CompleteStep();
        }
    }
    private void OnDeActivatedTriggerInteractableDrillBody(DeactivateEventArgs args)
    {
        SceneManagerScript.Instance.DrillBitRotationManager.EnableDrill(false);
        SceneManagerScript.Instance.SoundManager.DrillSound(false);
        
    }

    void Step3() 
    { 
        SceneManagerScript.Instance.DrillBitRotationManager.DrillBitPrefab.GetComponent<MeshCollider>().isTrigger = true;
        Debug.Log("Performing Step 3 actions.");
        //unscrew the bolt
    }
    void Step4()
    {
        grabInteractableScrew_Cross_Top.enabled = true;
        Debug.Log("Performing Step 4 actions.");
    }
    private void OnSelectEnteredgrabInteractableScrew_Cross_Top(SelectEnterEventArgs args)
    {
        if (currentStepIndex < 4 ) 
            return;

        SceneManagerScript.Instance.ScrewRotationWithCollisionBottom.isCompleted = false;

        if (TakeOutTopScrew ) 
        {
            Debug.Log("TakeOutTopScrew");
            TakeOutTopScrew = false;    
            Debug.Log("Takeout Top Bolt");
            CompleteStep();
        }
        
        if (PlaceBackTopScrew)
        {
            PlaceBackTopScrew = false;
            Debug.Log("Pick up the top screw and carefully position it in its place.");
            SceneManagerScript.Instance.ScrewRotationWithCollisionTop.isScrewIn = true;
            CompleteStep();
        }
    }
    void Step5() 
    {
        //drill out bottom screw
        xRSocketInteractorScrewTop.enabled = false;
        Debug.Log("Performing Step 5 actions.");  
    }
    void Step6() 
    {
        grabInteractableScrew_Cross_Bottom.enabled = true;


        //take out bottom screw
        Debug.Log("Performing Step 6 actions.");  
    }
    private void OnSelectEnteredgrabInteractableScrew_Cross_Bottom(SelectEnterEventArgs args)
    {
        if (currentStepIndex < 6 ) 
            return;

        if (TakeOutBottomScrew)
        {
            TakeOutBottomScrew = false;
            Debug.Log("Bottom bolt takeout");
            CompleteStep();
        }
        if (PlaceBackBottomScrew)
        {
            PlaceBackBottomScrew = false;
            Debug.Log("Pick up the top screw and carefully position it in its place.");
            SceneManagerScript.Instance.ScrewRotationWithCollisionBottom.isScrewIn = true;
            CompleteStep();
        }
    }
    void Step7() 
    { 
        Debug.Log("Performing Step 7 actions.");
        //Take Out Door
        xRSocketInteractorScrewBottom.enabled = false;
        grabInteractableDoor.enabled = true;
    }
    private void OnSelectEnteredgrabInteractableDoor(SelectEnterEventArgs args)
    {
        if (currentStepIndex < 7 )
            return;
        if (TakeOutDoor)
        {
            TakeOutDoor = false;
            Debug.Log("Next, grab the Door Wheel handle with your left hand and remove it");
            xRSocketInteractorDoor.enabled = false;
            CompleteStep();
        }
        if (PlaceBackDoor)
        {
            Debug.Log("Place the vault door back onto the vault.");
            CompleteStep();
        }
    }
    void Step8() 
    { 
        Debug.Log("Performing Step 8 actions.");
        //Congratulation
        // Start the coroutine
        StartCoroutine(CallFunctionAfterDelayCongratulation(5.0f));
    }
    private IEnumerator CallFunctionAfterDelayCongratulation(float delay)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delay);

        CompleteStep();
    }
    void Step9() 
    { 
        Debug.Log("Performing Step 9 actions.");
        //NextStepInst
        // Start the coroutine
        StartCoroutine(CallFunctionAfterDelay(5.0f));
    }
    private IEnumerator CallFunctionAfterDelay(float delay)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delay);

        CompleteStep();
    }
    void Step10()
    {
        xRSocketInteractorDoor.enabled = true;
        PlaceBackDoor = true;
        Debug.Log("Performing Step 10 actions.");
    }
    
    void Step11() 
    {
        xRSocketInteractorScrewTop.enabled = true;
        PlaceBackTopScrew = true;
        Debug.Log("Performing Step 11 actions.");  
    }
    
    void Step12() 
    {

        Debug.Log("Performing Step 12 actions.");  
    }
    void Step13() 
    {

        xRSocketInteractorScrewBottom.enabled = true;
        PlaceBackBottomScrew = true;
        Debug.Log("Performing Step 13 actions.");  
    }
    
    void Step14() 
    {

        Debug.Log("Performing Step 14 actions.");  
    }
    void Step15() {
        grabInteractableScrew_Cross_Bottom.enabled = false;
        grabInteractableScrew_Cross_Top.enabled = false;
        grabInteractableDoor.enabled = false;

        Debug.Log("Performing Step 15 actions.");  
    }

    
}