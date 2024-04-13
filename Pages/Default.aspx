<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="TrivialGame._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 30px;">
            <h2>Trivia Game</h2>
            <asp:Label ID="lblPregunta" runat="server" Text="Aquí aparecerá la pregunta." Font-Bold="True" Font-Size="Large"></asp:Label>
            <br />
            <br />
            <asp:RadioButtonList ID="rblRespuestas" runat="server" RepeatDirection="Vertical" Font-Size="Medium">
            </asp:RadioButtonList>
            <br />
            <asp:Button ID="btnResponder" runat="server" Text="Responder" OnClick="btnResponder_Click" CssClass="btn btn-primary" />
            <asp:Label ID="lblResultado" runat="server" Text="" ForeColor="Red" Font-Size="Medium"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnReiniciar" runat="server" Text="Reiniciar Juego" OnClick="btnReiniciar_Click" Visible="False" CssClass="btn btn-secondary" />
        </div>>
    </form>t>

</body>
</html>
