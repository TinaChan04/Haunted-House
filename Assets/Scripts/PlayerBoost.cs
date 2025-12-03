using UnityEngine;
using UnityEngine.UI;

public class PlayerBoost : MonoBehaviour
{
    [Header("Boost Settings")]
    public float boostMultiplier = 3f;
    public float boostDuration = 2f;
    public float boostCooldown = 5f;

    [Header("UI")]
    public Image boostIcon;

    private bool boostReady = true;
    private bool isBoosting = false;

    private float boostTimer = 0f;
    private float cooldownTimer = 0f;

    private PlayerMovement movement;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        UpdateIcon();
    }

    void Update()
    {
        // Boost duration countdown
        if (isBoosting)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0f)
                EndBoost();
        }

        // Cooldown countdown
        if (!boostReady && !isBoosting)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                boostReady = true;
                if (boostIcon) boostIcon.gameObject.SetActive(true);
            }
        }

        UpdateIcon();
    }

    public void ActivateBoost()
    {
        if (!boostReady) return;

        isBoosting = true;
        boostReady = false;

        boostTimer = boostDuration;
        movement.currentSpeedMultiplier = boostMultiplier;

        if (boostIcon) boostIcon.gameObject.SetActive(false);
    }

    void EndBoost()
    {
        isBoosting = false;

        movement.currentSpeedMultiplier = 1f;
        cooldownTimer = boostCooldown;
    }

    void UpdateIcon()
    {
        if (!boostIcon) return;

        if (isBoosting)
            boostIcon.color = Color.cyan;
        else if (boostReady)
            boostIcon.color = Color.white;
        else
            boostIcon.color = new Color(1,1,1,0.3f);
    }
    public bool IsBoosting()
    {
        return isBoosting;
    }
}
