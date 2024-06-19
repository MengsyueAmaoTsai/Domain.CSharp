var solution = "./RichillCapital.Domain.sln";
var project = "./RichillCapital.Domain.csproj";
var buildConfiguration = Argument("configuration", "Release");
var artifactsDirectory = "./artifacts";

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(artifactsDirectory);
        DotNetClean(solution);
    });

Task("Restore")
    .Does(() =>
    {
        DotNetRestore(solution);
    });

Task("Build")
    .Does(() =>
    {
        DotNetBuild(
            solution,
            new DotNetBuildSettings
            {
                Configuration = buildConfiguration,
                NoRestore = true,
            });
    });


Task("UnitTests")
    .Does(() =>
    {
        DotNetTest(
            "./Tests/RichillCapital.Domain.UnitTests",
            new DotNetTestSettings
            {
                Configuration = buildConfiguration,
                NoBuild = true,
                NoRestore = true,
            });
    });

Task("Pack")
    .Does(() =>
    {
        DotNetPack(project, new DotNetPackSettings
        {
            Configuration = buildConfiguration,
            NoBuild = true,
            NoRestore = true,
            OutputDirectory = artifactsDirectory,
        });
    });

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("UnitTests")
    .IsDependentOn("Pack");

var target = Argument("target", "Default");
RunTarget(target);