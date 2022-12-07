using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cell_window_position
{
    public partial class RenameEditorDialog : Form
    {
        public RenameEditorDialog() => InitializeComponent();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Rename";
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            textBoxRename.KeyDown += (sender, e) =>
            {
                switch (e.KeyData)
                {
                    case Keys.Enter:
                        DialogResult = DialogResult.OK;
                        e.Handled = e.SuppressKeyPress = true;
                        break;
                    case Keys.Escape:
                        DialogResult = DialogResult.Cancel;
                        break;
                    default:
                        break;
                }
            };
        }
        public DialogResult ShowDialog(IWin32Window owner, string text)
        {
            textBoxRename.Text = text;
            return base.ShowDialog(owner);
        }
        public string UserInput => textBoxRename.Text;
    }
}
