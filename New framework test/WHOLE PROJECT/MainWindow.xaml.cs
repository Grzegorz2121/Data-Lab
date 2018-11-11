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

using static DataLab.Blocks;
using System.Reflection;

namespace New_framework_test
{
    /*

        THIS IS MAIN PROGRAM SECTION, I"M STILL WORKING AT IT, LOOKING FOR SUGGESTIONS FOR FEW THINGS BUT I DON"T WANNA CHANGE EVERYTINHG I LIKE MY BLOCK and io for now!

    */
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static dynamic previous_sockiet;
        public static dynamic next_sockiet;

        static string author = "CODE made by Grzegorz Machura (Grzegorz2121, Poland, Dolnyśląsk, I LO w Jaworze)";

        public enum block_type { Input, Output, Processing, Flow };

        public static List<dynamic> block_list = new List<dynamic>(); // MAIN BLOCK LIST!

        public static Window MainWindow_ref;
        public static Grid MainGrid_ref;

        public string application_path = Assembly.GetExecutingAssembly().CodeBase;//I used it to implement pictures but i'm to lazy now (it worked but it was ugly because of group box)

        public int name = 0; //Name of Block to spawn (to recognise them) I NEED TO DO IT BETTER!
        
        public static string selected_block;


        public MainWindow()
        {
            InitializeComponent();

            MainWindow_ref = MainWindow1;
            MainGrid_ref = MainGrid;

            //timer for GC and move , TO DO change names 
            Create_timer(100, dispatcherTimer_Tick);
            Create_timer(100, GC_timer2_Tick);

            //I want to do it dynamicaly but i don't think i can do it, maybe as a external file? now it's easier to put it here, or maybe wrap in class like button infos
            block_listbox1.Items.Add("Add_string");
            block_listbox1.Items.Add("Console_output");
            block_listbox1.Items.Add("Debug_string");
            block_listbox1.Items.Add("While_Loop");
            block_listbox1.Items.Add("For_Every");
            block_listbox1.Items.Add("ReadLines");
        }

        //Spawns blocks and adds them to list in order to activate their move command and dispose then and ect
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dynamic ob = Activator.CreateInstance(Type.GetType(selected_block), name.ToString());
            //TO DO DYNAMIC NAMES -> THIS IS ONLY A TEMP SMALL FIX
            name++;
            block_list.Add(ob);
        }


        //MAIN GC TIMER-> I HAVE NO IDEA WHICH LOOP IS GOOD / the other one is obsolete 
        private void GC_timer2_Tick(object sender, EventArgs e)
        {
             foreach (dynamic graf in block_list)
             {
                 if (graf.is_ded == true)
                 {
                     graf.Dispose();
                     block_list.Remove(graf);
                     Console.WriteLine("RIP");
                     break;
                 }
             }

            for (int i = 0; i < block_list.Count; i++)
            {
                if (block_list[i].is_ded == true)
                {
                    block_list[i].Disconnect();
                    block_list[i].Dispose();
                    block_list[i] = null;
                    block_list.Remove(block_list[i]);
                    Console.WriteLine("RIP");
                    break;
                }
            }

        }



        // Main linking function
        public static void Link_objects()
        {

            if (next_sockiet != null && previous_sockiet != null)
            {
                //next_sockiet.Disconnect_Input();
                //previous_sockiet.Disconnect_Output();

                next_sockiet.previous_s = previous_sockiet;
                previous_sockiet.next_s = next_sockiet;

                next_sockiet = null;
                previous_sockiet = null;
            }
        }


        //Function for creating timers yay!
        public static void Create_timer(int interval, EventHandler function)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += function;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(10);
            dispatcherTimer.Start();
        }

        //TIMER FOR MOVING
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            foreach(Blocks.Basic_block t in block_list)
            {
                t.Move();
            }
        }

        //Changes selected block that we wanna spawn
        private void block_listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected_block = "DataLab.Blocks+" + block_listbox1.SelectedItem.ToString();
        }


        //Shows object present in blocks list
        private void DEBUG_Click(object sender, RoutedEventArgs e)
        {
            foreach(dynamic dy in block_list)
            {
                Console.WriteLine(dy);
            }
            
        }
    }
}