Imports TrivialGame.Models
Public Interface IPreguntaRepositorio
    Function ObtenerPreguntaAleatoria(preguntasYaRespondidas As List(Of Integer)) As Pregunta
    Function ObtenerRespuestas(idPregunta As Integer) As List(Of Respuesta)
    Function VerificarRespuesta(idPregunta As Integer, idRespuesta As Integer) As Boolean
End Interface
