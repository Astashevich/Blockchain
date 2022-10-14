namespace Blockchain
{
    public partial class MainForm : Form
    {
        private Chain _chain = new Chain();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(_chain.Blocks.ToArray());
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            _chain.Add(textBox1.Text, "Admin");

            listBox1.Items.AddRange(_chain.Blocks.ToArray());
        }
    }
}
