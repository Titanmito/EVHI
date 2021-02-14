using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddNamesToDropdown : MonoBehaviour
{
    public Dropdown dropdown;
    public Button ContinueButton;
    [HideInInspector]
    public Dictionary<Dropdown.OptionData, int> MapOptionsToID;
    // Start is called before the first frame update
    void Start()
    {
        dropdown.interactable = true;
        ContinueButton.interactable = true;
        MapOptionsToID = new Dictionary<Dropdown.OptionData, int>();
        dropdown.options = new List<Dropdown.OptionData>();
        List<User> users = GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().users;
        foreach (User user in users)
        {
            Dropdown.OptionData option = new Dropdown.OptionData(user.Name + " (ID : " + user.UserID + ")");
            dropdown.options.Add(option);
            MapOptionsToID.Add(option, user.UserID);
        }
        if (dropdown.options.Count == 0)
        {
            dropdown.options.Add(new Dropdown.OptionData("Pas de prénoms sauvegardés"));
            dropdown.interactable = false;
            ContinueButton.interactable = false;
        }
    }
}
