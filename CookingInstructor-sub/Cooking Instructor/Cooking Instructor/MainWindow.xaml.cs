using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cooking_Instructor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // List(Array) of all the screens
        Grid[] listOfScreens;
        // Previous screen to go back onclick "Back"
        Grid previousScreen;
        Grid previousFooter;
        Grid currentscreen;

        public MainWindow()
        {
            InitializeComponent();
            listOfScreens = new Grid[] {
                HomePage, RecipesPage, GlossaryPage, SettingsPage,
                MainFooterGrid };

            // Bring up the homepage
            BringFront(HomePage, MainFooterGrid);
            currentscreen = HomePage;
            
        }




        /// <summary>
        ///     This function hides all the screens except the 
        ///     one that is sent in as a parameter, and brings 
        ///     that to the front. Also has an option to bring 
        ///     a footer to front as well.
        /// </summary>
        /// <param name="screen"> Screen to bring front </param>
        /// <param name="footer"> Footer to bring front </param>
        public void BringFront(Grid screen, Grid footer)
        {
            foreach (Grid g in listOfScreens)
            {
                if (g != screen) // || g != footer)
                {
                    g.Visibility = Visibility.Collapsed;
                }

            }

            // Set the screen
            screen.Visibility = Visibility.Visible;

            // Set the footer
            if (footer != null)
            {
                footer.Visibility = Visibility.Visible;
            }

        }

        // Hide the current window 
        public void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //App.Current.MainWindow.Visibility = Visibility.Collapsed;
            //previousScreen.Visibility = Visibility.Visible;
            BringFront(previousScreen, previousFooter);
        }

        /*
        public void BringFront(Grid screen, Grid footer)
        {
            foreach(Grid g in listOfScreens)
            {
                //Console.WriteLine("g: " + g + " -> "+ (g != gd));
                if (g != screen)
                {
                    Canvas.SetZIndex(g, 0);
                } 

            }
            Canvas.SetZIndex(screen, 1);
            if(footer != null)
            {
                Canvas.SetZIndex(footer, 1);
            }
        }
        */

        // allows the screen to be moved around
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) {
                Close();
            }
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

 


        private void GeneralSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            GeneralSettingsButton.Visibility = Visibility.Collapsed;
            BringFront(SettingsPage, null);
        }

        // Settings_Home
        private void SettingsButton_Home_Click(object sender, RoutedEventArgs e)
        {
            previousScreen = HomePage;
            previousFooter = MainFooterGrid;
            BringFront(SettingsPage, null);
            currentscreen = SettingsPage;
        }

        // Settings_Recipes
        private void SettingsButton_Recipes_Click(object sender, RoutedEventArgs e)
        {
            previousScreen = RecipesPage;
            previousFooter = MainFooterGrid;
            BringFront(SettingsPage, null);
            currentscreen = SettingsPage;
        }

        // Settings_Glossary
        private void SettingsButton_Glossary_Click(object sender, RoutedEventArgs e)
        {
            previousScreen = GlossaryPage;
            previousFooter = MainFooterGrid;
            BringFront(SettingsPage, null);
            currentscreen = SettingsPage;
        }

        private void RecipesButtonControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            BringFront(RecipesPage, MainFooterGrid);
            RecipeTab.Visibility = Visibility.Hidden;
            RecipeTab2.Visibility = Visibility.Visible;
            if (currentscreen == HomePage)
            {
                HomeTab.Visibility = Visibility.Visible;
                HomeTab2.Visibility = Visibility.Hidden;
    
            }
            else if(currentscreen == GlossaryPage)
            {
                GlossaryTab.Visibility = Visibility.Visible;
                GlossaryTab2.Visibility = Visibility.Hidden;
            }
            currentscreen = RecipesPage;

        }

        private void HomeButtonControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BringFront(HomePage, MainFooterGrid);
            HomeTab.Visibility = Visibility.Hidden;
            HomeTab2.Visibility = Visibility.Visible;

            if (currentscreen == RecipesPage)
            {
                RecipeTab.Visibility = Visibility.Visible;
                RecipeTab2.Visibility = Visibility.Hidden;

            }
            else if (currentscreen == GlossaryPage)
            {
                GlossaryTab.Visibility = Visibility.Visible;
                GlossaryTab2.Visibility = Visibility.Hidden;
            }
            currentscreen = HomePage;
        }

        private void GlossaryButtonControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BringFront(GlossaryPage, MainFooterGrid);
            GlossaryTab.Visibility = Visibility.Hidden;
            GlossaryTab2.Visibility = Visibility.Visible;

            if (currentscreen == RecipesPage)
            {
                RecipeTab.Visibility = Visibility.Visible;
                RecipeTab2.Visibility = Visibility.Hidden;

            }
            else if (currentscreen == HomePage)
            {
                HomeTab.Visibility = Visibility.Visible;
                HomeTab2.Visibility = Visibility.Hidden;
            }
            currentscreen = GlossaryPage;
        }
    }
}
