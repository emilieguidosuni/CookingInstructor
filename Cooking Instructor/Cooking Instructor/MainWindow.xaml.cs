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

        // CheckedBoxes
        List <String> checkedBoxes;

        public MainWindow()
        {
            InitializeComponent();
            listOfScreens = new Grid[] {
                HomePage, RecipesPage, GlossaryPage, SettingsPage,
                MainFooterGrid };
            checkedBoxes = new List<String>();

            // Bring up the homepage
            BringFront(HomePage, MainFooterGrid);
            
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



        /////////////////////////////////////////


        private List<CheckBox> GetSelectedCheckBoxes(ItemCollection items)
        {
            List<CheckBox> list = new List<CheckBox>();
            foreach (TreeViewItem item in items)
            {
                UIElement elemnt = GetChildControl(item, "chk");
                if (elemnt != null)
                {
                    CheckBox chk = (CheckBox)elemnt;
                    if (chk.IsChecked.HasValue && chk.IsChecked.Value)
                    {
                        list.Add(chk);
                    }
                }

                List<CheckBox> l = GetSelectedCheckBoxes(item.Items);
                list = list.Concat(l).ToList();
            }

            return list;
        }

        private UIElement GetChildControl(DependencyObject parentObject, string childName)
        {

            UIElement element = null;

            if (parentObject != null)
            {
                int totalChild = VisualTreeHelper.GetChildrenCount(parentObject);
                for (int i = 0; i < totalChild; i++)
                {
                    DependencyObject childObject = VisualTreeHelper.GetChild(parentObject, i);

                    if (childObject is FrameworkElement &&
                ((FrameworkElement)childObject).Name == childName)
                    {
                        element = childObject as UIElement;
                        break;
                    }

                    // get its child
                    element = GetChildControl(childObject, childName);
                    if (element != null) break;
                }
            }

            return element;
        }



        /////////////////////////////////////////


        ///////////////////
        // BUTTON CLICKS //
        ///////////////////
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            BringFront(HomePage, MainFooterGrid);
        }

        private void RecipesButton_Click(object sender, RoutedEventArgs e)
        {
            BringFront(RecipesPage, MainFooterGrid);
        }

        private void GlossaryButton_Click(object sender, RoutedEventArgs e)
        {
            BringFront(GlossaryPage, MainFooterGrid);
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
        }

        // Settings_Recipes
        private void SettingsButton_Recipes_Click(object sender, RoutedEventArgs e)
        {
            previousScreen = RecipesPage;
            previousFooter = MainFooterGrid;
            BringFront(SettingsPage, null);
        }

        // Settings_Glossary
        private void SettingsButton_Glossary_Click(object sender, RoutedEventArgs e)
        {
            previousScreen = GlossaryPage;
            previousFooter = MainFooterGrid;
            BringFront(SettingsPage, null);
        }

        

        private void RecipeIngredients_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            if (item.IsExpanded)
            {
                item.IsExpanded = false;
            } else
            {
                item.IsExpanded = true;
            }
            if (item.IsSelected)
            {
                e.Handled = true;
               
            }

            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            HandleCheckBox(sender as CheckBox);
            e.Handled = true;
            
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            HandleCheckBox(sender as CheckBox);
            e.Handled = false;
        }

        void HandleCheckBox(CheckBox checkBox)
        {
            // Use IsChecked.
            bool flag = checkBox.IsChecked.Value;

            string cb = (string) checkBox.Tag;

            if(flag == true)
            {
                if (!checkedBoxes.Contains(cb))
                {
                    checkedBoxes.Add(cb);
                }
            } else
            {
                checkedBoxes.Remove(cb);
            }

            // Check if all the checboxes are checkedy checked
            if (checkedBoxes.Contains("Ingredients") &&
                checkedBoxes.Contains("Cookware"))
            {                
                StartButton.Opacity = 100;
                StartButton.ClickMode = ClickMode.Press;
            }

            // Assign Window Title.
            Console.WriteLine("IsChecked = " + checkBox.Tag);
            checkedBoxes.ForEach(Console.WriteLine);


        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private void StartButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Console.WriteLine("StartButton_MouseLeave");
            if (StartButton.ClickMode == ClickMode.Press)
            {
                Console.WriteLine("StartButton_MouseLeave");
                StartButton.Opacity = 100;
            }
        }

        private void StartButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Console.WriteLine("StartButton_MouseEnter");
            if (StartButton.ClickMode == ClickMode.Press)
            {
                Console.WriteLine("StartButton_MouseEnter");
                StartButton.Opacity = 80;
            }
        }

        private void StartButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("StartButton_MouseLeftButtonDown");
            if (StartButton.ClickMode == ClickMode.Press)
            {
                Console.WriteLine("StartButton_MouseLeftButtonDown");
                StartButton.Opacity = 60;
            }
        }

        private void StartButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("StartButton_MouseLeftButtonUp");
            if (StartButton.ClickMode == ClickMode.Press)
            {
                Console.WriteLine("StartButton_MouseLeftButtonUp");
                StartButton.Opacity = 100;
            }
        }

        private void RecipeSteps_Click(object sender, RoutedEventArgs e)
        {
            RecipeStepsScreen.Visibility = Visibility.Visible;
            RecipeMainPage.Visibility = Visibility.Hidden;
        }

        private void RecipeInformation1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RecipeStepsScreen.Visibility = Visibility.Visible;
        }


        private void btn_Click(object sender, RoutedEventArgs e)
        {
            RecipeSteps_Main.Visibility = Visibility.Visible;
        }

        private void Recipes_Pasta_Button_Click(object sender, RoutedEventArgs e)
        {
            RecipeMainPage.Visibility = Visibility.Visible;
        }

        private void BackButton_RecipeMain_Click(object sender, RoutedEventArgs e)
        {
            RecipeMainPage.Visibility = Visibility.Hidden;
        }
    }
}
