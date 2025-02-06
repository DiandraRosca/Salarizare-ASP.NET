using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace SalarizareApp
{
    public partial class StergereAngajat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IncarcaAngajati(); // Afișează toți angajații la încărcarea paginii
            }
        }

        private void IncarcaAngajati(string numeCautat = "")
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID, NUME, PRENUME, FUNCTIE FROM Angajati";
                    bool hasFilter = !string.IsNullOrEmpty(numeCautat);

                    if (hasFilter)
                    {
                        query += " WHERE NUME LIKE :NUME OR PRENUME LIKE :PRENUME";
                    }

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        if (hasFilter)
                        {
                            cmd.Parameters.Add(":NUME", OracleDbType.Varchar2).Value = "%" + numeCautat + "%";
                            cmd.Parameters.Add(":PRENUME", OracleDbType.Varchar2).Value = "%" + numeCautat + "%";
                        }

                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            // Afișează tabelul indiferent dacă sunt sau nu rezultate la căutare
                            gvAngajati.DataSource = dt;
                            gvAngajati.DataBind();
                            gvAngajati.Visible = true;

                            // Dacă nu există rezultate pentru căutare, dar există angajați în tabel, arată un mesaj
                            if (hasFilter && dt.Rows.Count == 0)
                            {
                                Response.Write("<script>alert('Nu a fost găsit niciun angajat cu acest nume.');</script>");
                                IncarcaAngajati(); // Reafișează toți angajații dacă nu există rezultate
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Eroare: " + ex.Message + "');</script>");
                }
            }
        }

        protected void btnCauta_Click(object sender, EventArgs e)
        {
            string numeCautat = txtCauta.Text.Trim();
            if (string.IsNullOrEmpty(numeCautat))
            {
                Response.Write("<script>alert('Introduceți un nume pentru căutare!');</script>");
                return;
            }

            IncarcaAngajati(numeCautat);
        }

        protected void gvAngajati_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idAngajat = Convert.ToInt32(gvAngajati.DataKeys[e.RowIndex].Value);
            string connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Angajati WHERE ID = :ID";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":ID", OracleDbType.Int32).Value = idAngajat;
                        cmd.ExecuteNonQuery();
                    }

                    Response.Write("<script>alert('Angajat șters cu succes!'); window.location='StergereAngajat.aspx';</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Eroare: " + ex.Message + "');</script>");
                }
            }
        }
    }
}
