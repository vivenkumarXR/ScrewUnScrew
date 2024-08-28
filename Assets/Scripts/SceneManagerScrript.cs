using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.Rendering.DebugUI;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript Instance { get; private set; }

    [SerializeField]
    public DrillBitRotataionManager DrillBitRotationManager;
    [SerializeField]
    public SoundManager SoundManager;
    [SerializeField]
    public InstructionManager InstructionManager;
    [SerializeField]
    public ProcessStepHandler ProcessStepHandler;
    [SerializeField]
    public ScrewRotationWithCollisionBottom ScrewRotationWithCollisionBottom;
    [SerializeField]
    public ScrewRotationWithCollisionTop ScrewRotationWithCollisionTop;


    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Optionally persist across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // Ensure DrillBitRotationManager is properly assigned (if not done via Inspector)
        if (DrillBitRotationManager == null)
        {
            DrillBitRotationManager = FindObjectOfType<DrillBitRotataionManager>();
        }
        if (SoundManager == null)
        {
            SoundManager = FindObjectOfType<SoundManager>();
        }
        if (InstructionManager == null)
        {
            InstructionManager = FindObjectOfType<InstructionManager>();
        }
        if (ProcessStepHandler == null)
        {
            ProcessStepHandler = FindObjectOfType<ProcessStepHandler>();
        }
        if (ScrewRotationWithCollisionBottom == null)
        {
            ScrewRotationWithCollisionBottom = FindObjectOfType<ScrewRotationWithCollisionBottom>();
        }
        if (ScrewRotationWithCollisionTop == null)
        {
            ScrewRotationWithCollisionTop = FindObjectOfType<ScrewRotationWithCollisionTop>();
        }

    }



}
