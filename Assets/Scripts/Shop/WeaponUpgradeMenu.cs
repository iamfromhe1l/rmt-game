using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeMenu : MonoBehaviour
{
    public static GameObject WeaponUpgradeMenuUi;
    public static bool isActive;
    private void Start()
    {
        isActive = false;
    }

    public static void ChangeView()
    {
        WeaponUpgradeMenuUi.SetActive(!isActive);
        isActive = !isActive;
    }
}
