Namespace Models
    Public Class Respuesta
        Public Property ID As Integer
        Public Property Texto As String
        Public Property EsCorrecta As Boolean


        Public Sub New()

        End Sub


        Public Sub New(id As Integer, texto As String, esCorrecta As Boolean)
            Me.ID = id
            Me.Texto = texto
            Me.EsCorrecta = esCorrecta
        End Sub
    End Class

End Namespace