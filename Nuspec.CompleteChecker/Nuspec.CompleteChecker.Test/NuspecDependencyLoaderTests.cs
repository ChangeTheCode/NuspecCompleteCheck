using FluentAssertions;
using Nuspec.CompleteChecker.Model;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Nuspec.CompleteChecker.Test
{
    public class NuspecDependencyLoaderTest
    {
        [Fact]
        public void LoadDependenciesOfNuspec_Happy()
        {
            var loader = new NuspecDependencyLoader();

            var expectedPackages = new List<Package>
            {
                new Package("Newtonsoft.Json","11.0.2"),
                new Package("RestSharp", "106.3.1"),
                new Package("Selenium.Support", "3.14.0"),
                new Package("Selenium.WebDriver", "3.14.0"),
            };

            var allNuspecDependencies = loader.LoadAllPackagesDependencies(Path.Combine(Directory.GetCurrentDirectory(), "TestFiles\\QuickNuspecExample.nuspec"));

            allNuspecDependencies.Should().HaveCount(4);
            allNuspecDependencies.Should().BeEquivalentTo(expectedPackages);
        }


        [Fact]
        public void LoadDependenciesOfNuspec_WrongDependenciesExpected_ShouldFail()
        {
            var loader = new NuspecDependencyLoader();

            var expectedPackages = new List<Package>
            {
                new Package("Newtonsoft.Json","11.0.2"),
                new Package("RestSharp", "106.3.1"),
                new Package("Selenium.Support", "3.14.0"),
                new Package("Selenium.WebDriver", "3.14.0"),
                new Package("Selenium.Web", "3.14.0"),
            };

            var allNuspecDependencies = loader.LoadAllPackagesDependencies(Path.Combine(Directory.GetCurrentDirectory(), "TestFiles\\QuickNuspecExample.nuspec"));

            allNuspecDependencies.Should().HaveCount(4);
            allNuspecDependencies.Should().NotBeEquivalentTo(expectedPackages);
        }

        [Fact]
        public void LoadDependenciesOfNuspec_WrongVersionExpected_ShouldFail()
        {
            var loader = new NuspecDependencyLoader();

            var expectedPackages = new List<Package>
            {
                new Package("Newtonsoft.Json","11.0.2"),
                new Package("RestSharp", "106.3.1"),
                new Package("Selenium.Support", "3.14.0"),
                new Package("Selenium.WebDriver", "3.19.0"),
            };

            var allNuspecDependencies = loader.LoadAllPackagesDependencies(Path.Combine(Directory.GetCurrentDirectory(), "TestFiles\\QuickNuspecExample.nuspec"));

            allNuspecDependencies.Should().HaveCount(4);
            allNuspecDependencies.Should().NotBeEquivalentTo(expectedPackages);
        }
    }
}