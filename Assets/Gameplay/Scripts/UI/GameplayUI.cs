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

        [SerializeField]
        private TextMeshProUGUI _killedEnemiesLabel;

        [Inject]
        private readonly SpellSpawner _spellSpawner;

        [Inject]
        private readonly GameplayStatistics _gameplayStatistics;

        public void Update()
        {
            RefreshSpellCooldownTimer(SpellBase.SpellType.FireStrike, _fireSpellCooldownLabel);
            RefreshSpellCooldownTimer(SpellBase.SpellType.IceBlast, _iceSpellCooldownLabel);

            if (_killedEnemiesLabel)
                _killedEnemiesLabel.text = $"Killed enemies: {_gameplayStatistics.KilledEnemies}";
        }

        private void RefreshSpellCooldownTimer(SpellBase.SpellType spellType, TextMeshProUGUI label)
        {
            if (!label)
                return;

            var now = Time.time;

            var cooldownEnd = _spellSpawner.CooldownEndTime(spellType);
            var countdown = cooldownEnd - now;
            label.text = countdown > 0f
                ? $"{countdown.ToString("F1")}"
                : string.Empty;
        }
    }
}
