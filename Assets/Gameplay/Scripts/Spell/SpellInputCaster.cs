using System;
using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class SpellInputCaster : ITickable
    {
        [Serializable]
        public class Settings
        {
            public KeyCode FireSpellInputCode;
        }

        [Inject]
        private readonly Settings _settings;

        [Inject]
        private readonly SpellSpawner _spellSpawner;

        public void Tick()
        {
            if (Input.GetKey(_settings.FireSpellInputCode))
                _spellSpawner.CastSpellIfPossible(SpellBase.SpellType.FireStrike);
        }
    }
}
