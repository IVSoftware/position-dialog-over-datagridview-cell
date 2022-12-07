using System.ComponentModel;
using System.Windows.Forms;

namespace cell_window_position
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dataGridView.DataSource = Records;

            #region G e n e r a t e    C o l u m n s
            Records.Add(new Record());
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            Records.Clear();
            #endregion G e n e r a t e    C o l u m n s

            dataGridView.CellMouseDown += (sender, e) =>
            {
                if (MouseButtons.Equals(MouseButtons.Right))
                {
                    BeginInvoke(() =>
                    {
                        dataGridView.EndEdit();
                        if (!(e.ColumnIndex == -1) || (e.RowIndex == -1))
                        {
                            var cell = dataGridView[e.ColumnIndex, e.RowIndex];
                            var text = cell.Value?.ToString();

                            // This 'is' the location. If you show a control
                            // instead of a dialog, it's a perfect fit. But...
                            var location = 
                                dataGridView.PointToScreen(
                                    dataGridView
                                    .GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false)
                                    .Location);

                            // ...it seems "Not unusual" for a Form to have a wonky
                            // offset depending on the border style.
                            // https://stackoverflow.com/q/39602218/5438626
                            // https://stackoverflow.com/q/16484894/5438626
                            RenameEditorDialog.Location = new Point(location.X - 10, location.Y);

                            if (DialogResult.OK.Equals(RenameEditorDialog.ShowDialog(dataGridView, text ?? string.Empty)))
                            {
                                cell.Value = RenameEditorDialog.UserInput;
                            }
                        }
                    });
                }
            };
        }
        BindingList<Record> Records = new BindingList<Record>();

        RenameEditorDialog RenameEditorDialog = new RenameEditorDialog();
    }
    class Record
    {
        public string Cell1 { get; set; }
        public string Cell2 { get; set; }
        public string Cell3 { get; set; }
    }
}