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
            foreach(var block in _chain.Blocks)
            {
                listBox1.Items.Add(block);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            _chain.Add(textBox1.Text, "Admin");

            foreach (var block in _chain.Blocks)
            {
                listBox1.Items.Add(block);
            }
        }
    }
}
