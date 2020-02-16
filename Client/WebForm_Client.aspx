<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm_Client.aspx.cs" Inherits="Client.WebForm_Client" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="Stylesheet" href="styleWebFormCient.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:100%; vertical-align: top;">
            <div class="SetDataEmployee" >
                <div class="titleBlocs">
                <asp:Label ID="lblBlockSetDataEmployee_title" runat="server" Text="Введите данные добавляемого сотрудника"></asp:Label>
                </div>

                <div>
                    <asp:Label ID="lblSetFirstNameText" runat="server" Text="Имя"></asp:Label>
                    <asp:TextBox ID="tbxSetFirstName" runat="server"></asp:TextBox>
                    <br />
                </div>
                <div>
                    <asp:Label ID="lblSetLastNameText" runat="server" Text="Фамилия"></asp:Label>
                    <asp:TextBox ID="tbxSetLastName" runat="server"></asp:TextBox><br />
                </div>
                <div>
                    <asp:Label ID="lblSetPatronumic" runat="server" Text="Отчество"></asp:Label>
                    <asp:TextBox ID="tbxSetPatronumic" runat="server"></asp:TextBox><br />
                </div>
                <div>
                    <asp:Calendar ID="CalendarBirchDay" runat="server"></asp:Calendar>
                </div>
                <br />
                <div>
                    <br />
                    <asp:Label ID="lblStatusInsert" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <asp:Button ID="btnInsertEmploye" runat="server" Text="Добавить сотрудника" OnClick="btnInsertEmploye_Click" /><br />
                </div>
            </div>

            <div class="GetEmployees">
                <div class="titleBlocs">
                <asp:Label ID="lblBlocGetEmployees_titile" runat="server" Text="Введите искомых сотрудников (Поиск по отчеству осуществляется по первым символам)"></asp:Label>
                </div>
                
                <div>
                    <asp:Label ID="lblFirstNameText" runat="server" Text="Имя"></asp:Label>
                    <asp:TextBox ID="tbxFirstName" runat="server"></asp:TextBox>
                    <br />
                </div>
                <div>
                    <asp:Label ID="lblLastNameText" runat="server" Text="Фамилия"></asp:Label>
                    <asp:TextBox ID="tbxLastName" runat="server"></asp:TextBox><br />
                </div>
                <div>
                    <asp:Label ID="lblPatronumic" runat="server" Text="Отчество"></asp:Label>
                    <asp:TextBox ID="tbxPatronumic" runat="server"></asp:TextBox><br />
                </div>
                <br />
                <asp:Button ID="btnLoad" runat="server" Text="Загрузить" OnClick="btnLoad_Click" /><br />
                <br />
                <br />
                <asp:Label ID="lblResualt" runat="server" Text=""></asp:Label>
                <br />

                <asp:GridView runat="server" ID="tableEmployee" Width="100%">
                </asp:GridView>
            </div>


        </div>
    </form>
</body>
</html>
