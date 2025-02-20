using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScanManager : MonoBehaviour
{
    public GameObject talkPanel;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public bool isScan;

    public void Scan(GameObject scanObj)
    {
        if (isScan)
        {
            isScan = false;
        }
        else
        {
            isScan = true;
            scanObject = scanObj;
            talkText.text = "�̰��� " + scanObj.name + "�Դϴ�.";
        }

        talkPanel.SetActive(isScan);
    }
}
