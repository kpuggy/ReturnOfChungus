﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

        public void SetHealth(int health)
    {
        slider.maxValue = health;
    }

    internal void SetHealth(float currentHealth)
    {
        throw new NotImplementedException();
    }
}
