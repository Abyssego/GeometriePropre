using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Threading;
using Timer = System.Timers.Timer;
using Path = System.IO.Path;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace GeometriePropre
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            //Faire cette condition, sinon impossible de restart l'application quand l'utilisateur est à 0 tour dans le quiz
            if (tourZero == false)
            {
                Closing += MainWindow_Closing;
            }

            // Chemin vers le répertoire "Mes Documents" de l'utilisateur
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Nom du fichier de sauvegarde
            string fileName = "txtSauvegarde.txt";

            // Chemin complet du fichier de sauvegarde
            string filePath = Path.Combine(folderPath, fileName);

            try
            {
                // Vérifier si le fichier existe
                if (!File.Exists(filePath))
                {
                    // Le fichier n'existe pas, on crée un nouveau fichier
                    using (StreamWriter fileText = File.CreateText(filePath))
                    {
                        // Écrire des données par défaut dans le fichier
                        fileText.WriteLine("0\n0\n0\n0\n0"); // Données par défaut
                    }
                }
            }
            catch (Exception ex)
            {
                // Gérer l'exception ici, si nécessaire
                Console.WriteLine("Une erreur s'est produite : " + ex.Message);
            }

            // Lire toutes les lignes du fichier
            string[] lines = File.ReadAllLines(filePath);

            // Convertir les données lues en double
            double[] contents = Array.ConvertAll(lines, Double.Parse);

            // Récupérer les valeurs individuelles
            double contenuRecord = contents[0];
            double contenuScore = contents[1];
            double contenuTentative = contents[2];
            double contenuScoreQuiz = contents[3];
            double contenuToursQuiz = contents[4];

            // Afficher les valeurs dans les contrôles correspondants
            txtRecordEntrainement.Text = contenuRecord.ToString();
            txtScoreEntrainement.Text = contenuScore.ToString();
            txtTentativeEntrainement.Text = contenuTentative.ToString();
            txtScoreQuizSave.Text = contenuScoreQuiz.ToString();
            txtNombreToursSave.Text = contenuToursQuiz.ToString();
        }

        //Initialiser les valeurs avec un nombre impossible, pour empecher le joueur d'avoir juste alors qu'il na appuyé sur aucun boutton de figure, qui initialise avec les nombre de la figure affiché
        public double surfaceJuste = 1000;
        public double perimetreJuste = 10000;

        public Boolean estAppuye = false;
        public Boolean startCompteur = false;
        public Double Difficulter = 1;
        public Boolean tourZero = false;

        public Boolean pasMessageQuiz = false;

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs fermer)
        {
            //Vérifier si l'utilisateur dans le quiz n'est pas a son dernier tour, car sinon impossible de restart l'application
            if (tourZero == false && pasMessageQuiz == false) 
            { 
                MessageBoxResult comfirmationFermeture = MessageBox.Show("Voulez-vous vraiment fermer l'application ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (comfirmationFermeture == MessageBoxResult.No)
                {
                    fermer.Cancel = true;
                }
            }
        }



        private void btnComfirme_Click(object sender, RoutedEventArgs e)
        {
            //Vérifier si une figure est dessiné
            if(estAppuye == false)
            {
                MessageBoxResult verificationFigure = MessageBox.Show("Vous n'avez pas dessiner de figure !!", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            String alphabet = "abcdefghijklmnopqrstvwxyz";
            String alphabetMaj = alphabet.ToUpper();
            String listeSigne = "&é#{[|`^@]}&é'(-è_çà)=+,?;:/!§ù%*µ$£^¨";

            String surface = Convert.ToString(txtSurface.Text);
            String perimetre = Convert.ToString(txtPerimetre.Text);


            Boolean signeInterdit = false;

            //Vérifié si les chaines ne sont pas vides
            if (string.IsNullOrEmpty(surface))
            {
                signeInterdit = true;
            }
            if (string.IsNullOrEmpty(perimetre))
            {
                signeInterdit = true;
            }

            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des lettres minuscules dans la textBox de surface
                foreach (char lettre in alphabet)
                {
                    foreach (char lettreInSurface in surface)
                    {
                        if (lettre == lettreInSurface)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }

            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des lettres majuscules dans la textBox de surface
                foreach (char lettre in alphabetMaj)
                {
                    foreach (char lettreInSurface in surface)
                    {
                        if (lettre == lettreInSurface)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }

            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des signe interdit dans la textBox de surface
                foreach (char signe in listeSigne)
                {
                    foreach (char lettreInSurface in surface)
                    {
                        if (signe == lettreInSurface)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }




            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des lettres minuscules dans la textBox de perimetre
                foreach (char lettre in alphabet)
                {
                    foreach (char lettreInPerimetre in perimetre)
                    {
                        if (lettre == lettreInPerimetre)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }

            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des lettres majuscules dans la textBox de perimetre
                foreach (char lettre in alphabetMaj)
                {
                    foreach (char lettreInPerimetre in perimetre)
                    {
                        if (lettre == lettreInPerimetre)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }

            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des signe interdit dans la textBox de perimetre
                foreach (char signe in listeSigne)
                {
                    foreach (char lettreInPerimetre in perimetre)
                    {
                        if (signe == lettreInPerimetre)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }

            if(signeInterdit == true)
            {
                MessageBoxResult verificationTexte = MessageBox.Show("Vous avez surement mis des lettres, ou signes, veuillez réessayé, en les enlevants, ou vous avez laissé un espace vide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }



            if (signeInterdit == false && estAppuye == true)
            {
                Double Surface = Convert.ToDouble(surface);
                Double Perimetre = Convert.ToDouble(perimetre);

                String Message = "";

                Double corectionCoulleur = 0;
                if (Surface == surfaceJuste)
                {
                    Message += "Bravo, vous avez trouvé la surface !\n";
                    corectionCoulleur += 1;
                }
                else if (Surface != surfaceJuste)
                {
                    Message += "Dommage, vous avez échoué à trouver la surface, c'était : " + surfaceJuste + "\n";
                }


                if (Perimetre == perimetreJuste)
                {
                    Message += "Bravo, vous avez trouvé le perimetre !\n";
                    corectionCoulleur += 1;
                }
                else if (Perimetre != perimetreJuste)
                {
                    Message += "Dommage, vous avez échoué à trouver le perimetre, c'était : " + perimetreJuste + "\n";
                }


                //Changer la coulleur en fonction de si la réponse est juste ou non
                if (corectionCoulleur == 0)
                {
                    txtCorrection.Background = Brushes.Red;
                }
                if (corectionCoulleur == 1)
                {
                    txtCorrection.Background = Brushes.Yellow;
                }
                if (corectionCoulleur == 2)
                {
                    txtCorrection.Background = Brushes.GreenYellow;
                }

                txtCorrection.Text = Message;



                //Vider les txtBox pour que l'utilisateur est une interface propre pour recommencer
                txtSurface.Clear();
                txtPerimetre.Clear();

                // Chemin vers le répertoire "Documents" de l'utilisateur
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Nom du fichier de sauvegarde
                string fileName = "txtSauvegarde.txt";

                // Chemin complet du fichier de sauvegarde
                string filePath = Path.Combine(folderPath, fileName);

                try
                {
                    // Vérifier si le fichier existe
                    if (!File.Exists(filePath))
                    {
                        // Le fichier n'existe pas, on crée un nouveau fichier
                        using (StreamWriter fileText = File.CreateText(filePath))
                        {
                            // Écrire des données par défaut dans le fichier
                            fileText.WriteLine("0\n0\n0\n0\n0"); // Données par défaut
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Gérer l'exception ici, si nécessaire
                    Console.WriteLine("Une erreur s'est produite : " + ex.Message);
                }






                int ligneRecord = 0;
                int ligneScore = 1;
                int ligneTentative = 2;
                int ligneScoreQuiz = 3;
                int ligneToursQuiz = 4;


                // Lire toutes les lignes du fichier
                string[] lines = File.ReadAllLines(filePath);

                // Récupérer la ligne souhaitée
                Double contenuRecord = Convert.ToDouble(lines[ligneRecord]);
                Double contenuScore = Convert.ToDouble(lines[ligneScore]);
                Double contenuTentative = Convert.ToDouble(lines[ligneTentative]) + 1;
                Double contenuScoreQuiz = Convert.ToDouble(lines[ligneScoreQuiz]);
                Double contenuToursQuiz = Convert.ToDouble(lines[ligneToursQuiz]);

                //Vérifier correctionCoulleur pour savoir combien le joueur gagne de point
                if (corectionCoulleur == 1)
                {
                    contenuRecord += 1;
                    contenuScore += 1;
                }
                if (corectionCoulleur == 2)
                {
                    contenuRecord += 2;
                    contenuScore += 2;
                }

                String contenu = Convert.ToString(contenuRecord) + "\n" + Convert.ToString(contenuScore) + "\n" + Convert.ToString(contenuTentative) + "\n" + Convert.ToString(contenuScoreQuiz) + "\n" + Convert.ToString(contenuToursQuiz);

                // Supprimer tout le contenu du fichier
                File.WriteAllText(filePath, string.Empty);

                if (!File.Exists(filePath))
                {
                    // Le fichier n'existe pas, on le crée
                    using (StreamWriter fileText = File.CreateText(filePath))
                    {
                        // On écrit les données dans le fichier
                        fileText.WriteLine(contenu);
                        fileText.Close();
                    }
                }
                else
                {
                    // Le fichier existe déjà, on ajoute les données à la fin du fichier
                    using (StreamWriter fileText = File.AppendText(filePath))
                    {
                        // On écrit les données dans le fichier
                        fileText.WriteLine(contenu);
                        fileText.Close();
                    }
                }

                txtScoreEntrainement.Text = Convert.ToString(contenuScore);
                txtRecordEntrainement.Text = Convert.ToString(contenuRecord);
                txtTentativeEntrainement.Text = Convert.ToString(contenuTentative);

                //Faire un random pour que l'utilisateur tombe sur une autre figure
                btnRandom_Click(this, new RoutedEventArgs());

            }
        }


        private void btnCarre_Click(object sender, RoutedEventArgs e)
        {
            ClasseCarre carre = new ClasseCarre();
            carre.Dessin(CanvaDessin);
            resetButon();
            btnCarre.IsEnabled = false;

            surfaceJuste = carre.Surface();
            perimetreJuste = carre.Perimetre();

            txtMesure.Text = "Côtés : " + Convert.ToString(carre._UneValeur);

            estAppuye = true;
        }

        private void btnRectangle_Click(object sender, RoutedEventArgs e)
        {
            ClasseRectangle rectangle = new ClasseRectangle();
            rectangle.Dessin(CanvaDessin);
            resetButon();
            btnRectangle.IsEnabled = false;

            surfaceJuste = rectangle.Surface();
            perimetreJuste = rectangle.Perimetre();

            txtMesure.Text = "Longueur : " + Convert.ToString(rectangle._UneValeur) + "\n" + "Largeur : " + Convert.ToString(rectangle._Largeur);

            estAppuye = true;
        }

        private void btnTriangle_Click(object sender, RoutedEventArgs e)
        {
            ClasseTriangle triangle = new ClasseTriangle();
            triangle.Dessin(CanvaDessin);
            resetButon();
            btnTriangle.IsEnabled = false;

            surfaceJuste = triangle.Surface();
            perimetreJuste = triangle.Perimetre();

            txtMesure.Text = "Côtés 1 : " + Convert.ToString(triangle._C1) + "\n" + "Côtés 2 : " + Convert.ToString(triangle._C2) + "\n" + "Côtés 3 : " + Convert.ToString(triangle._UneValeur);

            estAppuye = true;
        }

        private void btnCercle_Click(object sender, RoutedEventArgs e)
        {
            ClasseCercle cercle = new ClasseCercle();
            cercle.Dessin(CanvaDessin);
            resetButon();
            btnCercle.IsEnabled = false;

            surfaceJuste = cercle.Surface();
            perimetreJuste = cercle.Perimetre();

            txtMesure.Text = "Rayon : " + Convert.ToString(cercle._UneValeur);

            estAppuye = true;
        }

        private void btnEllipse_Click(object sender, RoutedEventArgs e)
        {
            ClasseEllipse ellipse = new ClasseEllipse();
            ellipse.Dessin(CanvaDessin);
            resetButon();
            btnEllipse.IsEnabled = false;

            surfaceJuste = ellipse.Surface();
            //Désactivé le perimetre comme il y en a pas
            txtPerimetre.IsEnabled = false;

            txtMesure.Text = "Rayon 1 : " + Convert.ToString(ellipse._UneValeur) + "\n" + "Rayon 2 : " + Convert.ToString(ellipse._PR);

            estAppuye = true;
        }


        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();

            Boolean different = false;

            //Boucle pour que la prochaine figure soit différente de la précédente
            while (different == false)
            {
                
                Double dessinRandom = random.Next(1, 5);
                if (dessinRandom == 1 && btnCarre.IsEnabled == true)
                {
                    btnCarre_Click(this, new RoutedEventArgs());
                    different = true;
                }
                if (dessinRandom == 2 && btnRectangle.IsEnabled == true)
                {
                    btnRectangle_Click(this, new RoutedEventArgs());
                    different = true;
                }
                if (dessinRandom == 3 && btnTriangle.IsEnabled == true)
                {
                    btnTriangle_Click(this, new RoutedEventArgs());
                    different = true;
                }
                if (dessinRandom == 4 && btnCercle.IsEnabled == true)
                {
                    btnCercle_Click(this, new RoutedEventArgs());
                    different = true;
                }
                if (dessinRandom == 5 && btnEllipse.IsEnabled == true)
                {
                    btnEllipse_Click(this, new RoutedEventArgs());
                    different = true;
                }
            }

        }

        //Fonction utilisé pour que tous les boutons soit appuiyable, le bouton qui ne sera plus apuiyable sera désactivé dans la fonction du bouton
        private void resetButon()
        {
            btnCarre.IsEnabled = true;
            btnRectangle.IsEnabled = true;
            btnTriangle.IsEnabled = true;
            btnCercle.IsEnabled = true;
            btnEllipse.IsEnabled = true;
            
            //Réactivé le txtPerimetre qui peut être en false si le bouton cliqué est une ellipse
            txtPerimetre.IsEnabled=true;
        }

        public double nombreToursDouble = 0;
        //Valeur qui sera enregistré dans le fichier texte
        public double nombreToursFixe = 0;
        private void btnEntrer_Click(object sender, RoutedEventArgs e)
        {
            String alphabet = "abcdefghijklmnopqrstvwxyz";
            String alphabetMaj = alphabet.ToUpper();
            String listeSigne = "&é#{[|`^@]}&é'(-è_çà)=+,?;:/!§ù%*µ$£^¨";

            String nombreTours = Convert.ToString(txtNombreTours.Text);

            nombreToursFixe = Convert.ToDouble(txtNombreTours.Text);


            Boolean signeInterdit = false;

            //Vérifié si les chaines ne sont pas vides
            if (string.IsNullOrEmpty(nombreTours))
            {
                signeInterdit = true;
            }

            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des lettres minuscules dans la textBox du nombre de tours
                foreach (char lettre in alphabet)
                {
                    foreach (char lettreInNombreTours in nombreTours)
                    {
                        if (lettre == lettreInNombreTours)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }

            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des lettres majuscules dans la textBox du nombre de tours
                foreach (char lettre in alphabetMaj)
                {
                    foreach (char lettreInNombreTours in nombreTours)
                    {
                        if (lettre == lettreInNombreTours)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }

            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des signe interdit dans la textBox du nombre de tours
                foreach (char signe in listeSigne)
                {
                    foreach (char lettreInNombreTours in nombreTours)
                    {
                        if (signe == lettreInNombreTours)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }

            Boolean selectionDifficulte = true;
            //Vérifier si une difficulté est séléctionné
            if(rdbDifficile.IsChecked == false && rdbFacile.IsChecked == false && rdbNormal.IsChecked == false)
            {
                selectionDifficulte = false;
            }

            //Vérifier si tous les informations demandé sont remplie
            if(signeInterdit == false && selectionDifficulte == true)
            {
                txtNombreTours.Visibility = Visibility.Hidden;
                labelTours.Visibility = Visibility.Hidden;
                labelDifficulté.Visibility = Visibility.Hidden;
                btnEntrer.Visibility = Visibility.Hidden;
                rdbDifficile.Visibility = Visibility.Hidden;
                rdbFacile.Visibility = Visibility.Hidden;
                rdbNormal.Visibility = Visibility.Hidden;

                labelSurfaceQuiz.Visibility = Visibility.Visible;
                labelPerimetreQuiz.Visibility = Visibility.Visible;
                btnComfirmeQuiz.Visibility = Visibility.Visible;
                txtSurfaceQuiz.Visibility = Visibility.Visible;
                txtScoreQuiz.Visibility = Visibility.Visible;
                txtPerimetreQuiz.Visibility = Visibility.Visible;
                labelScoreQuiz.Visibility = Visibility.Visible;
                labelTemps.Visibility = Visibility.Visible;
                txtTemps.Visibility = Visibility.Visible;
                labelScoreQuizSave.Visibility = Visibility.Visible;
                labelLastGame.Visibility = Visibility.Visible;
                txtScoreQuizSave.Visibility = Visibility.Visible;
                labelNombreTours.Visibility = Visibility.Visible;
                txtNombreToursSave.Visibility = Visibility.Visible;
                labelToursRestant.Visibility = Visibility.Visible;
                txtToursRestant.Visibility = Visibility.Visible;

                nombreToursDouble = Convert.ToDouble(nombreTours);

                MessageBoxResult attention = MessageBox.Show("Le quiz va commencer, veuillez faire attention au temps", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                if(attention == MessageBoxResult.OK)
                {
                    //Vérifié qu'elle difficulté est séléctionner, et la mettre dans une variable pour l'utiliser pour décider du temps du joueur
                    if (rdbFacile.IsChecked == true)
                    {
                        Difficulter = 0;
                    }
                    else if (rdbNormal.IsChecked == true)
                    {
                        Difficulter = 1;
                    }
                    else if (rdbDifficile.IsChecked == true)
                    {
                        Difficulter = 2;
                    }

                    if (Difficulter == 0)
                    {
                        // Initialisez le compteur
                        _countdown = 50;
                    }
                    if (Difficulter == 1)
                    {
                        // Initialisez le compteur
                        _countdown = 25;
                    }
                    if (Difficulter == 2)
                    {
                        // Initialisez le compteur
                        _countdown = 15;
                    }


                    // Configurez le timer
                    _timer.Elapsed += OnTimerElapsed;
                    _timer.AutoReset = true;
                    _timer.Enabled = true;

                    // Mettez à jour l'affichage du compteur
                    UpdateCountdownDisplay();

                    //Afficher une figure 
                    afficherFigureQuiz(CanvaDessinQuiz);
                }
            } else
            {
                if (selectionDifficulte == false)
                {
                    MessageBoxResult verificationDifficulte = MessageBox.Show("Vous n'avez pas sélectionner de dificulté", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if(signeInterdit == true)
                {
                    MessageBoxResult verificationTexte = MessageBox.Show("Vous avez surement mis des lettres, ou signes, veuillez réessayé, en les enlevants, ou vous avez laissé un espace vide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private readonly Timer _timer = new Timer(1000);
        private int _countdown = 30;

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Decrease countdown
            if (_countdown > 0)
            {
                _countdown--;
            }
            else
            {

                // Countdown reached 0, stop timer
                //_timer.Stop();

                //Vérifier si l'erreur est deja afficher sinon il l'affiche a l'infini
                if(_countdown == 0)
                {
                    //Changer le countdown, sinon plien de messageBox s'affiche
                    _countdown = 100000;
                    MessageBoxResult tempsEcoule = MessageBox.Show("Votre temps est écoulé, vous avez échoué, l'application va redémarrer, car il n'y a pas de seconde chance.", "Temps écoulé", MessageBoxButton.OK, MessageBoxImage.Error);

                    if (tempsEcoule == MessageBoxResult.OK)
                    {
                        pasMessageQuiz = true;
                        //Faire comme cela sinon, on obtient une erreur de priorité de thread
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Application.Current.Shutdown();
                            System.Windows.Forms.Application.Restart();
                        });
                    }
                }
            }

            // Update countdown display on UI thread
            Dispatcher.Invoke(() =>
            {
                UpdateCountdownDisplay();
            });
        }


        private void UpdateCountdownDisplay()
        {
            // Update textbox with current countdown value
            txtTemps.Text = _countdown.ToString();
        }


        private void afficherFigureQuiz(Canvas canvaOuDessiner)
        {
            Random random = new Random();

            Boolean different = false;

            //Boucle pour que la prochaine figure soit différente de la précédente
            while (different == false)
            {

                Double dessinRandom = random.Next(1, 5);
                if (dessinRandom == 1 && btnCarre.IsEnabled == true)
                {
                    ClasseCarre carre = new ClasseCarre();
                    carre.Dessin(canvaOuDessiner);
                    resetButon();
                    btnCarre.IsEnabled = false;

                    surfaceJuste = carre.Surface();
                    perimetreJuste = carre.Perimetre();

                    txtMesureQuiz.Text = "Côtés : " + Convert.ToString(carre._UneValeur);

                    estAppuye = true;

                    different = true;
                }
                if (dessinRandom == 2 && btnRectangle.IsEnabled == true)
                {
                    ClasseRectangle rectangle = new ClasseRectangle();
                    rectangle.Dessin(canvaOuDessiner);
                    resetButon();
                    btnRectangle.IsEnabled = false;

                    surfaceJuste = rectangle.Surface();
                    perimetreJuste = rectangle.Perimetre();

                    txtMesureQuiz.Text = "Longueur : " + Convert.ToString(rectangle._UneValeur) + "\n" + "Largeur : " + Convert.ToString(rectangle._Largeur);

                    estAppuye = true;

                    different = true;
                }
                if (dessinRandom == 3 && btnTriangle.IsEnabled == true)
                {
                    ClasseTriangle triangle = new ClasseTriangle();
                    triangle.Dessin(canvaOuDessiner);
                    resetButon();
                    btnTriangle.IsEnabled = false;

                    surfaceJuste = triangle.Surface();
                    perimetreJuste = triangle.Perimetre();

                    txtMesureQuiz.Text = "Côtés 1 : " + Convert.ToString(triangle._C1) + "\n" + "Côtés 2 : " + Convert.ToString(triangle._C2) + "\n" + "Côtés 3 : " + Convert.ToString(triangle._UneValeur);

                    estAppuye = true;

                    different = true;
                }
                if (dessinRandom == 4 && btnCercle.IsEnabled == true)
                {
                    ClasseCercle cercle = new ClasseCercle();
                    cercle.Dessin(canvaOuDessiner);
                    resetButon();
                    btnCercle.IsEnabled = false;

                    surfaceJuste = cercle.Surface();
                    perimetreJuste = cercle.Perimetre();

                    txtMesureQuiz.Text = "Rayon : " + Convert.ToString(cercle._UneValeur);

                    estAppuye = true;

                    different = true;
                }
                if (dessinRandom == 5 && btnEllipse.IsEnabled == true)
                {
                    ClasseEllipse ellipse = new ClasseEllipse();
                    ellipse.Dessin(canvaOuDessiner);
                    resetButon();
                    btnEllipse.IsEnabled = false;

                    surfaceJuste = ellipse.Surface();
                    //Désactivé le perimetre comme il y en a pas
                    txtPerimetre.IsEnabled = false;

                    txtMesureQuiz.Text = "Rayon 1 : " + Convert.ToString(ellipse._UneValeur) + "\n" + "Rayon 2 : " + Convert.ToString(ellipse._PR);

                    estAppuye = true;

                    different = true;
                }
            }
        }

        private void btnComfirmeQuiz_Click(object sender, RoutedEventArgs e)
        {
            //Vérifier si une figure est dessiné
            if (estAppuye == false)
            {
                MessageBoxResult verificationFigure = MessageBox.Show("Vous n'avez pas dessiner de figure !!", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            String alphabet = "abcdefghijklmnopqrstvwxyz";
            String alphabetMaj = alphabet.ToUpper();
            String listeSigne = "&é#{[|`^@]}&é'(-è_çà)=+,?;:/!§ù%*µ$£^¨";

            String surface = Convert.ToString(txtSurfaceQuiz.Text);
            String perimetre = Convert.ToString(txtPerimetreQuiz.Text);


            Boolean signeInterdit = false;

            //Vérifié si les chaines ne sont pas vides
            if (string.IsNullOrEmpty(surface))
            {
                signeInterdit = true;
            }
            if (string.IsNullOrEmpty(perimetre))
            {
                signeInterdit = true;
            }

            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des lettres minuscules dans la textBox de surface
                foreach (char lettre in alphabet)
                {
                    foreach (char lettreInSurface in surface)
                    {
                        if (lettre == lettreInSurface)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }

            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des lettres majuscules dans la textBox de surface
                foreach (char lettre in alphabetMaj)
                {
                    foreach (char lettreInSurface in surface)
                    {
                        if (lettre == lettreInSurface)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }

            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des signe interdit dans la textBox de surface
                foreach (char signe in listeSigne)
                {
                    foreach (char lettreInSurface in surface)
                    {
                        if (signe == lettreInSurface)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }




            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des lettres minuscules dans la textBox de perimetre
                foreach (char lettre in alphabet)
                {
                    foreach (char lettreInPerimetre in perimetre)
                    {
                        if (lettre == lettreInPerimetre)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }

            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des lettres majuscules dans la textBox de perimetre
                foreach (char lettre in alphabetMaj)
                {
                    foreach (char lettreInPerimetre in perimetre)
                    {
                        if (lettre == lettreInPerimetre)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }

            //Vérifie si une boucle na pas deja detecté une lettre ou signe interdit, pour ne pas faire de boucle inutile.
            if (signeInterdit == false)
            {
                //Vérifier si il y a des signe interdit dans la textBox de perimetre
                foreach (char signe in listeSigne)
                {
                    foreach (char lettreInPerimetre in perimetre)
                    {
                        if (signe == lettreInPerimetre)
                        {
                            signeInterdit = true;
                        }
                    }
                }
            }

            if (signeInterdit == true)
            {
                MessageBoxResult verificationTexte = MessageBox.Show("Vous avez surement mis des lettres, ou signes, veuillez réessayé, en les enlevants, ou vous avez laissé un espace vide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }



            if (signeInterdit == false && estAppuye == true)
            {
                nombreToursDouble -= 1;
                txtToursRestant.Text = Convert.ToString(nombreToursDouble);

                Double Surface = Convert.ToDouble(surface);
                Double Perimetre = Convert.ToDouble(perimetre);

                Double contenuRecord = 0;
                Double contenuScore = 0;

                String Message = "";

                Double corectionCoulleur = 0;
                if (Surface == surfaceJuste)
                {
                    Message += "Bravo, vous avez trouvé la surface !\n";
                    corectionCoulleur += 1;
                }
                else if (Surface != surfaceJuste)
                {
                    Message += "Dommage, vous avez échoué à trouver la surface, c'était : " + surfaceJuste + "\n";
                }


                if (Perimetre == perimetreJuste)
                {
                    Message += "Bravo, vous avez trouvé le perimetre !\n";
                    corectionCoulleur += 1;
                }
                else if (Perimetre != perimetreJuste)
                {
                    Message += "Dommage, vous avez échoué à trouver le perimetre, c'était : " + perimetreJuste + "\n";
                }


                //Changer la coulleur en fonction de si la réponse est juste ou non
                if (corectionCoulleur == 0)
                {
                    txtCorrectionQuiz.Background = Brushes.Red;
                }
                if (corectionCoulleur == 1)
                {
                    txtCorrectionQuiz.Background = Brushes.Yellow;
                }
                if (corectionCoulleur == 2)
                {
                    txtCorrectionQuiz.Background = Brushes.GreenYellow;
                }

                txtCorrectionQuiz.Text = Message;


                //Vérifier correctionCoulleur pour savoir combien le joueur gagne de point
                if (corectionCoulleur == 1)
                {
                    contenuRecord += 1;
                    contenuScore += 1;
                }
                if (corectionCoulleur == 2)
                {
                    contenuRecord += 2;
                    contenuScore += 2;
                }


                txtScoreQuiz.Text = Convert.ToString(contenuScore);


                if (Difficulter == 0)
                {
                    // Initialisez le compteur
                    _countdown = 50;
                }
                if (Difficulter == 1)
                {
                    // Initialisez le compteur
                    _countdown = 25;
                }
                if (Difficulter == 1)
                {
                    // Initialisez le compteur
                    _countdown = 15;
                }

                //Vider les txtBox pour que l'utilisateur est une interface propre pour recommencer
                txtSurfaceQuiz.Clear();
                txtPerimetreQuiz.Clear();

                //Condition pour voir si il reste encore des tentatives
                if(nombreToursDouble == 0)
                {
                    string fileName = "txtSauvegarde.txt";
                    string folderPath = "E:\\Mes Documents\\Julliard\\GeometriePropre\\GeometriePropre\\Ressources";

                    string filePath = System.IO.Path.Combine(folderPath, fileName);

                    try
                    {
                        // Vérifier si le fichier existe sur le disque D
                        if (!System.IO.File.Exists(filePath))
                        {
                            // Si le fichier n'existe pas sur le disque D, essayer le disque E
                            folderPath = "D:\\Mes Documents\\Julliard\\GeometriePropre\\GeometriePropre\\Ressources";
                            filePath = System.IO.Path.Combine(folderPath, fileName);

                            // Vérifier si le fichier existe sur le disque E
                            if (!System.IO.File.Exists(filePath))
                            {
                                // Le fichier n'existe sur aucun des disques, gérer cette situation selon vos besoins
                                // Par exemple, vous pouvez créer un nouveau fichier ou afficher un message d'erreur
                                Console.WriteLine("Le fichier n'existe pas sur les disques D et E.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Gérer l'exception ici, si nécessaire
                        Console.WriteLine("Une erreur s'est produite : " + ex.Message);
                    }


                    int ligneRecord = 0;
                    int ligneScore = 1;
                    int ligneTentative = 2;
                    int ligneScoreQuiz = 3;
                    int ligneToursQuiz = 4;


                    // Lire toutes les lignes du fichier
                    string[] lines = File.ReadAllLines(filePath);

                    // Récupérer la ligne souhaitée
                    Double contenuRecordDeux = Convert.ToDouble(lines[ligneRecord]);
                    Double contenuScoreDeux = Convert.ToDouble(lines[ligneScore]);
                    Double contenuTentative = Convert.ToDouble(lines[ligneTentative]) + 1;
                    Double contenuScoreQuiz = Convert.ToDouble(lines[ligneScoreQuiz]);
                    Double contenuToursQuiz = Convert.ToDouble(lines[ligneToursQuiz]);


                    String contenu = Convert.ToString(contenuRecordDeux) + "\n" + Convert.ToString(contenuScoreDeux) + "\n" + Convert.ToString(contenuTentative) + "\n" + Convert.ToString(contenuScore) + "\n" + Convert.ToString(nombreToursFixe);

                    // Supprimer tout le contenu du fichier
                    File.WriteAllText(filePath, string.Empty);

                    if (!File.Exists(filePath))
                    {
                        // Le fichier n'existe pas, on le crée
                        using (StreamWriter fileText = File.CreateText(filePath))
                        {
                            // On écrit les données dans le fichier
                            fileText.WriteLine(contenu);
                            fileText.Close();
                        }
                    }
                    else
                    {
                        // Le fichier existe déjà, on ajoute les données à la fin du fichier
                        using (StreamWriter fileText = File.AppendText(filePath))
                        {
                            // On écrit les données dans le fichier
                            fileText.WriteLine(contenu);
                            fileText.Close();
                        }
                    }

                    txtScoreQuizSave.Text = Convert.ToString(contenuScoreQuiz);
                    txtNombreToursSave.Text = Convert.ToString(contenuToursQuiz);



                    //Variable mise à true pour pouvoir fermer l'application
                    tourZero = true;
                    MessageBoxResult comfirmationFermeture = MessageBox.Show("Voulez-vous continuer à jouer", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (comfirmationFermeture == MessageBoxResult.No)
                    {
                        Application.Current.Shutdown();
                    }
                    else if (comfirmationFermeture == MessageBoxResult.Yes)
                    {
                        Application.Current.Shutdown();
                        System.Windows.Forms.Application.Restart();
                    }

                }
                //Condition qui est un détail pour faire des transitions plus propre
                if (nombreToursDouble != 0)
                {
                    //Faire un random pour que l'utilisateur tombe sur une autre figure
                    afficherFigureQuiz(CanvaDessinQuiz);
                }
            }
        }
    }
}
