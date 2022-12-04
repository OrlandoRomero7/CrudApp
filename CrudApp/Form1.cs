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

namespace CrudApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refreshTable();
        }
        #region HELPER
        private void refreshTable()
        {
            using (CrudAppBDEntities db = new CrudAppBDEntities())
            {
                var lst = (from d in db.Personas orderby d.id descending
                          select d).AsQueryable();

                if (!textBoxSearchName.Text.Trim().Equals(""))
                {
                    lst = lst.Where(d=> d.nombre.Contains(textBoxSearchName.Text.Trim()));
                }
                if (!textBoxSearchEmail.Text.Trim().Equals(""))
                {
                    lst = lst.Where(d => d.correo.Contains(textBoxSearchEmail.Text.Trim()));
                }

                dataGridView1.DataSource = lst.ToList();
            }

        }
        #endregion

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Views.FormAdd tabla = new Views.FormAdd();
            tabla.ShowDialog();
            refreshTable();
        }

        private int? GetID()
        {
            try
            {
                return int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int? id = GetID();
            if (id != null)
            {
                Views.FormAdd tabla = new Views.FormAdd(id);
                tabla.ShowDialog();

                refreshTable();

            }

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int? id = GetID();
            if (id != null)
            {
                using(CrudAppBDEntities db = new CrudAppBDEntities())
                {
                    Personas tablaPeronas = db.Personas.Find(id);
                    db.Personas.Remove(tablaPeronas);
                    db.SaveChanges();
                }

                refreshTable();

            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonSerch_Click(object sender, EventArgs e)
        {
            refreshTable();
        }

        private void buttonClearSearch_Click(object sender, EventArgs e)
        {
            textBoxSearchEmail.Text = "";
            textBoxSearchName.Text = "";
            refreshTable();
        }
    }
}
