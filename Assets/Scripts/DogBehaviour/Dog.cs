using System.Collections;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public static Dog Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    [HideInInspector] public bool isSick;
    [SerializeField] private DiseaseAnnotation _disease;
    [SerializeField] [Range(0, 1)] public float health;
    [SerializeField] [Range(0, 1)] public float fastingLevel;
    [SerializeField] [Range(0, 1)] public float caringLevel;
    [SerializeField] private float _caringThreshold;

    private float _caringFrequency;
    private float CaringFrequency { get { return _caringFrequency; }
        set
        {
            if (_caringFrequency > -10 && _caringFrequency < 25)
                _caringFrequency += value;
        }
    }

    [SerializeField] private bool _GenerateRandomly;

    private void Start()
    {     
        if (_GenerateRandomly)
        {
            health = Random.Range(0.1f, 0.8f);
            fastingLevel = Random.Range(0.1f, 0.7f);
            caringLevel = Random.Range(0.1f, 0.4f);
            
            isSick = health - fastingLevel > 0;
            if (isSick)
                _disease = DiseasesList.GetRandomDisease();
        }

        if (isSick)
            StartCoroutine(DiseaseSpreading(_disease));

        StartCoroutine(Fasting(0.5f));
        StartCoroutine(FormCaringLevel(2f));
    }

    public void Feed(float nourishmen)
    {
        fastingLevel += nourishmen;

        CaringFrequency += 10f;
    }

    public void Heal()
    {
        health += _disease.HealAmount;
        if (health >= 1)
            isSick = false;

        CaringFrequency += 10f;
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health >= 1)
            isSick = false;

        CaringFrequency += 10f;
    }

    private IEnumerator DiseaseSpreading(DiseaseAnnotation disease)
    {
        while (isSick && health > 0)
        {
            yield return new WaitForSeconds(3f);
            health = Mathf.Lerp(health, 0, Time.deltaTime * disease.Heaviness);

            CaringFrequency -= 0.25f;
        }

        if (isSick && health == 0)
            Death();
    }

    private IEnumerator Fasting(float speed)
    {
        while (fastingLevel > 0)
        {
            yield return new WaitForSeconds(3f);
            fastingLevel = Mathf.Lerp(fastingLevel, 0, Time.deltaTime * speed);

            CaringFrequency -= 0.25f;
        }

        if (fastingLevel == 0)
            Death();
    }

    private IEnumerator FormCaringLevel(float speed)
    {
        while (caringLevel < 1)
        {
            yield return new WaitForSeconds(3f);
            int destination = CaringFrequency > _caringThreshold ? 1 : 0;
            caringLevel = Mathf.Lerp(caringLevel, destination, Time.deltaTime * speed);
        }
    }

    private void Death()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    private static class DiseasesList
    {
        private static DiseaseAnnotation[] diseases;

        public static DiseaseAnnotation GetRandomDisease()
        {
            if (diseases == null)
                Fill();

            return diseases[Random.Range(0, diseases.Length)];
        }

        private static void Fill()
        {
            diseases = Resources.LoadAll<DiseaseAnnotation>("Dog/Diseases");
        }
    }
}
