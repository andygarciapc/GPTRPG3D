using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Basic.Events;
using Basic.Quests.Manager;
public class KillXQuestStep : QuestStep
{
    private int enemiesKilled = 0;
    private int enemiesToKill = 5;
    private void Start()
    {
        UpdateState();
    }
    private void OnEnable()
    {
        GameEventsManager.instance.combatEvents.onEnemyKilled += EnemyKilled;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.combatEvents.onEnemyKilled -= EnemyKilled;
    }
    private void EnemyKilled()
    {
        if(enemiesKilled < enemiesToKill)
        {
            ++enemiesKilled;
            UpdateState();
            Debug.Log("Enemy Killed - Quest Advanced");
        }
        if(enemiesKilled >= enemiesToKill)
        {
            Debug.Log("Enemy Killed - Quest Completed");
            FinishQuestStep();
        }
    }
    private void UpdateState()
    {
        string state = enemiesKilled.ToString();
        string status = "Collected " + enemiesKilled + " / " + enemiesToKill + " coins.";
        ChangeState(state, status);
    }
    protected override void SetQuestStepState(string state)
    {
        this.enemiesKilled = System.Int32.Parse(state);
        UpdateState();
    }
}
