## MadWorld.Monaco
### How to install MadWorld.Monaco

1. Add the MadWorld.Monaco project to your blazor project
2. Add the following lines to your index.cshtml
```html
 <link href="_content/MadWorld.Monaco/lib/monaco/min/vs/editor/editor.main.css" rel="stylesheet" />
 <script type="text/javascript" src="_content/MadWorld.Monaco/lib/monaco/min/vs/loader.js"></script>
 <script>require.config({ paths: { 'vs': '_content/MadWorld.Monaco/lib/monaco/min/vs' } });</script>
 <script src="_content/MadWorld.Monaco/lib/monaco/min/vs/editor/editor.main.js"></script>
```
3. Add the following lines to your DI
```c#
builder.Services.AddMonacoEditor();
```
4. Add the following lines to your component
```c#
<MonacoEditor SoftwareLanguage="json" />
```
5. Have fun!