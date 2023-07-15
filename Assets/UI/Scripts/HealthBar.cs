using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Player.Scripts.Player _player;
    private Slider _slider;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player.Scripts.Player>();
        _slider = GetComponent<Slider>();
        
        _slider.maxValue = _player.PlayerStats.MaxHealth;
        _slider.minValue = 0f;
        _slider.value = _player.PlayerStats.Health;
        
        _player.HealthUpdateEvent += UpdateUI;
    }

    // Update is called once per frame
    public void UpdateUI(float health, float maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = health;
    }
}
