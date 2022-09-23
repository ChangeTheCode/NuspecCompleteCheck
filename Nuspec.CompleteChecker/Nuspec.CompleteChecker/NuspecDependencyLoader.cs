using System.Collections.Generic;
using System.Linq;
using Nuspec.CompleteChecker.Model;
using U8Xml;

namespace Nuspec.CompleteChecker
{
    public class NuspecDependencyLoader
    {
        public IReadOnlyCollection<Package> LoadAllPackagesDependencies(string filePath)
        {
            var nuspecPackageReferences = new List<Package>();

            using (XmlObject xml = XmlParser.ParseFile(filePath))
            {
                XmlNode root = xml.Root;

                var dependencies = root.Descendants.Where(c => c.Name == "dependency").Select(c =>
                new Package(c.Attributes.FirstOrDefault(element => element.Name == "id").Value.Value.ToString().Trim(),
                     c.Attributes.FirstOrDefault(element => element.Name == "version").Value.Value.ToString().Trim()))
                .ToArray();

                nuspecPackageReferences.AddRange(dependencies);
            }
            return nuspecPackageReferences;
        }
    }
}
