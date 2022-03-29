using System.Collections;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] private bool isSick;
    [SerializeField] [Range(0, 1)] private float health;
    [SerializeField] [Range(0, 1)] private float fastingLevel;
    [SerializeField] [Range(0, 1)] private float caringLevel;
    [SerializeField] float caringThreshold;
    private float caringFrequency; 

    [SerializeField] private bool GenerateRandomly;

    private void Start()
    {
        if (GenerateRandomly)
        {
            health = Random.Range(0f, 1f);
            fastingLevel = Random.Range(0f, 1f);
            caringLevel = Random.Range(0f, 0.4f);
            isSick = (fastingLevel - health * caringLevel) > 0;
        }

        if (isSick)
            StartCoroutine(DiseaseSpreading(1.2f));

        StartCoroutine(Fasting(0.5f));
        StartCoroutine(FormCaringLevel(2f));
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Feed(Food.Rice);
            Heal(0.15f);
        }
    }

    public void Feed(Food food)
    {
        fastingLevel += ((int)food)/100f;

        caringFrequency += 0.85f;
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health >= 1)
            isSick = false;

        caringFrequency += 0.85f;
    }

    private IEnumerator DiseaseSpreading(float speed)
    {
        while (isSick && health > 0)
        {
            yield return new WaitForSeconds(3f);
            health = Mathf.Lerp(health, 0, Time.deltaTime * speed);

            caringFrequency -= 0.25f;
        }
    }

    private IEnumerator Fasting(float speed)
    {
        while (fastingLevel > 0)
        {
            yield return new WaitForSeconds(3f);
            fastingLevel = Mathf.Lerp(fastingLevel, 0, Time.deltaTime * speed);

            caringFrequency -= 0.25f;
        }
    }

    private IEnumerator FormCaringLevel(float speed)
    {
        while (caringLevel < 1)
        {
            yield return new WaitForSeconds(3f);
            int destination = caringFrequency > caringThreshold ? 1 : 0;
            caringLevel = Mathf.Lerp(caringLevel, destination, Time.deltaTime * speed);
        }
    }
}
