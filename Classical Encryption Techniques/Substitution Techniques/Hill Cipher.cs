using Classical_Encryption_Techniques.Helper_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classical_Encryption_Techniques.Substitution_Techniques
{
    public class Hill_Cipher
    {
        public string Encryption(double[,] Key, string PT)
        {
            string pt = PT.ToLower();
            PT = "";
            for (int i = 0; i < pt.Length; i++)
            {
                if (char.IsLetter(pt[i]))
                    PT += pt[i];
            }
            string CT = "";
            double Length = Math.Sqrt(Key.Length);
            int m = int.Parse(Length.ToString());
            int n = (int)Math.Ceiling((double)PT.Length / (double)m);
            double[,] Input = new double[m, n];
            int r = 0, c = 0;
            for (int i = 0; i < PT.Length; i++)
            {
                Input[r++, c] = PT[i] - 'a';
                if (r >= m)
                {
                    r = 0;
                    c++;
                }
            }
            MatrixOperations MOperations = new MatrixOperations();
            double[,] Output = MOperations.MatrixMultiplication(Key, Input);
            for (c = 0; c < n; c++)
            {
                for (r = 0; r < m; r++)
                {
                    Output[r, c] %= 26;
                    double Counter = Output[r, c];
                    char ch = 'A';
                    while (Counter > 0)
                    {
                        ch++;
                        Counter--;
                    }
                    CT += ch;
                }
            }
            return CT;
        }

        public string Decryption(double[,] Key, string CT)
        {
            string ct = CT.ToUpper();
            CT = "";
            for (int i = 0; i < ct.Length; i++)
            {
                if (char.IsLetter(ct[i]))
                    CT += ct[i];
            }
            string PT = "";
            int m = Key.GetLength(0);
            int n = (int)Math.Ceiling((double)CT.Length / (double)m);
            double[,] Input = new double[m, n];
            int r = 0, c = 0;
            for (int i = 0; i < CT.Length; i++)
            {
                Input[r++, c] = CT[i] - 'A';
                if (r >= m)
                {
                    r = 0;
                    c++;
                }
            }
            MatrixOperations MOperations = new MatrixOperations();
            //1) Calculate det(k) mod 26
            double Determinant = MOperations.Determinant(Key) % 26;
            if (Determinant < 0) Determinant += 26;
            //2) Calculate b = det(k) -1 ( b x det(k) mod 26 =1 )
            int b = 0;
            for (b = 0; b <= Determinant; b++)
            {
                if ((b * Determinant) % 26 == 1)
                {
                    break;
                }
            }
            //3) Apply rule kij ={b x (-1)^(i+j) * Dij mod 26} mod 26
            double[,] KeyInv = new double[m, m];
            for (r = 0; r < m; r++)
            {
                for (c = 0; c < m; c++)
                {
                    KeyInv[r, c] = (b * Math.Pow(-1, r + c) * MOperations.Determinant(MOperations.SubMatrixDeterminant(Key, r, c))) % 26;
                    if (KeyInv[r, c] < 0) KeyInv[r, c] += 26;
                }
            }
            //4) Final K^(-1) = transpose K^(-1)
            KeyInv = MOperations.Transpose(KeyInv);
            //
            double[,] Output = MOperations.MatrixMultiplication(KeyInv, Input);
            for (c = 0; c < n; c++)
            {
                for (r = 0; r < m; r++)
                {
                    Output[r, c] %= 26;
                    double Counter = Output[r, c];
                    char ch = 'a';
                    while (Counter > 0)
                    {
                        ch++;
                        Counter--;
                    }
                    PT += ch;
                }
            }
            return PT;
        }
    }
}
