using System;
using System.Windows.Forms;

namespace Neitzel.Forms
{
    /// <summary>
    /// Manages open Forms and makes sure that the ApplicationContext of the main loop always has
    /// an open form till the last form is closed.
    /// </summary>
    /// <remarks>
    /// To use this class: Simply replace the Application.Run call in program.cs with
    /// FormsApplication.Run.
    /// </remarks>
    public static class FormsApplication
    {
        /// <summary>
        /// Application context
        /// </summary>
        private static ApplicationContext _appContext;

        /// <summary>
        /// Run the Application Loop
        /// </summary>
        public static void Run()
        {
            _appContext = new ApplicationContext();
            Application.Run(_appContext);
        }

        /// <summary>
        /// Run the Application Loop with a given context.
        /// </summary> 
        public static void Run(ApplicationContext context)
        {
            // Validate arguments
            if (context == null) throw new ArgumentNullException(nameof(context));

            // Set the application context 
            _appContext = context;

            // add us to the closed event.
            if (context.MainForm != null)
                context.MainForm.Closed += ClosedEventHandler;

            // Start the main loop.
            Application.Run(context);
        }

        /// <summary>
        /// Run the Application Loop with a given form.
        /// </summary>
        public static void Run(Form form)
        {
            // Validate arguments
            if (form == null) throw new ArgumentNullException(nameof(form));

            form.Closed += ClosedEventHandler;
            _appContext = new ApplicationContext { MainForm = form };
            Application.Run(_appContext);
        }

        /// <summary>
        /// Event handler that removes a form when it is closed
        /// </summary>
        /// <remarks>
        /// The Application.OpenForms collection contained the window that was closing in my tests. But
        /// that is something that I do not trust because that might change depending on implementation
        /// details. So this code is safe.
        /// 
        /// Important: This code is not thread safe! I trust that all changes are done by the UI thread
        /// as it should be done in all Windows Forms actions!
        /// </remarks>
        private static void ClosedEventHandler(object sender, EventArgs e)
        {
            // Validate arguments
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (e == null) throw new ArgumentNullException(nameof(e));

            // Do nothing if the closed form is not the main form or if the count of open forms is 0.
            if (_appContext.MainForm != sender || Application.OpenForms.Count == 0)
                return;

            if (Application.OpenForms[0] != sender)
            {
                // The first form is not the the closed form so we set it as new MainForm.
                _appContext.MainForm = Application.OpenForms[0];
            }
            else if (Application.OpenForms.Count > 1)
            {
                // The first form was the closed form but there is another one.
                _appContext.MainForm = Application.OpenForms[1];
            }
            else
            {
                // No form available to replace the closed form so nothing to do
                return;
            }

            // Add us to the Closed event of the new MainForm.
            _appContext.MainForm.Closed += ClosedEventHandler;
        }
    }
}
