using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonUPConfig
{
    public static Dictionary<string, Dictionary<int, int>> personLevels = new Dictionary<string, Dictionary<int, int>>()
    {
        {"maxHealth", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
        {"speed", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
        {"healthRes", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
    };
}