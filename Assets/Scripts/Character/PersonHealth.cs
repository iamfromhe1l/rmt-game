using System;
using System.Threading;
using Assets.Scripts;
using UnityEngine;

public class PersonHealth : MonoBehaviour
{
    private static Assets.Scripts.UpgradableParametr maxHealth;
    private static Assets.Scripts.UpgradableParametr healthRes;
    private static int _maxHealth;
    [SerializeField]
    public int _health;

    private void Awake()
    {
        maxHealth = ResetUpgradbleParam("maxHealth");
        _maxHealth = maxHealth._current;
        _health = _maxHealth;
        healthRes = ResetUpgradbleParam("healthRes");
    }

    private UpgradableParametr ResetUpgradbleParam(string perString)
    {
        UpgradableParametr perParam = new();
        perParam._current = PersonUPConfig.personLevels[perString][0];
        perParam._currentLvl = 0;
        perParam._lvlsDictionary = PersonUPConfig.personLevels[perString];
        return perParam;
    }

    public static UpgradableParametr Upgrade(string param)
    {
        if (param == "health")
        {
            UpgradableParametr res = UpgradeByParam(maxHealth);
            _maxHealth = maxHealth._current;
            return res;
        } else if (param == "healthRes")
        {
            return UpgradeByParam(healthRes);
        }
        throw new NotImplementedException();
    }
    private static UpgradableParametr UpgradeByParam(UpgradableParametr perParam)
    {
        perParam._currentLvl += 1;
        perParam._current = perParam._lvlsDictionary[perParam._currentLvl];
        return perParam;
    }
}