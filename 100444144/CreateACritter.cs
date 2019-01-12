using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _100444144
{
    public class CreateACritter : CritterBrains.ICritterFactory
    {
        const int totalCritterCount = 4;
        const int critterVarieties = 4;

        public IEnumerable<CritterBrains.CritterBrain> GetCritterBrains()
        {
            for (int i = 0; i < totalCritterCount; i++)
            {
                switch (i % critterVarieties)
                {
                    case 0: yield return new One(); break;
                    case 1: yield return new Two(); break;
                    case 2: yield return new Three(); break;
                    case 3: yield return new Four(); break;
                }
            }
        }
    }
}
