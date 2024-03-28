using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace GeometriePropre
{
    class ClasseCarre : Classe4Angles
    {
        public ClasseCarre() : base()
        {
            this.Init();
        }
        public void Dessin(Canvas canvas)
        {
            canvas.Children.Clear();

            double longueur = UneValeur;
            double x = canvas.ActualWidth / 2 - longueur / 2;
            double y = canvas.ActualHeight / 2 - longueur / 2;

            Rectangle rectangle = new Rectangle()
            {
                Width = longueur * 5,   // On fait x5, car sinon est trop petite à l'affichage
                Height = longueur * 5,
                Stroke = Brushes.PeachPuff,

                StrokeThickness = 1,
            };

            rectangle.Fill = Brushes.PeachPuff;

            Canvas.SetLeft(rectangle, x);
            Canvas.SetTop(rectangle, y);

            canvas.Children.Add(rectangle);
        }

        public void Init()
        {
            //Récupère la valeur de calcul UneValeur
        }

        public double Perimetre()
        {
            Double perimetre = Multiplication(4, UneValeur);
            return perimetre;
        }

        public double Surface()
        {
            //return UneValeur * UneValeur;
            return Multiplication(UneValeur, UneValeur);
        }
    }
}
