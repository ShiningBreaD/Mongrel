using UnityEngine;
using UnityEngine.UI;

public class SetSliders : MonoBehaviour
{
    [SerializeField] private Slider caringLevel, health, fasting;

    private void Update()
    {
        caringLevel.value = Dog.Instance.caringLevel;
        health.value = Dog.Instance.health;
        fasting.value = Dog.Instance.fastingLevel;
    }
}
