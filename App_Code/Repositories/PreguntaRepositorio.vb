Imports System.Data
Imports System.Data.SqlClient
Imports TrivialGame.Models

Public Class PreguntaRepositorio
    Implements IPreguntaRepositorio

    ' Función para obtener una pregunta aleatoria que no haya sido respondida previamente.
    Public Function ObtenerPreguntaAleatoria(preguntasYaRespondidas As List(Of Integer)) As Pregunta Implements IPreguntaRepositorio.ObtenerPreguntaAleatoria
        Dim pregunta As New Pregunta()
        Using conexion As New SqlConnection(ConexionBD.CadenaConexion)

            ' Construimos una cláusula IN para excluir preguntas ya respondidas.
            Dim inClause As String = String.Empty
            If preguntasYaRespondidas.Any() Then
                inClause = String.Join(",", preguntasYaRespondidas.Select(Function(id) id.ToString()).ToArray())
            End If

            ' Construimos la consulta SQL para obtener una pregunta aleatoria.
            Dim consulta As String = If(inClause.Length > 0,
                                    $"SELECT TOP 1 * FROM Preguntas WHERE IDPregunta NOT IN ({inClause}) ORDER BY NEWID()",
                                    "SELECT TOP 1 * FROM Preguntas ORDER BY NEWID()")

            ' Ejecutamos la consulta y llenamos un DataSet con el resultado.
            Dim dataSet As New DataSet()
            Using adapter As New SqlDataAdapter(consulta, conexion)
                adapter.Fill(dataSet)

                ' Verificamos si se encontró alguna pregunta.
                If dataSet.Tables(0).Rows.Count > 0 Then
                    ' Obtenemos la primera fila de resultados y asignamos sus valores a la pregunta.
                    Dim row As DataRow = dataSet.Tables(0).Rows(0)
                    pregunta.ID = Convert.ToInt32(row("IDPregunta"))
                    pregunta.Texto = Convert.ToString(row("TextoPregunta"))
                End If
            End Using
        End Using
        ' Devolvemos la pregunta obtenida.
        Return pregunta
    End Function

    ' Función para obtener las respuestas asociadas a una pregunta específica.
    Public Function ObtenerRespuestas(idPregunta As Integer) As List(Of Respuesta) Implements IPreguntaRepositorio.ObtenerRespuestas
        Dim respuestas As New List(Of Respuesta)
        Using conexion As SqlConnection = ConexionBD.ObtenerConexion()
            ' Construimos la consulta SQL para obtener las respuestas de la pregunta específica.
            Dim consulta As String = "SELECT * FROM Respuestas WHERE IDPregunta = @IDPregunta"
            Dim dataSet As New DataSet()
            Using dataAdapter As New SqlDataAdapter(consulta, conexion)
                ' Añadimos el parámetro IDPregunta a la consulta.
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IDPregunta", idPregunta)
                ' Llenamos un DataSet con los resultados de la consulta.
                dataAdapter.Fill(dataSet)

                ' Verificamos si se encontraron respuestas.
                If dataSet.Tables(0).Rows.Count > 0 Then
                    ' Iteramos sobre las filas de resultados y creamos objetos Respuesta con sus valores.
                    For Each row As DataRow In dataSet.Tables(0).Rows
                        Dim respuesta As New Respuesta(
                            Convert.ToInt32(row("IDRespuesta")),
                            row("TextoRespuesta").ToString(),
                            Convert.ToBoolean(row("EsCorrecta"))
                        )
                        ' Agregamos la respuesta a la lista de respuestas.
                        respuestas.Add(respuesta)
                    Next
                End If
            End Using
        End Using
        ' Devolvemos la lista de respuestas obtenida.
        Return respuestas
    End Function

    ' Función para verificar si una respuesta a una pregunta es correcta.
    Public Function VerificarRespuesta(idPregunta As Integer, idRespuesta As Integer) As Boolean Implements IPreguntaRepositorio.VerificarRespuesta
        Using conexion As New SqlConnection(ConexionBD.CadenaConexion)
            ' Creamos un nuevo DataSet para almacenar el resultado de la consulta.
            Dim dataset As New DataSet()
            ' Definimos la consulta SQL para verificar la respuesta.
            Dim consulta As String = "SELECT EsCorrecta FROM Respuestas WHERE IDPregunta = @IDPregunta AND IDRespuesta = @IDRespuesta"
            ' Creamos un SqlDataAdapter con la consulta y la conexión.
            Using dataAdapter As New SqlDataAdapter(consulta, conexion)
                ' Añadimos los parámetros necesarios para la consulta.
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IDPregunta", idPregunta)
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IDRespuesta", idRespuesta)
                ' Llenamos el DataSet con el resultado de la consulta.
                dataAdapter.Fill(dataset, "ResultadoVerificacion")
            End Using

            ' Verificamos si se encontró alguna fila en el DataSet.
            If dataset.Tables("ResultadoVerificacion").Rows.Count > 0 Then
                ' Obtenemos la primera fila de resultados y devolvemos el valor de EsCorrecta.
                Dim row As DataRow = dataset.Tables("ResultadoVerificacion").Rows(0)
                Return Convert.ToBoolean(row("EsCorrecta"))
            Else
                ' Si no se encontraron resultados, la respuesta no es correcta o no existe.
                Return False
            End If
        End Using
    End Function

End Class
