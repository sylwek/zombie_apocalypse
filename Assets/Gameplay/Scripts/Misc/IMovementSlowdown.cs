namespace ZombieApocalypse
{
    public interface IMovementSlowdown
    {
        float SlowdownEndTime { get; }
        float SlowdownMovementMul { get; }

        void Slowdown(float duration, float multiplier);
    }
}
