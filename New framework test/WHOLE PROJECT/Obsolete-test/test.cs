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

namespace New_framework_test
{

    /// <summary>
    /// Test class for testing block class, lines, ect
    /// </summary>
    class test
    {
        public bool move = false;
        public GroupBox groupBox;
        public Window main_ref;

        public test(Grid grid_ref, Window main_ref)
        {
            Thickness main_thicc = grid_ref.Margin;

            groupBox = new GroupBox()
            {
                Height = 100,
                Width = 100,
            };
            Button button = new Button()
            {
                
                Margin = new Thickness(0, 0, 0, 0),
                Height = 20,
                Width = 100,
                Content = string.Format("Button for {0}", 10),
                Tag = 1
            };

            button.Click += new RoutedEventHandler(Button_Click);
            groupBox.Content = button;
            groupBox.HorizontalAlignment = HorizontalAlignment.Left;
            groupBox.VerticalAlignment = VerticalAlignment.Top;
            grid_ref.Children.Add(groupBox);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            move = true;

        }

        public void Move()
        {
            if (move == true)
            {
                Point mouse_loc = Mouse.GetPosition(Application.Current.MainWindow);
                Thickness group_loc = groupBox.Margin;
                if(mouse_loc.X>0 && mouse_loc.Y>0)
                {
                    group_loc.Left = mouse_loc.X;
                    group_loc.Top = mouse_loc.Y;
                }
                
                groupBox.Margin = group_loc;
                Console.WriteLine(group_loc);
                Console.WriteLine(mouse_loc);
                /*
                Point p = groupBox.Location;
                p.X = (main_ref.MousePosition.X - form1.Location.X);
                p.Y = (Form1_obj.MousePosition.Y - form1.Location.Y);
                p.X = 10 * (int)Math.Floor((double)(p.X / 10));
                p.Y = 10 * (int)Math.Floor((double)(p.Y / 10));
                p.X -= 40;
                p.Y -= 60;

                groupBox.Location = p;*/
            }
        }
    }
}
