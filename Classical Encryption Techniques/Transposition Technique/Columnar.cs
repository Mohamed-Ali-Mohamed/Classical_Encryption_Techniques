using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classical_Encryption_Techniques.Transposition_Technique
{
    public class Columnar : Transposition_Technique
    {
        public Columnar(string Input, string Key)
            : base(Input, Key)
        { }

        public override string Encryption()
        {
            string Encrypted_Text = "", Input = this.Input;
            Input = Input.ToUpper();
            this.Input = "";
            for (int index = 0; index < Input.Length; index++)
            {
                if (char.IsLetter(Input[index]))
                {
                    this.Input += Input[index];
                }
            }
            int Width = this.Key.Length;
            int Depth = (int)Math.Ceiling((double)this.Input.Length / (double)Width);
            char[,] Matrix = new char[Depth, Width];
            int Index = 0;
            for (int r = 0; r < Depth; r++)
            {
                for (int c = 0; c < Width; c++)
                {
                    if (Index < this.Input.Length)
                    {
                        Matrix[r, c] = this.Input[Index++];
                    }
                    else
                    {
                        Matrix[r, c] = ' ';
                    }
                }
            }
            for (int index = 1; index <= Width; index++)
            {
                int c = 0;
                for (int i = 0; i < this.Key.Length; i++)
                {
                    if (this.Key[i].ToString() == index.ToString())
                    {
                        c = i;
                        break;
                    }
                }
                for (int r = 0; r < Depth; r++)
                {
                    Encrypted_Text += Matrix[r, c];
                }
            }
            return Encrypted_Text;
        }

        public override string Decryption()
        {
            string Decrypted_Text = "", Input = this.Input;
            Input = Input.ToLower();
            this.Input = "";
            for (int index = 0; index < Input.Length; index++)
            {
                if (char.IsLetter(Input[index]))
                {
                    this.Input += Input[index];
                }
            }
            int Width = this.Key.Length;
            int Depth = (int)Math.Ceiling((double)this.Input.Length / (double)Width);
            char[,] Matrix = new char[Depth, Width];
            int Index = 0;

            for (int index = 1; index <= Width; index++)
            {
                int c = 0;
                for (int i = 0; i < this.Key.Length; i++)
                {
                    if (this.Key[i].ToString() == index.ToString())
                    {
                        c = i;
                        break;
                    }
                }
                for (int r = 0; r < Depth; r++)
                {
                    if (Index < this.Input.Length)
                    {
                        Matrix[r, c] = this.Input[Index++];
                    }
                    else
                    {
                        Matrix[r, c] = ' ';
                    }

                }
            }
            for (int r = 0; r < Depth; r++)
            {
                for (int c = 0; c < Width; c++)
                {
                    Decrypted_Text += Matrix[r, c];
                }
            }
            return Decrypted_Text;
        }
    }
}