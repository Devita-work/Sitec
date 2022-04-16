namespace Sitec
{
    public partial class Form1 : Form
    {
        private string name1;
        private string name2;
        bool secondfileuploading = false;
        public Form1()
        {
            InitializeComponent();
            button2.Visible = false;
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!secondfileuploading)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    name1 = openFileDialog1.FileName;
                    string[] subs = name1.Split('\\');
                    string filename = subs[subs.Length - 1];
                    textBox1.Text = File.ReadAllText(name1);
                    textBox2.Text = filename;
                }
                secondfileuploading = !secondfileuploading;
            }
            else
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    name2 = openFileDialog1.FileName;
                    string[] subs = name2.Split('\\');
                    string filename = subs[subs.Length - 1];
                    textBox4.Text = File.ReadAllText(name2);
                    textBox3.Text = filename;
                }
                button2.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] rows1 = textBox1.Text.Split('\n');
            string[] rows2 = textBox4.Text.Split('\n');

            string[] rowssemi1;
            string[] rowssemi2;

            List<string> names = new List<string>();

            for (int i = 0; i < rows1.Length; i++)
            {
                rowssemi1 = rows1[i].Split('\t', ';');

                names.Add(rowssemi1[0]);

                for (int k = 0; k < rowssemi1.Length; k++)
                {
                    if (rowssemi1[k] == "")
                    {
                        rowssemi1.ToList().RemoveAt(k);
                        rowssemi1.ToArray();
                    }
                    if (rowssemi1[k].Contains("(Отв.)"))
                    {
                        rowssemi1[k].Replace("(Отв.)", " ");
                        rowssemi1[k].Remove(rowssemi1[k].Length - 2, 2);
                        names.Remove(rowssemi1[0]);
                        names.Add(rowssemi1[k]);
                    }
                }
            }
            
            for (int i = 0; i < rows2.Length; i++)
            {
                rowssemi2 = rows2[i].Split('\t', ';');

                names.Add(rowssemi2[0]);

                for (int k = 0; k < rowssemi2.Length; k++)
                {
                    if (rowssemi2[k] == "")
                    {
                        rowssemi2.ToList().RemoveAt(k);
                        rowssemi2.ToArray();
                    }
                    if (rowssemi2[k].Contains("(Отв.)"))
                    {
                        rowssemi2[k].Replace("(Отв.)", " ");
                        rowssemi2[k].Remove(rowssemi2[k].Length - 2, 2);
                        names.Remove(rowssemi2[0]);
                        names.Add(rowssemi2[k]);
                    }
                }
            }

            IEnumerable<string> distinctNames = names.Distinct();

            List<string> distinctNamesList = new List<string>();

            distinctNamesList = distinctNames.ToList();

            for (int i = 1; i < distinctNamesList.Count(); i++)
            {
                dataGridView1.Columns.Add("", i.ToString());
            }

            for (int i = 1; i < distinctNamesList.Count()+1; i++)
            {
                dataGridView1.Rows.Add(i.ToString());
            }

            for (int i = 0; i < distinctNames.Count(); i++)
                distinctNamesList[i].Remove(distinctNamesList[i].Length) ;

            dataGridView1.RowCount = distinctNamesList.Count;
            dataGridView1.ColumnCount = 5;

            for (int i = 0; i < distinctNamesList.Count; i ++)
                dataGridView1.Rows[i].Cells[1].Value = distinctNamesList[i];

        }
        }
    }