using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class IceSpell : SpellBase
    {
        [Inject]
        private readonly GameSettingsInstaller.SpellSettings _settings;

        protected override void OnDamageDone(GameObject obj)
        {
            base.OnDamageDone(obj);

            var movementSlowdown = obj.GetComponent<IMovementSlowdown>();
            if (movementSlowdown != null)
                movementSlowdown.Slowdown(_settings.IceSpell.SlowdownDuration, _settings.IceSpell.SlowdownMultiplier);
        }

        public class Factory : PlaceholderFactory<Vector3, Quaternion, float, float, int, float, IceSpell>
        {
        }
    }
}
