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
    class ClasseRectangle : Classe4Angles
    {
        protected double Largeur;

        public ClasseRectangle() : base()
        {
            this.Init();
        }

        public double _Largeur
        {
            get { return Largeur; }
            set { Largeur = value; }
        }

        public void Dessin(Canvas canvas)
        {
            canvas.Children.Clear();

            // Créer un rectangle
            Rectangle rect = new Rectangle();
            rect.Width = UneValeur * 5;   // On fait x5, car sinon est trop petite à l'affichage
            rect.Height = Largeur * 5;
            rect.Fill = Brushes.Blue;

            // Ajouter le rectangle au canvas
            canvas.Children.Add(rect);

            // Définir la position du rectangle sur le canvas
            Canvas.SetLeft(rect, (canvas.ActualWidth - rect.Width) / 2);
            Canvas.SetTop(rect, (canvas.ActualHeight - rect.Height) / 2);

        }

        public void Init()
        {
            //base.Init();
            Random aleatoire = new Random();
            Largeur = aleatoire.Next(1, 15);
            // Le mettre sinon on obtient toujours PR = UneValeur
            while (Largeur == UneValeur)
            {
                Largeur = aleatoire.Next(1, 15);
            }
        }

        public double Perimetre()
        {
            Double perimetre = Addition(UneValeur, Largeur);
            perimetre = Multiplication(2, perimetre);
            return perimetre;
        }

        public double Surface()
        {
            Double surface = Multiplication(UneValeur, Largeur);
            return surface;
            //return UneValeur * Largeur;
        }
    }
}
