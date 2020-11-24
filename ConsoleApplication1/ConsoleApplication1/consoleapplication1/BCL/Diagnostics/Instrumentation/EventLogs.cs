using System.Diagnostics;

namespace EstudosCSharp.BCL.Diagnostics.Instrumentation
{
    /// <summary>
    /// Event logs are a centralized means in the Windows platform to keep track of diagnostic
    /// information about running software components in the system.Both the operating
    /// system and its services and third-party applications can plug in to it. The designers of the
    /// .NET Framework deemed it useful to have a managed wrapper around the APIs used to communicate with the event logging component of Win32.
    /// Various static methods are available to create what’s known as an event source.
    /// </summary>
    public static class EventLogs
    {
        const string SOURCE = "EstudosCSharp";
        const string APPLICATION = "EstudosCSharp.BCL.Diagnostics.Instrumentation.EventLogs";

        /// <summary>
        /// All EventLogs needs a source. This method checks for the existence of a source, followed by the creation thereof in case it’s found to be missing.
        /// REQUIRES ADMINISTRATIVE PRIVILEGIES
        /// </summary>
        public static void CreateEventSource()
        {
            if (!EventLog.SourceExists(SOURCE))
                EventLog.CreateEventSource(SOURCE, APPLICATION);
        }

        public static void WriteLogToEventSource()
        {
            const int STARTED = 1001;
            const short LIFECYCLE = 1;
            using (var log = new EventLog(APPLICATION, ".", SOURCE)) // the dot param means local computer.
            {
                log.WriteEntry("An info entry wrote by code", 
                    EventLogEntryType.Information,
                    STARTED, 
                    LIFECYCLE);

                log.WriteEntry("A warning entry wrote by code",
                    EventLogEntryType.Warning,
                    STARTED,
                    LIFECYCLE);

                log.WriteEntry("An error entry wrote by code",
                    EventLogEntryType.Error,
                    STARTED,
                    LIFECYCLE);

                log.WriteEntry("A FailureAudit entry wrote by code",
                   EventLogEntryType.FailureAudit,
                   STARTED,
                   LIFECYCLE);

                log.WriteEntry("A SuccessAudit entry wrote by code",
                   EventLogEntryType.SuccessAudit,
                   STARTED,
                   LIFECYCLE);
            }
        }
    }
}
