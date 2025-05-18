using System;
using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    [SerializeField] CreditsEntryUI creditsEntryUIPrefab;
    [SerializeField] CreditsList creditsListSO;
    [SerializeField] Transform creditsEntryContainer;

    private void Awake()
    {
        foreach (var credit in creditsListSO.Credits)
        {
            var creditsEntryUI = Instantiate(creditsEntryUIPrefab, creditsEntryContainer);
            creditsEntryUI.Initialize(credit);
        }
    }
}