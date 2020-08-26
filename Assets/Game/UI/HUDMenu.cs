using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDMenu : MonoBehaviour
{
    [SerializeField]
    private Text ResourceText;
    private float insufficientCountdown = 1.0f;
    private float timer = 0.0f;
    private Color previousColor;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.EventManager.ResourceCurrencyChanged.AddListener(ResourcesChanged);
        GameManager.instance.EventManager.InsufficientResources.AddListener(InsufficientResources);
    }

    private void OnDestroy()
    {
        GameManager.instance.EventManager.ResourceCurrencyChanged.RemoveListener(ResourcesChanged);
        GameManager.instance.EventManager.InsufficientResources.RemoveListener(InsufficientResources);
    }

    private void ResourcesChanged(int newAmount)
    {
        if(ResourceText != null)
        {
            ResourceText.text = "Resources: " + newAmount;
        }
    }

    private void InsufficientResources()
    {
        previousColor = ResourceText.color;
        ResourceText.color = Color.red;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(ResourceText.color == Color.red)
        {
            timer += Time.deltaTime;
            if(timer >= insufficientCountdown)
            {
                timer = 0.0f;
                ResourceText.color = previousColor;
            }
        }
    }
}
