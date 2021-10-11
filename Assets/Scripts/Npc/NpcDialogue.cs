using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialogue : Interactable
{

    public Text text;

    public Dialogue dialogue;

    public ToolTip toolTip;

    PlayerStats playerStats;

    // Questing
    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }

    [SerializeField]
    private GameObject quests;
    [SerializeField]
    private string questType;
    private Quest Quest { get; set; }

    public int requiredQuestID;

    public delegate void NewQuestAdded();
    public static NewQuestAdded newQuestAdded;

    public delegate void QuestFinished(Quest quest);
    public static QuestFinished questFinished;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
            if (questType != "")
            {
                if (!AssignedQuest && !Helped)
                {
                    AssignQuest();
                }
                else if (AssignedQuest && !Helped)
                {
                    CheckQuest();
                }
            }
        }
    }

    void AssignQuest()
    {
        if (requiredQuestID == 0)
        {
            AssignedQuest = true;
            Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
            if (newQuestAdded != null)
                newQuestAdded();
        }
    }

    void CheckQuest()
    {
        if (Quest.Completed)
        {
            Quest.GiveReward();
            Helped = true;
            AssignedQuest = false;
            if (questFinished != null)
            {
                questFinished(Quest);
            }
        }
    }

    public override void Interact()
    {
        FindObjectOfType<TooltipManager>().StartTooltip(toolTip);
        inTrigger = true;
    }

    public override void Detach()
    {
        inTrigger = false;
        FindObjectOfType<TooltipManager>().EndTooltip();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this, Quest);
    }

}
