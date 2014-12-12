using System;
using System.Windows.Forms;

namespace ExifViewer
{
    public partial class FormMain : Form
    {
        private Controller controller = new Controller();

        private OpenFileDialog fileDialog = new OpenFileDialog();

        public FormMain(string[] args)
        {
            InitializeComponent();

            if (args.Length == 1)
            {
                this.loadFile(args[0]);
            }
        }

        private void loadFile(string path)
        {
            try
            {
                this.controller.LoadFile(path);
            }
            catch(Exception exception)
            {
                // hacky error handling
                MessageBox.Show(exception.Message, "Exception");
                return;
            }

            this.treeViewMain.Nodes.Clear();

            this.treeViewMain.Nodes.Add(this.controller.BuildRootNode());
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.loadFile(this.fileDialog.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
