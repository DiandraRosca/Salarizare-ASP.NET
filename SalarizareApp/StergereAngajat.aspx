<%@ Page Title="Ștergere Angajat" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="StergereAngajat.aspx.cs" Inherits="SalarizareApp.StergereAngajat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Ștergere Angajat</h2>

    <label for="txtCauta">Caută angajat după nume:</label>
    <asp:TextBox ID="txtCauta" runat="server" Placeholder="Introduceți numele"></asp:TextBox>
    <asp:Button ID="btnCauta" runat="server" Text="Caută" OnClick="btnCauta_Click" />

    <asp:GridView ID="gvAngajati" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
        OnRowDeleting="gvAngajati_RowDeleting">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="NUME" HeaderText="Nume" />
            <asp:BoundField DataField="PRENUME" HeaderText="Prenume" />
            <asp:BoundField DataField="FUNCTIE" HeaderText="Funcție" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
</asp:Content>
