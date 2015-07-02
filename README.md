
<h1>CodeGen ORM</h1>
<h5>Yes, this is yet another ORM.  When to use it?
<ol >
<li>When you need COMPLETE control as to what tables and what table relationships are mapped to objects</li>
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
<h3>Configure "Tools" in Visual Studio:</h3>
To run the class generator when you click on an xml generator file, you can configure the Tools option in Visual Studio:
<ol>
<li> Download and copy files <a href="https://github.com/christoforosl/codegen-lib/releases/download/v4.0.1/codeGenWin4.exe">codeGen.win.exe</a>  and <a href="https://github.com/christoforosl/codegen-lib/releases/download/v4.0.1/org.codegen.lib.4.0.dll">org.codegen.lib.4.0.dll</a> to a folder
<li>Click Tools/External Tools..., and then click Add
<li>Specify the following: 
<ul><li>Title: Generator
<li>Command: [path you specify in step 1]\CodeGenWin4.exe
<li>Arguments $(ItemPath)
</ul>

</ol>

</p>
