using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _fireSpellCooldownLabel;

        [SerializeField]
        private TextMeshProUGUI _iceSpellCooldownLabel;

        [Inject]
        private readonly SpellSpawner _spellSpawner;

        public void Update()
        {
            var now = Time.time;

            if (_fireSpellCooldownLabel)
            {
                var cooldownEnd = _spellSpawner.CooldownEndTime(SpellBase.SpellType.FireStrike);
                var countdown = cooldownEnd - now;
                _fireSpellCooldownLabel.text = countdown > 0f
                    ? $"{countdown.ToString("F1")}"
                    : string.Empty;
            }

            if (_iceSpellCooldownLabel)
            {
                var cooldownEnd = _spellSpawner.CooldownEndTime(SpellBase.SpellType.IceBlast);
                var countdown = cooldownEnd - now;
                _iceSpellCooldownLabel.text = countdown > 0f
                    ? $"{countdown.ToString("F1")}"
                    : string.Empty;
            }
        }
    }
}
