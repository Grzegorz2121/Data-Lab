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
using static New_framework_test.MainWindow;
using System.Reflection;

namespace New_framework_test
{
    public class IO_sockiet
    {
        //Input sockiet for recieving data
        public class input_sockiet
        {
            public dynamic parent;
            public Button button;

            public delegate void parent_function(dynamic input);
            public parent_function function_ref;

            public dynamic previous_s;

            public input_sockiet(dynamic parent_ref, Block_info sockiet_pos, ref parent_function func_ref)
            {
                parent = parent_ref;
                create_button(sockiet_pos);
                button.Click += new RoutedEventHandler(In_sockiet_click);

                function_ref = func_ref;
            }

            //unnessesary function i guess
            public Button create_button(Block_info position)
            {
               button = Fabricator.Create_Button(position);
               button.Click += new RoutedEventHandler(In_sockiet_click);
               return button;
            }

            //Linking button calls main link funtion
            void In_sockiet_click(object sender, EventArgs e)
            {
                next_sockiet = this;
                Link_objects();
            }

            //GC is buggy and confusing sorry i want to improve it!
            public void Disconnect_Input()
            {
                if(previous_s!=null)
                {
                    previous_s.next_s = null;
                }
                previous_s = null;
            }
        }

        //Output sockiet for outputing data
        public class output_sockiet
        {
            public dynamic parent;
            public Button button;

            public dynamic next_s;

            public output_sockiet(dynamic parent_ref, Block_info sockiet_pos)
            {
                parent = parent_ref;
                create_button(sockiet_pos);

                button.Click += new RoutedEventHandler(Out_sockiet_click);
            }

            //unnessesary function i guess
            public Button create_button(Block_info position)
            {
               button = Fabricator.Create_Button(position);
               button.Click += new RoutedEventHandler(Out_sockiet_click);
               return button;
            }

            //Linking button calls main link funtion
            void Out_sockiet_click(object sender, EventArgs e)
            {
                previous_sockiet = this;
                Link_objects();
            }

            //MAIN FUNCTION that will call function referenced in input block!!!!!!!!!!!!!!!!!!!!!!!!!
            public void Call_next(dynamic input)
            {
                if(next_s!=null)
                {
                    next_s.function_ref(input);
                } 
            }

            //GC is buggy and confusing sorry i want to improve it!
            public void Disconnect_Output()
            {
                next_s = null;
            }
        }

    }
}
