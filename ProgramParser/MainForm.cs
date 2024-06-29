namespace ProgramParser
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void RunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBoxCode.SelectAll();
            TextBoxCode.SelectionBackColor = SystemColors.Control;
            TextBoxCode.DeselectAll();
            TextBoxCode.Text += " ";
            TextBoxOutput.Text = "";
            Cursor = Cursors.WaitCursor;

            var p = new Parser(TextBoxCode.Text);
            p.Parse();
            if (p.HasError)
            {
                TextBoxCode.Select(p.ErrorPosition, p.ErrorLenght);
                TextBoxCode.SelectionBackColor = Color.Red;
                TextBoxCode.DeselectAll();
            }

            TextBoxOutput.Text = p.Result;
            Cursor = Cursors.Default;
            //TextBoxOutput.Text += "\r\n<>" + string.Join(';', p.Lexems.Select((l) => $"{l.Type} ({l.Value})"));
        }

    }
}
