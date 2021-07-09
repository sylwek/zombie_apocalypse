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

        [Inject]
        private readonly IceSpell.Factory _iceSpellFactory;

        private Vector3 SpawnPosition => _player.Position + _player.Forward * _settings.SpawnOffsetDistance;
        private Quaternion SpawnRotation => _player.Rotation;

        private IDictionary<SpellBase.SpellType, float> _lastCastTime = new Dictionary<SpellBase.SpellType, float>();

        public void CastSpellIfPossible(SpellBase.SpellType spellType)
        {
            if (!_lastCastTime.TryGetValue(spellType, out var lastCastTime)
                || CooldownEndTime(spellType) < Time.time)
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
                        _settings.FireSpell.CooldownDuration);
                    break;
                case SpellBase.SpellType.IceBlast:
                    _iceSpellFactory.Create(
                        SpawnPosition,
                        SpawnRotation,
                        _settings.FireSpell.MoveSpeed,
                        _settings.FireSpell.Lifetime,
                        _settings.FireSpell.Damage,
                        _settings.FireSpell.CooldownDuration);
                    break;
                default:
                    throw new ArgumentException();
            }

            _lastCastTime[spellType] = Time.time;
        }

        public float CooldownEndTime(SpellBase.SpellType spellType)
        {
            if (_lastCastTime.ContainsKey(spellType))
                return _lastCastTime[spellType] + CooldownDuration(spellType);

            return 0f;
        }

        private float CooldownDuration(SpellBase.SpellType spellType) => GetGameplaySettings(spellType).CooldownDuration;

        private GameSettingsInstaller.SpellGameplaySettings GetGameplaySettings(SpellBase.SpellType spellType)
        {
            switch (spellType)
            {
                case SpellBase.SpellType.FireStrike: return _settings.FireSpell;
                case SpellBase.SpellType.IceBlast: return _settings.IceSpell;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
