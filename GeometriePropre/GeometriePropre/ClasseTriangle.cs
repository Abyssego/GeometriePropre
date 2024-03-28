using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GeometriePropre
{
    class ClasseTriangle : ClasseAvecAngle
    {
        protected double C1;
        protected double C2;

        public ClasseTriangle() : base()
        {
            this.Init();
        }

        public double _C1
        {
            get { return C1; }
            set { C1 = value; }
        }

        public double _C2
        {
            get { return C2; }
            set { C2 = value; }
        }


        public void Dessin(Canvas canvas)
        {
            canvas.Children.Clear();

            double baseX = canvas.ActualWidth / 2 - (3 * UneValeur) / 2;
            double baseY = canvas.ActualHeight / 2 - (3 * UneValeur) / 2;

            Polygon triangle = new Polygon();
            triangle.Points = new PointCollection();
            triangle.Points.Add(new Point(baseX, baseY));
            triangle.Points.Add(new Point(baseX + 3 * C1, baseY));
            triangle.Points.Add(new Point(baseX, baseY + 3 * UneValeur));
            triangle.Fill = Brushes.Red;
            canvas.Children.Add(triangle);
        }

        public void Init()
        {
            Random aleatoire = new Random();
            C1 = aleatoire.Next(1, 15);
            C2 = aleatoire.Next(1, 15);

            // Le mettre sinon on obtient toujours C1 = UneValeur
            while (C1 == UneValeur)
            {
                C1 = aleatoire.Next(1, 15);
            }
            // Le mettre sinon on obtient toujours C2 = UneValeur
            while (C2 == UneValeur)
            {
                C2 = aleatoire.Next(1, 15);
            }
        }

        public double Perimetre()
        {
            Double perimetre = Addition(C1, C2);
            perimetre = Addition(perimetre, UneValeur);
            return perimetre;
        }

        public double Surface()
        {
            Double surface = Multiplication(C1, UneValeur);
            surface = Multiplication(surface, 0.5);
            return surface;
            //return C1 * Haut / 2;
        }
    }
}
