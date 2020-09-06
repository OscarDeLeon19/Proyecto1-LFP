using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1_LabLFP
{
   
    class Analizador
    {
        private string[] palabras = new string[100];
        private int[] espacios = new int[100];
        public Analizador()
        {
            
        }

        public void Analizar(RichTextBox Area, String LineaDeTexto, int Posicion, int NumLinea)
        {
            LineaDeTexto = LineaDeTexto + " ";
            int NoEspacio = 0;
            char[] caracter = LineaDeTexto.ToCharArray();
            for (int i = 0; i < caracter.Length; i++)
            {
                if (Char.IsWhiteSpace(caracter[i]))
                {
                    if (NoEspacio == 0)
                    {
                        palabras[NoEspacio] = LineaDeTexto.Substring(0, i);
                    }
                    else
                    {
                        palabras[NoEspacio] = LineaDeTexto.Substring(espacios[NoEspacio -1]+1, i - espacios[NoEspacio - 1]);
                    }
                    espacios[NoEspacio] = i;
                    NoEspacio++;
                }
            }
            NoEspacio++;
            MessageBox.Show("Palabras: " + NoEspacio.ToString());
            for (int i = 0; i < NoEspacio - 1; i++)
            {
                MessageBox.Show(palabras[i]);
            }
        }
    }
}
