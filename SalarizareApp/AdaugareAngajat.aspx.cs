﻿using System;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Web.UI;

namespace SalarizareApp
{
    public partial class AdaugareAngajat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSalveaza_Click(object sender, EventArgs e)
        {
            // Verifică dacă toate câmpurile sunt completate
            if (string.IsNullOrWhiteSpace(txtNume.Text) ||
                string.IsNullOrWhiteSpace(txtPrenume.Text) ||
                string.IsNullOrWhiteSpace(txtFunctie.Text) ||
                string.IsNullOrWhiteSpace(txtSalariu.Text) ||
                string.IsNullOrWhiteSpace(txtSpor.Text) ||
                string.IsNullOrWhiteSpace(txtPremii.Text))
            {
                Response.Write("<script>alert('Toate câmpurile sunt obligatorii!');</script>");
                return;
            }

            // Verifică dacă valorile numerice sunt valide
            if (!decimal.TryParse(txtSalariu.Text, out decimal salariu) ||
                !decimal.TryParse(txtSpor.Text, out decimal spor) ||
                !decimal.TryParse(txtPremii.Text, out decimal premii))
            {
                Response.Write("<script>alert('Introduceți valori numerice valide pentru salariu, spor și premii!');</script>");
                return;
            }

            // Obține string-ul de conexiune din Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO Angajati (NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR, PREMII_BRUTE) 
                                     VALUES (:NUME, :PRENUME, :FUNCTIE, :SALAR_BAZA, :SPOR, :PREMII_BRUTE)";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        // Adaugă parametrii pentru a preveni SQL Injection
                        cmd.Parameters.Add(":NUME", OracleDbType.Varchar2).Value = txtNume.Text;
                        cmd.Parameters.Add(":PRENUME", OracleDbType.Varchar2).Value = txtPrenume.Text;
                        cmd.Parameters.Add(":FUNCTIE", OracleDbType.Varchar2).Value = txtFunctie.Text;
                        cmd.Parameters.Add(":SALAR_BAZA", OracleDbType.Decimal).Value = salariu;
                        cmd.Parameters.Add(":SPOR", OracleDbType.Decimal).Value = spor;
                        cmd.Parameters.Add(":PREMII_BRUTE", OracleDbType.Decimal).Value = premii;

                        cmd.ExecuteNonQuery();
                    }

                    // Afișează un mesaj de succes și resetează câmpurile
                    Response.Write("<script>alert('Angajat adăugat cu succes! Calculul salariului s-a făcut automat!');</script>");
                    ClearFields();
                }
                catch (Exception ex)
                {
                    // Afișează eroarea dacă ceva nu merge bine
                    Response.Write("<script>alert('Eroare: " + ex.Message + "');</script>");
                }
            }
        }

        private void ClearFields()
        {
            txtNume.Text = "";
            txtPrenume.Text = "";
            txtFunctie.Text = "";
            txtSalariu.Text = "";
            txtSpor.Text = "";
            txtPremii.Text = "";
        }
    }
}
