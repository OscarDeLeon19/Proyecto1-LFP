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
                            else if (Char.IsWhiteSpace(caracter[j]) || Char.IsSymbol(caracter[j]) || Char.IsPunctuation(caracter[j]))
                            {
                                
                                PintarPalabra(Posicion, i, j - i, aux, Area, NumLinea);
                                i = j - 1;
                                j = caracter.Length;
                                palabras[CantidadPalabras] = aux;
                                CantidadPalabras++;
                            }
                        }
                    }
                    if (Char.IsNumber(caracter[i]))
                    {
                        string aux = "";
                    
                        bool No_Decimal = false;
                        for (int j = i; j < caracter.Length; j++)
                        {
                        string caracter_numero = caracter[j].ToString();
                            if (Char.IsNumber(caracter[j]))
                            {
                                aux = aux + caracter[j].ToString();
                            }
                            if (caracter_numero == ".")
                            {
                                aux = aux + caracter[j].ToString();
                                No_Decimal = true;
                            }
                            if (caracter_numero == ";")
                            {
                                PintarNumero(Posicion, i, j - i, aux, Area, NumLinea, No_Decimal);
                                i = j - 1;
                                j = caracter.Length;
                                palabras[CantidadPalabras] = aux;
                                CantidadPalabras++;
                            }
                            else if (Char.IsWhiteSpace(caracter[j]) ||  Char.IsSymbol(caracter[j]) || Char.IsLetter(caracter[j]))
                            {
                                PintarNumero(Posicion, i, j - i, aux, Area, NumLinea, No_Decimal);
                                i = j - 1;
                                j = caracter.Length;
                                palabras[CantidadPalabras] = aux;
                                CantidadPalabras++;
                            }
                        }
                    }
                if (mini == "\"")
                {
                    string aux = "\"";
                    for (int j = i + 1; j < caracter.Length; j++)
                    {
                        string comilla = caracter[j].ToString();
                        if (Char.IsLetter(caracter[j]) || Char.IsNumber(caracter[j]) || Char.IsPunctuation(caracter[j]) || Char.IsSymbol(caracter[j]) || Char.IsWhiteSpace(caracter[j]))
                        {
                            aux = aux + caracter[j].ToString();

                        }
                        if (comilla == "\"")
                        {

                            aux = aux + caracter[j].ToString();
                            j++;
                            PintarPalabra(Posicion, i, j - i, aux, Area, NumLinea);
                            i = j -1;
                            j = caracter.Length;
                            palabras[CantidadPalabras] = aux;
                            CantidadPalabras++;
                        }
                    }
                }
                if (mini == "/")
                {
                    string comprobacion = caracter[i + 1].ToString();
                    if (comprobacion == "/" )
                    {
                        string aux = "";
                        for (int j = i; j < caracter.Length; j++)
                        {
                            aux = aux + caracter[j].ToString();
                        }
                        PintarPalabra(Posicion, i, caracter.Length - i, aux, Area, NumLinea);
                        i = caracter.Length;
                        palabras[CantidadPalabras] = aux;
                        CantidadPalabras++;
                    }
                    if (comprobacion == "*")
                    {
                        string aux = "";
                        for (int j = i; j < caracter.Length; j++)
                        {
                            
                            aux = aux + caracter[j].ToString();
                            if (aux.EndsWith("*/"))
                            {
                                j++;
                                PintarPalabra(Posicion, i, j - i, aux, Area, NumLinea);
                                i = j;
                                j = caracter.Length;
                                palabras[CantidadPalabras] = aux;
                                CantidadPalabras++;
                            }
                        }
                    }
                }
                if (mini == "+" || mini == "-" || mini == "/" || mini == "*" || mini == "<" || mini == ">" || mini == "!" || mini == "|" || mini == "&" || mini == "(" || mini == ")")
                {
                    PintarSigno(Posicion, i, 1, mini, Area, NumLinea, 1);
                }
                if(mini == ";")
                {
                    PintarSigno(Posicion, i, 1, mini, Area, NumLinea, 2);
                }
                if (mini == "=")
                {
                    if (i > 0) {
                        string comprobacion = caracter[i - 1].ToString();
                        if (comprobacion == "=" || comprobacion == "<" || comprobacion == ">" || comprobacion == "!")
                        {
                            PintarSigno(Posicion, i - 1, 2, comprobacion + mini , Area, NumLinea, 1);
                        }
                        else {
                            PintarSigno(Posicion, i, 1, mini, Area, NumLinea, 2);
                        }
                    }
                }
                if (mini == "_")
                {
                    PintarSigno(Posicion, i, 1, mini, Area, NumLinea, 3);
                }

            }
            
        }

        public void PintarPalabra(int position, int inicio, int final, string palabra, RichTextBox Area, int linea)
        {
            if (palabra == "entero")
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
            else if (palabra == "SI" || palabra == "SINO" || palabra == "MIENTRAS" || palabra == "HACER" || palabra == "DESDE" || palabra == "HASTA" || palabra == "INCREMENTO" )
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
            else if (palabra == "verdadero")
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Orange;
                Area.SelectionStart = position;
            }
            else if (palabra == "falso")
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Orange;
                Area.SelectionStart = position;
            }
            else if (palabra.StartsWith("\""))
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Gray;
                Area.SelectionStart = position;
            }
            else if (palabra.StartsWith("//") || (palabra.StartsWith("/*") || palabra.EndsWith("*/")))
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Red;
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

        public void PintarNumero(int position, int inicio, int final, string palabra, RichTextBox Area, int linea, bool Decimal)
        {
            if (Decimal == true)
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Aquamarine;
                Area.SelectionStart = position;
            }
            else
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Magenta;
                Area.SelectionStart = position;
            }
        }

        public void PintarSigno(int position, int inicio, int final, string palabra, RichTextBox Area, int linea, int parametro)
        {
            if (parametro == 1)
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Blue;
                Area.SelectionStart = position;
            }
            if (parametro == 2)
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.Pink;
                Area.SelectionStart = position;
            } 
            if (parametro == 3)
            {
                int pos = ObtenerPosicion(Area, linea, inicio);
                Area.Select(pos, final);
                Area.SelectionColor = Color.LightGreen;
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
