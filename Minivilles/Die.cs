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
        private int nbFace = 6;
        public int currentFace { get; private set; }

        public Die(int nbFace = 6)
        {
            this.nbFace = nbFace;
        }

        public int Roll()
        {
            currentFace = random.Next(1, nbFace + 1);
            return currentFace;
        }
    }
}


