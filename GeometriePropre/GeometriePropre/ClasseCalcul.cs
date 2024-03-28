using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometriePropre
{
    public class ClasseCalcul
    {
        protected double UneValeur;

        public ClasseCalcul()
        {
            this.Init();
        }

        public double _UneValeur
        {
            get { return UneValeur; }
            set { UneValeur = value; }
        }


        public double Addition(Double ValeurA, Double ValeurB)
        {
            Double valeurAddition = ValeurA + ValeurB;
            return valeurAddition;
        }

        public void Init()
        {
            Random aleatoire = new Random();
            UneValeur = aleatoire.Next(1, 15);
        }

        public double Multiplication(Double ValeurA, Double ValeurB)
        {
            Double valeurMultiplier = ValeurA * ValeurB;
            return valeurMultiplier;
        }
    }
}
