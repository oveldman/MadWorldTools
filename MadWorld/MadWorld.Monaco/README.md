## MadWorld.Monaco
### How to install MadWorld.Monaco
1. Run the following command in terminal
```bash
# When libman is not installed
dotnet tool install -g Microsoft.Web.LibraryManager.Cli
libman restore
```
2. Add the MadWorld.Monaco project to your blazor project
3. Add the following lines to your index.cshtml
```html
 <link href="_content/MadWorld.Monaco/lib/monaco/min/vs/editor/editor.main.css" rel="stylesheet" />
 <script type="text/javascript" src="_content/MadWorld.Monaco/lib/monaco/min/vs/loader.js"></script>
 <script>require.config({ paths: { 'vs': '_content/MadWorld.Monaco/lib/monaco/min/vs' } });</script>
 <script src="_content/MadWorld.Monaco/lib/monaco/min/vs/editor/editor.main.js"></script>
```
4. Add the following lines to your DI
```c#
builder.Services.AddMonacoEditor();
```
5. Add the following lines to your component
```c#
<MonacoEditor SoftwareLanguage="json" />
```
6. Have fun!