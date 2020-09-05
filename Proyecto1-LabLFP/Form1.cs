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
        string[] Reservadas = new string[] { "else", "if" };
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

        private void Area1_TextChanged(object sender, EventArgs e)
        {

            int position = Area1.SelectionStart;
            int linea = Area1.GetLineFromCharIndex(position);
            int Caracteres = position - Area1.GetFirstCharIndexOfCurrentLine();

            etiqueta1.Text = "Fila: " + linea + " Columna " + Caracteres + " Posicion" + position;
           

        }

        private void EventoClick(object sender, MouseEventArgs e)
        {
            int position = Area1.SelectionStart;
            int linea = Area1.GetLineFromCharIndex(position);
            int Caracteres = position - Area1.GetFirstCharIndexOfCurrentLine();

            etiqueta1.Text = "Fila: " + linea + " Columna " + Caracteres + " Posicion" + position;

        }

        private void PresionarEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                int position = Area1.SelectionStart;
                int linea = Area1.GetLineFromCharIndex(position);
                string LineaDeTexto = Area1.Lines[linea];
                char[] c = LineaDeTexto.ToCharArray();
                for (int i = 0; i < c.Length; i++)
                {
                    if (Char.IsDigit(c[i]))
                    {
                        int pos = 0;
                        for (int j = 0; j < linea; j++)
                        {
                            pos = pos + Area1.Lines[j].Length;
                        }
                        pos = pos + (linea + 1);
                        pos = pos + (i - 1);
                        Area1.Select(pos, 1);
                        Area1.SelectionColor = Color.Red;
                        Area1.SelectionStart = position;
                    }
                }
            }
        }
    }
}
