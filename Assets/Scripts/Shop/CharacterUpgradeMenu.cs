using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpgradeMenu : MonoBehaviour
{
    public static GameObject CharacterUpgradeMenuUi;
    public static bool isActive;
    private void Start()
    {
        isActive = true;
    }

    public static void ChangeView()
    {
        CharacterUpgradeMenuUi.SetActive(!isActive);
        isActive = !isActive;
    }
}
