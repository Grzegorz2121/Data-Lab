using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataLab;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//ing static New_framework_test.Input_blocks;
using static DataLab.Blocks;
using System.Reflection;

namespace DataLab
{
    public class Settings
    {
        public enum DataType {Integer, Float, Double, String}

        public class Field
        {
            protected Grid parent_grid;

            public Button apply_button;
            public TextBox textBox;

            public string setting_name;
            public string setting_data;

            public Field(string setting_n, string settting_d = "", int mult = 0)
            {
                setting_name = setting_n;
                setting_data = settting_d;

                textBox = new TextBox();
                textBox.Margin = new Thickness(0, 0 + 30 * mult, 0, 0);
                textBox.MaxWidth = 50;
                textBox.MaxHeight = 20;
                textBox.Text = setting_data;

                apply_button = new Button();
                apply_button.Margin = new Thickness(80, 0 + 30 * mult, 0, 0);
                apply_button.MaxHeight = 20;
                apply_button.MaxWidth = 50;
                apply_button.Content = setting_n;

                apply_button.Click += Apply_button_Click;
            }

            public void Apply_button_Click(object sender, RoutedEventArgs e)
            {
                setting_data = textBox.Text;
            }

            public void Display(Grid grid)
            {
                grid.Children.Add(textBox);
                grid.Children.Add(apply_button);
            }

        }






        public partial class OptionField
        {
          protected Grid parent_grid;

            public Button apply_button;
            public TextBox textBox;
            public ListBox list_box;

            public List<string> setting_list;

            public string setting_name;
            public string setting_data;

            public OptionField(List<string> setting_l, string setting_n, string settting_d = "", int mult = 0)
            {
                setting_name = setting_n;
                setting_data = settting_d;

                setting_list = setting_l;

                list_box = new ListBox();
                list_box.Margin = new Thickness(0, 0 + 30 * mult, 0, 0);
                list_box.MaxWidth = 50;
                list_box.MaxHeight = 80;


                apply_button = new Button();
                apply_button.Margin = new Thickness(80, 0 + 30 * mult, 0, 0);
                apply_button.MaxHeight = 20;
                apply_button.MaxWidth = 50;
                apply_button.Content = setting_n;

                apply_button.Click += List_button_Click;
            }

            public void List_button_Click(object sender, RoutedEventArgs e)
            {
                setting_data = list_box.SelectedItem.ToString();
            }
            
            public void Display(Grid grid)
            {
                foreach(dynamic d in setting_list)
                {
                    list_box.Items.Add(d);
                }

                grid.Children.Add(list_box);
                grid.Children.Add(apply_button);
            }

        }






        public class Box
        {
            dynamic parent;

            Window window;
            Grid grid;

            List<dynamic> fields = new List<dynamic>();

            Type T;

            public Box(dynamic paren)
            {
                parent = paren;
                grid = new Grid();
                window = new Window();
                window.Margin = new Thickness(0, 0, 100, 100);
                window.MaxHeight = 300;
                window.MaxWidth = 300;
                window.Content = grid;

                window.Closed += Window_Closed;
                        
            }

            private void Window_Closed(object sender, EventArgs e)
            {
                parent.GetSettings();
            }

            public void AddField(dynamic next_field)
            {
                fields.Add(next_field);
            }

            public Dictionary<string, string> GetSettings()
            {
                Dictionary<string, string> setting_pairs = new Dictionary<string, string>(); 

                foreach(dynamic f in fields)
                {
                    setting_pairs.Add(f.setting_name, f.setting_data);
                }

                return setting_pairs;
            }

            public void ShowFields()
            {
                grid.Children.Clear();
                foreach(dynamic f in fields)
                {
                    f.Display(grid);
                }
                window.Show();
                window.Focus();
            }

        }

    }
}
