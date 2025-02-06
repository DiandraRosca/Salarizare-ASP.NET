<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdaugareAngajat.aspx.cs" Inherits="SalarizareApp.AdaugareAngajat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Adăugare Angajat</h2>
    
    <asp:Label runat="server" Text="Nume: " />
    <asp:TextBox runat="server" ID="txtNume" /><br />

    <asp:Label runat="server" Text="Prenume: " />
    <asp:TextBox runat="server" ID="txtPrenume" /><br />

    <asp:Label runat="server" Text="Funcție: " />
    <asp:TextBox runat="server" ID="txtFunctie" /><br />

    <asp:Label runat="server" Text="Salariu Bază: " />
    <asp:TextBox runat="server" ID="txtSalariu" /><br />

    <asp:Label runat="server" Text="Spor %: " />
    <asp:TextBox runat="server" ID="txtSpor" /><br />

    <asp:Label runat="server" Text="Premii Brute: " />
    <asp:TextBox runat="server" ID="txtPremii" /><br />

    <asp:Button runat="server" ID="btnSalveaza" Text="Salvează" OnClick="btnSalveaza_Click" />

</asp:Content>
