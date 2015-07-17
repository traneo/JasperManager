using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperManager
{
    // One of the following formats: pdf, html, xls, xlsx, rtf, csv, xml, docx, odt, ods, jrprint.

    /// <summary>
    /// Formatos suportados pelo Jasperserver para emissão do documento.
    /// </summary>
    public enum JasperReportFormat
    {
        PDF,
        HTML,
        XLS,
        XLSX,
        RTF,
        CSV,
        XML,
        DOCX,
        ODT,
        ODS,
        JRPRINT
    }
}
