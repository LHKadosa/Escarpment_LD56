using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;
    public float typingSpeed = 0.025f;
    public float timeBetweenLines = 0.5f;
    public float endDialogDelay = 2.0f;

    private Queue<string> dialogLines;
    private bool isDialogActive;


    void Awake()
    {
        dialogLines = new Queue<string>();
        dialogBox.SetActive(false);
        isDialogActive = false;
    }

    public void StartDialog(List<string> lines)
    {
        if (isDialogActive)
        {
            Debug.LogWarning("Dialog is already active!");
            return;
        }

        dialogBox.SetActive(true);
        dialogLines.Clear();
        dialogText.text = "";
        isDialogActive = true;

        foreach (string line in lines)
        {
            dialogLines.Enqueue(line);
        }

        StartCoroutine(DisplayNextLineWithDelay());
    }

    IEnumerator DisplayNextLineWithDelay()
    {
        while (dialogLines.Count > 0)
        {
            string nextLine = dialogLines.Dequeue();
            yield return StartCoroutine(TypeLine(nextLine));

            yield return new WaitForSeconds(timeBetweenLines);
        }

        yield return new WaitForSeconds(endDialogDelay);
        EndDialog();
    }

    IEnumerator TypeLine(string line)
    {
        foreach (char letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        dialogText.text += "\n";
    }

    void EndDialog()
    {
        dialogBox.SetActive(false);
        isDialogActive = false;
    }

    public void OnSkip()
    {
        // TODO: skip dialog
    }
}
