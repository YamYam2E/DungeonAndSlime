using System;

namespace Data
{
    [Serializable]
    public class ActorStatus
    {
        public float AttackPower;
        public float AttackSpeed;
        public float AttackRange;
        public float AttackPer;
        public float HitPoint;
        public float Protection;
        public float CriticalPower;
        public float CriticalPer;
        public float MoveSpeed;

        public ActorStatus(ActorStatus status)
        {
            SetData(status);
        }

        public ActorStatus()
        {
        }

        public void SetData(ActorStatus status)
        {
            AttackPower = status.AttackPower;
            AttackSpeed = status.AttackSpeed;
            AttackRange = status.AttackRange;
            AttackPer = status.AttackPer;
            HitPoint = status.HitPoint;
            Protection = status.Protection;
            CriticalPower = status.CriticalPower;
            CriticalPer = status.CriticalPer;
            MoveSpeed = status.MoveSpeed;
        }
    }
}