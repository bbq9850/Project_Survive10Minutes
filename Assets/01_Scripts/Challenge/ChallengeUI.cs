using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeUI : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] ChallengeItemUI itemPrefab;

    List<ChallengeItemUI> items = new List<ChallengeItemUI>();

    void Start()
    {
        foreach (var ch in ChallengeManager.Instance.Challenges)
        {
            var item = Instantiate(itemPrefab, content);
            item.Init(ch);
            items.Add(item);
        }
    }

    void OnEnable()
    {
        RefreshAll();
    }

    public void RefreshAll()
    {
        foreach (var item in items)
        {
            item.Refresh();
        }
    }
}
