using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Reflection;
using static New_framework_test.MainWindow;
using static New_framework_test.IO_sockiet;
using New_framework_test;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataLab
{
    public struct Block_info
    {
        //Parameter struts for buttons
        public int pos_X;
        public int pos_Y;
        public int width_X;
        public int height_Y;
        public string name;
    }

    public class Info
    {
        //Different buttons that i use for now, they will be dynamic next time
        public Block_info Move_info;
        public Block_info Delete_info;
        public Block_info Input_info;
        public Block_info Output_info;
        public Block_info Read_info;

        public Info()
        {
            //Info for all buttons, it's easier to change them here than in code, can i store them in something that will be more convinient?
            // I need to initialisate it in every block now :( and static clases can't have variables boooo
            Move_info.pos_X = 5;
            Move_info.pos_Y = 5;
            Move_info.width_X = 50;
            Move_info.height_Y = 20;
            Move_info.name = "Move";

            Delete_info.pos_X = 5;
            Delete_info.pos_Y = 25;
            Delete_info.width_X = 50;
            Delete_info.height_Y = 20;
            Delete_info.name = "Delete";

            Input_info.pos_X = 5;
            Input_info.pos_Y = 45;
            Input_info.width_X = 50;
            Input_info.height_Y = 20;
            Input_info.name = "Pick input";

            Output_info.pos_X = 5;
            Output_info.pos_Y = 65;
            Output_info.width_X = 50;
            Output_info.height_Y = 20;
            Output_info.name = "Pick output";

            Read_info.pos_X = 5;
            Read_info.pos_Y = 85;
            Read_info.width_X = 50;
            Read_info.height_Y = 20;
            Read_info.name = "Read data";
        }
    }

    public static class Block_Info_class
    {
        //Now that you know my name, country and city what are you going to do? I'm just protecting myself from hackers and pirates
        //It will be used in compiled program, it's useless there because everybody can change it, but if you got it directly from me ... this means
        // i trust you :P (I HOPE optimaliser wont delete it!)
        static string author = "CODE made by Grzegorz Machura (Grzegorz2121, Poland, Dolnyśląsk, I LO w Jaworze)";  
    }

    /// <summary>
    /// Handy class for creating buttons canvises and misc GROUPBOX WILL BE REMOVED it's not nessesary and it's confusing to work with.
    /// </summary>
    public static class Fabricator
    {

        public static Button Create_Button(int pos_X, int pos_Y, int width_X, int height_Y, string text)
        {
  
            Button output_button = new Button();
            Thickness button_thicc = output_button.Margin;
            output_button.HorizontalAlignment = HorizontalAlignment.Left;
            output_button.VerticalAlignment = VerticalAlignment.Top;
            button_thicc.Left = pos_X;
            button_thicc.Top = pos_Y;
            output_button.Margin = button_thicc;
            output_button.Width = width_X;
            output_button.Height = height_Y;
            output_button.Content = text;
            return output_button;
        }

        public static Button Create_Button(Block_info button_info)
        {

            Button output_button = new Button();
            Thickness button_thicc = output_button.Margin;
            output_button.HorizontalAlignment = HorizontalAlignment.Left;
            output_button.VerticalAlignment = VerticalAlignment.Top;
            button_thicc.Left = button_info.pos_X;
            button_thicc.Top = button_info.pos_Y;
            output_button.Margin = button_thicc;
            output_button.Width = button_info.width_X;
            output_button.Height = button_info.height_Y;
            output_button.Content = button_info.name;
            return output_button;
        }

        public static GroupBox Create_GroupBox(int pos_X, int pos_Y, int width_X, int height_Y, string text)
        {
            GroupBox output_thing = new GroupBox();
            Thickness thing_thicc = output_thing.Margin;
            output_thing.HorizontalAlignment = HorizontalAlignment.Left;
            output_thing.VerticalAlignment = VerticalAlignment.Top;
            thing_thicc.Left = pos_X;
            thing_thicc.Top = pos_Y;
            output_thing.Margin = thing_thicc;
            output_thing.Width = width_X;
            output_thing.Height = height_Y;
            output_thing.Header = text;
            return output_thing;
        }
    }



    public class Blocks
    {
        //Used in block indentification in order to assign nessesary buttons
        public enum block_type {Input, Output, Processing, Flow};

        public class Basic_block
        {
            //variable for global GC
            public bool is_ded = false;
            //variable for move feature
            public bool is_movable = true;

            //They should be replaced someday... it's handled by io_sockiets now but i use them i want to delete those
            protected dynamic previous_block;
            protected dynamic next_block;
            
            public dynamic output_ref;

            //name of a block
            public string name;

            public GroupBox groupBox;
            public Canvas canvas;

            //I need to make those variables dynamic -> we don't need pick output in output block that already consumes data
            public Button Delete_button;
            public Button Move_button;
            public Button Read_button;
            public Button Pick_output_button;
            public Button Pick_input_button;

            public Info info;

            //positions of main groupbox, it will be used for lines that connect io_sockiets
            public double Main_X = 0;
            public double Main_Y = 0;

            /// //////////////////////////////////////////////////
            /* CONSTRUCTOR */

            ///Construction -> this is where we create canvas, basic move, delete butttons and initilate misc things for special features
            public Basic_block(string input_name, block_type b_type)
            {
                name = input_name;

                info = new Info();//I HATE IT i don't wanna to initialisate it :(

                groupBox = Fabricator.Create_GroupBox(100, 100, 100, 150, "Block");
                MainGrid_ref.Children.Add(groupBox);

                canvas = new Canvas();
                canvas.VerticalAlignment = VerticalAlignment.Top;
                canvas.HorizontalAlignment = HorizontalAlignment.Left;
                canvas.Width = 100;
                canvas.Height = 100;
                groupBox.Content = canvas;

                //Ahhh nice and readable button constructors :)
                Move_button = Fabricator.Create_Button(info.Move_info);
                Move_button.Click += new RoutedEventHandler(Move_button_click);
                canvas.Children.Add(Move_button);


                Delete_button = Fabricator.Create_Button(info.Delete_info);
                Delete_button.Click += new RoutedEventHandler(Delete_button_click);
                canvas.Children.Add(Delete_button);

                
                
                
                //////////////////////////

                /*Obsolete section! Used previously to show read, write and pick input,output buttons, everything in hadndled by ioblocks now, it's obsolete now*/
                // need to be replaced with better button api

                if (b_type==block_type.Input)
                {
                    //Read_button = Fabricator.Create_Button(5, 70, 50, 20, "Read");
                    
                    Read_button = Fabricator.Create_Button(info.Read_info);
                    canvas.Children.Add(Read_button);
                    Read_button.Click += new RoutedEventHandler(Read_button_click);
                    
                   
                    
                }
                
                if (b_type == block_type.Output || b_type == block_type.Processing || b_type == block_type.Flow)
                {
                    //Pick_input_button = Fabricator.Create_Button(5, 95, 50, 20, "Pick input");
                    /*
                    Pick_input_button = Fabricator.Create_Button(info.Input_info);
                    canvas.Children.Add(Pick_input_button);
                    Pick_input_button.Click += new RoutedEventHandler(Pick_input_button_click);
                    */
                }
                
                if (b_type == block_type.Input || b_type == block_type.Processing || b_type == block_type.Flow)
                {
                    //Pick_output_button = Fabricator.Create_Button(5, 125, 50, 20, "Pick output");
                    /*
                    Pick_output_button = Fabricator.Create_Button(info.Output_info);
                    canvas.Children.Add(Pick_output_button);
                    Pick_output_button.Click += new RoutedEventHandler(Pick_output_button_click);*/
                }

                //////////////////////////
            }

            /* CONSTRUCTOR */
            /// /////////////////////////////////////////////////////////////////////////////
            /* BLOCK FUNCTIONS */

            //Move feature function, used for moving blocks, called by main move timer
            public void Move()
            {
                if (is_movable == true)
                {
                    Point mouse_loc = Mouse.GetPosition(Application.Current.MainWindow);
                    Thickness group_loc = groupBox.Margin;

                    if (mouse_loc.X > 0 && mouse_loc.Y > 0)
                    {
                        group_loc.Left = mouse_loc.X-30;
                        group_loc.Top = mouse_loc.Y-30;
                    }

                    groupBox.Margin = group_loc;

                }
            }



            public virtual void Read_button_click(object sender, EventArgs e)
            {
               //VIRTUAL
            }
            
            //Button for deleting object, signaling main GC timer to delete it and object on main grid
            void Delete_button_click(object sender, EventArgs e)
            {
                Disconnect();
                is_ded = true;
            }

            void Move_button_click(object sender, EventArgs e)
            {
                if (is_movable == true)
                { is_movable = false; }
                else { is_movable = true; };
            }

            public void Dispose()
            {
                MainGrid_ref.Children.Remove(groupBox);
            }

            public virtual void Disconnect()
            {
                //VIRTUAL
            }

            /* BLOCK FUNCTIONS */
            ////////////////////////////////////////////////////////////////////////////////

        }

        /*



            NOW ONLY ADD STRING, DEBUG STRING and CONSOLE OBJECT WORK, rest of them are not converted yet to new sockiets!




            */

        ///////////////////////////////////////////////////////////////////////////////////////
        /* DIFFERENT CLASSES */

        public class Add_string:Basic_block
        {
            private DataProcessing.Processing.Math math;
            public output_sockiet output_io;
            public input_sockiet input_io;
            protected input_sockiet.parent_function funct_ref;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            public Add_string(string input) : base(input, block_type.Processing)
            {

                math  = new DataProcessing.Processing.Math();
                groupBox.Header = "Add string " + " Component";

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

            public override void Disconnect()
            {
                input_io.Disconnect_Input();
                input_io = null;
                output_io = null;
            }

        }

        ///////////////////////////////////////////////////////////////////////////////////////

        public class While_Loop : Basic_block
        {
            private DataProcessing.Flow.Loops loops;
            public bool condition = true;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            public While_Loop(string input) : base(input, block_type.Flow)
            {
                loops = new DataProcessing.Flow.Loops();
                groupBox.Header = "While loop " + " Component";
            }

            public void Input_function(string input)
            {
                Loop(input);
            }

            public void Loop(string input)
            {
                while(condition)
                {
                    Output_function(input);
                }
            }

            public void Output_function(string input)
            {
                if (next_block != null)
                {
                    next_block.Input_function(input);
                }
            }

        }

        ///////////////////////////////////////////////////////////////////////////////////////

        public class ReadLines : Basic_block
        {
            private DataProcessing.Input.File file;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            public ReadLines(string input) : base(input, block_type.Input)
            {
                file = new DataProcessing.Input.File();
                groupBox.Header = "Read lines " + " Component";
            }

            public override void Read_button_click(object sender, EventArgs e)
            {
                Input_function();
            }

            public void Input_function()
            {
                Output_function();
            }

            public void Output_function()
            {
                if (next_block != null)
                {
                    next_block.Input_function(file.ReadLines("C:\\Test.txt"));
                }
            }

        }

        ///////////////////////////////////////////////////////////////////////////////////////

        public class For_Every : Basic_block
        {
           
            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            public For_Every(string input) : base(input, block_type.Flow)
            {
                
                groupBox.Header = "For Every " + " Component";
            }

            public void Input_function(List<string> input)
            {
                Output_function(input);
            }

            public void Output_function(List<string> input)
            {
                if (next_block != null)
                {
                    foreach(string line in input)
                    {
                        next_block.Input_function(line);
                    }
                    
                }
            }

        }

        
        ///////////////////////////////////////////////////////////////////////////////////////

        public class Debug_string : Basic_block
        {
            private DataProcessing.Input.File file;
            public output_sockiet output_io;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            /// 
            public Debug_string(string input) : base(input, block_type.Input)
            {
                 file = new DataProcessing.Input.File();
                 groupBox.Header = "Debug string " + " Component";
               
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

            public override void Disconnect()
            {
                output_io.Disconnect_Output();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        public class Console_output : Basic_block
        {

            public input_sockiet input_io;
            protected input_sockiet.parent_function funct_ref;

            /// <summary>
            /// Block constructor, the most important place in the whole code: decodes what features are needed in a block.
            /// </summary>
            public Console_output(string input) : base(input, block_type.Output)
            {
                groupBox.Header = "Console output " + " Component";

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
                if (next_block != null)
                {
                    output_ref.Input_function(input);
                }
            }

            public override void Disconnect()
            {
                input_io.Disconnect_Input();
            }
        }

        /* DIFFERENT CLASSES */
        ///////////////////////////////////////////////////////////////////////////////////////

    }
}
