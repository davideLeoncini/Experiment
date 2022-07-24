using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalFaceCounter : MonoBehaviour {

    static public Action<int> showResult;
    
    public TextMeshProUGUI resultText;

    private void Awake() {
        showResult += AddFace;
    }

    void AddFace(int result) {
        resultText.text = result.ToString();
    }
}
