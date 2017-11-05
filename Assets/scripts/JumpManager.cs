using UnityEngine;
using System.Collections;

public class JumpManager : MonoBehaviour
{
    public float rechargeDelayInSeconds;
    public AudioSource jumpSnd;
    public RectTransform jumpBar;

    float currentJumpPoints, maxJumpPoints, jumpRechargeTime;
    bool wingsAreOpen, currentlyJumping, jumpAllowed, cooldowned, cooldownHappening;
    float jumpBarMaxHeight;

    void Awake()
    {
        currentlyJumping = false;
        jumpAllowed = false;
        cooldowned = true;
        cooldownHappening = false;
        
        jumpBarMaxHeight = jumpBar.rect.height;
    }

    public void setJumpStats(float maxJumpPoints, float jumpRechargeTime)
    {
        this.maxJumpPoints = maxJumpPoints;
        currentJumpPoints = maxJumpPoints;

        this.jumpRechargeTime = jumpRechargeTime;
    }

    public void setWingsState(bool wingsAreOpen)
    {
        this.wingsAreOpen = wingsAreOpen;
    }

    public bool isJumping()
    {
        return currentlyJumping;
    }

    void Update()
    {
        ///DEBUG
        currentJumpPoints = maxJumpPoints;
        ///DEBUG

        jumpAllowed = currentJumpPoints > 0;

        if (Input.GetButton("jump") && jumpAllowed)
        {
            cooldowned = false;
            currentlyJumping = true;

            if (!jumpSnd.isPlaying)
            {
                jumpSnd.Play();
            }
        }
        else
        {
            currentlyJumping = false;

            if (!cooldowned)
                StartCoroutine(jumpRechargeCooldown());

            jumpSnd.Stop();
        }

        // As a convention, jumping consumes 100 jump points per second
        if (currentlyJumping)
        {
            currentJumpPoints -= Time.deltaTime * 100;
        }
        else if (cooldowned && currentJumpPoints < maxJumpPoints)
        {
            currentJumpPoints += maxJumpPoints * Time.deltaTime / jumpRechargeTime;
        }

        currentJumpPoints = Mathf.Clamp(currentJumpPoints, 0, maxJumpPoints);

        jumpBar.sizeDelta = new Vector2(jumpBar.sizeDelta.x, jumpBarMaxHeight * currentJumpPoints / maxJumpPoints);
    }

    IEnumerator jumpRechargeCooldown()
    {
        if (!cooldownHappening)
        {
            cooldownHappening = true;
            yield return new WaitForSeconds(rechargeDelayInSeconds);
            cooldowned = true;
            cooldownHappening = false;
        }
        else
            yield return new WaitForSeconds(0);
    }
}

