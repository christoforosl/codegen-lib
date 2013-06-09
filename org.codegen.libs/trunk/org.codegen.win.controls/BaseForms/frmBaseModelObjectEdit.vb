Public Class frmBaseModelObjectEdit

    ''' <summary>
    ''' LoadData 
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub LoadData()

        Throw New NotImplementedException("clients must override this to load record.")

    End Sub

    ''' <summary>
    ''' SaveData 
    ''' </summary>
    Public Overrides Function SaveData() As enumSaveDataResult

        Throw New NotImplementedException("clients must override this to save record.")

    End Function

    Public Overrides Sub DeleteData()

        Throw New NotImplementedException("Client forms must override this method")

    End Sub

End Class
