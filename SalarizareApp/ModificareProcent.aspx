<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificareProcent.aspx.cs" Inherits="SalarizareApp.ModificareProcent" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Modificare Procent Impozit</title>
    <style>
        body { font-family: Arial, sans-serif; }
        .container { width: 40%; margin: auto; padding: 20px; border: 1px solid #ccc; border-radius: 10px; background-color: #f9f9f9; }
        h2 { text-align: center; }
        .form-group { margin-bottom: 15px; }
        label { font-weight: bold; }
        input, select { width: 100%; padding: 8px; margin-top: 5px; }
        .btn { width: 100%; padding: 10px; background-color: #4CAF50; color: white; border: none; cursor: pointer; font-size: 16px; }
        .btn:hover { background-color: #45a049; }
        .message { text-align: center; margin-top: 10px; font-weight: bold; }
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
            <h2>Modificare Procent Impozit</h2>
            
            <div class="form-group">
                <label for="ddlTipProcent">Tip Procent:</label>
                <asp:DropDownList ID="ddlTipProcent" runat="server">
                    <asp:ListItem Value="IMPOZIT">IMPOZIT</asp:ListItem>
                    <asp:ListItem Value="CAS">CAS</asp:ListItem>
                    <asp:ListItem Value="CASS">CASS</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="txtProcentNou">Procent Nou:</label>
                <asp:TextBox ID="txtProcentNou" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtParola">Parolă Administrator:</label>
                <asp:TextBox ID="txtParola" runat="server" TextMode="Password"></asp:TextBox>
            </div>

            <asp:Button ID="btnModifica" runat="server" Text="Modifică" CssClass="btn" OnClick="btnModifica_Click" />

            <div class="message">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
