using System;
using System.Diagnostics;

namespace Neitzel
{
    /// <summary>
    /// A disposable object.
    /// </summary>
    public class DisposableObject : IDisposable
    {
        /// <summary>
        /// Is the instance disposed?
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Creates a new instance of DisposableObject.
        /// </summary>
        public DisposableObject()
        {
            Disposed = false;
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~DisposableObject()
        {
            Debug.WriteLine("Instanze of {0} wasn't disposed!", GetType().Name);
            Dispose(false);
        }

        /// <summary>
        /// Dispose this instance.
        /// </summary>
        public void Dispose()
        {
            // Full dispose
            Dispose(true);

            // Suppress finalize
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose this instance.
        /// </summary>
        /// <param name="disposing">Controlled dispose not from finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed items.
            }

            // dispose unmanaged items.

            // Flag this instance as disposed.
            Disposed = true;
        }
    }
}
