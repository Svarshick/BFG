using System;
using Core.Query;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Core.Input
{
    public class InputRecorder : IInitializable, IDisposable
    {
        private QBuffer _qBuffer;

        public InputRecorder(QBuffer qBuffer)
        {
            _qBuffer = qBuffer;
        }
        
        public void Initialize() 
        {
            //Place for input
        }

        public void Dispose()
        {
        }
    }
}