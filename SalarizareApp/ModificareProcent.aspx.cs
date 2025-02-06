using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace SalarizareApp
{
    public partial class ModificareProcent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm1.aspx"); 
        }

        protected void btnModifica_Click(object sender, EventArgs e)
        {
            string tipProcent = ddlTipProcent.SelectedValue; // IMPOZIT, CAS, CASS
            string parolaAdmin = txtParola.Text.Trim();
            decimal procentNou;

            if (string.IsNullOrEmpty(txtProcentNou.Text) || !decimal.TryParse(txtProcentNou.Text, out procentNou))
            {
                lblMessage.Text = "⚠️ Introdu un procent valid!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (parolaAdmin != "admin123")
            {
                lblMessage.Text = "❌ Parolă incorectă!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 1️⃣ 🔹 Actualizare procent în tabelul Procentaje
                    string updateQuery = $"UPDATE Procentaje SET {tipProcent} = :procentNou WHERE ID = 1";
                    using (OracleCommand cmd = new OracleCommand(updateQuery, conn))
                    {
                        cmd.Parameters.Add(":procentNou", procentNou);
                        cmd.ExecuteNonQuery();
                    }

                    // 2️⃣ 🔹 Recalculare salarii folosind noul procent
                    string recalculateQuery = @"
                        UPDATE ANGAJATI
                        SET 
                            CAS = TOTAL_BRUT * (SELECT CAS FROM Procentaje WHERE ID = 1) / 100,
                            CASS = TOTAL_BRUT * (SELECT CASS FROM Procentaje WHERE ID = 1) / 100,
                            BRUT_IMPOZABIL = TOTAL_BRUT - (TOTAL_BRUT * (SELECT CAS FROM Procentaje WHERE ID = 1) / 100) 
                                            - (TOTAL_BRUT * (SELECT CASS FROM Procentaje WHERE ID = 1) / 100),
                            IMPOZIT = (TOTAL_BRUT - (TOTAL_BRUT * (SELECT CAS FROM Procentaje WHERE ID = 1) / 100) 
                                            - (TOTAL_BRUT * (SELECT CASS FROM Procentaje WHERE ID = 1) / 100))
                                      * (SELECT IMPOZIT FROM Procentaje WHERE ID = 1) / 100,
                            VIRAT_CARD = TOTAL_BRUT - ((TOTAL_BRUT - (TOTAL_BRUT * (SELECT CAS FROM Procentaje WHERE ID = 1) / 100) 
                                            - (TOTAL_BRUT * (SELECT CASS FROM Procentaje WHERE ID = 1) / 100))
                                            * (SELECT IMPOZIT FROM Procentaje WHERE ID = 1) / 100)
                                      - (TOTAL_BRUT * (SELECT CAS FROM Procentaje WHERE ID = 1) / 100)
                                      - (TOTAL_BRUT * (SELECT CASS FROM Procentaje WHERE ID = 1) / 100)
                                      - RETINERI";

                    using (OracleCommand cmd = new OracleCommand(recalculateQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    lblMessage.Text = "✅ Procentul a fost modificat și salariile au fost recalculate!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "❌ Eroare la actualizare: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}
