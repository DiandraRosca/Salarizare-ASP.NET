<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fluturasi.aspx.cs" Inherits="SalarizareApp.Fluturasi" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Fluturași de Salariu</title>
    <style>
        body { font-family: Arial, sans-serif; }
        .container { width: 80%; margin: auto; }
        h2 { text-align: center; }
        table { width: 100%; border-collapse: collapse; margin-top: 20px; }
        th, td { border: 1px solid black; padding: 8px; text-align: left; }
        .buttons { text-align: center; margin: 20px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="buttons">
              <asp:Button ID="btnHome" runat="server" Text="Home" CssClass="home-button" OnClick="btnHome_Click" 
               Style="background-color: #0078D7; color: white; border: none; padding: 12px 24px; font-size: 16px; border-radius: 5px; cursor: pointer; transition: background 0.3s ease-in-out; display: block; margin: 20px auto; text-align: center; width: 150px;" />
        </div>

        <div class="container">
            <h2>Fluturași de Salariu</h2>

            <!-- Căutare angajat -->
            <div style="text-align: center; margin-bottom: 20px;">
                <asp:Label runat="server" Text="Caută angajat: " />
                <asp:TextBox ID="txtCautare" runat="server"></asp:TextBox>
                <asp:Button ID="btnCauta" runat="server" Text="Caută" OnClick="btnCauta_Click" />
            </div>

            <asp:Repeater ID="rptFluturasi" runat="server">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td><b>DATA:</b> <%# DateTime.Now.ToString("dd/MM/yyyy") %></td>
                        </tr>
                        <tr>
                            <td><b>NR.CRT:</b> <%# Eval("ID") %></td>
                            <td><b>IMPOZIT:</b> <%# Eval("IMPOZIT") %></td>
                        </tr>
                        <tr>
                            <td><b>NUME:</b> <%# Eval("NUME") %></td>
                            <td><b>CAS :</b> <%# Eval("CAS") %></td>
                        </tr>
                        <tr>
                            <td><b>PRENUME:</b> <%# Eval("PRENUME") %></td>
                            <td><b>CASS :</b> <%# Eval("CASS") %></td> <!-- 🔴🔴🔴 AICI AM ADAUGAT -->
                        </tr>
                        <tr>
                            <td><b>FUNCȚIE:</b> <%# Eval("FUNCTIE") %></td>
                            <td><b>REȚINERI:</b> <%# Eval("RETINERI") %></td>
                        </tr>
                        <tr>
                            <td><b>SALAR_BAZA:</b> <%# Eval("SALAR_BAZA") %></td>
                            <td><b>VIRAT_CARD:</b> <%# Eval("VIRAT_CARD") %></td>
                        </tr>
                        <tr>
                            <td><b>SPOR %:</b> <%# Eval("SPOR") %></td>
                        </tr>
                        <tr>
                            <td><b>PREMII BRUTE:</b> <%# Eval("PREMII_BRUTE") %></td>
                        </tr>
                        <tr>
                            <td><b>TOTAL_BRUT:</b> <%# Eval("TOTAL_BRUT") %></td>
                        </tr>
                        <tr>
                            <td><b>BRUT_IMPOZABIL:</b> <%# Eval("BRUT_IMPOZABIL") %></td>
                        </tr>
                    </table>
                    <hr />
                </ItemTemplate>
            </asp:Repeater>

            <div class="buttons">
                <asp:Button ID="btnPrint" runat="server" Text="Tipărește" OnClientClick="window.print(); return false;" />
                <asp:Button ID="btnExportPDF" runat="server" Text="Exportă PDF" OnClick="btnExportPDF_Click" />
            </div>
        </div>
    </form>
</body>
</html>
