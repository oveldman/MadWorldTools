// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export function init(id, body, softwareLanguage) {
  monaco.editor.create(document.getElementById(id), {
    value: body,
    language: softwareLanguage,
    theme: 'vs-dark'
  });
}
