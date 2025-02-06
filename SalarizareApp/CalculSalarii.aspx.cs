using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace SalarizareApp
{
    public partial class CalculSalarii : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IncarcaSalarii();
            }
        }

        private void IncarcaSalarii(string nume = "")
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ID, NUME, PRENUME, SALAR_BAZA, SPOR, PREMII_BRUTE, TOTAL_BRUT, CAS, CASS, BRUT_IMPOZABIL, IMPOZIT, RETINERI, VIRAT_CARD FROM ANGAJATI";

                if (!string.IsNullOrEmpty(nume))
                {
                    query += " WHERE NUME LIKE :NUME";
                }

                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(nume))
                    {
                        cmd.Parameters.Add(":NUME", OracleDbType.Varchar2).Value = "%" + nume + "%";
                    }

                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvSalarii.DataSource = dt;
                        gvSalarii.DataBind();
                    }
                }
            }
        }

        protected void btnCauta_Click(object sender, EventArgs e)
        {
            IncarcaSalarii(txtCauta.Text.Trim());
        }
    }
}
