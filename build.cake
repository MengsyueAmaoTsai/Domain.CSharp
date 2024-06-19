var solution = "./RichillCapital.Domain.sln";
var project = "./RichillCapital.Domain.csproj";
var buildConfiguration = Argument("configuration", "Release");

Task("Clean")
    .Does(() =>
    {
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
    });

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("UnitTests")
    .IsDependentOn("Pack");

var target = Argument("target", "Default");
RunTarget(target);