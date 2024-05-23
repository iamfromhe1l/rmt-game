using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfig
{
    public static Dictionary<string, Dictionary<int, int>> swordLevels = new Dictionary<string, Dictionary<int, int>>()
    {
        {"damage", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
        {"distance", new Dictionary<int, int>() { { 0, 1 }, { 1, 2 }, { 2, 3 } }},
        {"timeout", new Dictionary<int, int>() { { 0, 3 }, { 1, 2 }, { 2, 1 } }},
    };

    public static Dictionary<string, Dictionary<int, int>> axeLevels = new Dictionary<string, Dictionary<int, int>>()
    {
        {"damage", new Dictionary<int, int>() { { 0, 7 }, { 1, 14 }, { 2, 21 } }},
        {"distance", new Dictionary<int, int>() { { 0, 1 }, { 1, 2 }, { 2, 3 } }},
        {"timeout", new Dictionary<int, int>() { { 0, 5 }, { 1, 4 }, { 2, 3 } }},
        {"enemyMaxCount", new Dictionary<int, int>() { { 0, 2 }, { 1, 3 }, { 2, 4 } }},
    };

    public static Dictionary<string, Dictionary<int, int>> fireLevels = new Dictionary<string, Dictionary<int, int>>()
    {
        {"damage", new Dictionary<int, int>() { { 0, 15 }, { 1, 25 }, { 2, 35 } }},
        {"speed", new Dictionary<int, int>() { { 0, 5 }, { 1, 10 }, { 2, 15 } }},
        {"timeout", new Dictionary<int, int>() { { 0, 5 }, { 1, 4 }, { 2, 3 } }},
        {"count", new Dictionary<int, int>() { { 0, 1 }, { 1, 2 }, { 2, 3 } }},
    };

    public static Dictionary<string, Dictionary<int, int>> windLevels = new Dictionary<string, Dictionary<int, int>>()
    {
        {"damage", new Dictionary<int, int>() { { 0, 15 }, { 1, 25 }, { 2, 35 } }},
        {"speed", new Dictionary<int, int>() { { 0, 5 }, { 1, 10 }, { 2, 15 } }},
        {"timeout", new Dictionary<int, int>() { { 0, 5 }, { 1, 4 }, { 2, 3 } }},
        {"reclining", new Dictionary<int, int>() { { 0, 5 }, { 1, 10 }, { 2, 15 } }},
    };
}
