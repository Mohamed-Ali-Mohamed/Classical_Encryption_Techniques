using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classical_Encryption_Techniques.Substitution_Techniques
{
    public class Polyalphabetic
    {
        private char[,] PolyalphabeticMatric;
        public Polyalphabetic()
        {
            PolyalphabeticMatric = new char[26, 26];
            char CH = 'A', ch;
            for (int c = 0; c < 26; c++)
            {
                ch = CH;
                for (int r = 0; r < 26; r++)
                {
                    PolyalphabeticMatric[r, c] = ch;
                    if (ch == 'Z')
                        ch = 'A';
                    else
                        ch++;
                }
                CH++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Input_Text"></param>
        /// <param name="Key"></param>
        /// <param name="AutoKey">0 for Repeating key & 1 for AutoKey</param>
        /// <returns></returns>
        public string Encryption(string Input_Text, string Key_Text, bool AutoKey)
        {
            string Encrypted_Text = "", KeyStream = "", Input = Input_Text, Key = Key_Text;
            Input = Input.ToLower();
            Key = Key.ToLower();
            Input_Text = Key = "";
            for (int index = 0; index < Input.Length; index++)
            {
                if (char.IsLetter(Input[index]))
                {
                    Input_Text += Input[index];
                }
            }
            for (int index = 0; index < Key.Length; index++)
            {
                if (char.IsLetter(Key[index]))
                {
                    Key_Text += Key[index];
                }
            }

            KeyStream = Key_Text;
            while (KeyStream.Length < Input_Text.Length)
            {
                if (AutoKey == true)
                {
                    KeyStream += Input_Text;
                }
                else
                {
                    KeyStream += Key_Text;
                }
            }
            for (int index = 0; index < Input_Text.Length; index++)
            {
                Encrypted_Text += PolyalphabeticMatric[Input_Text[index] - 'a', KeyStream[index] - 'a'];
            }
            return Encrypted_Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Encrypted_Text"></param>
        /// <param name="Key_Text"></param>
        /// <param name="AutoKey">0 for Repeating key & 1 for AutoKey</param>
        /// <returns></returns>
        public string Decryption(string Encrypted_Text, string Key_Text, bool AutoKey)
        {
            string Decrypted_Text = "", KeyStream = "", Input = Encrypted_Text, Key = Key_Text;
            Input = Input.ToUpper();
            Key = Key.ToLower();
            Encrypted_Text = Key = "";
            for (int index = 0; index < Input.Length; index++)
            {
                if (char.IsLetter(Input[index]))
                {
                    Encrypted_Text += Input[index];
                }
            }
            for (int index = 0; index < Key.Length; index++)
            {
                if (char.IsLetter(Key[index]))
                {
                    Key_Text += Key[index];
                }
            }

            KeyStream = Key_Text;
            while (AutoKey == false && KeyStream.Length < Encrypted_Text.Length)
            {
                KeyStream += Key_Text;
            }
            for (int index = 0; index < Encrypted_Text.Length; index++)
            {
                char ch = 'a';
                for (int r = 0; r < 26; r++)
                {
                    if (PolyalphabeticMatric[r, KeyStream[index] - 'a'] == Encrypted_Text[index])
                    {
                        break;
                    }
                    ch++;
                }
                Decrypted_Text += ch;
                //AutoKey
                if (KeyStream.Length < Encrypted_Text.Length)
                {
                    KeyStream += ch;
                }
            }
            return Decrypted_Text;
        }
    }
}
