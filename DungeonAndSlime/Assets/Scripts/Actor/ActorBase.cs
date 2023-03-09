using Data;
using UnityEngine;

namespace Actor
{
    public class ActorBase : MonoBehaviour
    {
        
        public string ActorLayer { get; protected set; }
        public bool IsDead { get; protected set; }
        public ActorStatus Status { get; private set; }
    }
}