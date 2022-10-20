using UnityEngine;

namespace UI.Utilities
{
    public class DestroyUtility : Utility
    {
        [SerializeField] private Object objectToDestroy;

        public Object ObjectToDestroy
        {
            get => objectToDestroy;
            set => objectToDestroy = value;
        }
        
        public override void DoUtility()
        {
            Destroy(ObjectToDestroy);
        }
    }
}