using System;
using System.Collections;
using ExtensionMethods;
using TMPro;
using UnityEngine;

public class QuestRewardUI : MonoBehaviour
{
    public TextMeshProUGUI questNameText;
    public Transform itemRewardListPanel;
    public Transform rewardUIPanel;
    public RewardItemUI itemRewardPrefab;
    public QuestEventChannel onCompleteQuestEventChannel;

    private void Awake()
    {
        onCompleteQuestEventChannel.Listeners += DisplayReward;
    }

    private void OnDestroy()
    {
        onCompleteQuestEventChannel.Listeners -= DisplayReward;
    }

    

    private void DisplayReward(ActiveQuest quest)
    {
        StartCoroutine(DisplayRewardCo(quest));
        // NewMethod(quest);
    }

    private IEnumerator DisplayRewardCo(ActiveQuest quest)
    {
        yield return new WaitForSeconds(1f);
        NewMethod(quest);

        yield return new WaitForSeconds(4f);
        HideReward();
    }

    private void NewMethod(ActiveQuest quest)
    {
        rewardUIPanel.gameObject.SetActive(true);
        // questNameText.text = "Congratulations! You have completed the " + quest.Quest.questName + " Quest!";
        questNameText.text = "" + quest.Quest.questName;

        // "You have been awarded with: "; add this to a child in the list

        itemRewardListPanel.Clear();
        foreach (var item in quest.Quest.rewardItems)
        {
            var rewardUI = Instantiate(itemRewardPrefab, itemRewardListPanel);
            rewardUI.SetItem(item);
        }
    }

    private void HideReward()
    {
        rewardUIPanel.gameObject.SetActive(false);
    }
}