using System;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Sound Effects")]
    [SerializeField] private AudioClip badMushroomSound;
    [SerializeField] private AudioClip goodMushroomSound;
    [Header("Controls")]
    [SerializeField] private PlayerInput playerInputScript;
    [SerializeField] private CinemachineInputAxisController cameraInputController;

    private int health = 3;
    private int score;
    private bool isGameEnded;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        UIManager.Instance?.UpdateHealth(health);
        UIManager.Instance?.UpdateScore(score);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGameEnded) return;

        if (other.TryGetComponent(out MushroomScript mushroom))
        {
            if (mushroom.type == MushroomScript.MushroomType.Bad)
            {
                health--;
                if(badMushroomSound != null) audioSource.PlayOneShot(badMushroomSound);
            }
            else
            {
                health++;
                score++;
                if (goodMushroomSound != null) audioSource.PlayOneShot(goodMushroomSound);
            }

            UIManager.Instance?.UpdateHealth(health);
            UIManager.Instance?.UpdateScore(score);

            // Знищення гриба і генерація нового замість знищеного
            mushroom.Consume();

            // Завершення гри
            if (health <= 0) TriggerEndGame(false);
            else if (score >= 10) TriggerEndGame(true);
        }
    }

    private void TriggerEndGame(bool isWin)
    {
        isGameEnded = true;

        StopPlayerAndInput();
        UIManager.Instance?.ShowEndGameScreen(isWin);
    }

    private void StopPlayerAndInput()
    {
        // Деактивація скрипта керування персонажем
        playerInputScript.enabled = false;
        // Зупинка інерції руху персонажа
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
        }
        if (TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.StopMovement();
        }
        // Деактивація обертання камери
        cameraInputController.enabled = false;
    }
}