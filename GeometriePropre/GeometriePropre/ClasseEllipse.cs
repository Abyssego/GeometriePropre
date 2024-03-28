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
    class ClasseEllipse : ClasseRond
    {
        protected double PR;

        public ClasseEllipse() : base()
        {
            this.Init();

        }

        public double _PR 
        {
            get { return PR; }
            set { PR = value; }
        }

        public void Dessin(Canvas canvas)
        {
            canvas.Children.Clear();

            Ellipse ellipse = new Ellipse();
            ellipse.Width = 2 * UneValeur * 5;    // On fait x5, car sinon est trop petite à l'affichage
            ellipse.Height = 2 * PR * 5;
            ellipse.Stroke = Brushes.Yellow;
            ellipse.Fill = Brushes.Yellow;

            //Canvas.SetLeft(ellipse, 100);
            //Canvas.SetTop(ellipse, 100);
            Canvas.SetLeft(ellipse, (canvas.ActualWidth - ellipse.Width) / 2);
            Canvas.SetTop(ellipse, (canvas.ActualHeight - ellipse.Height) / 2);

            canvas.Children.Add(ellipse);
        }

        public void Init()
        {
            Random aleatoire = new Random();
            PR = aleatoire.Next(1, 15);

            // Le mettre sinon on obtient toujours PR = UneValeur
            while (PR == UneValeur)
            {
                PR = aleatoire.Next(1, 15);
            }
        }

        public double Surface()
        {
            Double surface = Multiplication(Math.PI, UneValeur);
            surface = Multiplication(surface, PR);

            //Faire ça pour avoir seulement 2 chiffre, apres la virgule et pas 15
            string nombreFormate = surface.ToString("0.00");
            surface = Convert.ToDouble(nombreFormate);

            return surface;
        }
    }


}

