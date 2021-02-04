using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceSlider : MonoBehaviour
{
    [SerializeField] Slider slider;

    SnowmanMovement player;

    private void Start()
    {
        player = FindObjectOfType<SnowmanMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.GetForcePercentage();
    }
}
