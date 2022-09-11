using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nuspec.CompleteChecker.Model;
using U8Xml;

namespace Nuspec.CompleteChecker
{
    internal class CsprojDependencyLoader
    {
        public static IReadOnlyCollection<string> DirSearch(string rootDirectory)
        {
            var allfiles = new List<string>();
            foreach (string d in Directory.GetDirectories(rootDirectory))
            {
                foreach (string f in Directory.GetFiles(d, "*.csproj").Where(c => !c.Contains("Test")))
                {
                    allfiles.Add(f);
                }
                DirSearch(d);
            }

            return allfiles;
        }

        public IReadOnlyCollection<Package> LoadAllPackagesDependencies(string filePath)
        {
            var allCsProjPackageReferences = new List<Package>();

            using (XmlObject xml = XmlParser.ParseFile(filePath))
            {
                XmlNode root = xml.Root;

                var packageReferences = root.Descendants.Where(c => c.Name == "PackageReference").Select(c =>
                new Package(
                    c.Attributes.FirstOrDefault(element => element.Name == "Include").Value.Value.ToString().Trim(),
                    c.Attributes.FirstOrDefault(element => element.Name == "Version").Value.Value.ToString().Trim()))
                .ToArray();

                allCsProjPackageReferences.AddRange(packageReferences);
            }
            return allCsProjPackageReferences;
        }
    }
}
