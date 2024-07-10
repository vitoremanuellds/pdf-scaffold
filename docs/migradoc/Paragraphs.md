# Paragraphs

## Adding a paragraph in the document

We can add a paragraph in a section of the document like this:

```cs
var p1 = document.LastSection.AddParagraph(
    "Texto of the paragraph",
    "Style"
);
```

We can add an empty paragraph:

```cs
var p2 = document.LasSection.AddParagraph();
```

Later, we can add text to this paragraph:

```cs
p2.AddText("Text");
```
## Changing some aspects of the paragraph

### Alignment

We can change the alignment of the paragraph as well:

```cs
p2.Format.Alignment = ParagraphAlignment.Left;
```

The other alignment options are:

* ParagraphAlignment.Left
* ParagraphAlignment.Right
* ParagraphAlignment.Center
* ParagraphAlignment.Justify

### Indentation

We can change the indentation of the text at the paragraph:

```cs
p2.Format.LeftIndent = "2cm";
```

We can change the indentation using these attributes:

* Format.LeftIndent
* Format.RightIndent

If we want to change the indentation of the first line only, we use this attribute:

* Format.FirstLineIndent

We can use negative values for the indent values as well:

```cs
p2.Format.FirstLineIndent = "-1.0cm";
```

## Adding Formatted Text in the paragraph

We can add formatted text as well in the paragraph:

```cs
var ft = p2.AddFormattedText("Formatted Text", TextFormat.Bold);
```

The text formats we can use are these:

* TextFormat.Bold
* TextFormat.Italic

We can use both of them at the same time. We only need to do this:

```cs
ft = p2.AddFormattedText("Bold and Italic", TextFormat.Bold | TextFormat.Italic);
```

We can add a line break as well in the paragraph:

```cs
p2.AddLineBreak(); 
// It does not return anything
```

We can change the size of the font as well:

```cs
ft = p2.AddFormattedText("Size");
ft.Size = 15;
```

We can change the color of the text as well:

```cs
ft.Color = Colors.Firebrick;
```

We can change the font of the text as well:

```cs
ft = p2.AddFormattedText(
    "Font",
    new Font("Times New Roman", 12)
);
```

We can make the text a subscript or a superscript:

```cs
ft.Subscript = true;
ft.Superscript = true;
```

## Adding visual effects to the paragraph

We can add some effects on the paragraph. We can add borders to it, and shading (fill color).

### Borders

```cs
var p3 = document.LastSection.AddParagraph("Text", "Style");

p3.Format.Borders.Width = 2.5;
p3.Format.Borders.Colors = Colors.Navy;
p3.Format.Borders.Distance = 3;
```

### Shading

```cs
p3.Format.Shading.Color = Colors.LightCoral;
``` 

### Using both

There is an specific style for it:

```cs
p3.Style = "TextBox";
```