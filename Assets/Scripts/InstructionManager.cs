using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionManager : MonoBehaviour
{

    private TextMeshProUGUI m_TextMeshProUGUI;
    
    

    void Awake()
    {
        m_TextMeshProUGUI = GameObject.FindWithTag("Text (TMP)-Instruction").GetComponent<TextMeshProUGUI>();
        
    }
    // Dictionary to hold instructions
    private Dictionary<string, string> instructions = new Dictionary<string, string>()
    {
        { "Inst1", "Welcome! Start by picking up the Drill Machine using the <b>Grip</b> button on your right hand." },
        { "Inst2", "Great job! Now press or release the <b>Trigger</b> button to start or stop operating the drill." },
        { "Inst3", "Next, bring the drill close to the top screw and begin unscrewing it with the <b>Trigger</b> button." },
        { "Inst4", "Once the screw is unscrewed, use your left hand to remove it and place it on the table." },
        { "Inst5", "Now, position the drill near the bottom screw and start unscrewing it." },
        { "Inst6", "Again, use your left hand to remove the screw and place it on the table." },
        { "Inst7", "Next, grab the Door Wheel handle with your left hand and remove it from the vault and keep it holding." },
        { "Inst8", "<b>Congratulations!</b> You have successfully opened the vault." },
        { "Inst9", "Now, let's move on to fixing the vault back in place." },
        { "Inst10", "Place the vault door back onto the vault." },
        { "Inst11", "Pick up the top screw and carefully position it in its place." },
        { "Inst12", "Use the drill machine to screw the bolt into place." },
        { "Inst13", "Pick up the bottom screw and carefully position it in its place." },
        { "Inst14", "Repeat the process to screw the bolt into place." },
        { "Inst15", "<b>Congratulations!</b> You have successfully fixed the vault." }
    };


    // Function to update the instruction text based on the key
    public void UpdateInstruction(string key)
    {
        Debug.Log($"Executing step UpdateInstruction: {key}");

        if (instructions.TryGetValue(key, out string instruction))
        {
            if (m_TextMeshProUGUI.text != instruction)
            {
                m_TextMeshProUGUI.text = instruction;
            }
        }
        else
        {
            m_TextMeshProUGUI.text = "Error, Please Restart!!";
        }
    }
}
