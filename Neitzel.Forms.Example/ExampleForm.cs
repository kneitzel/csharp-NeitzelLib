using System.Windows.Forms;
using Neitzel.Forms.Example.Properties;

namespace Neitzel.Forms.Example
{
    public partial class ExampleForm : Form
    {
        #region static fields

        /// <summary>
        /// Count the number of forms created so far.
        /// </summary>
        private static int _formsCreated = 0;

        #endregion

        /// <summary>
        /// Instance of the Server Window that is open or null if none is open.
        /// </summary>
        public static ServerForm ServerWindow { get; set; }

        /// <summary>
        /// Creates a new instance of ExampleForm.
        /// </summary>
        public ExampleForm()
        {
            InitializeComponent();
            _formsCreated++;
            Text = Resources.FormName + _formsCreated;
        }

        /// <summary>
        /// Handle the click on OpenFormButton
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnOpenFormClick(object sender, System.EventArgs e)
        {
            var newForm = new ExampleForm();
            newForm.Show();
        }

        private void ServerButton_Click(object sender, System.EventArgs e)
        {
            if (ServerWindow != null)
            {
                ServerWindow.BringToFront();
                return;
            }

            ServerWindow = new ServerForm();
            ServerWindow.Visible = true;
        }

        private void OnClientButtonClick(object sender, System.EventArgs e)
        {
            var form = new ClientForm();
            form.Visible = true;
        }
    }
}
