using System;
using UnityEngine;

namespace UI.Event
{
    [Serializable]
    public class EventOption
    {
        [SerializeField][TextArea] protected string description;
        [SerializeField] protected Sprite icon;
        [SerializeField] protected string title;
        [SerializeField][TextArea] protected string postDescription;
        [SerializeField] protected string postTitle;

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

        public string PostDescription
        {
            get => postDescription;
            set => postDescription = value;
        }

        public string PostTitle
        {
            get => postTitle;
            set => postTitle = value;
        }

        public virtual void Choose()
        {
            
        }
    }
}