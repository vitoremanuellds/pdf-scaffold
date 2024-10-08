﻿using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;
using PDFScaffold.Visitors.Default;
using PdfSharp.Pdf;

namespace PDFScaffold.Scaffold;

/// <summary>
/// The <c>SDocument</c> class represents the PDF Document being built.
/// </summary>
/// <param name="title">The Title of the Document. It can be null.</param>
/// <param name="author">The Author of the Document. It can be null.</param>
/// <param name="subject">The Subject of the Docuemnt. It can be null.</param>
/// <param name="keywords">The Keywords related to this Document. It can be null.</param>
/// <param name="styles">
///     The list of styles that can be referenced inside
///     the document. It can be null as well. The styles inside must
///     have a name for it to be able to be referenced.
/// </param>
/// <param name="sections">
///     The list of the sections inside the Document. It can not be null and there must
///     be at least one SSection inside of the list.
/// </param>
public class SDocument(
    ICollection<SSection> sections,
    string? title = null,
    string? author = null,
    string? subject = null,
    ICollection<string>? keywords = null,
    ICollection<SStyle>? styles = null
) : IPdfScaffoldElement {
    /// <summary>
    /// The Title of the Document. It can be null.
    /// </summary>
    public string? Title { get; } = title;

    /// <summary>
    /// The Author of the Document. It can be null.
    /// </summary>
    public string? Author { get; } = author;

    /// <summary>
    /// The Subject of the Docuemnt. It can be null.
    /// </summary>
    public string? Subject { get; } = subject;

    /// <summary>
    /// The Keywords related to this Document. It can be null.
    /// </summary>
    public ICollection<string>? Keywords { get; } = keywords;

    /// <summary>
    ///     The list of the sections inside the Document. It can not be null and there must
    ///     be at least one SSection inside of the list.
    /// </summary>
    public ICollection<SSection> Sections { get; } = sections;

    /// <summary>
    ///     The list of styles that can be referenced inside
    ///     the document. It can be null as well. The styles inside must
    ///     have a name for it to be able to be referenced.
    /// </summary>
    public ICollection<SStyle>? Styles { get; } = styles;

    private IDictionary<string, SStyle> _styles = new Dictionary<string, SStyle>();

    /// <summary>
    /// The <c>Build</> method build the components inside the SDocument and add them to
    /// a Migradoc Document Object.
    /// </summary>
    /// <param name="document">A Migradoc Document object in which the new components will
    /// be created and inserted. If it is null, a new Migradoc Document object will be created.</param>
    /// <returns>Migradoc Document object populated with
    /// the corresponding components of the objects inside the SDocument.</returns>
    public Document Build(Document? document = null) {
        document ??= new Document();

        var visitor = new SVisitor {
            Document = document
        };

        Accept(visitor);
        return visitor.Document;
    }

    /// <summary>
    /// Build and render the PDF document.
    /// </summary>
    /// <param name="document">
    ///     The MigraDoc Document model in wich the components are going to be appended.
    ///     If not provided, a new MigraDoc Document model will be created.
    /// </param>
    /// <returns>A byte array representing the PDF genereated.</returns>
    public byte[] GeneratePdf(Document? document = null)
    {
        Document doc = this.Build(document);
        PdfDocumentRenderer renderer = new();
        renderer.Document = doc; // This is the document we've been builing
        renderer.PdfDocument.PageLayout = PdfPageLayout.SinglePage;
        renderer.PdfDocument.ViewerPreferences.FitWindow = true;

        // Rendering the document
        renderer.RenderDocument();

        MemoryStream stream = new();
        renderer.PdfDocument.Save(stream);
        byte[] result = stream.ToArray();
        stream.Close();

        return result;
    }

    public void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForDocument(this);
    }
}
