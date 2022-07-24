using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleBoard : MonoBehaviour {
    
    public Gusset[,] boardState = new Gusset[4, 4];
    public GameObject gussetPrefab;
    public List<int> possiblenumber;

    public Vector2 startPos;
    public int stepX, stepY;

    public static Action<Gusset> moveAction;

    public bool completed = false;

    public GameObject completedText;
    public TextMeshProUGUI timeText;
    private int nMoves = 0;
    public TextMeshProUGUI nMovesText;

    private void Awake() {
        moveAction += MoveGusset;
    }

    private void Start() {
        GenerateBoard();
    }

    public void GenerateBoard() {
        completed = false;
        while (true) {
            ClearBoard();
            InstantiateBoard();
            if (CheckIfSolvable()) {
                CheckWin();
                StartCoroutine(TimeToSolve());
                nMoves = 0;
                WriteNMoves();
                break;
            }
        }
    }

    public void InstantiateBoard() {
        for (int i = 0; i < boardState.Length; i++) {
            possiblenumber.Add(i);
        }
        for (int i = boardState.GetLowerBound(0); i <= boardState.GetUpperBound(0); i++) {
            for (int j = boardState.GetLowerBound(1); j <= boardState.GetUpperBound(1); j++) {
                int rnd = possiblenumber[UnityEngine.Random.Range(0, possiblenumber.Count)];
                possiblenumber.Remove(rnd);
                if (rnd == 0) {
                    boardState[i, j] = null;
                    continue;
                }
                var gussetObj = Instantiate(gussetPrefab, transform);
                gussetObj.transform.localPosition = startPos + new Vector2(stepX * j, stepY * i);
                Gusset gusset = gussetObj.GetComponent<Gusset>();
                boardState[i, j] = gusset;
                gusset.SetFaceNumber(rnd); 
            }
        }
    }

    public bool CheckIfSolvable() {
        List<Gusset> gussets = new();
        for (int i = boardState.GetLowerBound(0); i <= boardState.GetUpperBound(0); i++) {
            for (int j = boardState.GetLowerBound(1); j <= boardState.GetUpperBound(1); j++) {
                gussets.Add(boardState[i, j]);
            }
        }

        int invCount = 0;
        for (int i = 0; i < gussets.Count - 1; i++) {
            if (gussets[i] == null) continue;
            for (int j = i + 1; j < gussets.Count; j++) {
                if (gussets[j] == null) continue;
                if (gussets[i].faceNumber > gussets[j].faceNumber) {
                    invCount++;
                }
            }
        }

        if ((boardState.GetUpperBound(0) + 1) % 2 == 0) {
            // Even NRaw
            int nRaw = gussets.Count / (boardState.GetUpperBound(0) + 1);
            for (int i = 0; i < gussets.Count; i++) {
                if (gussets[i] == null) {
                    int raw = i / nRaw;
                    if (raw % 2 == 0) {
                        // Even Raw
                        return !(invCount % 2 == 0);
                    } else {
                        // Odd Raw
                        return invCount % 2 == 0;
                    }
                }
            }
            return false;
        } else {
            // Odd NRaw
            return !(invCount % 2 == 0);
        }
    }

    public void ClearBoard() {
        for (int i = boardState.GetLowerBound(0); i <= boardState.GetUpperBound(0); i++) {
            for (int j = boardState.GetLowerBound(1); j <= boardState.GetUpperBound(1); j++) {
                if (boardState[i, j] != null) {
                    Destroy(boardState[i, j].gameObject);
                    boardState[i, j] = null;
                }
            }
        }
    }

    public void CheckWin() {
        List<Gusset> gussets = new();
        for (int i = boardState.GetLowerBound(0); i <= boardState.GetUpperBound(0); i++) {
            for (int j = boardState.GetLowerBound(1); j <= boardState.GetUpperBound(1); j++) {
                gussets.Add(boardState[i, j]);
            }
        }

        var somethingDif = false;
        for (int i = 0; i < gussets.Count; i++) {
            if (gussets[i] == null) continue;
            if ((i + 1) != gussets[i].faceNumber) {
                somethingDif = true;
                gussets[i].HighLigth(false);
            } else {
                gussets[i].HighLigth(true);
            }
        }
        completed = !somethingDif;
        completedText.SetActive(completed);
    }

    public void MoveGusset(Gusset gussetToMove) {
        if (completed) return;
        for (int i = boardState.GetLowerBound(0); i <= boardState.GetUpperBound(0); i++) {
            for (int j = boardState.GetLowerBound(1); j <= boardState.GetUpperBound(1); j++) {
                if (boardState[i, j] != gussetToMove) continue;
                if (i + 1 <= boardState.GetUpperBound(0) && boardState[i + 1, j] == null) {
                    boardState[i + 1, j] = boardState[i, j];
                    boardState[i, j] = null;
                    boardState[i + 1, j].transform.localPosition = startPos + new Vector2(stepX * j, stepY * (i + 1));
                    nMoves++;
                    WriteNMoves();
                    goto End;
                }
                if (i - 1 >= 0 && boardState[i - 1, j] == null) {
                    boardState[i - 1, j] = boardState[i, j];
                    boardState[i, j] = null;
                    boardState[i - 1, j].transform.localPosition = startPos + new Vector2(stepX * j, stepY * (i - 1));
                    nMoves++;
                    WriteNMoves();
                    goto End;
                }
                if (j + 1 <= boardState.GetUpperBound(0) && boardState[i, j + 1] == null) {
                    boardState[i, j + 1] = boardState[i, j];
                    boardState[i, j] = null;
                    boardState[i, j + 1].transform.localPosition = startPos + new Vector2(stepX * (j + 1), stepY * i);
                    nMoves++;
                    WriteNMoves();
                    goto End;
                }
                if (j - 1 >= 0 && boardState[i, j - 1] == null) {
                    boardState[i, j - 1] = boardState[i, j];
                    boardState[i, j] = null;
                    boardState[i, j - 1].transform.localPosition = startPos + new Vector2(stepX * (j - 1), stepY * i);
                    nMoves++;
                    WriteNMoves();
                    goto End;
                }
            }
        }
        End:
        CheckWin();
    }

    public IEnumerator TimeToSolve() {
        DateTime startTime = DateTime.Now;
        while (!completed) {
            var passedTime = DateTime.Now - startTime;
            timeText.text = $"Time: {(int)passedTime.TotalSeconds}";
            yield return null;
        }
    }

    public void WriteNMoves() {
        nMovesText.text = $"Number of Moves: {nMoves}";
    }
}
