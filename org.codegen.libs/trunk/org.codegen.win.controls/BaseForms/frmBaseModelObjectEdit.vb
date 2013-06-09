''' <summary>
''' Base form Class that can be used to Load/Save model Objects.
''' It is using reflection to get/set properties from a model object
''' </summary>
''' <remarks></remarks>
Public Class frmBaseModelObjectEdit

    Private dataObject As IModelObject

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
