using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerAccessibleLevels : MonoBehaviour
{
    public Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        User currentUser = GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().currentUser;
        if (currentUser != null)
        {
            dropdown.options = new List<Dropdown.OptionData>();
            if (currentUser.CurrentLevel == 0)
                dropdown.options.Add(new Dropdown.OptionData(GlobalVariables.levelTitlePrefix +
                    GlobalVariables.levelTitleSep + GlobalVariables.levelTitleTestPostfix));
            else
                for (int i = 1; i < currentUser.CurrentLevel + 1; i++)
                    dropdown.options.Add(new Dropdown.OptionData(GlobalVariables.levelTitlePrefix +
                        GlobalVariables.levelTitleSep + i.ToString()));
        }
    }
}
