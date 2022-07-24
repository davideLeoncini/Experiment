using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Gusset : MonoBehaviour, IPointerClickHandler {
    
    public int faceNumber;
    public TextMeshProUGUI faceNumberText;
    public Image BG;

    public void SetFaceNumber(int val) {
        faceNumber = val;
        faceNumberText.text = val.ToString();
    }

    public void OnPointerClick(PointerEventData eventData) {
        PuzzleBoard.moveAction.Invoke(this);
    }

    public void HighLigth(bool rigthPosition) {
        BG.color = rigthPosition ? Color.yellow : Color.white;
    }
}
