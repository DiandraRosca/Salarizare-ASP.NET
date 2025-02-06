<%@ Page Title="Actualizare Date" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ActualizareDate.aspx.cs" Inherits="SalarizareApp.ActualizareDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Actualizare Date Angajați</h2>

    <label for="txtCauta">Caută angajat:</label>
    <asp:TextBox ID="txtCauta" runat="server" Placeholder="Introduceți numele"></asp:TextBox>
    <asp:Button ID="btnCauta" runat="server" Text="Caută" OnClick="btnCauta_Click" />

    <asp:GridView ID="gvAngajati" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
        OnRowEditing="gvAngajati_RowEditing"
        OnRowCancelingEdit="gvAngajati_RowCancelingEdit"
        OnRowUpdating="gvAngajati_RowUpdating">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="NUME" HeaderText="Nume" />
            <asp:BoundField DataField="PRENUME" HeaderText="Prenume" />
            <asp:BoundField DataField="FUNCTIE" HeaderText="Funcție" />
            <asp:BoundField DataField="SALAR_BAZA" HeaderText="Salariu Bază" />
            <asp:BoundField DataField="SPOR" HeaderText="Spor (%)" />
            <asp:BoundField DataField="PREMII_BRUTE" HeaderText="Premii Brute" />
            <asp:BoundField DataField="RETINERI" HeaderText="Retineri" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
    </asp:GridView>
</asp:Content>
