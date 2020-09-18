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
    public partial class VentanaIDE : Form
    {
        Analizador analizador = new Analizador();
        public VentanaIDE()
        {
            InitializeComponent();
            
        }

        

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Archivo nuevo");
            Area1.Text = null;
            Area1.Enabled = true;
            richTextBox1.Enabled = true;
            BotonCompilar.Enabled = true;
            richTextBox1.Text = null;
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
                        richTextBox1.Text = null;
                        Area1.SaveFile(SaveFile.FileName, RichTextBoxStreamType.PlainText);
                        MessageBox.Show("Archivo Guardado");
                    }
                }
                Area1.Text = null;
                Area1.Enabled = false;
                richTextBox1.Text = null;
                richTextBox1.Enabled = false;
                BotonCompilar.Enabled = false;
            } 
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Area1.Enabled = true;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                Area1.LoadFile(OpenFile.FileName, RichTextBoxStreamType.PlainText);
                richTextBox1.Text = null;
                BotonCompilar.Enabled = true;
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

        private void Area1_TextChanged(object sender, EventArgs e)
        {

            int position = Area1.SelectionStart;
            int linea = Area1.GetLineFromCharIndex(position);
            int Caracteres = position - Area1.GetFirstCharIndexOfCurrentLine();

            etiqueta1.Text = "Fila: " + linea + " Columna " + Caracteres;
           

        }

        private void EventoClick(object sender, MouseEventArgs e)
        {
            int position = Area1.SelectionStart;
            int linea = Area1.GetLineFromCharIndex(position);
            int Caracteres = position - Area1.GetFirstCharIndexOfCurrentLine();

            etiqueta1.Text = "Fila: " + linea + " Columna " + Caracteres;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Area1.Text != null)
            {
                richTextBox1.Text = null;
                int lineas = Area1.Lines.Length;
                for (int i = 0; i < lineas; i++)
                {
                    int position = Area1.SelectionStart;
                    string LineaDeTexto = Area1.Lines[i];
                    analizador.Analizar(Area1, LineaDeTexto, position, i, richTextBox1);
                }
                MessageBox.Show("Programa Compilado");
            }
        }

        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Area1.Text != null)
            {
                richTextBox1.Text = null;
                int lineas = Area1.Lines.Length;
                for (int i = 0; i < lineas; i++)
                {
                    int position = Area1.SelectionStart;
                    string LineaDeTexto = Area1.Lines[i];
                    analizador.Analizar(Area1, LineaDeTexto, position, i, richTextBox1);
                }
                MessageBox.Show("Programa Compilado");
            }
        }

        private void exportarErroresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != null)
            {
                if (SaveErrores.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SaveFile(SaveErrores.FileName, RichTextBoxStreamType.PlainText);
                    MessageBox.Show("Errores guardados");
                }
            }
            else
            {
                MessageBox.Show("No se guardaron los errores");
            }
        }

        private void PresionarFlecha(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up || e.KeyData == Keys.Down || e.KeyData == Keys.Left || e.KeyData == Keys.Right)
            {
                int position = Area1.SelectionStart;
                int linea = Area1.GetLineFromCharIndex(position);
                int Caracteres = position - Area1.GetFirstCharIndexOfCurrentLine();

                etiqueta1.Text = "Fila: " + linea + " Columna " + Caracteres;
            }
        }
    }
}
