using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLab.IO_sockiet;
using DataLab;
using static DataLab.Blocks;
using System.Windows.Input;
using System.Windows.Data;
using System.IO;
using System.Drawing;
using System.Reflection;
using static DataLab.MainWindow;
using New_framework_test;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace DataLab
{

    public partial class Blocks
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public class RS_232_output : Basic_block
        {
            public Button setting_button;

            protected System.IO.Ports.SerialPort serial_port;

            protected input_sockiet.parent_function funct_ref;

            public Settings.Box settings_box;

            public int param_baud_rate = 1200;
            public int param_data_bits = 8;
            public System.IO.Ports.Parity param_parity = System.IO.Ports.Parity.None;
            public System.IO.Ports.StopBits param_stop_bits = System.IO.Ports.StopBits.One;


            public RS_232_output(string input) : base(input, block_type.Output)
            {
                Block_info set_buttton_info = info.File_info;
                set_buttton_info.name = "Settings";

                setting_button = Fabricator.Create_Button(set_buttton_info);

                setting_button.Click += new RoutedEventHandler(Setting_button_click);

                canvas.Children.Add(setting_button);

                funct_ref = new input_sockiet.parent_function(Input_function);

                input_io = new input_sockiet(this, info.Input_info, ref funct_ref);

                canvas.Children.Add(input_io.button);
            }

            public void Input_function(dynamic input)
            {
                serial_port.WriteLine(input);
            }

            public void SetSettings()
            {
                settings_box = new Settings.Box(this);

                string[] ports = System.IO.Ports.SerialPort.GetPortNames();
                List<string> port_list = new List<string>();
                foreach(string s in ports)
                {
                    port_list.Add(s);
                }

                Settings.OptionField field = new Settings.OptionField(port_list ,"Port name", "COM1");
                settings_box.AddField(field);
                Settings.Field field1 = new Settings.Field("Baud rate", "1200",5);
                settings_box.AddField(field1);
                settings_box.ShowFields();
            }

            public void GetSettings()
            {
                Console.WriteLine();
                Dictionary<string, string> settings = settings_box.GetSettings();
                Console.WriteLine(settings["Port name"]);
                Console.WriteLine(settings["Baud rate"]);
                
                serial_port = new System.IO.Ports.SerialPort(settings["Port name"]);
                serial_port.BaudRate = Convert.ToInt32(settings["Baud rate"]);
                serial_port.Open();
                Console.WriteLine("Port open");
            }

            public void Setting_button_click(object sender, EventArgs e)
            {
                 
                if(serial_port!=null)
                {
                    serial_port.Close();
                }
                SetSettings();
            }


            public override void Render(double X, double Y)
            {
                input_io.Render(X, Y);
            }

            public override void Disconnect()
            {
                input_io.Disconnect();
                input_io = null;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public class Console_output : Basic_block
        {
            protected input_sockiet.parent_function funct_ref;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            public Console_output(string input) : base(input, block_type.Output)
            {
                funct_ref = new input_sockiet.parent_function(Input_function);

                input_io = new input_sockiet(this, info.Input_info, ref funct_ref);

                canvas.Children.Add(input_io.button);
            }

            public void Input_function(dynamic input)
            {
                Console.WriteLine(input);
            }

            public void Output_function(string input)
            {
                output_ref.Input_function(input);
            }

            public override void Render(double X, double Y)
            {
                input_io.Render(X, Y);
            }

            public override void Disconnect()
            {
                input_io.Disconnect();
                input_io = null;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }


}
