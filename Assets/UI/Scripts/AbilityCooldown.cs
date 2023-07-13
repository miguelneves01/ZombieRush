using Abilities.Scripts;
using Player.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Scripts
{
    public class AbilityCooldown : MonoBehaviour
    {
        [SerializeField] private Image[] _abilityIcons;

        [SerializeField] private TMP_Text[] _texts;

        private Player.Scripts.Player _pStats;
        private AbilitySpawner[] _abilities;
        private PlayerController _pController;

        private void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _pStats = player.GetComponent<Player.Scripts.Player>();
            _pController = player.GetComponent<PlayerController>();
            _abilities = player.GetComponents<AbilitySpawner>();
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < _texts.Length - 1; i++)
            {
                if (_pController.AbilitiesCooldown[i] <= 0 && _texts[i].text == "")
                {
                    continue;
                }
            
                _abilityIcons[i].fillAmount = 
                    (_abilities[i].AbilityStats.AbilityCooldown - _pController.AbilitiesCooldown[i])
                    /_abilities[i].AbilityStats.AbilityCooldown;
                _texts[i].text = _pController.AbilitiesCooldown[i] > 0 ? _pController.AbilitiesCooldown[i].ToString("F1") : "";
            }
            _abilityIcons[^1].fillAmount = 
                (_pStats.PlayerStats.DashCooldown - _pController.AbilitiesCooldown[^1])
                /_pStats.PlayerStats.DashCooldown;
            _texts[^1].text = _pController.AbilitiesCooldown[^1] > 0 ? _pController.AbilitiesCooldown[^1].ToString("F1") : "";
        
        }
    }
}
