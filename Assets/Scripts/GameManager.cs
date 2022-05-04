using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int score = 0;
    [SerializeField]
    private static int health = 5;

    public static int AddPoint()
    {
        return ++score;
    }
    public static int AddHealth()
    {
        return ++health;
    }
    public static int RemoveHealth()
    {
        return --health;
    }
}
