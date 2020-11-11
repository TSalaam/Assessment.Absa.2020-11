using System;
using System.ComponentModel;

namespace PhoneBook.Api.Domain.Logging {

    /// <summary>
    /// Contains constants and enums for consistent structured logging
    /// </summary>
    public static class LoggingConstants {

        // Template for consisted structured logging accross multiple functions, each field is described below:

        // EntityType: Business Entity Type being processed: e.g. Order, Shipment, etc.
        // EventDescription is a short description of the Event being logged. 
        // EntityId: Id of the Business Entity being processed: e.g. Order Number, Shipment Id, etc. 
        // Status: Status of the Log Event, e.g. Succeeded, Failed, Discarded.
        // CorrelationId: Unique identifier of the message that can be processed by more than one component. 
        // CheckPoint: To classify and be able to correlate tracking events.
        // CheckPointSource: 
        // Description: A detailed description of the log event.       

        public const string Template = "{CheckPoint}, {CheckPointSource}, {CorrelationId}, {EventId}, {EventDescription}, {EntityType}, {EntityId}, {Status}, {Description}";

        public const string Template_Info = "{CheckPoint}, {CheckPointSource}, {CorrelationId}, {EventId}, {EventDescription}";

        public const string Template_Warning = "{CheckPoint}, {CheckPointSource}, {CorrelationId}, {EventId}, {EventDescription}, {EntityType}, {EntityId}, {Status}, {Description}";

        public enum EntityType {
            Customer,
            Consignment
        }

        public enum CheckPoint {
            Publisher,
            Subscriber,
            Standard
        }

        /// <summary>
        /// Enumeration of all different EventId that can be used for logging
        /// </summary>
        public enum EventId {
            EndpointInitiated = 1000,
            SubmissionSucceeded = 1100,
            SubmissionFailed = 1101,
            ProcessingSucceeded = 1200,
            ProcessingFailedInvalidData = 1201,
            ProcessingFailedUnhandledException = 1202
        }

        public enum Status {
            SUCCEEDED = 2000,
            FAILED = 2001,
            FAILED_BADREQUEST = 2002,
            DISCARDED = 2010
        }
    }
}
