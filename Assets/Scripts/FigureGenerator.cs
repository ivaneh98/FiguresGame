using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] figures;
    [SerializeField]
    private GameObject leftBorder;
    [SerializeField]
    private GameObject rightBorder;
    [SerializeField]
    private GameObject bonusSpawnPosition;
    [SerializeField]
    private GameObject bonusFigure;
    [SerializeField]
    private float delay=3f;
    private float delayBonus=15f;
    private float y, z;
    [SerializeField]
    private bool isMuliplayer= false;
    private bool isPlay=true;
    // Start is called before the first frame update
    void Start()
    {
        y = leftBorder.transform.position.y;
        z = leftBorder.transform.position.z;
        if (!isMuliplayer)
        {
            StartCoroutine(DelayedSpawn());
            StartCoroutine(DelayedSpawnBonus());
            EventManager.OnPlayerDie += StopGenerator;
            EventManager.OnContinue += Continue;


        }
        else
        {
            EventManager.OnDelayedSpawn += SpawnInMultiplayer;
            EventManager.OnPVPResult += StopGenerator;
        }
    }

    private void Continue()
    {
        isPlay = true;

        StartCoroutine(DelayedSpawn());
        StartCoroutine(DelayedSpawnBonus());
    }

    private void StopGenerator(string arg1, int arg2)
    {
        Destroy(gameObject);
    }
    private void StopGenerator(int arg2)
    {
        isPlay=false;
    }
    void Spawn()
    {
        int id = Random.Range(0, figures.Length);
        float x = Random.Range(leftBorder.transform.position.x, rightBorder.transform.position.x);
        GameObject obj = Instantiate(figures[id],
            new Vector3(x,y,z),
            Quaternion.identity);
    }
    void SpawnInMultiplayer(int id, float position)
    {
        float x = rightBorder.transform.position.x - (Vector2.Distance(new Vector2(rightBorder.transform.position.x, 0),
             new Vector2(leftBorder.transform.position.x, 0)) * position);

        GameObject obj = Instantiate(figures[id],
            new Vector3(x, y, z),
            Quaternion.identity);
    }
    IEnumerator DelayedSpawn()
    {
        while (isPlay)
        {
            Spawn();
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator DelayedSpawnBonus()
    {
        while (isPlay)
        {
            yield return new WaitForSeconds(delayBonus);
            Instantiate(bonusFigure,
                bonusSpawnPosition.transform.position,
                Quaternion.identity);
        }
    }
    private void OnDestroy()
    {
        EventManager.OnDelayedSpawn -= SpawnInMultiplayer;
        EventManager.OnPVPResult -= StopGenerator;
        EventManager.OnPlayerDie -= StopGenerator;

    }
}
