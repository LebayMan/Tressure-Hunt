using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerTypingChallenge : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera typingCamera;

    [Header("UI Settings")]
    [SerializeField] private Canvas typingCanvas;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text targetQuestionText;

    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float attackAngle = 45f;
    [SerializeField] private float attackCooldown = 0.5f;

    [Header("Math Challenge")]
    [SerializeField] private int minNumber = 1;
    [SerializeField] private int maxNumber = 10;

    public Animator SwordAnimator;

    private float lastAttackTime;
    private int currentAnswer;
    private bool isChallengeActive = false;
    private GameObject currentEnemy;
    private Enemy enemy;  
    private SimplePlayerUI playerUI; 

    void Start()
    {
        typingCanvas.enabled = false;
        typingCamera.enabled = false;
        mainCamera.enabled = true;
        playerUI = GetComponent<SimplePlayerUI>();
        inputField.onEndEdit.AddListener(CheckAnswer);
    }

    void Update()
    {
        if (!isChallengeActive && Input.GetMouseButtonDown(0) &&
            Time.time >= lastAttackTime + attackCooldown)
        {
            PerformConeAttack();
            lastAttackTime = Time.time;
        }
    }

    void PerformConeAttack()
    {
        SwordAnimator.SetTrigger("Attack");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider hit in hitColliders)
        {
            Vector3 directionToTarget = (hit.transform.position - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
            float distanceToTarget = Vector3.Distance(transform.position, hit.transform.position);

            if (angleToTarget <= attackAngle / 2 && distanceToTarget <= attackRange)
            {
                enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    if (enemy.IsDead || playerUI.currentHealth <= 0) return;
                    enemy.Panel.SetActive(true);
                    StartChallenge(enemy);
                    break;
                }
            }
        }
    }

    void StartChallenge(Enemy enemy)
    {
        Debug.Log("Challenge started with enemy: " + enemy.name);
        currentEnemy = enemy.gameObject;
        isChallengeActive = true;

        mainCamera.enabled = false;
        typingCamera.enabled = true;

        typingCanvas.enabled = true;

        GenerateMathQuestion();
        inputField.text = "";
        inputField.ActivateInputField();

        Time.timeScale = 0f;
    }

    void GenerateMathQuestion()
    {
        int a = Random.Range(minNumber, maxNumber + 1);
        int b = Random.Range(minNumber, maxNumber + 1);
        currentAnswer = a + b;

        targetQuestionText.text = $"{a} + {b} = ?";
    }

    void CheckAnswer(string typedInput)
    {
        if (!int.TryParse(typedInput, out int playerAnswer))
        {
            inputField.text = "";
            inputField.ActivateInputField();
            Debug.Log("Not a number. Try again.");
            return;
        }

        if (playerAnswer == currentAnswer)
        {
            if (currentEnemy != null)
            {
                    enemy.TakeDamage(30);
                    playerUI.AddExp(10);
                    Debug.Log("Correct! Dealt damage.");
                    if (enemy != null && enemy.IsDead)
                    {
                        EndChallenge();
                        return;
                    }
                    // If enemy is still alive, generate another question
                    GenerateMathQuestion();
                    inputField.text = "";
                    inputField.ActivateInputField();
                
            }
        }
        else
        {
            playerUI.AddHealth(-10);
            Debug.Log("Incorrect! Try again.");
            inputField.text = "";
            inputField.ActivateInputField();
        }
    }

    public void EndChallenge()
    {
        Debug.Log("Challenge ended.");
        isChallengeActive = false;
        typingCanvas.enabled = false;
        typingCamera.enabled = false;
        mainCamera.enabled = true;
        Time.timeScale = 1f;
        currentEnemy = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 forward = transform.forward * attackRange;
        Quaternion leftRot = Quaternion.Euler(0, -attackAngle / 2, 0);
        Quaternion rightRot = Quaternion.Euler(0, attackAngle / 2, 0);

        Vector3 leftRay = leftRot * transform.forward * attackRange;
        Vector3 rightRay = rightRot * transform.forward * attackRange;

        Gizmos.DrawRay(transform.position, leftRay);
        Gizmos.DrawRay(transform.position, rightRay);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
