using IronPython;

namespace Noter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] text = this.textBox1.Lines;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text File|*.txt|All Files|*.*";
            saveFileDialog1.Title = "Save Your Notes";
            saveFileDialog1.ShowDialog();
            var engine = IronPython.Hosting.Python.CreateEngine();
            var scope = engine.CreateScope();
            engine.Execute(@"
def writefile(lines, filepath):
    file = open(filepath, 'w')
    for i in lines:
        if i[0] == '>':
            i.pop(0)
        file.write(i)
    file.close()
    return None
            ", scope);
            dynamic writer = scope.GetVariable("writefile");
            writer(text, saveFileDialog1.FileName);
        }
    }
}