﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

//<comments>
//************************************************************
// Template: ModelBaseExtender2.csharp.txt
// Class autogenerated on 12-May-15 11:20:47 AM by ModelGenerator
// Extends base model object class
// *** FELL FREE to change code in this class.  
//     It will NOT be re-generated and 
//     overwritten by the code generator ****
// 
//************************************************************
//</comments>

namespace OracleModel {

	public class TrainingCourseFactory {

		//Shared function to create a new instance of the class and return it.
		//you can create other shared functions to return a new 
		//instance with parameters
		public static TrainingCourse Create() {
			return new TrainingCourse();
		}

	}

	[Serializable()]
	public class TrainingCourse : TrainingCourseBase, ITrainingCourse {

		#region "Constructor"
		
		/// <summary>
		/// We want people to use the factory, 
		/// so the constructor has assembly level access
		/// </summary>
		internal TrainingCourse() {
			//Empty constructor.
		}

		#endregion

		#region "Overrides"

		#endregion

		#region "Before Save,After Save and Validate Overriden Methods "

		#endregion

		#region "Shadowed and Other Methods "

		#endregion

		#region "Methods "

		#endregion

	}

}

