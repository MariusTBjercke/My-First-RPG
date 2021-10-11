using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnarsWelcome : Quest
{

    void Start()
    {
        QuestID = 1;
        QuestName = "Gunnar's Welcome";
        Description = "Gunnar needs help killing monsters in the forest.";
        ItemReward = null;
        ExperienceReward = 100;

        Goals.Add(new KillGoal(this, 1, "Kill 2 Monsters", false, 0, 2));

        Goals.ForEach(g => g.Init());
    }

}
