using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour
{

    public List<Goal> Goals { get; set; } = new List<Goal>();
    public int RequiredQuestID { get; set; }
    public int QuestID { get; set; }
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int ExperienceReward { get; set; }
    public Item ItemReward { get; set; }
    public bool Completed { get; set; }

    public delegate void ExperienceAdded(int XP);
    public static ExperienceAdded experienceAdded;

    public void CheckGoals()
    {
        Completed = Goals.All(g => true);
        //if (Completed) GiveReward();
    }

    public void GiveReward()
    {
        PlayerManager.instance.player.GetComponent<PlayerStats>().AddExperience(ExperienceReward);
        if (experienceAdded != null)
        {
            experienceAdded(ExperienceReward);
        }
        Destroy(this);
    }

}
