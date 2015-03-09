using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classical_Encryption_Techniques.Transposition_Technique
{
    public abstract class Transposition_Technique
    {
        protected string Input;
        protected string Key;
        protected int Depth;

        public Transposition_Technique(string Input, string Key)
        {
            this.Input = Input;
            this.Key = Key;
        }

        public Transposition_Technique(string Input, int Depth)
        {
            this.Depth = Depth;
            this.Input = Input;
        }
        public abstract string Encryption();
        public abstract string Decryption();
    }
}
