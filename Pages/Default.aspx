<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="TrivialGame._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Trivial</title>
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../Content/custom.css" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-dark bg-primary">
            <div class="container-fluid">
                <a class="navbar-brand" href="Default.aspx">Trivial</a>
            </div>
        </nav>

        <div class="container pt-5">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <h2 class="text-center my-5">Trivial Game</h2>
                    <asp:Label ID="lblPregunta" runat="server" Text="Aquí aparecerá la pregunta." Font-Bold="True" Font-Size="Large" class="mb-3 d-block"></asp:Label>
                    <asp:RadioButtonList ID="rblRespuestas" runat="server" RepeatDirection="Vertical" Font-Size="Medium" class="mb-3">
                    </asp:RadioButtonList>
                    <asp:Button ID="btnResponder" runat="server" Text="Responder" OnClick="btnResponder_Click" CssClass="btn btn-primary d-block mx-auto mb-3" />
                    <asp:Label ID="lblResultado" runat="server" Text="" ForeColor="Red" Font-Size="Medium" CssClass="text-center mb-3"></asp:Label>
                    <asp:Button ID="btnReiniciar" runat="server" Text="Reiniciar Juego" OnClick="btnReiniciar_Click" Visible="False" CssClass="btn btn-secondary d-block mx-auto" />
                </div>
            </div>
        </div>
        
    </form>
    <script src="~/Content/bootstrap/js/bootstrap.bundle.min.js"></script>
</body>
</html>
