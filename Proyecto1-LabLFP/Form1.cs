using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1_LabLFP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Archivo nuevo");
            Area1.Enabled = true;
        }

        

        private void cerrarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (Area1.Enabled == true)
            {
                DialogResult condicion = MessageBox.Show("¿Deseas Guardar el Archivo?", "Aviso", MessageBoxButtons.YesNo);
                if(condicion == System.Windows.Forms.DialogResult.Yes)
                {
                    if (SaveFile.ShowDialog() == DialogResult.OK)
                    {

                        Area1.SaveFile(SaveFile.FileName, RichTextBoxStreamType.PlainText);
                        MessageBox.Show("Archivo Guardado");
                    }
                }
                Area1.Text = null;
                Area1.Enabled = false;
            } 
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Area1.Enabled = true;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                Area1.LoadFile(OpenFile.FileName, RichTextBoxStreamType.PlainText);
                MessageBox.Show("Archivo Cargado");
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Area1.Enabled == true)
            {
                if (SaveFile.ShowDialog() == DialogResult.OK)
                {
                    
                    Area1.SaveFile(SaveFile.FileName, RichTextBoxStreamType.PlainText);
                    MessageBox.Show("Archivo Guardado");
                }
            }
            else
            {
                MessageBox.Show("No haz abierto o creado un archivo para guardar");
            }
        }
    }
}
