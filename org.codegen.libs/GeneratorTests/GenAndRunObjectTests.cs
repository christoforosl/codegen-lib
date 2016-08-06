using NUnit.Framework;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

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
using org.model.lib.db;


namespace GeneratorTests {

	[NUnit.Framework.TestFixture]
	public class GenAndRunObjectTests {

		[NUnit.Framework.Test]
		public void generateAndRunVBObjectTests() {
			int i = 0;
			DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory());
			while (true) {
				i++;
				if(i>=20){
					throw new ApplicationException ("could not get a dir with name GeneratorTests");
				}
				d = d.Parent;
				if (d.Name == "GeneratorTests") {
					break;
				}

			}


			string path = d.FullName + "\\VbObjectTestsTmp.cs";
			if (File.Exists(path)) {
				File.Delete(path);
			}


			File.Copy(d.FullName + "\\CSharpObjectTests.cs", path);
			string readText = File.ReadAllText(path);

			readText = readText.Replace("using CsModelObjects;", "using ModelLibVBGenCode.VbBusObjects;");
			readText = readText.Replace("using CsModelMappers;", "using ModelLibVBGenCode.VbBusObjects.DBMappers;");
            //readText = readText.Replace("DateTime hireDate=new", "dim hireDate as DateTime=new");

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
			readText = readText.Replace("//NUnit.Framework.NUnit.Framework.Assert.IsTrue(false)", "NUnit.Framework.Assert.IsTrue(false);");
			readText = readText.Replace("//DBUtils.Current().ConnString=",
					"org.model.lib.db.DBUtils.Current().ConnString=\"" + DBUtils.Current().ConnString.Replace("\\", "\\\\") + "\";");

            readText = readText.Replace("EnumProjectType.EXTERNAL", "ModelLibVBGenCode.EnumProjectType.EXTERNAL");
            readText = readText.Replace("void testCSharp", "void testVBNet");

			File.WriteAllText(path, readText);

			CSharpCodeProvider provider = new CSharpCodeProvider();
			CompilerParameters cp = new CompilerParameters();

			// this line is here otherwise ModelLibVBGenCode is not returned 
			// in call to this.GetType().Assembly.GetReferencedAssemblies
			ModelLibVBGenCode.VbBusObjects.Employee e = null;
			//NUnit.Framework.Guard x = null; // note: DO NOT REMOVE!

			var assemblies = this.GetType().Assembly.GetReferencedAssemblies().ToList();
			var assemblyLocations =
						assemblies.Select(a =>
						Assembly.ReflectionOnlyLoad(a.FullName).Location);

			var lstAssemblyLocations = assemblyLocations.Where(a => !a.Contains("Microsoft.VisualStudio.QualityTools.UnitTestFramework")).ToList();
            //Assembly.ReflectionOnlyLoad("")
			cp.ReferencedAssemblies.AddRange(lstAssemblyLocations.ToArray());

			cp.GenerateInMemory = false;// True - memory generation, false - external file generation
			cp.GenerateExecutable = false;// True - exe file generation, false - dll file generation
			CompilerResults results = provider.CompileAssemblyFromSource(cp, readText);
			Assert.AreEqual(0, results.Errors.Count, 
                    "There should be no compilation errors, first error was:" +
                    (results.Errors.Count>0 ? results.Errors[0].ErrorText : ""));

			CoreExtensions.Host.InitializeService();
			TestPackage package = new TestPackage(results.CompiledAssembly.Location);
			RemoteTestRunner remoteTestRunner = new RemoteTestRunner();
			remoteTestRunner.Load(package);
			TestResult result = remoteTestRunner.Run(new NullListener(),
									TestFilter.Empty, false, LoggingThreshold.All);

			Assert.IsTrue(result.HasResults, " must have test results ");
			Assert.IsTrue(result.IsSuccess, "dynamic vb tests must return success ");


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


		[NUnit.Framework.Test]
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
