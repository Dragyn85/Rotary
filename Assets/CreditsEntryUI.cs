using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditsEntryUI : MonoBehaviour
{
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text type;
    
    private string description;
    private string webPage;

    public void Initialize(in CreditsItem creditsItem)
    {
        SetName(creditsItem.Creator);
        SetType(creditsItem.Type);
        SetHomePage(creditsItem.Link);
        SetDescription(creditsItem.Description);
        var button = GetComponentInChildren<Button>();
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        Application.OpenURL(webPage);
    }

    private void SetName(string name)
    {
        this.name.SetText(name);
    }

    private void SetType(string role)
    {
        this.type.SetText(role);
    }

    private void SetHomePage(string homePage)
    {
        webPage = homePage;
    }
    private void SetDescription(string description)
    {
        this.description = description;
    }
    
}