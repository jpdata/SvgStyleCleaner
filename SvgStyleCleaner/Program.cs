using ExCSS;
using System.Xml.Linq;

if (args.Length != 2)
{
    Console.WriteLine("Usage: SvgStyleCleaner <input.svg> <output.svg>");
    return;
}

string inputFile = args[0];
string outputFile = args[1];

try
{
    // Load the SVG file
    XDocument svgDocument = XDocument.Load(inputFile);

    // Find the <style> element and extract the CSS
    XElement? styleElement = svgDocument.Descendants().FirstOrDefault(static e => e.Name.LocalName == "style");
    XElement? defsElement = svgDocument.Descendants().FirstOrDefault(static e => e.Name.LocalName == "defs");

    if (styleElement != null)
    {
        string cssContent = styleElement.Value;

        var parser = new StylesheetParser();
        var stylesheet = parser.Parse(cssContent);

        // Remove the <style> element
        styleElement.Remove();
        defsElement?.Remove();

        // Iterate over each SVG element and apply the corresponding styles
        foreach (XElement element in svgDocument.Descendants())
        {
            var classAttribute = element.Attribute("class")?.Value;
            if (!string.IsNullOrEmpty(classAttribute))
            {
                var styles = stylesheet.StyleRules.OfType<StyleRule>().Where(r => r.SelectorText.Contains( $".{classAttribute}")).ToList();
                foreach (var style in styles ?? [])
                {
                    if (style != null)
                        ApplyStylesToElement(element, style);
                }
            }
        }
    }

    // Save the modified SVG to the output file
    svgDocument.Save(outputFile);
    Console.WriteLine($"SVG file saved to: {outputFile}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}


// Apply the parsed styles to an SVG element
static void ApplyStylesToElement(XElement element, StyleRule style)
{
    if (!string.IsNullOrEmpty(style.Style.Fill))
    {
        element.SetAttributeValue("fill", style.Style.Fill);
    }

    if (!string.IsNullOrEmpty(style.Style.Stroke))
    {
        element.SetAttributeValue("stroke", style.Style.Stroke);
    }

    if (!string.IsNullOrEmpty(style.Style.Opacity))
    {
        element.SetAttributeValue("opacity", style.Style.Opacity);
    }

}