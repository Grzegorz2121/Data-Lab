using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using New_framework_test;
using DataLab;
using static DataLab.Block_info;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static DataLab.Fabricator;
using static DataLab.MainWindow;
using System.Reflection;
using System.Threading;

namespace DataLab
{
    public class IO_sockiet
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Input sockiet for recieving data
        public class input_sockiet
        {

            public dynamic parent;
            public Button button;

            public double X;
            public double Y;

            public delegate void parent_function(dynamic input);
            public parent_function function_ref;

            public dynamic link_ref;

            public input_sockiet(dynamic parent_ref, Block_info sockiet_pos, ref parent_function func_ref)
            {
                parent = parent_ref;
                create_button(sockiet_pos);
                //button.Click += new RoutedEventHandler(In_sockiet_click);
                button.MouseLeftButtonDown += new MouseButtonEventHandler(In_sockiet_click);
                function_ref = func_ref;
            }

            //unnessesary function i guess
            public Button create_button(Block_info position)
            {
               button = Fabricator.Create_Button(position);
               button.Click += new RoutedEventHandler(In_sockiet_click);
               return button;
            }

            public void Render(double in_X, double in_Y)
            {
                X = in_X + button.Margin.Left + button.Width/2;
                Y = in_Y + button.Margin.Top + button.Height/2;
                if (link_ref != null)
                {
                    link_ref.Update_Line();
                }
            }

            public void debug()
            {
                Console.WriteLine("debug in");
            }

            //Linking button calls main link funtion
            void In_sockiet_click(object sender, EventArgs e)
            {
                //Disconnect_link();
                next_sockiet = this;
                Link_objects();
            }

            public void Disconnect()
            {
                if (link_ref != null)
                {
                    link_ref.Disconnect_link(this);
                    link_ref = null;
                };
            }


        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Output sockiet for outputing data
        public class output_sockiet
        {

            public dynamic parent;
            public Button button;

           // public Line line;
            public double X;
            public double Y;

            //public delegate void parent_function(dynamic input);
            public input_sockiet.parent_function function_ref;

            public dynamic link_ref;

            public output_sockiet(dynamic parent_ref, Block_info sockiet_pos)
            {
                parent = parent_ref;
                create_button(sockiet_pos);
              //  button.Click += new RoutedEventHandler(Out_sockiet_click);
                button.MouseLeftButtonDown += new MouseButtonEventHandler(Out_sockiet_click);
            }

            //unnessesary function i guess
            public Button create_button(Block_info position)
            {
               button = Fabricator.Create_Button(position);
               button.Click += new RoutedEventHandler(Out_sockiet_click);
               return button;
            }

            //MAIN FUNCTION that will call function referenced in input block!!!!!!!!!!!!!!!!!!!!!!!!!
            public void Call_next(dynamic input)
            {
                if(link_ref!=null)
                {
                    function_ref(input);
                } 
            }

            public void Render(double in_X, double in_Y)
            {
                X = in_X + button.Margin.Left + button.Width / 2;
                Y = in_Y + button.Margin.Top + button.Height / 2;
                if (link_ref!=null)
                {
                    link_ref.Update_Line();
                }
            }

            public void debug()
            {
                Console.WriteLine("debug out");
            }

            //Linking button calls main link funtion
            void Out_sockiet_click(object sender, EventArgs e)
            {
                //Disconnect_link();
                previous_sockiet = this;
                Link_objects();
            }

            public void Disconnect()
            {
                if (link_ref != null)
                {
                    link_ref.Disconnect_link(this);
                    link_ref = null;
                };
            }

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}
