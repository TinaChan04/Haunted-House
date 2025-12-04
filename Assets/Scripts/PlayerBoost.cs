using UnityEngine;
using UnityEngine.UI;

public class PlayerBoost : MonoBehaviour
{
    public float boostMultiplier = 3f;
    public float boostTime = 2f;
    public float cooldownTime = 5f;

    public Text boostStatusText;
    public Text boostCooldownText;

    PlayerMovement move;

    bool isBoosting = false;
    bool canBoost = true;

    float boostCounter;
    float cooldownCounter;

    void Start()
    {
        move = GetComponent<PlayerMovement>();

        if (boostStatusText) boostStatusText.gameObject.SetActive(false);
        if (boostCooldownText) boostCooldownText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isBoosting)
        {
            boostCounter -= Time.deltaTime;
            if (boostCounter <= 0)
            {
                StopBoost();
            }
        }

        if (!canBoost && !isBoosting)
        {
            cooldownCounter -= Time.deltaTime;

            if (boostCooldownText)
                boostCooldownText.text = "Cooldown: " + Mathf.Ceil(cooldownCounter) + "s";

            if (cooldownCounter <= 0)
            {
                canBoost = true;
                if (boostCooldownText) boostCooldownText.gameObject.SetActive(false);
            }
        }
    }

    public void ActivateBoost()
    {
        if (!canBoost) return;

        isBoosting = true;
        canBoost = false;

        boostCounter = boostTime;
        move.currentSpeedMultiplier = boostMultiplier;

        if (boostStatusText)
        {
            boostStatusText.text = "Speed Boost Active!";
            boostStatusText.gameObject.SetActive(true);
        }
    }

    void StopBoost()
    {
        isBoosting = false;
        move.currentSpeedMultiplier = 1f;

        cooldownCounter = cooldownTime;

        if (boostStatusText) boostStatusText.gameObject.SetActive(false);
        if (boostCooldownText) boostCooldownText.gameObject.SetActive(true);
    }

    public bool IsBoosting()
    {
        return isBoosting;
    }
}