# PDFScaffold

PDFScaffold is a library built on top of [PDFsharp & MigraDoc](https://github.com/empira/PDFsharp) meant to provide a way to build a PDF document using a declarative programming approach. However, it does not have all the features available on PDFsharp & MigraDoc yet, but it provides basic PDF contruction features.

## Features

The library provides the following entities to build the PDF:

- SDocument,
- SSection,
- SHeading,
- SParagraph,
- SText,
- SLink,
- STable,
- STableRow,
- STableCell,
- SContainer,
- SRow,
- SColumn,
- SStyle and,
- SMeasure.

There are additionals entities used for other roles that are going to be explored further in this document.

## Declaring a basic document with "Hello, World!"

The code to create a document with "Hello, World!" in it is available below:

```cs
var doc = new SDocument(sections: [
    new(elements: [
        new SParagraph("Hello, World!")
    ])
]);
```

After this, you can get the PDFsharp & MigraDoc Document instance out of the `SDocument` instance, or to get a byte array out of it:

```cs
var document = doc.Build();
var byteArray = doc.GeneratePdf();
```

## SDocument and SSection

Using PDFsharp & MigraDoc, a `Document` is composed of `Sections` and there must be at least one Section in it. Here, it is no different. In the `SDocument`, you can declare the following data:

- Title,
- Author,
- Subject,
- Keywords,
- Styles and
- Sections.

The `Keywords` property is an array of Strings, while the other metadata of the document is a simple String. The `Styles` are a collection of named Styles, used to be referenced inside the document through the `useStyle` property present inside all PDFscaffold visual components.

The Sections are a collection of `SSection` present on the document. Every section can have its size and margins declared.

### SSection

The `SSection` entity is composed of data declaring its format (size of the page), margins and elements.

#### SPageFormat and SMargin

Through the use of `SPageFormat` and `SMargin`, we can define the size and the margin of the page inside the section. The `SPageFormat` has static methods to generate specific formats for the section:

- A0 to A6,
- B5,
- Letter, Legal and Ledger.

The default `SPageFormat` used is A4. The developer has the power to use a custom value for the `SPageFormat` by using the Custom static method to generate a custom `SPageFormat`.

To define the margin of the page, we can use the SMargin constructor. The library provides three ways of generating the margins:

```cs
var oneInch = new SMeasure(inches: 1);

var margin = new SMargin(all: oneInch);
margin = new SMargin(
    leftAndRight: oneInch, 
    topAndBottom: oneInch);

margin = new SMargin(
    left: oneInch
    right: oneInch,
    top: oneInch,
    bottom: oneInch);
```

When one of the sides are not defined, then the size will be 0. The default one is one inch for every margin. The `SMeasure` entity will be explained further in this document.

## SHeading, SParagraph, SText and SLink

These are the entities used to declare text inside the document. The `SHeading` entity is used to declare a heading inside the document:

```cs
var heading = new SHeading(
    content: "New Heading",
    level: 1 // It goes from 1 to 6 and the sizes and boldness are already defined
);

heading = new SHeading(
    content: [ new SText("New Heading") ],
    level: 1
); // Alternative constructor used to style the text inside the heading
```

The SParagraph works very similar to the SHeading:

```cs
var paragraph = new SParagraph(
    content: "New Heading"
);

paragraph = new SParagraph(
    content: [ new SText("New Heading") ]
); // Alternative constructor used to style the text inside the paragraph.
```

The SText is the entity that represents the text inside the document:

```cs
var text = new SText("New Text");
```

The SLink is the entity that represents a link inside the document. With it, we can declare a web link or a link to a component inside the document. To make a reference to a component, the component must be named. Then, we need to define the link of the SLink being the name of the component prefixed with `#`:

```cs
var webLink = new Slink(link: "https://www.google.com/", text: "Google"); // The text attribute is used to create a placeholder for the link. If it is not provided, then the link is displayed.

var component = new SParagraph("Hello, World!", name: "component");
var componentLink = new SLink("#component", text: "A paragraph");
```

## STable, STableRow and STableCell

These entities declares a table inside the document. Their behaivor was inspired by `HTML` tables. With it, we can declare tables with rows inside, and inside the rows live the cells of the table. Furthermore, we can declare the `columnSpan` and `rowSpan` of the cells. We can declare the sizes of the columns as well.

```cs
var table = new STable(rows: [
    new STableRow(cells: [
        new STableCell(content: new SParagraph("Hello, World!"))
    ])
]);

var twoColumnCell = new STableCell(
    columnSpan: 2,
    content: new SPargraph("Hello, World!")
);

var twoRowCell = new STableCell(
    rowSpan: 2,
    content: new SPargraph("Hello, World!")
);

table = new(
    columnSizes: [
        new(percentage: 1/3),
        new(percentage: 1/3),
        new(percentage: 1/3)],
    rows: [
        new(cells: [
            new(content: twoRowCell),
            new(content: twoColumnCell),
        ]),
        new(cells: [
            new(content: twoColumnCell)
        ])
    ]);
```

If the `columnSpan` property overflows the quantity of cells inside the row with the most cells, the columns of the table will grow. However, that logic doesn't apply to the `rowSpan` as well.

We can declare the sizes of the columns, but if it differs from the actual quantity of columns inside the table, then an exception is raised.

## SContainer, SRow and SColumn

These entities do not exist on PDFsharp & MigraDoc. These entities are used to layout components on the page and were inspired by the Container, Row and Column widgets of Flutter.

The `SColumn` and `SRow` entity will be able to cross to another page when used, however the `SContainer` will not be able to. When one of these entities is placed inside the other, this property is lost. So use these entities when you know the size of its children and know when they are do not need to cross the page. Furthermore, the STable entity shares the same behaivor. The `SContainer` entity is specifically used to layout one element using the [SStyle](#sstyle) entity.

```cs
var column = new SColumn(elements: [
    new SParagraph("First row of the column"),
    new SParagraph("Second row of the column"),
    new SParagraph("Third row of the column"),
]);
var row = new SRow(elements: [
    new SParagraph("First column of the row"),
    new SParagraph("Second column of the row"),
    new SParagraph("Third column of the row"),
]); // The size of the columns of the row will be equal if the size of its children are not defined.
var container = new SContainer(
    content: new SParagraph("Hello, World!")
);
```

## SStyle

The `SStyle` entity is used to style the component. We can style a component attributing an `SStyle` instance to the `style` property that can be found in every visual component of the library. These are the aspects of the components that we can style:

- fontColor
    - The color of the font. It is an Color value, a type provided by MigraDoc. It works on SParagraph, SHeading, SText and SLink;
- bold
    - Tells if the text is going to be bold or not. It works on SParagraph, SHeading, SText and SLink;
- italic:
    - Tells if the text is going to be italic or not. It works on SParagraph, SHeading, SText and SLink;
- underline
    - Tells how text is going to be underlined. We can define it using the `SUnderline` enumeration. It works on SParagraph, SHeading, SText and SLink;
- fontSize 
    - The size of the font. It is an [`SMeasure`](##smeasure) value. It works on SParagraph, SHeading, SText and SLink;
- superscript
    - If true, the text inside the component will be superscripted. Only works if `subscript` is false. It is applied to SParagraph, SText and Slink.
- subscript 
    - If true, the text inside the component will be subscripted. Only works if `superscript` is false. It is applied to SParagraph, SText and Slink.
- lineSpacing
    - The size of the space between the lines of a paragraph. Only works on SParagraphs. It is an [`SMeasure`](##smeasure) value.
- spaceBefore
    - The size of the space before the paragraph. Only works on SParagraphs. It is an [`SMeasure`](##smeasure) value.
- spaceAfter 
    - The size of the space after the paragraph. Only works on SParagraphs. It is an [`SMeasure`](##smeasure) value.
- alignment
    - The alignment of an SParagraph. Its value is one of the `SAlignment` value.
- shading
    - The color of the background inside the component. It is applied to `SContainer`, `SColumn`, `SRow`, `STable`, `STableRow`, `STableCell` and `SParagraph`. It is an Color value, a type provided by MigraDoc.
- width*
    - The width of the whole component. If not set, the component will use all the size remaining or the size that it needs. It is applied to all components, except SSection, STableRow and STableCell. This value is only applied on the current component. It is an [`SMeasure`](##smeasure) value.
- height*
    - The height of the whole component. If not set, the component will use the size it needs. It is applied to all components, except SSection, and STableCell. This value is only applied on the current component. It is an [`SMeasure`](##smeasure) value.
- positionType*
    - The type of the position of the component. It is applied to SImage. Its value is a value from the `SPositionType` enumeration.
- topPosition*
    - The position of the component relative to the top of the document. It is applied to `SImage`. It is an [`SMeasure`](##smeasure) value.
- leftPosition*
    - The position of the component relative to the left of the document. It is applied to `SImage`. It is an [`SMeasure`](##smeasure) value.
- resolution
    - The resolution of the component. It is applied to `SImage`. This is a double value and represents the resolution in points per inch.
- verticalAlignment
    - The vertical aligment of the content inside the component. It determines the alignment relative to the height of the document. It is applied to `SContainer`, `STableRow` and `STableCell`. Its value is one of the values of the SAlignment enumeration.
- horizontalAlignment
    - The alignment of the component. It is applied to `SContainer`. Its value is one of the values of the SAlignment enumeration.
- borders*
    - The style of the component's borders. It is applied to SContainer, `SImage`, `SParagraph`, `SColumn`, `SRow`, `STable`, `STableRow` and `STableCell`. In `SContainer` and `SImage`, only the Left Border is used to configure the whole border of the component. This is an [`SBorders`](#sborders) instance.
- padding*
    - The style of the component's `padding`. It is applied to `SContainer`. Its value is an [`SPadding`](#spadding) instance.
- centered*
    - If true, the content inside the component will be centered and the `padding` will be disconsidered.It is applied to `SContainer` and only works correctly if both the `SContainer` and its content have their Widths and Heights set. This value is only applied to the current component.

When a property of the SStyle is set in a component higher in the document, this property is passed down to the children of the component. The properties marked with the `*` are not passed to its children.

If one of the properties is not set on the consctructor, then it is not applied to the component.

The SStyle instance provides a Merge method, used to get the properties defined in another instance of SStyle use it if that property is not defined in itself. This method then returns the resulting SStyle.

We can define a name to the SStyle as well. This is going to be used by other components to reference the style placed inside the styles properties inside the SDocument.

### SAlignment, SUnderline and SPositionType

These enumerations are used to declare some predefined aspects of a component:

- SAlignment:
    - Start,
    - Center,
    - End,
    - Justified
        - It only work with the alignment property.
- SUnderline:
    - None,
    - Dash,
    - DotDash,
    - DotDotDash, 
    - Dotted, 
    - Single, 
    - OnWords
- SPositionType:
    - Fixed,
        - This makes the image position relative to the page size.
    - Relative
        - This makes the image position relative to the last paragraph and column used.

### SBorders

These object is used to define the borders of the component. It has four instances of an `SBorder` inside of it, one for each border side. We can use the constructor to declare the borders of all sides, the left and right or top and bottom, or the border for each side individually.

#### SBorder

The `SBorder` defines one border. With it, we can define the width, color, type, visibility and the distance from the content of the border. The visibility is a boolean that tells if the border is visible or not. By default it is true.

```cs
var borders = new SBorders(all: new SBorder(
    width: new(points: 10),
    color: Colors.Black,
    distanceFromContent: new(points: 10),
    borderType: SBorderType.Single,
    visible: true
));
```

#### SBorderType

This is an enumeration used to declare the style of the border. It can be:

- None,
    - Single,
        - Single solid Line. Works on SColumn, SRow, STable, STableRow and STableCell.
    - Dot,
        - A dotted line. Works on SColumn, SRow, STable, STableRow and STableCell.
    - DashSmallGap,
        - A dash line with small gaps. Works on SColumn, SRow, STable, STableRow and STableCell.
    - DashLargeGap,
        - A dash line with large gaps. Works on SColumn, SRow, STable, STableRow and STableCell.
    - DashDot,
        - A line with dashes and dots. Works on SColumn, SRow, STable, STableRow, STableCell, SImage and SContainer.
    - DashDotDot,
        - A line with dashs and two dots intercalating. Works on SColumn, SRow, STable, STableRow, STableCell, SImage and SContainer.
    - Dash,
        - A dash line. Works on SImage and SContainer.
    - Solid,
        - A solid line. Works on SImage and SContainer.
    - SquareDot
        - A line with square dots. Works on SImage and SContainer.

### SPadding

The SPadding instance is used to define the padding of an SContainer:

```cs
var padding = new SPadding(all: new SMeasure(points: 10));
```

## SMeasure

The SMeasure entity is used to declare sizes inside the SDocument. With it, we can define the size in:

- Points
- Inches,
- Picas,
- Centimeters,
- Millimeters,
- Pixels and
- Percentage.

The pixel value is relative to the 72 points per inch resolution. The percentage value is relative to the size of the parent component (for the width and height properiies) or the size of the component itself. In the constructor of this entity, we can define value for all these properties, but the precedence follow the order above. All the values are double, with the percentage value being from 0 to 1, excluding the 0 and including the 1.

```cs
var size = new SMeasure(pixels: 10);
size = new SMeasure(percentage: 1.0/2.0);
```