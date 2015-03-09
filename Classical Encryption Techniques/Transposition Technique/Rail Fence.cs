using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classical_Encryption_Techniques.Transposition_Technique
{
    public class Rail_Fence : Transposition_Technique
    {
        public Rail_Fence(string Input, int Depth)
            : base(Input, Depth)
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

            int Width = (int)Math.Ceiling((double)this.Input.Length / (double)this.Depth);
            char[,] Matrix = new char[this.Depth, Width];
            int Index = 0;
            for (int c = 0; c < Width; c++)
            {
                for (int r = 0; r < this.Depth; r++)
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
            for (int r = 0; r < this.Depth; r++)
            {
                for (int c = 0; c < Width; c++)
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

            int Width = (int)Math.Ceiling((double)this.Input.Length / (double)this.Depth);
            char[,] Matrix = new char[this.Depth, Width];
            int Index = 0;

            for (int r = 0; r < this.Depth; r++)
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

            for (int c = 0; c < Width; c++)
            {
                for (int r = 0; r < this.Depth; r++)
                {
                    Decrypted_Text += Matrix[r, c];
                }
            }
            return Decrypted_Text;
        }
    }
}
