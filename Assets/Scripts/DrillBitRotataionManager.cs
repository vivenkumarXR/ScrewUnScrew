using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBitRotataionManager : MonoBehaviour
{
    private Animator AnimatorDrillBit;
    public GameObject DrillBitPrefab;

    void Awake()
    {
        AnimatorDrillBit = GameObject.FindWithTag("DrillBit").GetComponent<Animator>();
        DrillBitPrefab = AnimatorDrillBit.gameObject;
    }
    public void EnableDrill(bool value)
    {
        AnimatorDrillBit.enabled = value;
    }

    

    public void DebugLogs(string value)
    {
        Debug.Log(value);
    }
}
