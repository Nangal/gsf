//*******************************************************************************************************
//  RawDataExporter.cs - Gbtc
//
//  Tennessee Valley Authority, 2009
//  No copyright is claimed pursuant to 17 USC § 105.  All Other Rights Reserved.
//
//  This software is made freely available under the TVA Open Source Agreement (see below).
//
//  Code Modification History:
//  -----------------------------------------------------------------------------------------------------
//  10/17/2007 - Pinal C. Patel
//       Original version of source code generated.
//  02/14/2008 - Pinal C. Patel
//       Added Protected GetBufferedData() method for inheritting class to use the biffered data.
//  04/17/2009 - Pinal C. Patel
//       Converted to C#.
//  09/15/2009 - Stephen C. Wills
//       Added new header and license agreement.
//  12/16/2009 - Pinal C. Patel
//       Modified to use the process queue exposed by the base class for processing real-time data.
//
//*******************************************************************************************************

#region [ TVA Open Source Agreement ]
/*

 THIS OPEN SOURCE AGREEMENT ("AGREEMENT") DEFINES THE RIGHTS OF USE,REPRODUCTION, DISTRIBUTION,
 MODIFICATION AND REDISTRIBUTION OF CERTAIN COMPUTER SOFTWARE ORIGINALLY RELEASED BY THE
 TENNESSEE VALLEY AUTHORITY, A CORPORATE AGENCY AND INSTRUMENTALITY OF THE UNITED STATES GOVERNMENT
 ("GOVERNMENT AGENCY"). GOVERNMENT AGENCY IS AN INTENDED THIRD-PARTY BENEFICIARY OF ALL SUBSEQUENT
 DISTRIBUTIONS OR REDISTRIBUTIONS OF THE SUBJECT SOFTWARE. ANYONE WHO USES, REPRODUCES, DISTRIBUTES,
 MODIFIES OR REDISTRIBUTES THE SUBJECT SOFTWARE, AS DEFINED HEREIN, OR ANY PART THEREOF, IS, BY THAT
 ACTION, ACCEPTING IN FULL THE RESPONSIBILITIES AND OBLIGATIONS CONTAINED IN THIS AGREEMENT.

 Original Software Designation: openPDC
 Original Software Title: The TVA Open Source Phasor Data Concentrator
 User Registration Requested. Please Visit https://naspi.tva.com/Registration/
 Point of Contact for Original Software: J. Ritchie Carroll <mailto:jrcarrol@tva.gov>

 1. DEFINITIONS

 A. "Contributor" means Government Agency, as the developer of the Original Software, and any entity
 that makes a Modification.

 B. "Covered Patents" mean patent claims licensable by a Contributor that are necessarily infringed by
 the use or sale of its Modification alone or when combined with the Subject Software.

 C. "Display" means the showing of a copy of the Subject Software, either directly or by means of an
 image, or any other device.

 D. "Distribution" means conveyance or transfer of the Subject Software, regardless of means, to
 another.

 E. "Larger Work" means computer software that combines Subject Software, or portions thereof, with
 software separate from the Subject Software that is not governed by the terms of this Agreement.

 F. "Modification" means any alteration of, including addition to or deletion from, the substance or
 structure of either the Original Software or Subject Software, and includes derivative works, as that
 term is defined in the Copyright Statute, 17 USC § 101. However, the act of including Subject Software
 as part of a Larger Work does not in and of itself constitute a Modification.

 G. "Original Software" means the computer software first released under this Agreement by Government
 Agency entitled openPDC, including source code, object code and accompanying documentation, if any.

 H. "Recipient" means anyone who acquires the Subject Software under this Agreement, including all
 Contributors.

 I. "Redistribution" means Distribution of the Subject Software after a Modification has been made.

 J. "Reproduction" means the making of a counterpart, image or copy of the Subject Software.

 K. "Sale" means the exchange of the Subject Software for money or equivalent value.

 L. "Subject Software" means the Original Software, Modifications, or any respective parts thereof.

 M. "Use" means the application or employment of the Subject Software for any purpose.

 2. GRANT OF RIGHTS

 A. Under Non-Patent Rights: Subject to the terms and conditions of this Agreement, each Contributor,
 with respect to its own contribution to the Subject Software, hereby grants to each Recipient a
 non-exclusive, world-wide, royalty-free license to engage in the following activities pertaining to
 the Subject Software:

 1. Use

 2. Distribution

 3. Reproduction

 4. Modification

 5. Redistribution

 6. Display

 B. Under Patent Rights: Subject to the terms and conditions of this Agreement, each Contributor, with
 respect to its own contribution to the Subject Software, hereby grants to each Recipient under Covered
 Patents a non-exclusive, world-wide, royalty-free license to engage in the following activities
 pertaining to the Subject Software:

 1. Use

 2. Distribution

 3. Reproduction

 4. Sale

 5. Offer for Sale

 C. The rights granted under Paragraph B. also apply to the combination of a Contributor's Modification
 and the Subject Software if, at the time the Modification is added by the Contributor, the addition of
 such Modification causes the combination to be covered by the Covered Patents. It does not apply to
 any other combinations that include a Modification. 

 D. The rights granted in Paragraphs A. and B. allow the Recipient to sublicense those same rights.
 Such sublicense must be under the same terms and conditions of this Agreement.

 3. OBLIGATIONS OF RECIPIENT

 A. Distribution or Redistribution of the Subject Software must be made under this Agreement except for
 additions covered under paragraph 3H. 

 1. Whenever a Recipient distributes or redistributes the Subject Software, a copy of this Agreement
 must be included with each copy of the Subject Software; and

 2. If Recipient distributes or redistributes the Subject Software in any form other than source code,
 Recipient must also make the source code freely available, and must provide with each copy of the
 Subject Software information on how to obtain the source code in a reasonable manner on or through a
 medium customarily used for software exchange.

 B. Each Recipient must ensure that the following copyright notice appears prominently in the Subject
 Software:

          No copyright is claimed pursuant to 17 USC § 105.  All Other Rights Reserved.

 C. Each Contributor must characterize its alteration of the Subject Software as a Modification and
 must identify itself as the originator of its Modification in a manner that reasonably allows
 subsequent Recipients to identify the originator of the Modification. In fulfillment of these
 requirements, Contributor must include a file (e.g., a change log file) that describes the alterations
 made and the date of the alterations, identifies Contributor as originator of the alterations, and
 consents to characterization of the alterations as a Modification, for example, by including a
 statement that the Modification is derived, directly or indirectly, from Original Software provided by
 Government Agency. Once consent is granted, it may not thereafter be revoked.

 D. A Contributor may add its own copyright notice to the Subject Software. Once a copyright notice has
 been added to the Subject Software, a Recipient may not remove it without the express permission of
 the Contributor who added the notice.

 E. A Recipient may not make any representation in the Subject Software or in any promotional,
 advertising or other material that may be construed as an endorsement by Government Agency or by any
 prior Recipient of any product or service provided by Recipient, or that may seek to obtain commercial
 advantage by the fact of Government Agency's or a prior Recipient's participation in this Agreement.

 F. In an effort to track usage and maintain accurate records of the Subject Software, each Recipient,
 upon receipt of the Subject Software, is requested to register with Government Agency by visiting the
 following website: https://naspi.tva.com/Registration/. Recipient's name and personal information
 shall be used for statistical purposes only. Once a Recipient makes a Modification available, it is
 requested that the Recipient inform Government Agency at the web site provided above how to access the
 Modification.

 G. Each Contributor represents that that its Modification does not violate any existing agreements,
 regulations, statutes or rules, and further that Contributor has sufficient rights to grant the rights
 conveyed by this Agreement.

 H. A Recipient may choose to offer, and to charge a fee for, warranty, support, indemnity and/or
 liability obligations to one or more other Recipients of the Subject Software. A Recipient may do so,
 however, only on its own behalf and not on behalf of Government Agency or any other Recipient. Such a
 Recipient must make it absolutely clear that any such warranty, support, indemnity and/or liability
 obligation is offered by that Recipient alone. Further, such Recipient agrees to indemnify Government
 Agency and every other Recipient for any liability incurred by them as a result of warranty, support,
 indemnity and/or liability offered by such Recipient.

 I. A Recipient may create a Larger Work by combining Subject Software with separate software not
 governed by the terms of this agreement and distribute the Larger Work as a single product. In such
 case, the Recipient must make sure Subject Software, or portions thereof, included in the Larger Work
 is subject to this Agreement.

 J. Notwithstanding any provisions contained herein, Recipient is hereby put on notice that export of
 any goods or technical data from the United States may require some form of export license from the
 U.S. Government. Failure to obtain necessary export licenses may result in criminal liability under
 U.S. laws. Government Agency neither represents that a license shall not be required nor that, if
 required, it shall be issued. Nothing granted herein provides any such export license.

 4. DISCLAIMER OF WARRANTIES AND LIABILITIES; WAIVER AND INDEMNIFICATION

 A. No Warranty: THE SUBJECT SOFTWARE IS PROVIDED "AS IS" WITHOUT ANY WARRANTY OF ANY KIND, EITHER
 EXPRESSED, IMPLIED, OR STATUTORY, INCLUDING, BUT NOT LIMITED TO, ANY WARRANTY THAT THE SUBJECT
 SOFTWARE WILL CONFORM TO SPECIFICATIONS, ANY IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 PARTICULAR PURPOSE, OR FREEDOM FROM INFRINGEMENT, ANY WARRANTY THAT THE SUBJECT SOFTWARE WILL BE ERROR
 FREE, OR ANY WARRANTY THAT DOCUMENTATION, IF PROVIDED, WILL CONFORM TO THE SUBJECT SOFTWARE. THIS
 AGREEMENT DOES NOT, IN ANY MANNER, CONSTITUTE AN ENDORSEMENT BY GOVERNMENT AGENCY OR ANY PRIOR
 RECIPIENT OF ANY RESULTS, RESULTING DESIGNS, HARDWARE, SOFTWARE PRODUCTS OR ANY OTHER APPLICATIONS
 RESULTING FROM USE OF THE SUBJECT SOFTWARE. FURTHER, GOVERNMENT AGENCY DISCLAIMS ALL WARRANTIES AND
 LIABILITIES REGARDING THIRD-PARTY SOFTWARE, IF PRESENT IN THE ORIGINAL SOFTWARE, AND DISTRIBUTES IT
 "AS IS."

 B. Waiver and Indemnity: RECIPIENT AGREES TO WAIVE ANY AND ALL CLAIMS AGAINST GOVERNMENT AGENCY, ITS
 AGENTS, EMPLOYEES, CONTRACTORS AND SUBCONTRACTORS, AS WELL AS ANY PRIOR RECIPIENT. IF RECIPIENT'S USE
 OF THE SUBJECT SOFTWARE RESULTS IN ANY LIABILITIES, DEMANDS, DAMAGES, EXPENSES OR LOSSES ARISING FROM
 SUCH USE, INCLUDING ANY DAMAGES FROM PRODUCTS BASED ON, OR RESULTING FROM, RECIPIENT'S USE OF THE
 SUBJECT SOFTWARE, RECIPIENT SHALL INDEMNIFY AND HOLD HARMLESS  GOVERNMENT AGENCY, ITS AGENTS,
 EMPLOYEES, CONTRACTORS AND SUBCONTRACTORS, AS WELL AS ANY PRIOR RECIPIENT, TO THE EXTENT PERMITTED BY
 LAW.  THE FOREGOING RELEASE AND INDEMNIFICATION SHALL APPLY EVEN IF THE LIABILITIES, DEMANDS, DAMAGES,
 EXPENSES OR LOSSES ARE CAUSED, OCCASIONED, OR CONTRIBUTED TO BY THE NEGLIGENCE, SOLE OR CONCURRENT, OF
 GOVERNMENT AGENCY OR ANY PRIOR RECIPIENT.  RECIPIENT'S SOLE REMEDY FOR ANY SUCH MATTER SHALL BE THE
 IMMEDIATE, UNILATERAL TERMINATION OF THIS AGREEMENT.

 5. GENERAL TERMS

 A. Termination: This Agreement and the rights granted hereunder will terminate automatically if a
 Recipient fails to comply with these terms and conditions, and fails to cure such noncompliance within
 thirty (30) days of becoming aware of such noncompliance. Upon termination, a Recipient agrees to
 immediately cease use and distribution of the Subject Software. All sublicenses to the Subject
 Software properly granted by the breaching Recipient shall survive any such termination of this
 Agreement.

 B. Severability: If any provision of this Agreement is invalid or unenforceable under applicable law,
 it shall not affect the validity or enforceability of the remainder of the terms of this Agreement.

 C. Applicable Law: This Agreement shall be subject to United States federal law only for all purposes,
 including, but not limited to, determining the validity of this Agreement, the meaning of its
 provisions and the rights, obligations and remedies of the parties.

 D. Entire Understanding: This Agreement constitutes the entire understanding and agreement of the
 parties relating to release of the Subject Software and may not be superseded, modified or amended
 except by further written agreement duly executed by the parties.

 E. Binding Authority: By accepting and using the Subject Software under this Agreement, a Recipient
 affirms its authority to bind the Recipient to all terms and conditions of this Agreement and that
 Recipient hereby agrees to all terms and conditions herein.

 F. Point of Contact: Any Recipient contact with Government Agency is to be directed to the designated
 representative as follows: J. Ritchie Carroll <mailto:jrcarrol@tva.gov>.

*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;

namespace TVA.Historian.Exporters
{
    /// <summary>
    /// Represents an exporter that can export real-time time-series data in CSV or XML format to a file.
    /// </summary>
    /// <example>
    /// Definition of a sample <see cref="Export"/> that can be processed by <see cref="RawDataExporter"/>:
    /// <code language="XML">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-16"?>
    /// <Export xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    ///   <Name>RawDataExport</Name>
    ///   <Type>Intervaled</Type>
    ///   <Interval>10</Interval>
    ///   <Exporter>RawDataExporter</Exporter>
    ///   <Settings>
    ///     <ExportSetting>
    ///       <Name>OutputFile</Name>
    ///       <Value>c:\RawDataExporterOutput.xml</Value>
    ///     </ExportSetting>
    ///     <ExportSetting>
    ///       <Name>OutputFormat</Name>
    ///       <Value>XML</Value>
    ///     </ExportSetting>    
    ///   </Settings>
    ///   <Records>
    ///     <ExportRecord>
    ///       <Instance>P3</Instance>
    ///       <Identifier>3261</Identifier>
    ///     </ExportRecord>
    ///     <ExportRecord>
    ///       <Instance>P3</Instance>
    ///       <Identifier>3266</Identifier>
    ///     </ExportRecord>  
    ///  </Records>
    /// </Export>
    /// ]]>
    /// </code>
    /// <para>
    /// Description of custom settings required by <see cref="RawDataExporter"/> in an <see cref="Export"/>:
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting Name</term>
    ///         <description>Setting Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term>OutputFile</term>
    ///         <description>Name of the CSV or XML file (including path) where export data is to be written.</description>
    ///     </item>
    ///     <item>
    ///         <term>OutputFormat</term>
    ///         <description>Format (CSV or XML) in which export data is to be written to the output file.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// </example>
    /// <seealso cref="Export"/>
    public class RawDataExporter : ExporterBase
    {
        #region [ Members ]

        // Fields
        private Dictionary<string, DataSet> m_rawData;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="RawDataExporter"/> class.
        /// </summary>
        public RawDataExporter()
            : this("RawDataExporter")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RawDataExporter"/> class.
        /// </summary>
        /// <param name="name"><see cref="ExporterBase.Name"/> of the exporter.</param>
        protected RawDataExporter(string name)
            : base(name)
        {
            m_rawData = new Dictionary<string, DataSet>();

            // Register handlers.
            ExportAddedHandler = CreateBuffer;
            ExportRemovedHandler = RemoveBuffer;
            RealTimeExportQueue.ProcessItemsFunction = ProcessRealTimeExport;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Processes the <paramref name="export"/> using the current <see cref="DataListener.Data"/>.
        /// </summary>
        /// <param name="export"><see cref="Export"/> to be processed.</param>
        /// <exception cref="ArgumentException"><b>OutputFile</b> or <b>OutputFormat</b> setting is missing from the <see cref="Export.Settings"/> of the <paramref name="export"/>.</exception>
        protected override void ProcessExport(Export export)
        {
            DataSet rawData = GetBufferedData(export.Name);
            try
            {
                // Ensure that required settings are present.
                ExportSetting outputFileSetting = export.FindSetting("OutputFile");
                if (outputFileSetting == null)
                    throw new ArgumentException("OutputFile setting is missing");
                ExportSetting outputFormatSetting = export.FindSetting("OutputFormat");
                if (outputFormatSetting == null)
                    throw new ArgumentException("OutputFormat setting is missing");

                if (rawData != null)
                {
                    // Buffered data exists for exporting.
                    lock (rawData)
                    {
                        // Update the export timestamp, row count and interval.
                        rawData.Tables[1].Rows.Add(DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss tt"), rawData.Tables[0].Rows.Count, string.Format("{0} seconds", export.Interval));

                        // Write the buffered raw data for the export to the specified files in specified format.
                        FileHelper.WriteToFile(outputFileSetting.Value, outputFormatSetting.Value, rawData);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rawData != null)
                {
                    lock (rawData)
                    {
                        // Clear the buffered data to make space for new data that is to be buffered.
                        rawData.Tables[0].Clear();
                        rawData.Tables[1].Clear();
                    }
                }
            }
        }

        /// <summary>
        /// Processes the <paramref name="export"/> using the real-time <paramref name="data"/>.
        /// </summary>
        /// <param name="export"><see cref="Export"/> to be processed.</param>
        /// <param name="listener"><see cref="DataListener"/> that provided the <paramref name="data"/>.</param>
        /// <param name="data">Real-time time-series data received by the <paramref name="listener"/>.</param>
        /// <exception cref="NotSupportedException">Always</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void ProcessRealTimeExport(Export export, DataListener listener, IList<IDataPoint> data)
        {
            throw new NotSupportedException();  // Real-Time exports not supported.
        }

        /// <summary>
        /// Handles the <see cref="DataListener.DataExtracted"/> event for all the <see cref="ExporterBase.Listeners"/>.
        /// </summary>
        /// <param name="sender"><see cref="DataListener"/> object that raised the event.</param>
        /// <param name="e"><see cref="EventArgs{T}"/> object where <see cref="EventArgs{T}.Argument"/> is the collection of real-time time-sereis data received.</param>
        protected override void ProcessRealTimeData(object sender, EventArgs<IList<IDataPoint>> e)
        {
            DataListener listener = (DataListener)sender;
            List<Export> exportsList = new List<Export>();
            lock (Exports)
            {
                // Get a local copy of all the exports this exporter is responsible for.
                exportsList.AddRange(Exports);
            }

            // Queue the export and parsed data for processing if:
            // - Export has records that are from the listener whose data we have received
            // - Export is not "Real-Time", since real-time exports are not supported by this exporter
            foreach (Export export in exportsList)
            {
                if (export.FindRecords(listener.ID).Count > 0 && export.Type != ExportType.RealTime)
                    RealTimeExportQueue.Add(new RealTimeData(export, listener, e.Argument));
            }
        }

        /// <summary>
        /// Returns a <see cref="DataSet"/> containing the buffered real-time time-series data.
        /// </summary>
        /// <param name="exportName"><see cref="Export.Name"/> of the export whose buffered data is to be retrieved.</param>
        /// <returns>A <see cref="DataSet"/> object if buffered data exists; otherwise null.</returns>
        protected DataSet GetBufferedData(string exportName)
        {
            DataSet data = null;
            lock (m_rawData)
            {
                // Get the buffered data for the export.
                m_rawData.TryGetValue(exportName, out data);
            }

            return data;
        }

        private void ProcessRealTimeExport(RealTimeData[] items)
        {
            DataSet rawData;
            IList<ExportRecord> exportRecords;
            foreach (RealTimeData item in items)
            {
                try
                {                  
                    lock (m_rawData)
                    {
                        m_rawData.TryGetValue(item.Export.Name, out rawData);
                    }

                    if (rawData != null)
                    {
                        // Buffer-up the parsed data for the export.
                        exportRecords = item.Export.FindRecords(item.Listener.ID);
                        if (exportRecords.Count == 1 && exportRecords[0].Identifier == -1)
                        {
                            // Include all data from the listener.
                            if (Monitor.TryEnter(rawData))
                            {
                                try
                                {
                                    foreach (IDataPoint dataPoint in item.Data)
                                    {
                                        rawData.Tables[0].Rows.Add(item.Listener.ID, dataPoint.HistorianID, dataPoint.Time.ToString(), dataPoint.Value, (int)dataPoint.Quality);
                                    }
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    Monitor.Exit(rawData);
                                }
                            }
                        }
                        else
                        {
                            // Buffer data for selected records only (filtered).
                            ExportRecord comparer = new ExportRecord(item.Listener.ID, -1);
                            if (Monitor.TryEnter(rawData))
                            {
                                try
                                {
                                    foreach (IDataPoint dataPoint in item.Data)
                                    {
                                        if (exportRecords.FirstOrDefault(record => record.Identifier == dataPoint.HistorianID) != null)
                                            rawData.Tables[0].Rows.Add(item.Listener.ID, dataPoint.HistorianID, dataPoint.Time.ToString(), dataPoint.Value, (int)dataPoint.Quality);
                                    }
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    Monitor.Exit(rawData);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnStatusUpdate(string.Format("Data prep failed for export \"{0}\" - {1}", item.Export.Name, ex.Message));
                }
            }
        }

        private void CreateBuffer(Export export)
        {
            DataSet rawData = null;
            lock (m_rawData)
            {
                if (!m_rawData.TryGetValue(export.Name, out rawData))
                {
                    // Dataset for buffering export data does not exist.
                    rawData = ExporterBase.DatasetTemplate(null);

                    // Save the dataset.
                    m_rawData.Add(export.Name, rawData);

                    // Provide a status update.
                    OnStatusUpdate(string.Format("Data buffer created for export {0}", export.Name));
                }
            }
        }

        private void RemoveBuffer(Export export)
        {
            lock (m_rawData)
            {
                DataSet data = null;
                if (m_rawData.TryGetValue(export.Name, out data))
                {
                    // Remove the dataset used for buffering export data.
                    data.Dispose();
                    m_rawData.Remove(export.Name);

                    // Provide a status update.
                    OnStatusUpdate(string.Format("Data buffer of export {0} removed", export.Name));
                }
            }
        }
      
        #endregion
    }
}