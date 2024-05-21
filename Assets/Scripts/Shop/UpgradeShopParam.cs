using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeShopParam : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textCur;
    [SerializeField]
    private TMP_Text _textNext;
    [SerializeField]
    private string _paramString;
    [SerializeField]
    private string _pointString;
    private bool _isMax;

    private void Start()
    {
        _isMax = false;
        if (_pointString == "sword")
        {
            _textCur.text = WeaponConfig.swordLevels[_paramString][0].ToString();
            _textNext.text = WeaponConfig.swordLevels[_paramString][1].ToString();
        } else if (_pointString == "axe")
        {
            _textCur.text = WeaponConfig.axeLevels[_paramString][0].ToString();
            _textNext.text = WeaponConfig.axeLevels[_paramString][1].ToString();
        } else if (_pointString == "fire")
        {
            _textCur.text = WeaponConfig.fireLevels[_paramString][0].ToString();
            _textNext.text = WeaponConfig.fireLevels[_paramString][1].ToString();
        } else if (_pointString == "wind")
        {
            _textCur.text = WeaponConfig.windLevels[_paramString][0].ToString();
            _textNext.text = WeaponConfig.windLevels[_paramString][1].ToString();
        } else if (_pointString == "person")
        {
            _textCur.text = PersonUPConfig.personLevels[_paramString][0].ToString();
            _textNext.text = PersonUPConfig.personLevels[_paramString][1].ToString();
        }
    }

    public void Upgrade()
    {
        if (_isMax)
        {
            return;
        }
        Assets.Scripts.UpgradableParametr res;
        if (_pointString != "person")
        {
            res = PlayerAttack.Upgrade(_pointString, _paramString);
        } else
        {
            if (_paramString == "speed") res = Person.Upgrade(_paramString);
            else res = PersonHealth.Upgrade(_paramString);
        }
        _textCur.text = res._current.ToString();
        if (res._currentLvl == 2)
        {
            _textNext.text = "Макс."; 
            _isMax = true;
        } else
        {
            _textNext.text = res._lvlsDictionary[res._currentLvl+1].ToString();
        }
    }
}
