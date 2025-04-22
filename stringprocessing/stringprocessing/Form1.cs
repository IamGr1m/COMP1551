using System;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;   

namespace stringprocessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class StringProcessing
        {
            private string inputStringValue;
            private int shiftValue;

            public string InputString
            {
                get
                {
                    return inputStringValue;
                }
                set
                {
                    inputStringValue = value;
                }
            }

            public int Shift
            {
                get
                {
                    return shiftValue;
                }
                set
                {
                    shiftValue = value;
                }
            }

            public StringProcessing(string inputString, int shift)
            {
                InputString = inputString;
                Shift = shift;
            }

            public string Encode()
            {
                StringBuilder encoded = new StringBuilder();
                foreach (char c in inputStringValue)
                {
                    char newChar = (char)(((c - 'A' + shiftValue + 26) % 26) + 'A');
                    encoded.Append(newChar);
                }
                return encoded.ToString();
            }

            public string Print()
            {
                return Encode();
            }

            public int[] InputCode()
            {
                return GetAsciiCodes(inputStringValue);
            }

            public int[] OutputCode()
            {
                return GetAsciiCodes(Print());
            }

            public string Sort()
            {
                return new string(inputStringValue.ToCharArray().OrderBy(c => c).ToArray());
            }

            private int[] GetAsciiCodes(string str)
            {
                return str.Select(c => (int)c).ToArray();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string inputString = textBox1.Text;
                if (string.IsNullOrEmpty(inputString))
                {
                    MessageBox.Show("Please fill the first box.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (inputString.Length >= 40 || !inputString.All(char.IsUpper))
                {
                    MessageBox.Show("Input must be capital letters and at most 40 characters.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox2.Text, out int shiftValue))
                {
                    MessageBox.Show("Please enter a valid integer for the shift value between -25 and 25.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (shiftValue < -25 || shiftValue > 25)
                {
                    MessageBox.Show("Input shift value must be between -25 and 25.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var sp = new StringProcessing(inputString, shiftValue);
                textBox3.Text = sp.Print();
                textBox4.Text = string.Join(", ", sp.InputCode());
                textBox5.Text = string.Join(", ", sp.OutputCode());
                textBox6.Text = sp.Sort();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
