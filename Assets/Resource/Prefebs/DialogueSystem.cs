using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speakerName;
        [TextArea(2, 4)] public string dialogueText;
    }
    public DialogueLine[] dialogueLines;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    private int currentLineIndex = 0;

    void Start()    
    {
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
    }
    public void ResetLine()
    {
        currentLineIndex = 0;
        ShowLine();
    }

    public void ShowLine()
    {
        dialogueBox.SetActive(true);
        if (currentLineIndex < dialogueLines.Length)
        {
            nameText.text = dialogueLines[currentLineIndex].speakerName;
            dialogueText.text = dialogueLines[currentLineIndex].dialogueText;
        }
    }

    void NextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length)
        {
            ShowLine();
        }
        else
        {
            dialogueBox.SetActive(false);
            dialogueText.text = "";
            nameText.text = "";
            Debug.Log("Dialogue finished.");
        }
    }
}
