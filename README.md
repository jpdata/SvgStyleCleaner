<img src="https://www.w3.org/Graphics/SVG/svglogo.svg" width="200" height="100"> SvgStyleCleaner
============



> Removes ```<defs />``` and ```<style/>``` tags elements and its content from SVG files wich can cause rendering issues in Flutter
> Most styles are inlide int the SVG elements, so this tool is useful to remove unused styles and reduce file size

[![License](https://img.shields.io/:license-MIT-blue.svg)](https://opensource.org/licenses/mit-license.php)


## Usage
<!-- #content -->

```pwsh
.\SvgStyleCleaner.exe "C:\path\to\input.svg" "C:\path\to\output.svg"
```