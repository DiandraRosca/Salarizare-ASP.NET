<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatPlata.aspx.cs" Inherits="SalarizareApp.StatPlata" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Stat de Plata</title>
    <style>
        body { font-family: Arial, sans-serif; }
        .container { width: 80%; margin: auto; }
        h2 { text-align: center; }
        table { width: 100%; border-collapse: collapse; margin-top: 20px; }
        th, td { border: 1px solid black; padding: 8px; text-align: center; }
        .total-row { font-weight: bold; }
        .buttons { text-align: center; margin: 20px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="buttons">
             <asp:Button ID="btnHome" runat="server" Text="Home" 
               OnClick="btnHome_Click" 
               Style="background-color: #0078D7; color: white; border: none; padding: 12px 24px; font-size: 16px; border-radius: 5px; cursor: pointer; transition: background 0.3s ease-in-out; display: block; margin: 20px auto; text-align: center; width: 150px;" />
        </div>

        <div class="container">
            <h2>STAT DE PLATA</h2>
            <p style="text-align: right;">DATA: <asp:Label ID="lblDate" runat="server" /></p>
            
            <asp:GridView ID="gvStatPlata" runat="server" AutoGenerateColumns="False" CssClass="table" ShowFooter="True"
                OnRowDataBound="gvStatPlata_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="NR.CRT" />
                    <asp:BoundField DataField="NUME" HeaderText="NUME" />
                    <asp:BoundField DataField="PRENUME" HeaderText="PRENUME" />
                    <asp:BoundField DataField="FUNCTIE" HeaderText="FUNCTIE" />
                    <asp:BoundField DataField="SALAR_BAZA" HeaderText="SALAR_BAZA" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="VIRAT_CARD" HeaderText="VIRAT_CARD" DataFormatString="{0:N0}" />
                </Columns>
            </asp:GridView>

            <div class="buttons">
                <asp:Button ID="btnPrint" runat="server" Text="Tipărește" OnClientClick="window.print(); return false;" />
                <asp:Button ID="btnExportPDF" runat="server" Text="Exportă PDF" OnClick="btnExportPDF_Click" />
            </div>
        </div>
    </form>
</body>
</html>
