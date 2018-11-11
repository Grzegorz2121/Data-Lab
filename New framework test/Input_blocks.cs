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
            // public output_sockiet output_io;

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
                //  groupBox.Header = "Debug string " + " Component";

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
            // public output_sockiet output_io;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            /// 
            public Debug_string(string input) : base(input, block_type.Input)
            {
                file = new DataProcessing.Input.File();
                //  groupBox.Header = "Debug string " + " Component";

                output_io = new output_sockiet(this, info.Output_info);



                canvas.Children.Add(output_io.button);

            }

            public override void Read_button_click(object sender, EventArgs e)
            {
                Input_function();
            }

            public void Input_function()
            {
                Console.WriteLine("WOW");
                Output_function(file.ReadLine("C:\\Test.txt"));
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
        /*
        public class ReadAllLines : Basic_block
        {
            //public output_sockiet output_io;
            //public input_sockiet input_io;
            protected input_sockiet.parent_function funct_ref;

            protected StreamReader sr;
                
            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            public ReadAllLines(string input) : base(input, block_type.Input)
            {

                output_io = new output_sockiet(this, info.Output_info);
               // funct_ref = new input_sockiet.parent_function(Input_function);
               // input_io = new input_sockiet(this, info.Input_info, ref funct_ref);


                canvas.Children.Add(output_io.button);
                canvas.Children.Add(input_io.button);
            }

            public override void Read_button_click(object sender, EventArgs e)
            {
                Input_function();
            }

            public void Input_function()
            {
                sr = new StreamReader("Test.txt");
                string line = sr.ReadLine();
                while (line != null);
                {
                    Output_function(line);
                    line = sr.ReadLine();
                }

            }

            public void Output_function(string input)
            {
                if (output_io != null)
                {
                    output_io.Call_next(input);
                }
            }

            public override void Render(double X, double Y)
            {
                output_io.Render(X, Y);
                input_io.Render(X, Y);
            }

            public override void Disconnect()
            {
                input_io.Disconnect();
                output_io.Disconnect();
                input_io = null;
                output_io = null;
            }

        }
        */
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}


/*
  public class Add_string:Basic_block
        {
            private DataProcessing.Processing.Math math;
            //public output_sockiet output_io;
            //public input_sockiet input_io;
            protected input_sockiet.parent_function funct_ref;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            public Add_string(string input) : base(input, block_type.Processing)
            {

                math  = new DataProcessing.Processing.Math();
               // groupBox.Header = "Add string " + " Component";

                output_io = new output_sockiet(this, info.Output_info);
                funct_ref = new input_sockiet.parent_function(Input_function);
                input_io = new input_sockiet(this, info.Input_info,ref funct_ref);
                

                canvas.Children.Add(output_io.button);
                canvas.Children.Add(input_io.button);
            }

            public void Input_function(dynamic input)
            {
               // Console.WriteLine(input);
                Output_function(math.CustomOperation(input));
            }

            public void Output_function(string input)
            {
                if(output_io!=null)
                {
                    output_io.Call_next(input);
                }  
            }

            public override void Render(double X, double Y)
            {
                output_io.Render(X,Y);
                input_io.Render(X, Y);
            }

            public override void Disconnect()
            {
                input_io.Disconnect();
                output_io.Disconnect();
                input_io = null;
                output_io = null;
            }

        }
        
     
      public class Debug_string : Basic_block
        {
            private DataProcessing.Input.File file;
           // public output_sockiet output_io;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            /// 
            public Debug_string(string input) : base(input, block_type.Input)
            {
                 file = new DataProcessing.Input.File();
               //  groupBox.Header = "Debug string " + " Component";
               
                 output_io = new output_sockiet(this, info.Output_info);

                
                    
                 canvas.Children.Add(output_io.button);

            }

            public override void Read_button_click(object sender, EventArgs e)
            {
                Input_function();
            }

            public void Input_function()
            {
                Console.WriteLine("WOW");
                Output_function(file.ReadLine("C:\\Test.txt"));  
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
     
     
     
     
     
     
     
     */
