Imports System.Collections.Generic

Public Class _Default
    Inherits System.Web.UI.Page

    Private repositorio As IPreguntaRepositorio = New PreguntaRepositorio()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("Respondidas") = New List(Of Integer)()
            Session("Correctas") = 0
            Try
                CargarPregunta()
            Catch ex As Exception
                lblResultado.Text = "Error al cargar la pregunta: " & ex.Message
            End Try
        End If
    End Sub

    Private Sub CargarPregunta()

        Dim preguntasYaRespondidas As List(Of Integer) = CType(Session("Respondidas"), List(Of Integer))
        Dim pregunta = repositorio.ObtenerPreguntaAleatoria(preguntasYaRespondidas)

        If pregunta IsNot Nothing Then
            lblPregunta.Text = pregunta.Texto
            lblPregunta.Attributes("IDPregunta") = pregunta.ID.ToString()
            CargarRespuestas(pregunta.ID)
        Else
            MostrarResultadoFinal()
        End If
    End Sub




    Private Sub CargarRespuestas(idPregunta As Integer)
        Try
            Dim respuestas = repositorio.ObtenerRespuestas(idPregunta)
            If respuestas IsNot Nothing AndAlso respuestas.Count > 0 Then
                rblRespuestas.DataSource = respuestas
                rblRespuestas.DataTextField = "Texto"
                rblRespuestas.DataValueField = "ID"
                rblRespuestas.DataBind()
            Else
                lblResultado.Text = "No hay respuestas disponibles para esta pregunta."
            End If
        Catch ex As Exception
            lblResultado.Text = "Error al cargar respuestas: " & ex.Message
        End Try
    End Sub

    Protected Sub btnResponder_Click(sender As Object, e As EventArgs) Handles btnResponder.Click
        Try
            Dim idPregunta As Integer
            Dim idRespuestaSeleccionada As Integer
            Dim esCorrecta As Boolean = False

            ' Usa TryParse para una conversión segura de ID de pregunta.
            If Not Integer.TryParse(lblPregunta.Attributes("IDPregunta"), idPregunta) Then
                Throw New Exception("El ID de la pregunta no es válido.")
            End If

            ' Usa TryParse para una conversión segura de ID de respuesta seleccionada.
            If Not Integer.TryParse(rblRespuestas.SelectedValue, idRespuestaSeleccionada) Then
                Throw New Exception("El ID de la respuesta seleccionada no es válido.")
            End If

            esCorrecta = repositorio.VerificarRespuesta(idPregunta, idRespuestaSeleccionada)

            If esCorrecta Then
                lblResultado.Text = "¡Correcto!"
                Session("Correctas") = CType(Session("Correctas"), Integer) + 1
            Else
                lblResultado.Text = "Incorrecto."
            End If

            Dim respondidas As List(Of Integer) = CType(Session("Respondidas"), List(Of Integer))
            respondidas.Add(idPregunta)
            Session("Respondidas") = respondidas

            If respondidas.Count >= 10 Then
                MostrarResultadoFinal()
            Else
                CargarPregunta()
            End If
        Catch ex As Exception
            lblResultado.Text = "Error durante la respuesta: " & ex.Message
        End Try
    End Sub


    Private Sub MostrarResultadoFinal()
        Dim correctas As Integer = CType(Session("Correctas"), Integer)
        lblResultado.Text = $"Juego completado. Has acertado {correctas} de 10 preguntas."
        btnReiniciar.Visible = True
        btnResponder.Visible = False
        rblRespuestas.Items.Clear()
        lblPregunta.Text = ""
    End Sub

    Protected Sub btnReiniciar_Click(sender As Object, e As EventArgs) Handles btnReiniciar.Click
        Session("Respondidas") = New List(Of Integer)()
        Session("Correctas") = 0
        lblResultado.Text = String.Empty
        btnResponder.Visible = True
        btnReiniciar.Visible = False
        CargarPregunta()
    End Sub


End Class
