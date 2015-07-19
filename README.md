
<h1>CodeGen ORM</h1>
<h5>Yes, this is yet another ORM.  When to use it?
<ol >
<li>When you need COMPLETE control as to what tables and what table relationships are mapped to objects.
On other words, nothing is generated unless you want it to.</li>

<li>When you need COMPLETE control on the inheritance on each mapped object. For example, CodeGen ORM allows a group of mapped objects to inherit from a base class and another group of objects from another class.</li>

<li>When you need COMPLETE control on the mapped orm classes without having to worry about lost code when mapped objects are re-generated after schema change. For example, you need to add custom properties and methods to a mapped object.</li>

<li>When you need <u>Complete Code Generation</u> from database tables. The generator  detects schema changes and regenerates without loss of custom code</li>
</ol>
</h5>


The project is a Visual Studio 10 solution. The solution consits of the following projects:
<ul>

<li>org.codegen.model.lib: A class library project containing base classes and interfaces used by the generated ORM classes.

<li>org.codegen.lib.db: A class library project containing base classes and interfaces used for database access.

<li>4 test projects: 
<table>
<tr><td>GeneratorTests</td><td>Project Units Tests</td></tr>
<tr><td>ModelLibCSharpGeneratedCode</td><td>Units Tests for generated C# classes (MSSQL Database)</td></tr>
<tr><td>ModelLibCSharpOracleGenCode</td><td>Units Tests for generated c# classes for Oracle database</td></tr>
<tr><td>ModelLibTestsVisualBasicGeneratedCode </td><td>Units Tests for generated Visual Basic classes  (MSSQL Database)</td></tr>
</table>
</li>
</ul>

<p>
<h3>Getting the binaries into your project</h3>
Using the Nu Package Manager menu in Visual Studio, search for <b>CodeGen.Model.Library</b>, select it and include it in your projects.  
</p>

<p>
<h3>Configure "Tools" in Visual Studio for Code Generation</h3>
After get the binaries with Nu Package Manager, folder packages\CodeGen.Model.Library.4.x.x.x\tools is created with the orm's Code Generator executable CodeGenWin4.exe.  To run this code generator when you click on an xml generator control file, you can configure the Tools option in Visual Studio.
<ol>
<li>Click Tools/External Tools..., and then click Add</li>
<li>Specify the following: </li>
<ul><li>Title: Generator</li>
<li>Command: [path in step 1]\CodeGenWin4.exe</li>
<li>Arguments $(ItemPath)</li>
</li>
</ul>
</ol>
</p>
