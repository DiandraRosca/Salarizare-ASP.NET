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
    public partial class StatPlata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                IncarcaStatPlata();
            }
        }
        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm1.aspx"); // Redirecționează către pagina principală
        }


        private void IncarcaStatPlata()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ID, NUME, PRENUME, FUNCTIE, SALAR_BAZA, VIRAT_CARD FROM ANGAJATI";
                using (OracleDataAdapter da = new OracleDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvStatPlata.DataSource = dt;
                    gvStatPlata.DataBind();
                }
            }
        }

        protected void gvStatPlata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[4].Text = string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[4].Text));
                e.Row.Cells[5].Text = string.Format("{0:N0}", Convert.ToDecimal(e.Row.Cells[5].Text));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                decimal totalSalariu = 0, totalVirat = 0;

                foreach (GridViewRow row in gvStatPlata.Rows)
                {
                    totalSalariu += Convert.ToDecimal(row.Cells[4].Text.Replace(",", ""));
                    totalVirat += Convert.ToDecimal(row.Cells[5].Text.Replace(",", ""));
                }

                e.Row.Cells[3].Text = "Total:";
                e.Row.Cells[3].Font.Bold = true;
                e.Row.Cells[4].Text = string.Format("{0:N0}", totalSalariu);
                e.Row.Cells[4].Font.Bold = true;
                e.Row.Cells[5].Text = string.Format("{0:N0}", totalVirat);
                e.Row.Cells[5].Font.Bold = true;
            }
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 10f);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                PdfPTable table = new PdfPTable(gvStatPlata.Columns.Count);
                table.WidthPercentage = 100;

                // Header
                foreach (TableCell headerCell in gvStatPlata.HeaderRow.Cells)
                {
                    PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                    pdfCell.BackgroundColor = new BaseColor(200, 200, 200);
                    pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(pdfCell);
                }

                // Rows
                foreach (GridViewRow row in gvStatPlata.Rows)
                {
                    foreach (TableCell cell in row.Cells)
                    {
                        PdfPCell pdfCell = new PdfPCell(new Phrase(cell.Text));
                        pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(pdfCell);
                    }
                }

                // Footer (Total)
                PdfPCell totalLabelCell = new PdfPCell(new Phrase("Total:", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                totalLabelCell.Colspan = 4;
                totalLabelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(totalLabelCell);

                decimal totalSalariu = 0, totalVirat = 0;
                foreach (GridViewRow row in gvStatPlata.Rows)
                {
                    totalSalariu += Convert.ToDecimal(row.Cells[4].Text.Replace(",", ""));
                    totalVirat += Convert.ToDecimal(row.Cells[5].Text.Replace(",", ""));
                }

                PdfPCell totalSalariuCell = new PdfPCell(new Phrase(string.Format("{0:N0}", totalSalariu), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                totalSalariuCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(totalSalariuCell);

                PdfPCell totalViratCell = new PdfPCell(new Phrase(string.Format("{0:N0}", totalVirat), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                totalViratCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(totalViratCell);

                pdfDoc.Add(table);
                pdfDoc.Close();

                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=StatPlata.pdf");
                Response.OutputStream.Write(bytes, 0, bytes.Length);
                Response.Flush();
                Response.End();
            }
        }
    
    }
}
