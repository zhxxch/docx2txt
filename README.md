docx2txt - C#
========

[Download docx2txt.exe](https://github.com/zhxxch/docx2txt/releases/download/v1/docx2txt.exe)

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