using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minivilles
{
    public class Die
    {
        private Random random = new Random();
        public int nbFace;

        public Die(int nbFace)
        {
            this.nbFace = nbFace;
        }

        public int Roll()
        {
            int currentFace = random.Next(1, nbFace + 1);
            return currentFace;
        }
    }
}


