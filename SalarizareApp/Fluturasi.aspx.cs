using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace SalarizareApp
{
    public partial class Fluturasi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IncarcaFluturasi();
            }
        }
        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm1.aspx"); // Redirecționează către pagina principală
        }


        private void IncarcaFluturasi(string numeAngajat = "")
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ID, NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR, PREMII_BRUTE, TOTAL_BRUT, BRUT_IMPOZABIL, VIRAT_CARD, IMPOZIT, CAS, CASS, RETINERI FROM ANGAJATI";

                if (!string.IsNullOrEmpty(numeAngajat))
                {
                    query += " WHERE LOWER(NUME) LIKE :nume OR LOWER(PRENUME) LIKE :nume";
                }

                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(numeAngajat))
                    {
                        cmd.Parameters.Add(":nume", "%" + numeAngajat.ToLower() + "%");
                    }

                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        rptFluturasi.DataSource = dt;
                        rptFluturasi.DataBind();
                    }
                }
            }
        }

        protected void btnCauta_Click(object sender, EventArgs e)
        {
            string numeAngajat = txtCautare.Text.Trim();
            IncarcaFluturasi(numeAngajat);
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                string connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ID, NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR, PREMII_BRUTE, TOTAL_BRUT, BRUT_IMPOZABIL, VIRAT_CARD, IMPOZIT, CAS, CASS, RETINERI FROM ANGAJATI";

                    if (!string.IsNullOrEmpty(txtCautare.Text))
                    {
                        query += " WHERE LOWER(NUME) LIKE :nume OR LOWER(PRENUME) LIKE :nume";
                    }

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(txtCautare.Text))
                        {
                            cmd.Parameters.Add(":nume", "%" + txtCautare.Text.ToLower() + "%");
                        }

                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                pdfDoc.Add(new Paragraph("Nu există date pentru angajatul căutat.", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                            }
                            else
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    PdfPTable table = new PdfPTable(2);
                                    table.WidthPercentage = 100;

                                    void AddRow(string label, string value)
                                    {
                                        PdfPCell cell1 = new PdfPCell(new Phrase(label, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)));
                                        cell1.BackgroundColor = new BaseColor(220, 220, 220);
                                        cell1.Padding = 5;
                                        table.AddCell(cell1);

                                        PdfPCell cell2 = new PdfPCell(new Phrase(value, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                                        cell2.Padding = 5;
                                        table.AddCell(cell2);
                                    }

                                    AddRow("DATA:", DateTime.Now.ToString("dd/MM/yyyy"));
                                    AddRow("NR.CRT:", row["ID"].ToString());
                                    AddRow("NUME:", row["NUME"].ToString());
                                    AddRow("PRENUME:", row["PRENUME"].ToString());
                                    AddRow("FUNCTIE:", row["FUNCTIE"].ToString());
                                    AddRow("SALAR_BAZA:", row["SALAR_BAZA"].ToString());
                                    AddRow("SPOR %:", row["SPOR"].ToString());
                                    AddRow("PREMII BRUTE:", row["PREMII_BRUTE"].ToString());
                                    AddRow("TOTAL_BRUT:", row["TOTAL_BRUT"].ToString());
                                    AddRow("BRUT_IMPOZABIL:", row["BRUT_IMPOZABIL"].ToString());
                                    AddRow("VIRAT_CARD:", row["VIRAT_CARD"].ToString());
                                    AddRow("IMPOZIT:", row["IMPOZIT"].ToString());
                                    AddRow("CAS (25%):", row["CAS"].ToString());
                                    AddRow("CASS (10%):", row["CASS"].ToString());  // 🔴🔴🔴 AICI AM ADAUGAT CASS
                                    AddRow("RETINERI:", row["RETINERI"].ToString());

                                    pdfDoc.Add(table);
                                    pdfDoc.Add(new Paragraph("\n"));
                                }
                            }
                        }
                    }
                }

                pdfDoc.Close();

                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=Fluturasi.pdf");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
        }

    }
}
