using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLab
{
    interface IBlock_input
    {
        /// <summary>
        /// Function that is going to be called by previous block sockiet
        /// </summary>
        void Input_function();

        /// <summary>
        /// Function that is going to call another block
        /// </summary>
        void Output_function();

        /// <summary>
        /// 
        /// </summary>
        void Render();
        void Disconnect();
    }

    interface IBlock_output
    {
        /// <summary>
        /// Function that is going to be called by previous block sockiet
        /// </summary>
        void Input_function();

        /// <summary>
        /// Function that is going to call another block
        /// </summary>
        void Output_function();

        /// <summary>
        /// 
        /// </summary>
        void Render();
        void Disconnect();
    }

    interface IBlock_processing
    {
        /// <summary>
        /// Function that is going to be called by previous block sockiet
        /// </summary>
        void Input_function();

        /// <summary>
        /// Function that is going to call another block
        /// </summary>
        void Output_function();

        /// <summary>
        /// 
        /// </summary>
        void Render();
        void Disconnect();
    }

}
