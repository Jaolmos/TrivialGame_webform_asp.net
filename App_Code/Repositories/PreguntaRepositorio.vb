Imports System.Data
Imports System.Data.SqlClient
Imports TrivialGame.Models


Public Class PreguntaRepositorio
    Implements IPreguntaRepositorio

    Public Function ObtenerPreguntaAleatoria(preguntasYaRespondidas As List(Of Integer)) As Pregunta Implements IPreguntaRepositorio.ObtenerPreguntaAleatoria
        Dim pregunta As New Pregunta()
        Using conexion As New SqlConnection(ConexionBD.CadenaConexion)
            ' Construyendo dinámicamente la parte de la consulta SQL para la cláusula IN
            Dim inClause As String = String.Empty
            If preguntasYaRespondidas.Any() Then
                inClause = String.Join(",", preguntasYaRespondidas.Select(Function(id) id.ToString()).ToArray())
            End If

            Dim consulta As String = If(inClause.Length > 0,
                                    $"SELECT TOP 1 * FROM Preguntas WHERE IDPregunta NOT IN ({inClause}) ORDER BY NEWID()",
                                    "SELECT TOP 1 * FROM Preguntas ORDER BY NEWID()")

            Dim dataSet As New DataSet()
            Using adapter As New SqlDataAdapter(consulta, conexion)
                adapter.Fill(dataSet)

                If dataSet.Tables(0).Rows.Count > 0 Then
                    Dim row As DataRow = dataSet.Tables(0).Rows(0)
                    pregunta.ID = Convert.ToInt32(row("IDPregunta"))
                    pregunta.Texto = Convert.ToString(row("TextoPregunta"))
                End If
            End Using
        End Using
        Return pregunta
    End Function


    Public Function ObtenerRespuestas(idPregunta As Integer) As List(Of Respuesta) Implements IPreguntaRepositorio.ObtenerRespuestas
        Dim respuestas As New List(Of Respuesta)
        Using conexion As SqlConnection = ConexionBD.ObtenerConexion()
            Dim consulta As String = "SELECT * FROM Respuestas WHERE IDPregunta = @IDPregunta"
            Dim dataSet As New DataSet()
            Using dataAdapter As New SqlDataAdapter(consulta, conexion)
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IDPregunta", idPregunta)
                dataAdapter.Fill(dataSet)

                If dataSet.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In dataSet.Tables(0).Rows
                        Dim respuesta As New Respuesta(
                        Convert.ToInt32(row("IDRespuesta")),
                        row("TextoRespuesta").ToString(),
                        Convert.ToBoolean(row("EsCorrecta"))
                    )
                        respuestas.Add(respuesta)
                    Next
                End If
            End Using
        End Using
        Return respuestas
    End Function


    Public Function VerificarRespuesta(idPregunta As Integer, idRespuesta As Integer) As Boolean Implements IPreguntaRepositorio.VerificarRespuesta
        Using conexion As New SqlConnection(ConexionBD.CadenaConexion)
            ' Creamos un nuevo DataSet
            Dim dataset As New DataSet()
            ' Definimos la consulta SQL
            Dim consulta As String = "SELECT EsCorrecta FROM Respuestas WHERE IDPregunta = @IDPregunta AND IDRespuesta = @IDRespuesta"
            ' Creamos un SqlDataAdapter con la consulta y la conexión
            Using dataAdapter As New SqlDataAdapter(consulta, conexion)
                ' Añadimos los parámetros necesarios para la consulta
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IDPregunta", idPregunta)
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IDRespuesta", idRespuesta)
                ' Llenamos el DataSet con el resultado de la consulta
                dataAdapter.Fill(dataset, "ResultadoVerificacion")
            End Using

            ' Comprobamos si hay alguna fila en la tabla del DataSet
            If dataset.Tables("ResultadoVerificacion").Rows.Count > 0 Then
                ' Obtenemos la fila y devolvemos el valor de EsCorrecta
                Dim row As DataRow = dataset.Tables("ResultadoVerificacion").Rows(0)
                Return Convert.ToBoolean(row("EsCorrecta"))
            Else
                ' Si no hay filas, la respuesta no es correcta o no existe
                Return False
            End If
        End Using
    End Function



End Class
