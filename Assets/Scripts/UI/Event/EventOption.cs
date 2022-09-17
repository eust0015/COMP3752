using System;
using UnityEngine;

namespace UI.Event
{
    [Serializable]
    public abstract class EventOption
    {
        [SerializeField] protected string description;
        [SerializeField] protected Sprite icon;
        [SerializeField] protected string title;

        public virtual string Description
        {
            get => description;
            set => description = value;
        }

        public virtual Sprite Icon
        {
            get => icon;
            set => icon = value;
        }

        public virtual string Title
        {
            get => title;
            set => title = value;
        }

        public abstract void Choose();
    }
}