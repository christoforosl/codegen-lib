
This is an .net ORM project, capable of generating c# or vb plain old objects from database tables. 
The project is a Visual Studio 10 solution. The solution consits of the following projects:
<ul>

<li>org.codegen.lib: A class library project containing classes, interfaces and templates for generating code.</li>

<li>org.codegen.win: A windows forms application that is the UI for generating code.</li>

<li>org.codegen.model.lib: A class library project containing base classes and interfaces used by the generated classes.

<li>org.codegen.lib.db: A class library project containing base classes and interfaces used for database access. Used by the generated classes.

<li>org.codegen.common: A class library project containing common utility classes.

<li>4 test projects: GeneratorTests, ModelLibCSharpGeneratedCode, ModelLibCSharpOracleGenCode, ModelLibTestsVisualBasicGeneratedCode </li>

</ul>

<p>
<h3>Configure "Tools" in Visual Studio:</h3>
<ol>
<li> Download and copy files <a href="https://github.com/christoforosl/codegen-lib/releases/download/v4.0.1/codeGenWin4.exe">codeGen.win.exe</a>  and <a href="https://github.com/christoforosl/codegen-lib/releases/download/v4.0.1/org.codegen.lib.4.0.dll">org.codegen.lib.4.0.dll</a> to a folder
<li>Click Tools/External Tools..., and then click Add
<li>Specify the following: 
<ul><li>Title: Generator
<li>Command: [path you specify in step 1]\odeGenWin4.exe
<li>Arguments $(ItemPath)
</ul>
This will allow you to run the class generator when you click on the xml generator file 
</ol>

</p>
