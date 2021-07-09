using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class SpellSpawner
    {
        [Inject]
        private readonly GameSettingsInstaller.SpellSettings _settings;

        [Inject]
        private readonly Player _player;

        [Inject]
        private readonly FireSpell.Factory _fireSpellFactory;

        private Vector3 SpawnPosition => _player.Position + _player.Forward * _settings.SpawnOffsetDistance;
        private Quaternion SpawnRotation => _player.Rotation;

        private IDictionary<SpellBase.SpellType, float> _lastCastTime = new Dictionary<SpellBase.SpellType, float>();

        public void CastSpellIfPossible(SpellBase.SpellType spellType)
        {
            if (!_lastCastTime.TryGetValue(spellType, out var lastCastTime)
                || (lastCastTime + CooldownTime(spellType)) < Time.time)
                CastSpell(spellType);
        }

        public void CastSpell(SpellBase.SpellType spellType)
        {
            switch (spellType)
            {
                case SpellBase.SpellType.FireStrike:
                    _fireSpellFactory.Create(
                        SpawnPosition,
                        SpawnRotation,
                        _settings.FireSpell.MoveSpeed,
                        _settings.FireSpell.Lifetime,
                        _settings.FireSpell.Damage,
                        _settings.FireSpell.CooldownTime);
                    break;
            }

            _lastCastTime[spellType] = Time.time;
        }

        private float CooldownTime(SpellBase.SpellType type)
        {
            switch (type)
            {
                case SpellBase.SpellType.FireStrike:
                    return _settings.FireSpell.CooldownTime;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
