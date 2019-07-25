Imports org.codegen.common.ProgressIndicator

Namespace BackroundProgressIndicator

    ''' <summary>
    ''' Encapsulates a progressable process
    ''' To use:
    ''' 1. Define a class and implement IProgressable interface
    ''' 2. Write a method in the class that does a loop and raise event processGoing
    ''' 3. In method "startProcess", start your backround process.
    ''' 
    ''' 4. Instantiate ProgressIndicator and pass the class
    ''' 5. Call ProgressIndicator.startProcess
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ProgressIndicator
        Implements IDisposable

        Private _frmProgress As frmProgress2

        Property progressable As IProgressable
        Public Property showCancel As Boolean
        Public Property defaultMessage As String = WinControlsLocalizer.getString("progress.pleasewait")

        Public Sub New(prg As IProgressable)

            Me.progressable = prg

        End Sub

        Private Delegate Sub SetTextCallback(ByVal [text] As String)
        Private Delegate Sub CloseCallback()

        Private Sub SetText(ByVal x As String)
            Me.frmProgress.Text = x
        End Sub

        Private Sub CloseForm()
            Me.frmProgress.Close()
        End Sub

        Public Sub startProcess()

            Me.frmProgress.Text = Me.progressWindowTitle
            Me.frmProgress.Refresh()
            Me.frmProgress.setShowCancelButton(Me.showCancel)
            Me.frmProgress.setMessageLabelText(String.Empty)
            Me.frmProgress.TopMost = True
            Me.frmProgress.Show()
            Me.frmProgress.Activate()
            Me.frmProgress.BringToFront()
            Me.frmProgress.Refresh()

            AddHandler Me.progressable.processStart, AddressOf processStart
            AddHandler Me.progressable.processGoing, AddressOf reportProgress
            AddHandler Me.progressable.processFinished, AddressOf processFinished

            Try
                Me.progressable.startProcess()
            Catch ex As Exception
                Me.processFinished(Me, New ErrorProgressableEventArgs(ex))
            End Try

        End Sub

        Private Sub processStart(sender As Object, e As ProgressableEventArgs)
            Me.frmProgress.resetProgress(e.progressMessage)
        End Sub

        Private Sub processFinished(sender As Object, e As ProgressableEventArgs)
            If e.inError Then
                Call winUtils.MsgboxInfo(e.progressMessage)
            End If
            Me.frmProgress.Close()
        End Sub

        Private Sub reportProgress(sender As Object, e As ProgressableEventArgs)

            Me.frmProgress.ReportProgress(
                    CInt(e.currentStep),
                    CStr(IIf(String.IsNullOrEmpty(e.progressMessage),
                             Me.defaultMessage, e.progressMessage)))

        End Sub

        Private ReadOnly Property frmProgress As frmProgress2
            Get
                If _frmProgress Is Nothing Then

                    If Application.OpenForms(BackroundProgressIndicator.STR_FORM_PROGRESS) Is Nothing Then
                        _frmProgress = New frmProgress2
                    Else
                        _frmProgress = CType(Application.OpenForms(BackroundProgressIndicator.STR_FORM_PROGRESS),
                                    controls.BackroundProgressIndicator.frmProgress2)
                    End If

                End If
                Return _frmProgress

            End Get
        End Property

        Public Property progressWindowTitle() As String
            Get
                Return Me.frmProgress.Text
            End Get
            Set(ByVal value As String)

                ' InvokeRequired required compares the thread ID of the 
                ' calling thread to the thread ID of the creating thread. 
                ' If these threads are different, it returns true. 
                If Me.frmProgress.InvokeRequired Then
                    Dim d As New SetTextCallback(AddressOf SetText)
                    Me.frmProgress.Invoke(d, New Object() {value})
                Else
                    Me.frmProgress.Text = value
                End If
            End Set
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    If Me._frmProgress IsNot Nothing Then
                        _frmProgress.Dispose()
                    End If
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

        
    End Class

End Namespace