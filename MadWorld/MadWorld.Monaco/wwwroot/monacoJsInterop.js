// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

let madworldEditor;
export function init(id, body, softwareLanguage) {
  madworldEditor = monaco.editor.create(document.getElementById(id), {
    value: body,
    language: softwareLanguage,
    theme: 'vs-dark'
  });
}

export function setBody(body) {
  madworldEditor.getModel().setValue(body);
}
