using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.model.lib.Model;
using org.model.lib;
using System.Threading;
using System.Globalization;
using org.codegen.lib;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using NUnit.Core;
using System.Collections;


namespace GeneratorTests {

	[TestClass]
	public class GenAndRunObjectTests {

        [TestMethod]
        public void runObjectTests() {
            
            DirectoryInfo d = new DirectoryInfo("..\\..\\..\\");

            string path = d.FullName + "VbObjectTestsTmp.cs";
			if (File.Exists(path)) {
				File.Delete(path);
			}
            

            File.Copy( d.FullName + "CSharpObjectTests.cs",  path) ;
            string readText = File.ReadAllText(path);
            //using ModelLibVBGenCode.VbBusObjects;
            //using ModelLibVBGenCode.VbBusObjects.DBMappers;
            readText = readText.Replace("using CsModelObjects;", "using ModelLibVBGenCode.VbBusObjects;");
            readText = readText.Replace("using CsModelMappers;", "using ModelLibVBGenCode.VbBusObjects.DBMappers;");

			readText = readText.Replace("namespace GeneratorTests {", "namespace GeneratorTests.VB {");
			readText = readText.Replace("public class CSharpObjectTests {", "public class VBObjectTests {");
			readText = readText.Replace("public void createCsRecords()", "public void createVbRecords()");
			readText = readText.Replace("[TestClass]", "[NUnit.Framework.TestFixture]");
			readText = readText.Replace("[TestMethod]", "[NUnit.Framework.Test]");
			readText = readText.Replace("using Microsoft.VisualStudio.TestTools.UnitTesting;", "using NUnit.Framework;");
			readText = readText.Replace("Assert.", "NUnit.Framework.Assert.");
			readText = readText.Replace("public static void MyClassInitialize(TestContext testContext)", "public static void MyClassInitialize()");
			readText = readText.Replace("[ClassInitialize()]", "[NUnit.Framework.SetUp]");
			readText = readText.Replace("[ClassCleanup()]", "[NUnit.Framework.TearDown]");	
		
            File.WriteAllText(path, readText);

			CSharpCodeProvider provider = new CSharpCodeProvider();
			CompilerParameters cp = new CompilerParameters();

			// this line is here otherwise ModelLibVBGenCode is not returned 
			// in call to this.GetType().Assembly.GetReferencedAssemblies
			ModelLibVBGenCode.VbBusObjects.Employee e =  null;
			NUnit.Framework.Guard x=null; // note: DO NOT REMOVE!

			var assemblies = this.GetType().Assembly.GetReferencedAssemblies().ToList();
			var assemblyLocations =  
						assemblies.Select(a => 
						Assembly.ReflectionOnlyLoad(a.FullName).Location);

			var lstAssemblyLocations = assemblyLocations.Where(a => !a.Contains("Microsoft.VisualStudio.QualityTools.UnitTestFramework")).ToList();

			cp.ReferencedAssemblies.AddRange(lstAssemblyLocations.ToArray());
		
			cp.GenerateInMemory = true;// True - memory generation, false - external file generation
			cp.GenerateExecutable = false;// True - exe file generation, false - dll file generation
			CompilerResults results = provider.CompileAssemblyFromSource(cp, readText);
			Assert.AreEqual(0, results.Errors.Count,"There should be no compilation errors");

			TestSuite ts = GetTestSuiteFromAssembly(results.CompiledAssembly);
			//Assert.IsTrue( ts.Tests.Count > 0, "Expected at least one test");

			TestExecutionContext.CurrentContext.TestPackage = new TestPackage(results.CompiledAssembly.GetName().FullName);
			var suite = GetTestSuiteFromAssembly(results.CompiledAssembly);
			suite.Run(new NullListener(), TestFilter.Empty);

        }
		/// <summary>
		/// Converts a given assembly containing tests to a runnable TestSuite
		/// </summary>
		protected static TestSuite GetTestSuiteFromAssembly(Assembly assembly) {
			var treeBuilder = new NamespaceTreeBuilder(
				new TestAssembly(assembly, assembly.GetName().FullName));
			treeBuilder.Add(GetFixtures(assembly));
			return treeBuilder.RootSuite;
		}

		/// <summary>
		/// Creates a tree of fixtures and containing TestCases from the given assembly
		/// </summary>
		protected static List<Type> GetFixtures(Assembly assembly) {
			//return assembly.GetTypes()
			//	.Where(TestFixtureBuilder.CanBuildFrom)
			//	.Select(TestFixtureBuilder.BuildFrom).ToList();

			return assembly.GetTypes()
				.Where(a => a.Name == "VBObjectTests").ToList();
		}


		[TestMethod]
		public void runGenerateCodeTests() {

			DirectoryInfo d = new DirectoryInfo("..\\..\\..\\");
			System.Diagnostics.Debug.WriteLine(d.FullName);

			XMLClassGenerator.GenerateClassesFromFile(d.FullName + "ModelLibCSharpGeneratedCode\\CSharpModelGenerator.xml");
						
			CSharpCodeProvider provider = new CSharpCodeProvider();
			CompilerParameters parameters = new CompilerParameters();
			parameters.ReferencedAssemblies.Add("Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll");
			// True - memory generation, false - external file generation
			parameters.GenerateInMemory = true;
			// True - exe file generation, false - dll file generation
			parameters.GenerateExecutable = true;
			CompilerResults results = provider.CompileAssemblyFromSource(parameters);

			XMLClassGenerator.GenerateClassesFromFile(d.FullName + "ModelLibTestsVisualBasicGeneratedCode\\VisualBasicModelGenerator.xml");

			XMLClassGenerator.GenerateClassesFromFile(d.FullName + "ModelLibCSharpOracleGenCode\\OracleCSharpModelGenerator.xml");

		}
		
	}
}
