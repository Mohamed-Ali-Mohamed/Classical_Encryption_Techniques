using Classical_Encryption_Techniques.Substitution_Techniques;
using Classical_Encryption_Techniques.Transposition_Technique;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Classical_Encryption_Techniques
{
    public partial class Cryptography : Form
    {
        public Cryptography()
        {
            InitializeComponent();
        }
        Playfair_Cipher Playfair;
        private void PlayfairGenerateTableButton_Click(object sender, EventArgs e)
        {
            try
            {
                string Key = PlayfairKeyTextBox.Text;
                Playfair = new Playfair_Cipher(Key);
                char[,] Table = Playfair.Get_Table;
                PlayfairTableGridView.RowCount = 5;
                PlayfairTableGridView.ColumnCount = 5;
                PlayfairTableGridView.ReadOnly = true;
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        string Value = Table[row, col].ToString();
                        if (Value == "I") Value = "I/J";
                        else if (Value == "J") Value = "I/J";
                        PlayfairTableGridView[col, row].Value = Value;
                    }
                }
                PlayfairEncryptionButton.Enabled = PlayfairDecryptionButton.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void PlayfairEncryptionButton_Click(object sender, EventArgs e)
        {
            try
            {
                string Input = "";
                for (int index = 0; index < PlayfairInputTextBox.Text.Length; index++)
                {
                    if (Input.Length > 0 && Input[Input.Length - 1] == PlayfairInputTextBox.Text[index])
                    {
                        Input += "x";
                    }
                    if (char.IsLetter(PlayfairInputTextBox.Text[index]))
                    {
                        Input += char.ToLower(PlayfairInputTextBox.Text[index]);
                    }
                }
                if (Input.Length % 2 != 0)
                {
                    Input += "x";
                }
                PlayfairOutputGridView.RowCount = 2;
                PlayfairOutputGridView.ColumnCount = Input.Length / 2;
                PlayfairOutputGridView.ReadOnly = true;
                for (int index = 0; index < Input.Length - 1; index += 2)
                {
                    char ch1 = Input[index], ch2 = Input[index + 1];
                    string In = ch1.ToString() + ch2.ToString();
                    Playfair.Encryption(ref ch1, ref ch2);
                    string Out = ch1.ToString() + ch2.ToString();
                    string Out1 = "";
                    for (int i = 0; i < Out.Length; i++)
                    {
                        if (Out[i] == 'I')
                        {
                            Out1 += 'J';
                        }
                        else if (Out[i] == 'J')
                        {
                            Out1 += 'I';
                        }
                        else
                        {
                            Out1 += Out[i];
                        }
                    }
                    if (Out != Out1)
                    {
                        Out += "/" + Out1;
                    }
                    PlayfairOutputGridView[index / 2, 0].Value = In;
                    PlayfairOutputGridView[index / 2, 1].Value = Out;
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void PlayfairDecryptionButton_Click(object sender, EventArgs e)
        {
            try
            {
                string Input = "";
                for (int index = 0; index < PlayfairInputTextBox.Text.Length; index++)
                {
                    if (char.IsLetter(PlayfairInputTextBox.Text[index]))
                    {
                        Input += char.ToUpper(PlayfairInputTextBox.Text[index]);
                    }
                }
                PlayfairOutputGridView.RowCount = 2;
                PlayfairOutputGridView.ColumnCount = Input.Length / 2;
                PlayfairOutputGridView.ReadOnly = true;
                for (int index = 0; index < Input.Length - 1; index += 2)
                {
                    char ch1 = Input[index], ch2 = Input[index + 1];
                    string In = ch1.ToString() + ch2.ToString();
                    Playfair.Decryption(ref ch1, ref ch2);
                    string Out = ch1.ToString() + ch2.ToString();
                    string Out1 = "";
                    for (int i = 0; i < Out.Length; i++)
                    {
                        if (Out[i] == 'i')
                        {
                            Out1 += 'j';
                        }
                        else if (Out[i] == 'j')
                        {
                            Out1 += 'i';
                        }
                        else
                        {
                            Out1 += Out[i];
                        }
                    }
                    if (Out != Out1)
                    {
                        Out += "/" + Out1;
                    }
                    PlayfairOutputGridView[index / 2, 0].Value = In;
                    PlayfairOutputGridView[index / 2, 1].Value = Out;
                }
            }
            catch
            {
                MessageBox.Show( "Error" );
            }
        }

        private void CaesarCipherEncryptionButton_Click(object sender, EventArgs e)
        {
            try
            {
                Caesar_Cipher Caesar = new Caesar_Cipher();
                CaesarCipherOutputTextBox.Text = Caesar.Encryption(CaesarCipherInputTextBox.Text, int.Parse(KeyTextBox0.Text));
            }
            catch
            {
                CaesarCipherOutputTextBox.Text = "Error";
            }
        }

        private void CaesarCipherDecryptionButton_Click(object sender, EventArgs e)
        {
            try
            {
                Caesar_Cipher Caesar = new Caesar_Cipher();
                CaesarCipherOutputTextBox.Text = Caesar.Decryption(CaesarCipherInputTextBox.Text, int.Parse(KeyTextBox0.Text));
            }
            catch
            {
                CaesarCipherOutputTextBox.Text = "Error";
            }
        }

        private void HillStartFillMatrixButton_Click(object sender, EventArgs e)
        {
            try
            {
                int m = int.Parse(HillMTextBox.Text);
                HillMatrixGridView.ColumnCount = m;
                HillMatrixGridView.RowCount = m;

                HillEncryptionButton.Enabled = HillDecryptionButton.Enabled = true;
            }
            catch
            {
                HillOutputTextBox.Text = "Error";
            }
        }

        private void HillEncryptionButton_Click(object sender, EventArgs e)
        {
            try
            {
                Hill_Cipher Hill = new Hill_Cipher();
                string PT, CT;
                PT = HillInputTextBox.Text;
                int m = HillMatrixGridView.RowCount;
                double[,] Key = new double[m, m];
                for (int r = 0; r < m; r++)
                {
                    for (int c = 0; c < m; c++)
                    {
                        Key[r, c] = double.Parse(HillMatrixGridView[c, r].Value.ToString());
                    }
                }
                CT = Hill.Encryption(Key, PT);
                HillOutputTextBox.Text = CT;
            }
            catch
            {
                HillOutputTextBox.Text = "Error";
            }
        }

        private void HillDecryptionButton_Click(object sender, EventArgs e)
        {
            try
            {
                Hill_Cipher Hill = new Hill_Cipher();
                string PT, CT;
                CT = HillInputTextBox.Text;
                int m = HillMatrixGridView.RowCount;
                double[,] Key = new double[m, m];
                for (int r = 0; r < m; r++)
                {
                    for (int c = 0; c < m; c++)
                    {
                        Key[r, c] = double.Parse(HillMatrixGridView[c, r].Value.ToString());
                    }
                }
                PT = Hill.Decryption(Key, CT);
                HillOutputTextBox.Text = PT;
            }
            catch
            {
                HillOutputTextBox.Text = "Error";
            }
        }

        private void PolyalphabeticEncryptionButton_Click(object sender, EventArgs e)
        {
            try
            {
                Polyalphabetic Poly = new Polyalphabetic();
                int ComboBox = PolyalphabeticKeyComboBox.SelectedIndex;
                bool AutoKey=false;
                if(ComboBox==1)AutoKey=true;
                PolyalphabeticOutputTextBox.Text = Poly.Encryption(PolyalphabeticInputTextBox.Text, PolyalphabeticKeyTextBox.Text, AutoKey);
            }
            catch
            {
                PolyalphabeticOutputTextBox.Text = "Error";
            }
        }

        private void PolyalphabeticDecryptionButton_Click(object sender, EventArgs e)
        {
            try
            {
                Polyalphabetic Poly = new Polyalphabetic();
                int ComboBox = PolyalphabeticKeyComboBox.SelectedIndex;
                bool AutoKey = false;
                if (ComboBox == 1) AutoKey = true;
                PolyalphabeticOutputTextBox.Text = Poly.Decryption(PolyalphabeticInputTextBox.Text, PolyalphabeticKeyTextBox.Text, AutoKey);
            }
            catch
            {
                PolyalphabeticOutputTextBox.Text = "Error";
            }
        }

        private void RailFenceEncryptionButton_Click(object sender, EventArgs e)
        {
            try
            {
                Rail_Fence Rail = new Rail_Fence(RailFenceInputTextBox.Text, int.Parse(RailFenceDepthTextBox.Text));
                RailFenceOutputTextBox.Text = Rail.Encryption();
            }
            catch
            {
                RailFenceOutputTextBox.Text = "Error";
            }
        }

        private void RailFenceDecryptionButton_Click(object sender, EventArgs e)
        {
            try
            {
                Rail_Fence Rail = new Rail_Fence(RailFenceInputTextBox.Text, int.Parse(RailFenceDepthTextBox.Text));
                RailFenceOutputTextBox.Text = Rail.Decryption();
            }
            catch
            {
                RailFenceOutputTextBox.Text = "Error";
            }
        }

        private void ColumnarEncryptionButton_Click(object sender, EventArgs e)
        {
            try
            {
                Columnar columnar = new Columnar(ColumnarInputTextBox.Text, ColumnarKeyTextBox.Text);
                ColumnarOutputTextBox.Text = columnar.Encryption();
            }
            catch
            {
                ColumnarOutputTextBox.Text = "Error";
            }
        }

        private void ColumnarDecryptionButton_Click(object sender, EventArgs e)
        {
            try
            {
                Columnar columnar = new Columnar(ColumnarInputTextBox.Text, ColumnarKeyTextBox.Text);
                ColumnarOutputTextBox.Text = columnar.Decryption();
            }
            catch
            {
                ColumnarOutputTextBox.Text = "Error";
            }
        }
    }
}
