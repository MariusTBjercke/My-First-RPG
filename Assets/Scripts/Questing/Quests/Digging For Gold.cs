using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingForGold : Quest
{

    void Start()
    {
        RequiredQuestID = 1;
        QuestID = 2;
        QuestName = "Digging For Gold";
        Description = "The Unknown man wants you to kill slimes.";
        ItemReward = null;
        ExperienceReward = 100;

        Goals.Add(new KillGoal(this, 1, "Kill 2 Slimes", false, 0, 2));

        Goals.ForEach(g => g.Init());
    }

}
