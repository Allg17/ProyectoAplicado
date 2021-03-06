﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using FacturacionAplicado.Entidades;

namespace FacturacionAplicado.BLL
{
    public class DepartamentoBLL
    {


        public static DataTable Consultar()
        {


            DataTable dt = new DataTable();

            MySqlDataAdapter con = new MySqlDataAdapter("select * from Departamento",  ConexionGlobal.ConexionGlobalDb.RetornarConexion());
            try
            {


                con.Fill(dt);



            }
            catch (Exception)
            {

                throw;
            }
            return dt;
        }

        public static bool Eliminar(string id)
        {

            MySqlDataAdapter dater = new MySqlDataAdapter();
            bool estado = true;
            try
            {

                dater.DeleteCommand = new MySqlCommand(" delete from Departamento where id=" + id,  ConexionGlobal.ConexionGlobalDb.RetornarConexion());

                dater.DeleteCommand.Connection =  ConexionGlobal.ConexionGlobalDb.RetornarConexion();
                dater.DeleteCommand.ExecuteNonQuery();

            }
            catch (Exception)
            {
                estado = false;
                throw;

            }

            return estado;
        }


        public static bool Guardar(Departamento depart)
        {
            MySqlDataAdapter dater = new MySqlDataAdapter();
            bool estado = true;
            try
            {

                dater.InsertCommand = new MySqlCommand("insert into Departamento (Nombre) values ('" + depart.Nombre + "')",  ConexionGlobal.ConexionGlobalDb.RetornarConexion());

                dater.InsertCommand.Connection =  ConexionGlobal.ConexionGlobalDb.RetornarConexion();
                dater.InsertCommand.ExecuteNonQuery();

            }
            catch (Exception)
            {
                estado = false;
                throw;

            }

            return estado;
        }


        //asi se pone un datatable en una lista 
        public static List<Departamento> Buscar()
        {

            DataTable dt = new DataTable();


            MySqlDataAdapter con = new MySqlDataAdapter("select * from Departamento",  ConexionGlobal.ConexionGlobalDb.RetornarConexion());
            try
            {


                con.Fill(dt);



            }
            catch (Exception)
            {

                throw;
            }



            List<Departamento> listName = dt.AsEnumerable().Select(m => new Departamento()
            {
                id = m.Field<int>("id"),
                Nombre = m.Field<string>("Nombre"),

            }).ToList();


            return listName;
        }

        public static List<Departamento> Buscar(string id)
        {

            DataTable dt = new DataTable();


            MySqlDataAdapter con = new MySqlDataAdapter("select * from Departamento where id =  '"+id+"'", ConexionGlobal.ConexionGlobalDb.RetornarConexion());
            try
            {


                con.Fill(dt);



            }
            catch (Exception)
            {

                throw;
            }



            List<Departamento> listName = dt.AsEnumerable().Select(m => new Departamento()
            {
                id = m.Field<int>("id"),
                Nombre = m.Field<string>("Nombre"),

            }).ToList();


            return listName;
        }

        public static bool Modificar(Departamento depart)
        {
            MySqlDataAdapter dater = new MySqlDataAdapter();
            bool estado = true;
            try
            {


                dater.UpdateCommand = new MySqlCommand("update Cliente set Nombre= '" + depart.Nombre + "' where id = '" + depart.id + "'",  ConexionGlobal.ConexionGlobalDb.RetornarConexion());

                dater.UpdateCommand.Connection =  ConexionGlobal.ConexionGlobalDb.RetornarConexion();
                dater.UpdateCommand.ExecuteNonQuery();

            }
            catch (Exception)
            {
                estado = false;
                throw;

            }

            return estado;

        }

        public static List<Departamento> GetList(string filtro, string criterio)
        {

            DataTable dt = new DataTable();


            MySqlDataAdapter con = new MySqlDataAdapter("select * from Departamento where "+filtro+"='"+criterio+"'", ConexionGlobal.ConexionGlobalDb.RetornarConexion());
            try
            {
              


                con.Fill(dt);



            }
            catch (Exception)
            {

                throw;
            }



            List<Departamento> listName = dt.AsEnumerable().Select(m => new Departamento()
            {
                id = m.Field<int>("id"),
                Nombre = m.Field<string>("Nombre"),

            }).ToList();


            return listName;
        }
    }
}
