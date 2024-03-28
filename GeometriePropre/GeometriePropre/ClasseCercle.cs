using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GeometriePropre
{
    class ClasseCercle : ClasseRond
    {
        public void Dessin(Canvas canvas)
        {
            canvas.Children.Clear();


            double rayon = UneValeur;

            // création d'une nouvelle forme Ellipse
            Ellipse cercle = new Ellipse();

            // définition de la taille et de la position de la forme
            cercle.Width = rayon * 2 * 5; // On fait x5, car sinon est trop petite à l'affichage
            cercle.Height = rayon * 2 * 5;
            cercle.Stroke = Brushes.Pink;
            cercle.Fill = Brushes.Pink;
            Canvas.SetLeft(cercle, (canvas.ActualWidth - cercle.Width) / 2);
            Canvas.SetTop(cercle, (canvas.ActualHeight - cercle.Height) / 2);

            // ajout de la forme au Canvas
            canvas.Children.Add(cercle);
        }

        public void Init()
        {
            Random aleatoire = new Random();
        }

        public double Perimetre()
        {
            Double perimetre = Multiplication(2, Math.PI);
            perimetre = Multiplication(perimetre, UneValeur);

            //Faire ça pour avoir seulement 2 chiffre, apres la virgule et pas 15
            string nombreFormate = perimetre.ToString("0.00");
            perimetre = Convert.ToDouble(nombreFormate);

            return perimetre;
            //return 2 * Math.PI * UneValeur;
        }

        public double Surface()
        {
            Double surface = Multiplication(UneValeur, UneValeur);
            surface = Multiplication(Math.PI, surface);

            //Faire ça pour avoir seulement 2 chiffre, apres la virgule et pas 15
            string nombreFormate = surface.ToString("0.00");
            surface = Convert.ToDouble(nombreFormate);

            return surface;
            //return Math.PI * UneValeur * UneValeur;
        }
    }
}
