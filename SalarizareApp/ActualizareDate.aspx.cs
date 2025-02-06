using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace SalarizareApp
{
    public partial class ActualizareDate : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IncarcaAngajati();
            }
        }

        private void IncarcaAngajati()
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Angajati";
                using (OracleDataAdapter da = new OracleDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvAngajati.DataSource = dt;
                    gvAngajati.DataBind();
                }
            }
        }

        protected void btnCauta_Click(object sender, EventArgs e)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Angajati WHERE NUME LIKE :Nume";
                using (OracleDataAdapter da = new OracleDataAdapter(query, conn))
                {
                    da.SelectCommand.Parameters.Add(":Nume", OracleDbType.Varchar2).Value = "%" + txtCauta.Text + "%";
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvAngajati.DataSource = dt;
                    gvAngajati.DataBind();
                }
            }
        }

        protected void gvAngajati_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAngajati.EditIndex = e.NewEditIndex;
            IncarcaAngajati();
        }

        protected void gvAngajati_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAngajati.EditIndex = -1;
            IncarcaAngajati();
        }

        protected void gvAngajati_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvAngajati.Rows[e.RowIndex];
            int id = Convert.ToInt32(gvAngajati.DataKeys[e.RowIndex].Values[0]);
            string nume = (row.Cells[1].Controls[0] as TextBox).Text;
            string prenume = (row.Cells[2].Controls[0] as TextBox).Text;
            string functie = (row.Cells[3].Controls[0] as TextBox).Text;
            decimal salarBaza = Convert.ToDecimal((row.Cells[4].Controls[0] as TextBox).Text);
            decimal spor = Convert.ToDecimal((row.Cells[5].Controls[0] as TextBox).Text);
            decimal premii = Convert.ToDecimal((row.Cells[6].Controls[0] as TextBox).Text);
            decimal retineri = Convert.ToDecimal((row.Cells[7].Controls[0] as TextBox).Text);

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Angajati SET NUME=:NUME, PRENUME=:PRENUME, FUNCTIE=:FUNCTIE, SALAR_BAZA=:SALAR_BAZA, SPOR=:SPOR, PREMII_BRUTE=:PREMII_BRUTE, RETINERI=:RETINERI WHERE ID=:ID";
                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":NUME", OracleDbType.Varchar2).Value = nume;
                    cmd.Parameters.Add(":PRENUME", OracleDbType.Varchar2).Value = prenume;
                    cmd.Parameters.Add(":FUNCTIE", OracleDbType.Varchar2).Value = functie;
                    cmd.Parameters.Add(":SALAR_BAZA", OracleDbType.Decimal).Value = salarBaza;
                    cmd.Parameters.Add(":SPOR", OracleDbType.Decimal).Value = spor;
                    cmd.Parameters.Add(":PREMII_BRUTE", OracleDbType.Decimal).Value = premii;
                    cmd.Parameters.Add(":RETINERI", OracleDbType.Decimal).Value = retineri;
                    cmd.Parameters.Add(":ID", OracleDbType.Int32).Value = id;

                    cmd.ExecuteNonQuery();
                }
            }

            gvAngajati.EditIndex = -1;
            IncarcaAngajati();
        }
    }
}
