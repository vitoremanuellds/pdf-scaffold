# Introduction

## Basic structure of a migradoc document

A document is built using the migradoc's Document class.

```cs
var document = new Document();
```

Document objects has sections in it. This sections contains the content of the document. To add texts, images and tables on the document, we need to create a section.

```cs
var section = document.AddSection();
```

When we have the section object, we can add some content inside of it. We can add a Paragraph inside the section and add some text in it. We can set some characteristics of the paragraph, like the font color.

```cs
var paragraph = section.AddParagraph();
var text = paragraph.AddFormattedText(
    "Hello, World!", TextFormat.Bold
);
paragraph.Font.Color = Colors.DarkBlue;
```

We can add a footer to the section as well and add some text in it.

```cs
var footer = section.Footers.Primary;
paragraph = footer.AddParagraph();
```

We can change the alignment of the text on the paragraphs.

```cs
paragraph.Format.Alignment = ParagraphAlignment.Center;
```

We can add some images as well.

```cs
section.AddImage("path-to-image.extension");
```

## Some useful tips

We can set some styles to use in the document, or change some of the default styles. The Document object has an attribute called Styles. It is a collection containing some styles, indexed by their names. Consequently, we can get them from anywhere in the code, just by getting them through the document object reference.

```cs
var style = document.Styles[StyleNames.Normal];
style.Font.Name = "Arial";
style.Font.Color = Colors.DarkBlue;
```

## Rendering the document

We need to create a PdfDocumentRenderer object to render de document. After this, we can save the document inside a file, using the path, or in a stream.

```cs
var pdfRenderer = new PdfDocumentRenderer {
    Document = document,
    PdfDocument =
    {
        PageLayout = PdfPageLayout.SinglePage,
        ViewerPreferences =
        {
            FitWindow = true
        }
    }
};

pdfRenderer.RenderDocument();

pdfRenderer.PdfDocument.Save("filename-path");
pdfRenderer.PdfDocument.Save(someStream);
```