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

        public class Read_all_lines : Basic_block
        {
            private DataProcessing.Input.File file;

            public Button File_button;
            protected OpenFileDialog ofd = new OpenFileDialog();
            protected StreamReader sr;
            string line;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            /// 
            public Read_all_lines(string input) : base(input, block_type.Input)
            {

                File_button = Fabricator.Create_Button(info.File_info);
                canvas.Children.Add(File_button);
                File_button.Click += new RoutedEventHandler(File_button_click);

                file = new DataProcessing.Input.File();

                output_io = new output_sockiet(this, info.Output_info);
                canvas.Children.Add(output_io.button);

            }

            public void File_button_click(object sender, EventArgs e)
            {
                ofd.ShowDialog();
            }

            public override void Read_button_click(object sender, EventArgs e)
            {
                Input_function();
            }

            public void Input_function()
            {
                sr = new StreamReader(ofd.FileName);
                line = sr.ReadLine();
                while (line != null)
                {
                    Output_function(line);
                    line = sr.ReadLine();
                }

            }

            public void Output_function(string input)
            {
                output_io.Call_next(input);
            }

            public override void Render(double X, double Y)
            {
                output_io.Render(X, Y);
            }

            public override void Disconnect()
            {
                output_io.Disconnect();
                output_io = null;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public class Debug_string : Basic_block
        {
            private DataProcessing.Input.File file;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            /// 
            public Debug_string(string input) : base(input, block_type.Input)
            {
                file = new DataProcessing.Input.File();
                output_io = new output_sockiet(this, info.Output_info);
                canvas.Children.Add(output_io.button);

            }

            public override void Read_button_click(object sender, EventArgs e)
            {
                Input_function();
            }

            public void Input_function()
            {
                Output_function("Debug string");
            }

            public void Output_function(string input)
            {
                output_io.Call_next(input);
            }

            public override void Render(double X, double Y)
            {
                output_io.Render(X, Y);
            }

            public override void Disconnect()
            {
                output_io.Disconnect();
                output_io = null;
            }
        }