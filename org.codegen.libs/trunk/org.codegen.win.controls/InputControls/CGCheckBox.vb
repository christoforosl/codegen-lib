Imports System.ComponentModel

Public Class CGCheckBox
    Inherits CheckBox
    Implements ICGBaseControl

    Private _ReadOnly As Boolean
    Private _isMandatory As Boolean
    Private _AssociatedLabel As Windows.Forms.Label

    Public Property MaxValue As Object Implements ICGBaseControl.MaxValue
    Public Property MinValue As Object Implements ICGBaseControl.MinValue
    Public Property ErrProvider As ErrorProvider Implements ICGBaseControl.ErrProvider

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
            'If value Then
            '    Me.BackColor = System.Drawing.Color.LightYellow
            'Else
            '    Me.BackColor = System.Drawing.Color.Transparent
            'End If
            _isMandatory = value
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

    Private Sub _ParentChanged(ByVal sender As Object, ByVal e As System.EventArgs)


        If Not Me.DesignMode AndAlso Me.isMandatory AndAlso Me.showAsteriskForMandatory Then

            CGTextBox.addAsteriskLabel(CType(Me, Control), CType(Me.Parent, Control))

        End If

    End Sub

    Public Sub makeReadOnly() Implements IReadOnlyEnabled.setReadOnly
        Me.ReadOnly = True
    End Sub

    Public Property showAsteriskForMandatory As Boolean = True _
        Implements ICGBaseControl.showAsteriskForMandatory
       
End Class
