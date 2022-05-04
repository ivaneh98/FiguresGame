using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialFill : MonoBehaviour
{

    // Public UI References
    public Image fillImage;
    private float maxTime;

    private bool isActive = false;
    // Trackers for min/max values
    protected float maxValue = 1f, minValue = 0f;

    // Create a property to handle the slider's value
    private float currentValue = 0f;
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            // Ensure the passed value falls within min/max range
            currentValue = Mathf.Clamp(value, minValue, maxValue);

            // Calculate the current fill percentage and display it
            float fillPercentage = currentValue / maxValue;
            fillImage.fillAmount = fillPercentage;
            //displayText.text =;
        }
    }

    void Start()
    {
        CurrentValue=currentValue;
        EventManager.OnChannellingStarted += StartChannelling;
        EventManager.OnChannellingStoped += StopChannelling;
    }
    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isActive)
            CurrentValue += 1.0f / maxTime * Time.fixedDeltaTime;
        if(CurrentValue > maxValue)
            CurrentValue = maxValue;
    }

    public void StartChannelling(float _maxTime)
    {
        maxTime = _maxTime;
        isActive = true;
    }
    public void StopChannelling()
    {
        CurrentValue = 0;
        isActive = false;
    }


}