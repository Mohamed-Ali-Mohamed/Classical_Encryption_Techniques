using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classical_Encryption_Techniques.Substitution_Techniques
{
    public class Playfair_Cipher
    {
        private char[,] Table;
        private char[] Char;
        private struct Index
        {
            public int row, col;
        };
        public char[,] Get_Table
        {
            get { return Table; }
        }
        public Playfair_Cipher(string Key)
        {
            Char = new Char[26];
            char ch = 'A';
            for (int index = 0; index < 26; index++)
            {
                Char[index] = ch++;
            }
            Table = new char[5, 5];
            bool[] Taken_Char = new bool[26];
            Index Ind = new Index();
            Key = Key.ToLower();
            for (int index = 0; index < Key.Length; index++)
            {
                if (Taken_Char[Key[index] - 'a'] == false)
                {
                    Taken_Char[Key[index] - 'a'] = true;
                    if (Key[index] == 'i')
                    {
                        Taken_Char['j' - 'a'] = true;
                    }
                    else if (Key[index] == 'j')
                    {
                        Taken_Char['i' - 'a'] = true;
                    }

                    Table[Ind.row, Ind.col++] = char.Parse(Key[index].ToString().ToUpper());
                    if (Ind.col == 5)
                    {
                        Ind.row++;
                        Ind.col = 0;
                    }
                }
            }
            for (int index = 0; index < 26; index++)
            {
                if (Taken_Char[index] == false)
                {
                    Taken_Char[index] = true;
                    if (index == 'i' - 'a')
                    {
                        Taken_Char[index + 1] = true;
                    }

                    Table[Ind.row, Ind.col++] = Char[index];
                    if (Ind.col == 5)
                    {
                        Ind.row++;
                        Ind.col = 0;
                    }
                }
            }
        }
        private Index Get_Index(char ch)
        {
            ch = char.Parse(ch.ToString().ToUpper());
            Index Ind = new Index();
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (Table[row, col] == ch || (ch == 'I' && Table[row, col] == 'J') || (ch == 'J' && Table[row, col] == 'I'))
                    {
                        Ind.col = col;
                        Ind.row = row;
                        return Ind;
                    }
                }
            }
            return Ind;
        }
        public void Encryption(ref char ch1, ref char ch2)
        {
            Index index1 = Get_Index(ch1);
            Index index2 = Get_Index(ch2);
            if (index1.row == index2.row)
            {
                index1.col = (index1.col + 1) % 5;
                index2.col = (index2.col + 1) % 5;
            }
            else if (index1.col == index2.col)
            {
                index1.row = (index1.row + 1) % 5;
                index2.row = (index2.row + 1) % 5;
            }
            else
            {
                index1.col = index1.col + index2.col;
                index2.col = index1.col - index2.col;
                index1.col = index1.col - index2.col;
            }
            ch1 = Table[index1.row, index1.col];
            ch2 = Table[index2.row, index2.col];
            ch1 = char.ToUpper(ch1);
            ch2 = char.ToUpper(ch2);
        }
        public void Decryption(ref char ch1, ref char ch2)
        {
            Index index1 = Get_Index(ch1);
            Index index2 = Get_Index(ch2);
            if (index1.row == index2.row)
            {
                index1.col = (index1.col - 1) < 0 ? 4 : (index1.col - 1);
                index2.col = (index2.col - 1) < 0 ? 4 : (index2.col - 1);
            }
            else if (index1.col == index2.col)
            {
                index1.row = (index1.row - 1) < 0 ? 4 : (index1.row - 1);
                index2.row = (index2.row - 1) < 0 ? 4 : (index2.row - 1);
            }
            else
            {
                index1.col = index1.col + index2.col;
                index2.col = index1.col - index2.col;
                index1.col = index1.col - index2.col;
            }
            ch1 = Table[index1.row, index1.col];
            ch2 = Table[index2.row, index2.col];
            ch1 = char.ToLower(ch1);
            ch2 = char.ToLower(ch2);
        }
    }
}
