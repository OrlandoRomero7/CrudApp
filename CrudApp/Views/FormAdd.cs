using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudApp.Models;

namespace CrudApp.Views
{
    public partial class FormAdd : Form
    {
        public int? id;
        Personas tablaPersonas;
        public FormAdd(int? id=null)
        {
            InitializeComponent();
            this.id = id;
            if (id != null)
                CargarData();
        }
        private void CargarData()
        {
            using (CrudAppBDEntities db = new CrudAppBDEntities())
            {
                tablaPersonas = db.Personas.Find(id);
                textBoxName.Text = tablaPersonas.nombre;
                textBoxEmail.Text = tablaPersonas.correo;
                dateTimePicker1.Value = tablaPersonas.fecha_nacimiento;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using(CrudAppBDEntities db = new CrudAppBDEntities())
            {
                if(id == null)
                    tablaPersonas = new Personas();

                tablaPersonas.nombre = textBoxName.Text;
                tablaPersonas.correo = textBoxEmail.Text;
                tablaPersonas.fecha_nacimiento = dateTimePicker1.Value;

                if (id == null)
                {
                    db.Personas.Add(tablaPersonas);
                }
                else
                {
                    db.Entry(tablaPersonas).State = System.Data.Entity.EntityState.Modified;
                }


                
                db.SaveChanges();

                this.Close();
            }
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
