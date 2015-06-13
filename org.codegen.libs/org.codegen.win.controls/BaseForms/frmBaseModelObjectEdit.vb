Imports System.Collections.Generic
Imports System.ComponentModel

''' <summary>
''' Base form Class that can be used to Load/Save model Objects.
''' It is using reflection to get/set properties from a model object
''' </summary>
''' <remarks></remarks>
Public Class frmBaseModelObjectEdit
    Implements IChangeTrackable

    Private _lastLoadedvalues As String

    Private Function getBindableControls(contCtrl As Control) _
                            As IEnumerable(Of ICGBaseControl)

        Dim ret As New List(Of ICGBaseControl)

        For Each c As Control In contCtrl.Controls

            If TypeOf c Is ICGBaseControl AndAlso _
                String.IsNullOrEmpty(CType(c, ICGBaseControl).DataPropertyName) = False Then

                ret.Add(CType(c, ICGBaseControl))

            End If

            ret.AddRange(getBindableControls(CType(c, Control)))
        Next

        Return ret

    End Function

    Private _bindableCotrols As List(Of ICGBaseControl)

    <Browsable(False)> _
    Public ReadOnly Property BindableControls As IList(Of ICGBaseControl)
        Get
            If _bindableCotrols Is Nothing Then
                _bindableCotrols = New List(Of ICGBaseControl)
                _bindableCotrols.AddRange(getBindableControls(Me))
            End If

            Return _bindableCotrols
        End Get
    End Property


    ''' <summary>
    ''' A type that implements IModelObject interface
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ModelObjectType As Type

    Private _ModelObjectInstance As IModelObject

    <Browsable(False)> _
    Public Property ModelObjectInstance As IModelObject
        Get
            If Me.DesignMode = False Then

                If _ModelObjectInstance Is Nothing Then

                    common.Validate.isNotNull(ModelObjectType, "ModelObjectType not given")
                    Dim loader As DBMapper = ModelContext.GetModelDefaultMapper(ModelObjectType)
                    If Me.NewRecord() Then
                        _ModelObjectInstance = loader.getModelInstance

                    Else
                        _ModelObjectInstance = loader.findByKey(Me.IdValue)
                    End If

                End If
            End If

            Return _ModelObjectInstance
        End Get
        Set(value As IModelObject)
            _ModelObjectInstance = value
        End Set
    End Property


    ''' <summary>
    ''' LoadData 
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub LoadData()

        For Each c As ICGBaseControl In Me.BindableControls

            c.Value = Me.ModelObjectInstance.getAttribute(c.DataPropertyName)

        Next


    End Sub

    ''' <summary>
    ''' SaveData 
    ''' </summary>
    Public Overrides Function SaveData() As enumSaveDataResult

        If Me.ValidateChildren() Then
            For Each c As ICGBaseControl In Me.BindableControls
                Me.ModelObjectInstance.setAttribute(c.DataPropertyName, c.Value)
            Next
            ModelContext.Current.saveModelObject(Me.ModelObjectInstance)
            Call Me.resetLastLoadedValues()
            Return enumSaveDataResult.SAVE_SUCESS_AND_CLOSE
        Else
            Return enumSaveDataResult.SAVE_FAIL
        End If

    End Function

    Public Overrides Sub DeleteData()

        ModelContext.Current.deleteModelObject(Me.ModelObjectInstance)

    End Sub


    Private Sub frmBaseModelObjectEdit_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Call Me.LoadData()
        Call Me.resetLastLoadedValues()

    End Sub

    Public Function getConcatenatedControlValues() As String Implements IChangeTrackable.getConcatenatedControlValues

        Return UcBaseEditControl.getConcatenatedControlValues(Me)

    End Function

    Public Function hasChanges() As Boolean Implements IChangeTrackable.hasChanges
        Return Not (Me._lastLoadedvalues.Equals(Me.getConcatenatedControlValues()))
    End Function

    Public Sub resetLastLoadedValues() Implements IChangeTrackable.resetLastLoadedValues
        Me._lastLoadedvalues = Me.getConcatenatedControlValues()
    End Sub

    ''' <summary>
    ''' Examines whether data has changed on the screen sice we loaded the data in the controls
    ''' </summary>
    Public Overrides Function dataChanged() As Boolean

        Return Me._lastLoadedvalues <> Me.getConcatenatedControlValues()

    End Function

End Class
