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
        private int CantidadPalabras;
        private int[] espacios = new int[100];
        public Analizador()
        {
            
        }

        public void Analizar(RichTextBox Area, String LineaDeTexto, int Posicion, int NumLinea)
        {
            if (LineaDeTexto.StartsWith("//"))
            {
                PintarPalabra(Posicion, 0, LineaDeTexto.Length, LineaDeTexto, Area, NumLinea);
            }
            else
            {
                CantidadPalabras = 0;
                LineaDeTexto = LineaDeTexto + " ";
                char[] caracter = LineaDeTexto.ToCharArray();
                for (int i = 0; i < caracter.Length; i++)
                {
                    string mini = caracter[i].ToString();
                    if (Char.IsLetter(caracter[i]))
                    {
                        string aux = "";
                        for (int j = i; j < caracter.Length; j++)
                        {

                            if (Char.IsLetter(caracter[j]) || Char.IsNumber(caracter[j]))
                            {
                                aux = aux + caracter[j].ToString();
                            }
                            else if (Char.IsWhiteSpace(caracter[j]))
                            {

                                PintarPalabra(Posicion, i, j - i, aux, Area, NumLinea);
                                i = j;
                                j = caracter.Length;
                                palabras[CantidadPalabras] = aux;
                                CantidadPalabras++;
                            }
                        }
                    }
                }
            }
        }

        public void PintarPalabra(int position, int inicio, int final, string palabra, RichTextBox Area, int linea)
        {
            if (palabra.StartsWith("//"))
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Red;
                Area.SelectionStart = position;
            }
            else if (palabra == "entero")
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Magenta;
                Area.SelectionStart = position;
            }
            else if (palabra == "decimal")
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Aquamarine;
                Area.SelectionStart = position;
            }
            else if (palabra == "cadena")
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Gray;
                Area.SelectionStart = position;
            }
            else if (palabra == "booleano")
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Orange;
                Area.SelectionStart = position;
            }
            else if (palabra == "caracter")
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Brown;
                Area.SelectionStart = position;
            }
            else if (palabra == "SI" || palabra == "SINO" || palabra == "SINOSI" || palabra == "MIENTRAS" || palabra == "HACER" || palabra == "DESDE" || palabra == "HASTA" || palabra == "INCREMENTO" )
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.LightGreen;
                Area.SelectionStart = position;
            }
            else if (palabra.Length == 1)
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Brown;
                Area.SelectionStart = position;
            }
            else
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Black;
                Area.SelectionStart = position;
            }
        }

        public int ObtenerPosicion(RichTextBox Area, int linea, int inicio)
        {
            int pos = 0;
            for (int j = 0; j < linea; j++)
            {
                pos = pos + Area.Lines[j].Length;
            }
            pos = pos + (linea + 1);
            pos = pos + (inicio - 1);
            return pos;
        }
    }
}
