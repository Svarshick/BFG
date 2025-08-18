using System;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer.Unity;

namespace UI
{
    public abstract class View : IStartable, IDisposable
    {
        public VisualElement Root { get; }
        public bool IsVisible => Root.style.display == DisplayStyle.Flex;
        
        public View(VisualElement root, bool hideOnAwake = false)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root)); 
            if (hideOnAwake) Hide();
        }

        public void Start()
        {
            SetVisualElements();
            BindViewData();
            RegisterInputCallbacks();
        }
        
        protected virtual void SetVisualElements() {}

        protected virtual void BindViewData() {}

        protected virtual void RegisterInputCallbacks() {}
        
        public void Show()
        {
            Root.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            Root.style.display = DisplayStyle.None;
        }

        public virtual void Dispose()
        {
            Root.parent?.Remove(Root);
        }
    }
}