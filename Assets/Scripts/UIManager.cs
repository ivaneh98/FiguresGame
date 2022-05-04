using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private GameObject[] lifes;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "0";
        EventManager.OnDamageTaken += UpdateLifes;
        EventManager.OnBonusPickUp += UpdateLifes;
        EventManager.OnEnemyKilled += ChangeScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeScore(int score)
    {
        scoreText.text = score.ToString();
    }
    void UpdateLifes(int lifesCount)
    {
        foreach(GameObject obj in lifes)
        {
            obj.SetActive(false);
        }
        for(int i = 0; i < lifesCount; i++)
        {
            lifes[i].SetActive(true);
        }
    }
    private void OnDestroy()
    {
        EventManager.OnDamageTaken -= UpdateLifes;
        EventManager.OnBonusPickUp -= UpdateLifes;
        EventManager.OnEnemyKilled -= ChangeScore;
    }
}
