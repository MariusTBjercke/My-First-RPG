using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTracker : MonoBehaviour
{

    #region Singleton

    public static QuestTracker instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public Text topCenterText;
    public Text questName;
    public Text questDescription;
    public Text goalDescription;
    public Text goalAmount;
    public Text completedText;

    public GameObject questTracker;

    public List<int> finishedQuests = new List<int>();

    public float delay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        finishedQuests.Add(0);
        NpcDialogue.newQuestAdded += NewQuestPopup;
        NpcDialogue.questFinished += disableTrackerAddToFinished;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Quest>() != null)
        {
            Quest quest = GetComponent<Quest>();
            questTracker.SetActive(true);
            questName.text = quest.QuestName;
            questDescription.text = quest.Description;
            foreach (var q in quest.Goals)
            {
                goalDescription.text = q.Description;
                goalAmount.text = q.CurrentAmount + " of " + q.RequiredAmount;
                if (q.Completed)
                {
                    completedText.text = "Quest Completed!";
                } else
                {
                    completedText.text = "";
                }
            }
        }
    }

    void disableTrackerAddToFinished(Quest quest)
    {
        questName.text = "";
        questDescription.text = "";
        goalDescription.text = "";
        goalAmount.text = "";
        completedText.text = "";
        questTracker.SetActive(false);

        // Add to list
        finishedQuests.Add(quest.QuestID);
    }

    void NewQuestPopup()
    {
        topCenterText.text = "New Quest Was Added!";
        StartCoroutine(DisableText(delay));
    }

    IEnumerator DisableText(float delay)
    {
        yield return new WaitForSeconds(delay);

        topCenterText.text = "";
    }

}
