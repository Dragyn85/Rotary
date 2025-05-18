using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreditsList", menuName = "Scriptable Objects/CreditsList")]
public class CreditsList : ScriptableObject
{
    public List<CreditsItem> Credits;
}

[Serializable]
public class CreditsItem
{
    public string Type;
    public string Creator;
    public string Description;
    public string Link;
}