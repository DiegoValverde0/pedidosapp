﻿using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NCategoria
    {
        //Metodo Insertar que llama al insertar de la clase DCategoria de la CapaDatos
        public static string Insertar(string nombre, string descripcion)
        {
            DCategoria Obj = new DCategoria();
            Obj.Nombre = nombre;
            Obj.Descripcion = descripcion;
            return Obj.Insertar(Obj);
        }

        //Metodo Editar que llama al metodo Editar de la clase DCategoria de la CapaDatos
        public static string Editar(int idcategaria, string nombre, string descripcion)
        {
            DCategoria Obj = new DCategoria();
            Obj.Idcategoria = idcategaria;
            Obj.Nombre = nombre;
            Obj.Descripcion = descripcion;
            return Obj.Editar(Obj);
        }
        //Metodo Eliminar que llama al metodo Eliminar de la clase DCategoria de la CapaDatos
        public static string Eliminar(int idcategaria)
        {
            DCategoria Obj = new DCategoria();
            Obj.Idcategoria = idcategaria;
            return Obj.Eliminar(Obj);
        }
        //Metodo Mostrar que llama al metodo Mostrar de la clase DCategoria de la CapaDatos
        public static DataTable Mostrar()
        {
            return new DCategoria().Mostrar();
        }
        //Metodo BuscarNombre que llama al metodo BuscarNombre de la clase DCategoria de la CapaDatos
        public static DataTable BuscarNombre(string textbuscar)
        {
            DCategoria Obj = new DCategoria();
            Obj.TextoBuscar = textbuscar;
            return Obj.BuscarNombre(Obj);
        }
    }
}
