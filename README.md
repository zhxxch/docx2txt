docx2txt - C#
========

## Usage

`.gitattributes`:
```
*.docx binary diff=docx
```

git config:
```
git config --local diff.docx.textconv docx2txt.exe
git config --local diff.docx.binary true
```

Windows only.

## Build

```
./build.ps1
```

## License

GNU GPL v3.0