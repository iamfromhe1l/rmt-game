using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfig
{
    public static Dictionary<string, Dictionary<int, int>> swordLevels = new Dictionary<string, Dictionary<int, int>>()
    {
        {"damage", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
        {"distance", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
        {"timeout", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
    };

    public static Dictionary<string, Dictionary<int, int>> axeLevels = new Dictionary<string, Dictionary<int, int>>()
    {
        {"damage", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
        {"distance", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
        {"timeout", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
        {"enemyMaxCount", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
    };

    public static Dictionary<string, Dictionary<int, int>> fireLevels = new Dictionary<string, Dictionary<int, int>>()
    {
        {"damage", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
        {"speed", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
        {"timeout", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
        {"count", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
    };

    public static Dictionary<string, Dictionary<int, int>> windLevels = new Dictionary<string, Dictionary<int, int>>()
    {
        {"damage", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
        {"speed", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
        {"timeout", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
        {"reclining", new Dictionary<int, int>() { { 0, 10 }, { 1, 20 }, { 2, 30 } }},
    };
}
