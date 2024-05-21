using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Advices", menuName = "ScriptableObjects/Advices", order = 5)]
public class AdvicesScriptableObject : ScriptableObject
{
    [SerializeField]
    private List<string> _advices;
    public List<string> Advices => _advices;

}
