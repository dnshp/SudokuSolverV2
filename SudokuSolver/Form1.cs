using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        TextBox[] boxes = new TextBox[81];
        bool[,] possibilities = new bool[,] 
        {
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, },
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, },
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, },
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, },
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, },
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, },
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, },
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, },
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, },
        };
        int count = 0;
        bool solved;

        public Form1()
        {
            InitializeComponent();
        }

        private void Refresh(int index)
        {
            TextBox box = boxes[index];
            
            //MessageBox.Show("Refreshing box " + box.Name);
            string valueString = box.Text;
            int value;
            Int32.TryParse(valueString, out value);
            string group = box.Name.Substring(3, 1);
            string row = box.Name.Substring(4, 1);
            string column = box.Name.Substring(5, 1);
            for (int a = 0; a < 9; a++)
            {
                if ((a + 1) != value)
                {
                    possibilities[a, index] = false;
                }
            }

            solved = true;
            foreach (TextBox a in boxes)
            {
                if (a.Text == "")
                {
                    solved = false;
                }
            }
            if (solved)
            {
                MessageBox.Show("Puzzle solved!");
                Environment.Exit(0);
            }
            else
            {
                //Eliminate possibilites in groups
                for (int x = 0; x < 81; x++) //for each box
                {
                    if (boxes[x].Name.Substring(3, 1) == group) //if in the same group
                    {
                        possibilities[value - 1, x] = false; //rule out possibility
                        count = 0;
                        for (int y = 0; y < 9; y++)
                        {
                            if (possibilities[y, x] == true)
                            {
                                count++;
                            }
                        }
                        if (count == 1) //check if only one possibility
                        {
                            for (int a = 0; a < 9; a++)
                            {
                                if (possibilities[a, x] == true) //find true possibility
                                {
                                    boxes[x].Text = (a + 1).ToString();
                                    if ((boxes[x].Name.Substring(4) != row) || (boxes[x].Name.Substring(5) != column)) //if not same box
                                    {
                                        //MessageBox.Show("Testing" + boxes[x].Name);
                                        Refresh(x);
                                    }
                                }
                            }
                        }
                    }
                }

                //Eliminate possibilities in rows
                for (int x = 0; x < 81; x++) //for each box
                {
                    if (boxes[x].Name.Substring(4, 1) == row) //if in the same row
                    {
                        possibilities[value - 1, x] = false; //rule out possibility
                        int count = 0;
                        for (int y = 0; y < 9; y++)
                        {
                            if (possibilities[y, x])
                            {
                                count++;
                            }
                        }
                        if (count == 1) //check if only one possibility
                        {
                            for (int a = 0; a < 9; a++)
                            {
                                if (possibilities[a, x]) //find true possibility
                                {
                                    boxes[x].Text = (a + 1).ToString();
                                    if ((boxes[x].Name.Substring(5) != column) || (boxes[x].Name.Substring(3) != group)) //if not same box
                                    {
                                        Refresh(x);
                                    }
                                }
                            }
                        }
                    }
                }

                //Eliminate possibilities in columns
                for (int x = 0; x < 81; x++) //for each box
                {
                    if (boxes[x].Name.Substring(5, 1) == column) //if in the same column
                    {
                        possibilities[value - 1, x] = false; //rule out possibility
                        int count = 0;
                        for (int y = 0; y < 9; y++)
                        {
                            if (possibilities[y, x])
                            {
                                count++;
                            }
                        }
                        if (count == 1) //check if only one possibility
                        {
                            for (int a = 0; a < 9; a++)
                            {
                                if (possibilities[a, x]) //find true possibility
                                {
                                    //MessageBox.Show(boxes[x].Name + (a + 1.ToString()));
                                    boxes[x].Text = (a + 1).ToString();
                                    if (boxes[x].Name != box.Name) //if not same box
                                    {
                                        Refresh(x);
                                    }
                                }
                            }
                        }
                    }
                }

                //Check if one possible spot in group
                bool[] sameGroup = new bool[81];
                for (int x = 0; x < 81; x++ )
                {
                    if ((boxes[x].Name.Substring(3,1) == group) && (boxes[x].Text != ""))
                    {
                        sameGroup[x] = true;
                    }
                    else
                    {
                        sameGroup[x] = false;
                    }
                }
                for (int y = 0; y < 9; y++ )
                {
                    count = 0;
                    for (int x = 0; x < 81; x++ )
                    {
                        if ((possibilities[y,x]) && (sameGroup[x]))
                        {
                            count++;
                        }
                    }
                    if (count == 1)
                    {
                        for (int x = 0; x < 81; x++)
                        {
                            if (possibilities[y, x])
                            {
                                boxes[x].Text = (y + 1).ToString();
                                Refresh(x);
                            }
                        }
                    }
                }

                //Check if one possible spot in row
                bool[] sameRow = new bool[81];
                for (int x = 0; x < 81; x++)
                {
                    if ((boxes[x].Name.Substring(4, 1) == row) && (boxes[x].Text != ""))
                    {
                        sameRow[x] = true;
                    }
                    else
                    {
                        sameRow[x] = false;
                    }
                }
                for (int y = 0; y < 9; y++)
                {
                    count = 0;
                    for (int x = 0; x < 81; x++)
                    {
                        if ((possibilities[y, x]) && (sameRow[x]))
                        {
                            count++;
                        }
                    }
                    if (count == 1)
                    {
                        for (int x = 0; x < 81; x++)
                        {
                            if (possibilities[y, x])
                            {
                                boxes[x].Text = (y + 1).ToString();
                                Refresh(x);
                            }
                        }
                    }
                }

                //Check if one possible spot in column
                bool[] sameColumn = new bool[81];
                for (int x = 0; x < 81; x++)
                {
                    if ((boxes[x].Name.Substring(5, 1) == column) && (boxes[x].Text != ""))
                    {
                        sameColumn[x] = true;
                    }
                    else
                    {
                        sameColumn[x] = false;
                    }
                }
                for (int y = 0; y < 9; y++)
                {
                    count = 0;
                    for (int x = 0; x < 81; x++)
                    {
                        if ((possibilities[y, x]) && (sameColumn[x]))
                        {
                            count++;
                        }
                    }
                    if (count == 1)
                    {
                        for (int x = 0; x < 81; x++)
                        {
                            if (possibilities[y, x])
                            {
                                boxes[x].Text = (y + 1).ToString();
                                Refresh(x);
                            }
                        }
                    }
                }
            }   
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            boxes[0] = box111;
            boxes[1] = box112;
            boxes[2] = box113;
            boxes[3] = box121;
            boxes[4] = box122;
            boxes[5] = box123;
            boxes[6] = box131;
            boxes[7] = box132;
            boxes[8] = box133;
            boxes[9] = box214;
            boxes[10] = box215;
            boxes[11] = box216;
            boxes[12] = box224;
            boxes[13] = box225;
            boxes[14] = box226;
            boxes[15] = box234;
            boxes[16] = box235;
            boxes[17] = box236;
            boxes[18] = box317;
            boxes[19] = box318;
            boxes[20] = box319;
            boxes[21] = box327;
            boxes[22] = box328;
            boxes[23] = box329;
            boxes[24] = box337;
            boxes[25] = box338;
            boxes[26] = box339;
            boxes[27] = box441;
            boxes[28] = box442;
            boxes[29] = box443;
            boxes[30] = box451;
            boxes[31] = box452;
            boxes[32] = box453;
            boxes[33] = box461;
            boxes[34] = box462;
            boxes[35] = box463;
            boxes[36] = box544;
            boxes[37] = box545;
            boxes[38] = box546;
            boxes[39] = box554;
            boxes[40] = box555;
            boxes[41] = box556;
            boxes[42] = box564;
            boxes[43] = box565;
            boxes[44] = box566;
            boxes[45] = box647;
            boxes[46] = box648;
            boxes[47] = box649;
            boxes[48] = box657;
            boxes[49] = box658;
            boxes[50] = box659;
            boxes[51] = box667;
            boxes[52] = box668;
            boxes[53] = box669;
            boxes[54] = box771;
            boxes[55] = box772;
            boxes[56] = box773;
            boxes[57] = box781;
            boxes[58] = box782;
            boxes[59] = box783;
            boxes[60] = box791;
            boxes[61] = box792;
            boxes[62] = box793;
            boxes[63] = box874;
            boxes[64] = box875;
            boxes[65] = box876;
            boxes[66] = box884;
            boxes[67] = box885;
            boxes[68] = box886;
            boxes[69] = box894;
            boxes[70] = box895;
            boxes[71] = box896;
            boxes[72] = box977;
            boxes[73] = box978;
            boxes[74] = box979;
            boxes[75] = box987;
            boxes[76] = box988;
            boxes[77] = box989;
            boxes[78] = box997;
            boxes[79] = box998;
            boxes[80] = box999;
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < 81; x++)
            {
                if (boxes[x].Text != "")
                {
                    for (int y = 0; y < 9; y++ )
                    {
                        if (boxes[x].Text != (y + 1).ToString())
                        {
                            possibilities[y, x] = false;
                        }
                    }
                }
            }
            for (int x = 0; x < 81; x++ )
            {
                if (boxes[x].Text != "")
                {
                    Refresh(x);
                }
            }
            buttonSolve.Enabled = false;
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            const char delim = ',';
            OpenFileDialog fileLoader = new OpenFileDialog();
            //fileLoader.Filter = "Text Files (.txt)|*.txt";
            fileLoader.FilterIndex = 1;
            fileLoader.Multiselect = false;
            if (fileLoader.ShowDialog() == DialogResult.OK)
            {
                Stream inFile = fileLoader.OpenFile();
                StreamReader reader = new StreamReader(inFile);
                string recordIn;
                string[] fields;
                recordIn = reader.ReadLine();
                while (recordIn != null)
                {
                    fields = recordIn.Split(delim);
                    boxes[Convert.ToInt32(fields[0])].Text = fields[1];
                    recordIn = reader.ReadLine();
                }
                reader.Close();
                inFile.Close();
                Console.ReadLine();
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            foreach (TextBox box in boxes)
            {
                box.Text = "";
            }
            for (int y = 0; y < 9; y++ )
            {
                for (int x = 0; x < 81; x++ )
                {
                    possibilities[y, x] = true;
                }
            }
            buttonSolve.Enabled = true;
        }

        private void buttonWriteFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileWriter = new SaveFileDialog();
            fileWriter.Filter = "Text Files (.txt)|*.txt";
            fileWriter.FilterIndex = 1;
            if (fileWriter.ShowDialog() == DialogResult.OK)
            {
                Stream outFile = fileWriter.OpenFile();
                StreamWriter writer = new StreamWriter(outFile);
                for (int x = 0; x < 81; x++)
                {
                    if (boxes[x].Text != "")
                    {
                        writer.WriteLine(x.ToString() + "," + boxes[x].Text);
                    }
                }
                MessageBox.Show("File written.");
                writer.Close();
                outFile.Close();
            }
        }
    }
}
