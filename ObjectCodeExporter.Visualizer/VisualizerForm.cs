using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectCodeExporter.Visualizer
{
    public partial class VisualizerForm : Form
    {
        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="objectLiteral">Text de l'objet traduit</param>
        public VisualizerForm(string objectLiteral)
        {
            InitializeComponent();
            textBox1.Text = objectLiteral;
            textBox1.SelectAll();
        }
    }
}
