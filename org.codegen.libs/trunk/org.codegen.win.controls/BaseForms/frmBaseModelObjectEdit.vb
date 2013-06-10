Imports System.Collections.Generic
Imports System.ComponentModel
Imports org.codegen.common.TranslationServices

''' <summary>
''' Base form Class that can be used to Load/Save model Objects.
''' It is using reflection to get/set properties from a model object
''' </summary>
''' <remarks></remarks>
Public Class frmBaseModelObjectEdit

    Protected Const LABEL_KEY As String = "_label"

    

    Private Function getBindableControls(contCtrl As ContainerControl) _
                            As IEnumerable(Of ICGBaseControl)

        Dim ret As New List(Of ICGBaseControl)

        For Each c As Control In contCtrl.Controls

            If TypeOf c Is ICGBaseControl AndAlso _
                String.IsNullOrEmpty(CType(c, ICGBaseControl).DataPropertyName) = False Then

                ret.Add(CType(c, ICGBaseControl))

            End If

            If TypeOf c Is ContainerControl Then
                ret.AddRange(getBindableControls(CType(c, ContainerControl)))
            End If

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
                    Dim loader As DBMapper = ModelContext.GetModelDefaultMapper(ModelObjectType)
                    If Me.IdValue > 0 Then
                        _ModelObjectInstance = loader.findByKey(Me.IdValue)
                    Else
                        _ModelObjectInstance = loader.getModelInstance
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

    End Sub
End Class
