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

    private bool _isSick;
    [SerializeField] private DiseaseAnnotation _disease;
    [SerializeField] [Range(0, 1)] private float _health;
    [SerializeField] [Range(0, 1)] private float _fastingLevel;
    [SerializeField] [Range(0, 1)] private float _caringLevel;
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
            _health = Random.Range(0.1f, 0.8f);
            _fastingLevel = Random.Range(0.1f, 0.7f);
            _caringLevel = Random.Range(0.1f, 0.4f);
            
            _isSick = _health - _fastingLevel > 0;
            if (_isSick)
                _disease = DiseasesList.GetRandomDisease();
        }

        if (_isSick)
            StartCoroutine(DiseaseSpreading(_disease));

        StartCoroutine(Fasting(0.5f));
        StartCoroutine(FormCaringLevel(2f));
    }

    public void Feed(float nourishmen)
    {
        _fastingLevel += nourishmen;

        CaringFrequency += 0.85f;
    }

    public void Heal()
    {
        _health += _disease.HealAmount;
        if (_health >= 1)
            _isSick = false;

        CaringFrequency += 0.85f;
    }

    private IEnumerator DiseaseSpreading(DiseaseAnnotation disease)
    {
        while (_isSick && _health > 0)
        {
            yield return new WaitForSeconds(3f);
            _health = Mathf.Lerp(_health, 0, Time.deltaTime * disease.Heaviness);

            CaringFrequency -= 0.25f;
        }

        if (_isSick && _health == 0)
            Death();
    }

    private IEnumerator Fasting(float speed)
    {
        while (_fastingLevel > 0)
        {
            yield return new WaitForSeconds(3f);
            _fastingLevel = Mathf.Lerp(_fastingLevel, 0, Time.deltaTime * speed);

            CaringFrequency -= 0.25f;
        }

        if (_fastingLevel == 0)
            Death();
    }

    private IEnumerator FormCaringLevel(float speed)
    {
        while (_caringLevel < 1)
        {
            yield return new WaitForSeconds(3f);
            int destination = CaringFrequency > _caringThreshold ? 1 : 0;
            _caringLevel = Mathf.Lerp(_caringLevel, destination, Time.deltaTime * speed);
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
