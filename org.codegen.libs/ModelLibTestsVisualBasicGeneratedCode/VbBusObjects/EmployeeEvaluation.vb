﻿Option Strict On
Imports System.Runtime.InteropServices
Imports System.Data.Linq.Mapping

'<comments>
'************************************************************
' Template: ModelBaseExtender2.visualBasic.txt
' Class autogenerated on 14/06/2015 8:10:32 AM by ModelGenerator
' Extends base model object class
' *** FELL FREE to change code in this class.  
'     It will NOT be re-generated and 
'     overwritten by the code generator ****
' 
'************************************************************
'</comments>

Namespace VbBusObjects
	<ComVisible(False)> _
	Public Class EmployeeEvaluationFactory
		
		'Shared function to create a new instance of the class and return it.
		'you can create other shared functions to return a new 
		'instance with parameters
		Public Shared Function Create() As EmployeeEvaluation
            Return New EmployeeEvaluation()
        End Function
    
	End Class

	Public partial class EmployeeEvaluation
        
#Region "Custom Properties and Other Methods "

#End Region

	End Class

End Namespace
