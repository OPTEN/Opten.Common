#tool "nuget:?package=NUnit.ConsoleRunner"
#tool "nuget:?package=NUnit.Extension.NUnitV2ResultWriter"
#tool "docfx.msbuild"
#addin "Cake.DocFx"
#addin "Cake.FileHelpers"
#addin "nuget:http://nuget.oss-concept.ch/nuget/?package=Opten.Cake"

var target = Argument("target", "Default");

string feedUrl = "https://www.nuget.org/api/v2/package";
string version = null;

var dest = Directory("./artifacts");
var docs = Directory(@"D:\inetpub\wwwroot\docs\apidocs");

// Cleanup

Task("Clean")
	.Does(() =>
{
	if (DirectoryExists(dest))
	{
		CleanDirectory(dest);
		DeleteDirectory(dest, recursive: true);
	}
});

// Versioning

Task("Version")
	.IsDependentOn("Clean") 
	.Does(() =>
{
	if (DirectoryExists(dest) == false)
	{
		CreateDirectory(dest);
	}

	version = "1.1.6.3"; //GetNextNuGetVersion("Opten.Common", feedUrl);

	PatchAssemblyInfo("../src/Opten.Common/Properties/AssemblyInfo.cs", version);
	FileWriteText(dest + File("Opten.Common.variables.txt"), "version=" + version);
});

// Building

Task("Restore-NuGet-Packages")
	.IsDependentOn("Version") 
	.Does(() =>
{ 
	NuGetRestore("../Opten.Common.sln", new NuGetRestoreSettings {
		NoCache = true
	});
});

Task("Build") 
	.IsDependentOn("Restore-NuGet-Packages") 
	.Does(() =>
{
	MSBuild("../src/Opten.Common/Opten.Common.csproj", settings =>
		settings.SetConfiguration("Debug"));

	MSBuild("../src/Opten.Common/Opten.Common.csproj", settings =>
		settings.SetConfiguration("Release"));

	MSBuild("../tests/Opten.Common.Test/Opten.Common.Test.csproj", settings =>
		settings.SetConfiguration("Release"));
});

Task("Run-Unit-Tests")
	.IsDependentOn("Build")
	.Does(() =>
{
	var results = dest + Directory("tests");

	if (DirectoryExists(results) == false)
	{
		CreateDirectory(results);
	}

	//TODO: Why not csproj?
	NUnit3("../tests/Opten.Common.Test/bin/Release/Opten.Common.Test.dll", new NUnit3Settings {
		Results = results + File("Opten.Common.Test.xml"),
		Configuration = "Release",
		ResultFormat = "nunit2" // Wait until Bamboo 5.14 is out to support NUnit 3!
	});
});

Task("Pack")
	.IsDependentOn("Run-Unit-Tests")
	.Does(() =>
{
	NuGetPackWithDependencies("./Opten.Common.nuspec", new NuGetPackSettings {
		Version = version,
		BasePath = "../",
		OutputDirectory = dest
	}, feedUrl);
});

// Others

Task("Docs")
	.IsDependentOn("Pack")
	.Does(() =>
{
	if (DirectoryExists(docs))
	{
		CleanDirectory(docs + Directory("_site"));
		DeleteDirectory(docs + Directory("_site"), recursive: true);

		DocFx(docs + File("docfx.json"));
	}
});

// Deploying

Task("Deploy")
	.Does(() =>
{
	// This is from the Bamboo's Script Environment variables
	string packageId = "Opten.Common";

	// Get the Version from the .txt file
	string version = EnvironmentVariable("bamboo_inject_" + packageId.Replace(".", "_") + "_version");

	if (string.IsNullOrWhiteSpace(version))
	{
		throw new Exception("Version is missing for " + packageId + ".");
	}

	// Get the path to the package
	var package = File(packageId + "." + version + ".nupkg");
            
	// Push the package
	NuGetPush(package, new NuGetPushSettings {
		Source = feedUrl,
		ApiKey = EnvironmentVariable("NUGET_API_KEY")
	});

	// Notifications
	Slack(new SlackSettings {
		ProjectName = "Opten.Common"
	});
});

Task("Default")
	.IsDependentOn("Docs");

RunTarget(target);