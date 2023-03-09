using System.Collections.Generic;
using Actor;
using Data;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Controller
{
    public class AttackController
    {
        private bool isRunning;
        private float attackDelay;
        private int maxTargetNumber;

        private string actorLayer;
        private ActorStatus actorStatus;
        private CircleCollider2D collider2D;
        
        private readonly List<ActorBase> targets = new();
        private readonly CompositeDisposable disposable = new();
        
        public AttackController(ActorStatus status, CircleCollider2D collider)
        {
            isRunning = false;
            actorStatus = status;
            collider2D = collider;
            
            SetCollider();
        }
        
        public void UpdateAttackState()
        {
            if (isRunning == false)
                return;

            if (attackDelay < actorStatus.AttackSpeed)
                attackDelay += Time.deltaTime;
        }

        private void SetCollider()
        {
            collider2D.radius = actorStatus.AttackRange;
            collider2D
                .OnTriggerStay2DAsObservable()
                .Where( _ => isRunning )
                .Subscribe(ComputeTargets)
                .AddTo(disposable);
        }
        
        private void ComputeTargets(Collider2D target)
        {
            if (target.gameObject.layer != LayerMask.NameToLayer(actorLayer)) 
                return;
                        
            var newTarget = target.transform.GetComponent<Enemy>();
                        
            UpdateAbleAttackToTarget( newTarget );
                        
            if (attackDelay < actorStatus.AttackSpeed)
                return;
                
            targets.ForEach( Attack );
        }
        
        private void UpdateAbleAttackToTarget( ActorBase target )
        {
            targets.RemoveAll(target => target.IsDead || IsNearDistance(target.transform) == false);

            if( targets.Contains(target) == false && targets.Count < maxTargetNumber )
                targets.Add(target);
        }
        
        private bool IsNearDistance(Component target)
        {
            var sqrMagnitude = (collider2D.transform.position - target.transform.position).sqrMagnitude;
            return sqrMagnitude <= actorStatus.AttackRange * actorStatus.AttackRange;
        }
        
        private void Attack(ActorBase target)
        {
            // var skill = ObjectPoolManager.Instance.normalAttackPool.GetObject();
            //
            // skill.Initialize( collider2D.transform, () => target.GetDamage(parentActor.Status.AttackPower));
            // skill.SetTarget( target );
            
            attackDelay = 0f;
        }
    }
}