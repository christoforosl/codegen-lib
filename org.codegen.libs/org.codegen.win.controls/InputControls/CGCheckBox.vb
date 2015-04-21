Imports System.ComponentModel

Public Class CGCheckBox
    Inherits CheckBox
    Implements ICGBaseControl

    Private _ReadOnly As Boolean
    Private _isMandatory As Boolean
    Private _AssociatedLabel As Windows.Forms.Label

    Public Property MaxValue As String Implements ICGBaseControl.MaxValue
    Public Property MinValue As String Implements ICGBaseControl.MinValue
    Public Property ErrProvider As ErrorProvider Implements ICGBaseControl.ErrProvider

    ''' <summary>
    ''' The name of the field that corresponds to the name of the ModelObject property
    ''' </summary>
    ''' <remarks></remarks>
    Private _DataPropertyName As String

    ''' <summary>
    ''' Gets/Sets the DataPropertyName
    ''' </summary>
    Public Property DataPropertyName As String Implements ICGBaseControl.DataPropertyName
        Get
            If String.IsNullOrEmpty(_DataPropertyName) Then
                Return Me.Name
            End If
            Return _DataPropertyName
        End Get
        Set(value As String)
            _DataPropertyName = value
        End Set
    End Property

    Public Property AssociatedLabel As Label Implements ICGBaseControl.AssociatedLabel
        Get
            Return _AssociatedLabel
        End Get
        Set(ByVal value As Label)
            _AssociatedLabel = value
            If _AssociatedLabel IsNot Nothing Then
                _AssociatedLabel.Font = FormsApplicationContext.current.ApplicationDefaultFont

                
            End If

        End Set
    End Property

    Public Property [ReadOnly] As Boolean Implements ICGBaseControl.ReadOnly
        Get
            Return _ReadOnly
        End Get
        Set(ByVal value As Boolean)
            _ReadOnly = value
            Me.Enabled = Not value
        End Set
    End Property

    Public Property isMandatory() As Boolean Implements ICGBaseControl.isMandatory
        Get
            Return _isMandatory
        End Get
        Set(ByVal value As Boolean)

            _isMandatory = False ' for checkboxes, no need to mark them as mandatory
        End Set

    End Property

    Public Property intValue() As Integer
        Get
            If Me.Checked Then
                Return 1
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            Me.Checked = value <> 0
        End Set
    End Property

    Public Overrides Property Text As String
        Get
            If Me.Checked Then
                Return "true"
            Else
                Return "false"
            End If
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
        End Set
    End Property

    Public Property value() As Object Implements ICGBaseControl.Value
        Get
            Return Me.Checked
        End Get
        Set(ByVal value As Object)
            Me.Checked = CBool(value)
        End Set
    End Property

    Public Sub addError(ByVal errMesg As String) Implements ICGBaseControl.addError
        If Me.ErrProvider IsNot Nothing Then
            Me.ErrProvider.SetError(Me, errMesg)
        End If
    End Sub

    Public Function Label() As String Implements ICGBaseControl.Label
        If Me.AssociatedLabel IsNot Nothing Then
            Return Me.AssociatedLabel.Text
        Else
            Return String.Empty
        End If
    End Function


    Public Sub makeReadOnly() Implements IReadOnlyEnabled.setReadOnly
        Me.ReadOnly = True
    End Sub


    Private Sub CGCheckBox_ParentChanged(sender As Object, e As System.EventArgs) Handles Me.ParentChanged

        If _AssociatedLabel IsNot Nothing AndAlso _
                Me.isMandatory AndAlso _
                FormsApplicationContext.current.MarkMandatoryFieldsWithAsterisk Then

            AddHandler _AssociatedLabel.TextChanged, AddressOf CGTextBox.addAsteriskToLabel
            Call CGTextBox.addAsteriskToLabel(_AssociatedLabel, New EventArgs)
            _AssociatedLabel.ForeColor = FormsApplicationContext.current.requiredLabelsColor()
        End If

    End Sub
End Class
