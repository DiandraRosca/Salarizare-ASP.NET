<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalculSalarii.aspx.cs" Inherits="SalarizareApp.CalculSalarii" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Calcul Salarii</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }
        h1 {
            color: #333;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        th, td {
            border: 1px solid black;
            padding: 10px;
            text-align: center;
        }
        th {
            background-color: #0078D7;
            color: white;
        }
        .search-container {
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server"> <!-- ✅ Corect formatat -->

        <h1>Calcul Salarii</h1>

        <!-- Câmp de căutare angajat -->
        <div class="search-container">
            <label for="txtCauta">Caută angajat:</label>
            <asp:TextBox ID="txtCauta" runat="server" Placeholder="Introduceți numele"></asp:TextBox>
            <asp:Button ID="btnCauta" runat="server" Text="Caută" OnClick="btnCauta_Click" />
        </div>

        <!-- Tabelul cu salarii -->
        <asp:GridView ID="gvSalarii" runat="server" AutoGenerateColumns="False" CssClass="table">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="NUME" HeaderText="Nume" />
                <asp:BoundField DataField="PRENUME" HeaderText="Prenume" />
                <asp:BoundField DataField="SALAR_BAZA" HeaderText="Salariu Bază" />
                <asp:BoundField DataField="SPOR" HeaderText="Spor (%)" />
                <asp:BoundField DataField="PREMII_BRUTE" HeaderText="Premii Brute" />
                <asp:BoundField DataField="TOTAL_BRUT" HeaderText="Total Brut" />
                <asp:BoundField DataField="CAS" HeaderText="CAS" />
                <asp:BoundField DataField="CASS" HeaderText="CASS" />
                <asp:BoundField DataField="BRUT_IMPOZABIL" HeaderText="Brut Impozabil" />
                <asp:BoundField DataField="IMPOZIT" HeaderText="Impozit" />
                <asp:BoundField DataField="RETINERI" HeaderText="Retineri" />
                <asp:BoundField DataField="VIRAT_CARD" HeaderText="Virat Card" />
            </Columns>
        </asp:GridView>

    </form> <!-- ✅ Închiderea corectă a formularului -->
</body>
</html>
