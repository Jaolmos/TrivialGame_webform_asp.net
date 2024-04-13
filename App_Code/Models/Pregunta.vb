Namespace Models
    Public Class Pregunta

        Public Property ID As Integer
        Public Property Texto As String


        'Constructor por defecto
        Public Sub New()

        End Sub

        Public Sub New(id As Integer, texto As String)
            Me.ID = id
            Me.Texto = texto
        End Sub
    End Class

End Namespace
