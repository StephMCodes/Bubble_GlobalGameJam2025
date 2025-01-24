using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHealthSystem : MonoBehaviour
{
    public const int MAX_FRAGMENT_AMOUNT = 3;
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDead;



    private List<Heart> heartList;
    public HeartHealthSystem(int heartAmount)
    {
        heartList = new List<Heart>();

        for (int i = 0; i < heartAmount; i++)
        {
            Heart heart = new Heart(3);
            heartList.Add(heart);
        }

        // heartList[heartList.Count - 1].SetFragments(0);
    }

    public List<Heart> GetHeartList()
    {
        return heartList;
    }

    public void Damage(int damageAmount)
    { //cycle through all hearts starting from the end 
        for (int i = heartList.Count - 1; i >= 0; i--)
        {
            //Test if this heart can Absorb damage Amount 
            Heart heart = heartList[i];
            if (damageAmount > heart.GetFragmentAmount())
            {
                //Heart cannot Absorb full damage Amount, damage heart and keep going to the next heart
                damageAmount -= heart.GetFragmentAmount();
                heart.Damage(heart.GetFragmentAmount());
            }
            else
            {
                //Heart can absorb full damage , absorb and break out of the cycle  
                heart.Damage(damageAmount);
                break;
            }
        }

        if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);

        if (IsDead())
        {
            // FindObjectOfType<CheckpointSystem>().OnPlayerDead(gameObject, GetCurrentFragments());
            if (OnDead != null) OnDead(this, EventArgs.Empty);
            //respawnHandler.TriggerRespawn();
        }
    }

    public void Heal(int healAmount)
    {
        for (int i = 0; i < heartList.Count; i++)
        {
            Heart heart = heartList[i];
            int missingFragments = MAX_FRAGMENT_AMOUNT - heart.GetFragmentAmount();
            if (healAmount > missingFragments)
            {
                healAmount -= missingFragments;
                heart.Heal(missingFragments);
            }
            else
            {
                heart.Heal(healAmount);
                break;
            }
        }
        if (OnHealed != null) OnHealed(this, EventArgs.Empty);

    }

    private bool IsDead()
    {
        return heartList[0].GetFragmentAmount() == 0;
    }

    //represents a single heart
    public class Heart
    {
        private int fragments;
        public Heart(int fragments)
        {
            this.fragments = fragments;
        }

        public int GetFragmentAmount()
        {
            return fragments;
        }

        public void SetFragments(int fragments)
        {
            this.fragments = fragments;
        }

        public void Damage(int damageAmount)
        {
            if (damageAmount >= fragments)
            {
                fragments = 0;
            }

            else
            {
                fragments -= damageAmount;
            }
        }

        public void Heal(int healAmount)
        {
            if (fragments + healAmount >= MAX_FRAGMENT_AMOUNT)
            {
                fragments = MAX_FRAGMENT_AMOUNT;
            }
            else
            {
                fragments += healAmount;
            }
        }
    }
}
