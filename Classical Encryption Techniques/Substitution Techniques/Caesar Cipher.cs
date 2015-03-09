using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classical_Encryption_Techniques.Substitution_Techniques
{
    public class Caesar_Cipher
    {
        public string Encryption(string Input_Text, int Key)
        {
            string Encrypted_Text = "";
            Input_Text = Input_Text.ToLower();
            for (int index = 0; index < Input_Text.Length; index++)
            {
                if (char.IsLetter(Input_Text[index]))
                {
                    int Encrypted_Char_Index = (Input_Text[index] - 'a' + Key) % 26;
                    char Encrypted_Char = 'A';
                    while (Encrypted_Char_Index-- != 0)
                    {
                        Encrypted_Char++;
                    }
                    Encrypted_Text += Encrypted_Char;
                }
                else
                {
                    Encrypted_Text += Input_Text[index];
                }
            }
            return Encrypted_Text;
        }
        public string Decryption(string Encrypted_Text, int Key)
        {
            string Decrypted_Text = "";
            Encrypted_Text = Encrypted_Text.ToUpper();
            for (int index = 0; index < Encrypted_Text.Length; index++)
            {
                if (char.IsLetter(Encrypted_Text[index]))
                {
                    int Decrypted_Char_Index = (Encrypted_Text[index] - 'A' - Key) % 26;
                    if (Decrypted_Char_Index < 0)
                    {
                        Decrypted_Char_Index += 26;
                    }
                    char Decrypted_Char = 'a';
                    while (Decrypted_Char_Index-- != 0)
                    {
                        Decrypted_Char++;
                    }
                    Decrypted_Text += Decrypted_Char;
                }
                else
                {
                    Decrypted_Text += Encrypted_Text[index];
                }
            }
            return Decrypted_Text;
        }
    }
}
