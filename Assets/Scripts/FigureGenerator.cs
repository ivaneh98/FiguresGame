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

    // Start is called before the first frame update
    void Start()
    {
        y = leftBorder.transform.position.y;
        z = leftBorder.transform.position.z;
        StartCoroutine(DelayedSpawn());
        StartCoroutine(DelayedSpawnBonus());
    }


    void Spawn()
    {
        int id = Random.Range(0, figures.Length);
        float x = Random.Range(leftBorder.transform.position.x, rightBorder.transform.position.x);
        GameObject obj = Instantiate(figures[id],
            new Vector3(x,y,z),
            Quaternion.identity);
    }

    IEnumerator DelayedSpawn()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator DelayedSpawnBonus()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBonus);
            Instantiate(bonusFigure,
                bonusSpawnPosition.transform.position,
                Quaternion.identity);
        }
    }
}
