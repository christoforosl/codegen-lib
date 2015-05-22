Imports System.ComponentModel

''' <summary>
''' Public class inherited by all edit controls
''' </summary>
''' <remarks></remarks>
Public Class UcBaseEditControl
    Implements IUcEditControl, IReadOnlyEnabled

	''' <summary>
	''' Marks all base cgl base controls on the form as ReadOnly
	''' </summary>
	''' <remarks></remarks>
    Public Sub setReadOnly() Implements IReadOnlyEnabled.setReadOnly

        Call setReadOnly(Me)

    End Sub

    Private Sub setReadOnly(ByVal cparent As Control)

        For Each c As Control In cparent.Controls
            If TypeOf c Is IReadOnlyEnabled Then
                CType(c, IReadOnlyEnabled).setReadOnly()
            End If
            Me.setReadOnly(c)
        Next

    End Sub

    ''' <summary>
    ''' Loops thru all controls and assigns the error provider
    ''' </summary>
    ''' <param name="cparent"></param>
    ''' <remarks></remarks>
    Friend Sub assignErrProvider(ByVal cparent As Control)

        For Each c As Control In cparent.Controls
            If TypeOf c Is ICGBaseControl Then
                CType(c, ICGBaseControl).ErrProvider = Me.ErrProvider
            Else
                Me.assignErrProvider(c)
            End If
        Next
    End Sub

#Region "Fields"

    Private _initialized As Boolean = False
    Private _lastLoadedvalues As String = String.Empty

#End Region

#Region "Properties"

    Public Property isInitialized() As Boolean
        Get
            Return _initialized
        End Get
        Set(ByVal value As Boolean)
            _initialized = value
        End Set
    End Property

    <Browsable(False)> _
    Public Property ModelObject() As IModelObject Implements IUcEditControl.ModelObject

#End Region

#Region "Events"

    Public Event InitializeControl()
    Public Event evLoadToObject()
    Public Event evLoadObjectData()

#End Region

#Region "Methods"



    ''' <summary>
    ''' resets the _lastLoadedvalues variable to the concatenated values
    ''' </summary>
    Public Sub resetLastLoadedValues() Implements IUcEditControl.resetLastLoadedValues
        Me._lastLoadedvalues = Me.getConcatenatedControlValues()
    End Sub


    ''' <summary>
    ''' Fills the controls on the screen from data in the object
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadData() Implements IUcEditControl.loadData

        RaiseEvent InitializeControl()
        RaiseEvent evLoadObjectData()

        Me.resetLastLoadedValues()
        Me.ErrProvider.Clear()

    End Sub


    ''' <summary>
    ''' Returns all the controls' values concatenated
    ''' </summary>
    Public Function getConcatenatedControlValues() As String Implements IUcEditControl.getConcatenatedControlValues

        Return getConcatenatedControlValues(Me)

    End Function


    ''' <summary>
    ''' Loads the object from the database and then sets the proeperties 
    ''' of the object from values on the controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadToObject() Implements IUcEditControl.loadToObject
        RaiseEvent evLoadToObject()
    End Sub

    ''' <summary>
    ''' Checks if the data on screen are different than the original loaded 
    ''' data, stored in  _lastLoadedvalues when we load the object on the screen
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function hasChanges() As Boolean _
       Implements IUcEditControl.hasChanges

        Return Not (Me._lastLoadedvalues.Equals(Me.getConcatenatedControlValues()))

    End Function


#End Region

	Private Sub UcBaseEditControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Me.assignErrProvider(Me)
	End Sub



    Public Shared Function getConcatenatedControlValues(ByVal cparent As Control) As String
        Dim ret As String = String.Empty

        For Each c As Control In cparent.Controls
            If TypeOf c Is ICGBaseControl Then
                Dim lCValue As Object = CType(c, ICGBaseControl).Value
                If lCValue IsNot Nothing Then
                    ret = ret & lCValue.ToString
                End If

            Else
                ret = ret & getConcatenatedControlValues(c)
            End If
        Next
        Return ret
    End Function
End Class
